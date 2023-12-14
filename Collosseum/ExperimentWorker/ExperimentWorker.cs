using Colloseum.Model;
using Microsoft.Extensions.Hosting;

namespace Colloseum;

public class ExperimentWorker : IHostedService
{
    private readonly IHostApplicationLifetime _appLifeTime;
    
    private IExperiment _experiment;

    public ExperimentWorker(
        IExperiment experiment, 
        IHostApplicationLifetime appLifeTime)
    {
        _experiment = experiment;
        _appLifeTime = appLifeTime;
    }
    
    public Task StartAsync(CancellationToken cancellationToken)
    {
        Task.Run(async () =>
        {
            try
            {
                int iterations = 100000;
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
            }
            catch (TaskCanceledException)
            {

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
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
        
    }
}