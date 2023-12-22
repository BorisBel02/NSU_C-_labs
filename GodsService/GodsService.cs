using System.Collections;
using System.Text;
using Col.DB;
using Colloseum.Model.Deck;
using DB.Mapper;
using Microsoft.AspNetCore.Server.Kestrel.Core.Features;
using Newtonsoft.Json;

namespace GodsService;

public class GodsService : IHostedService
{
    private readonly int _elonPort = 15001;
    private readonly int _markPort = 15002;
    private readonly int _iterationsQty = 100;

    private readonly IHostApplicationLifetime _appLifeTime;
    private readonly Repo _repo;
    private IGods _gods;
    
    public GodsService(
        IHostApplicationLifetime applicationLifetime,
        Repo repo,
        IGods gods)
    {
        _appLifeTime = applicationLifetime;
        _repo = repo;
        _gods = gods;
    }
    
    public Task StartAsync(CancellationToken cancellationToken)
    {
        Task.Run(async () =>
        {
            generateConditions(_iterationsQty);

            float win = 0;
            Object locker = new();
            for (int i = 0; i < _iterationsQty; i++)
            {
                var condition = _repo.GetCondition(i + 1);
                if (condition == null)
                {
                    continue;
                }
                var elonCards = new Card[18];
                var markCards = new Card[18];
                
                Array.Copy(condition, 0, elonCards, 0, condition.Length / 2);
                Array.Copy(condition, condition.Length / 2, markCards,
                    0, _gods.GetDeck().Length / 2);

                
                Console.WriteLine("sending cards");
                var elonNum = await sendCardsAsync(elonCards, _elonPort);
                var markNum = await sendCardsAsync(markCards, _markPort);

                if (markNum == -1 || elonNum == -1)
                {
                    continue;
                }

                if (elonCards[markNum].CardColour == markCards[elonNum].CardColour)
                {
                    Console.WriteLine($"iteration {i} win");
                    
                    ++win;
                    
                }
            }
            
            Console.WriteLine($"wins = {win}, iterations = {_iterationsQty}\nP = {win / _iterationsQty}");
            
        });

        return Task.CompletedTask;
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("Stopping experiments");
    }

    private void generateConditions(int qty)
    {
        for (int i = 1; i <= qty; i++)
        {
            _gods.Shuffle();
            //Console.WriteLine(_gods.GetDeck().Length);
            _repo.SaveExperiment(_gods.GetDeck());    
        }
    }

    private async Task<int> sendCardsAsync(IEnumerable<Card> cards, int port)
    {
        using var client = new HttpClient();
        var json = JsonConvert.SerializeObject(CardMapper.MapRangeCardDto(cards));
        HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

        using var response = await client.PostAsync($"http://localhost:{port}/cards", content);

        if (response.IsSuccessStatusCode) return Convert.ToInt32(await response.Content.ReadAsStringAsync());
        
        Console.WriteLine($"Error in response, code = {response.StatusCode}");
        return -1;

    }
}