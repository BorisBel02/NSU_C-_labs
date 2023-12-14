using Col.DB;
using Colloseum.Model;
using Colloseum.Model.Deck;
using DB;
using DB.Mapper;
using Microsoft.Extensions.Hosting;

namespace Colloseum;

public class ExperimentWorkerSqlite : IHostedService
{
    private readonly IHostApplicationLifetime _appLifeTime;
    
    private readonly IConditionExperiment _experiment;

    private readonly Context _db;

    public ExperimentWorkerSqlite(
        IHostApplicationLifetime lifetime,
        IConditionExperiment experiment,
        Context db)
    {
        _appLifeTime = lifetime;
        _experiment = experiment;
        _db = db;
    }
    public Task StartAsync(CancellationToken cancellationToken)
    {
        Task.Run(async () =>
        {
            try
            {
                int iterations = 100;
                double wins = 0;
                for (int i = 0; i < iterations; ++i)
                {
                    if (_experiment.Run())
                    {
                        ++wins;
                    }
                }

                _db.SaveChanges();
                Console.WriteLine("Wins = " + wins + " Iterations = " + iterations);
                Console.WriteLine("P = " + wins / iterations);

                var savedExperiment = _db.Experiments.Find(99);

                if (savedExperiment == null)
                {
                    Console.WriteLine("no Experiment 100 in DB");
                    throw new TaskCanceledException();
                }

                var deck = new Card[36];

                int iteration = 0;
                foreach (var cardEntity in savedExperiment.Deck)
                {
                    deck[iteration] = CardMapper.MapCard(cardEntity);
                    ++iteration;
                }

                if (_experiment.RunCondition(deck))
                {
                    Console.WriteLine("saved experiment success");
                }
                else
                {
                    Console.WriteLine("saved experiment failed");
                }
            }
            catch (TaskCanceledException){}
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                
                _appLifeTime.StopApplication();
            }
        });
        
        
        return Task.CompletedTask;
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("Stopping experiments");
    }
}