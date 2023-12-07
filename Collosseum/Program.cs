using Colloseum;
using Colloseum.Model;
using Colloseum.Model.Deck;
using Colloseum.Model.Fighters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

internal sealed class Program
{
    private static async Task Main(string[] args)
    {
        await Host.CreateDefaultBuilder(args)
            .ConfigureServices(((hostContext, services) =>
            {
                services.AddHostedService<ExperimentWorker>();
                services.AddScoped<Experiment>();
                services.AddTransient<Fighter>();
                services.AddSingleton<Gods>();
            }))
            .Build()
            .RunAsync();
    }
}
