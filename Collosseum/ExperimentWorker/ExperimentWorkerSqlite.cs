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

    private readonly Repo _repo;

    public ExperimentWorkerSqlite(
        IHostApplicationLifetime lifetime,
        IConditionExperiment experiment,
        Repo repo)
    {
        _appLifeTime = lifetime;
        _experiment = experiment;
        _repo = repo;
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

                Console.WriteLine("Wins = " + wins + " Iterations = " + iterations);
                Console.WriteLine("P = " + wins / iterations);

                var deck = _repo.GetCondition(99);
                if (deck == null)
                {
                    throw new TaskCanceledException();
                }

                Console.WriteLine(_experiment.RunCondition(deck)
                    ? "saved experiment success"
                    : "saved experiment failed");
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