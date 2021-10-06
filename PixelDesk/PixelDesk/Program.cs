using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using PixelDesk.Domain.Extesnsions;

namespace PixelDesk
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args)
                .Build()
                .Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    var config = new ConfigurationBuilder()
                        .UseDotEnv("../../Docker/.env")
                        .AddEnvironmentVariables()
                        .AddJsonFile("appsettings.json", optional: false)
                        .AddCommandLine(args)
                        .Build();

                    webBuilder.UseStartup<Startup>()
                    .UseConfiguration(config)
                    .UseUrls($"http://*:{config["PIXELDESK_PORT"]}");
                });
    }
}