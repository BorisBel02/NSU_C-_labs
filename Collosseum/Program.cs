using Col.DB;
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
                services.AddHostedService<ExperimentWorkerSqlite>();
                services.AddScoped<IConditionExperiment, ExperimentSqlite>();
                services.AddTransient<IFighter, Fighter>();
                services.AddSingleton<IGods, Gods>();
                services.AddDbContext<Context>();
            }))
            .Build()
            .RunAsync();
    }
}
