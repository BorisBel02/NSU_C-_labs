using Col.DB;
using Colloseum.Model.Deck;

namespace GodsService;

public class Program
{
    private static async Task Main(string[] args)
    {
        await Host.CreateDefaultBuilder()
            .ConfigureServices(((hostContext, services) =>
            {
                services.AddHostedService<GodsService>();
                services.AddSingleton<IGods, Gods>();
                services.AddScoped<Repo>();
                services.AddDbContext<Context>();
            }))
            .Build()
            .RunAsync();
    }
}