public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder();
        builder.Services.AddControllers();

        var application = builder.Build();
        application.MapControllers();

        await application.RunAsync($"https://localhost:{args[0]}");
    }
}