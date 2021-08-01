using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PixelDesk.Shared.Abstractions.Services;
using PixelDesk.Shared.Models;
using PixelDesk.Shared.Services;

namespace PixelDesk.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            AddServices(builder);

            await builder.Build().RunAsync();
        }

        private static void AddServices(WebAssemblyHostBuilder builder)
        {
            builder.Services.AddSingleton(provider =>
            {
                var config = provider.GetService<IConfiguration>();
                return new WeatherApiConfig
                {
                    ApiKey = config.GetValue<string>("WeatherApiKey"),
                    City = config.GetValue<string>("City"),
                    Url = config.GetValue<string>("WeatherAPIUrl")
                };
            });

            builder.Services.AddSingleton<IIntercomService>(provider =>
                new IntercomService(provider.GetService<IConfiguration>().GetValue<string>("IntercomUrl")));
            builder.Services.AddSingleton<IWeatherService, WeatherService>();
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
        }
    }
}