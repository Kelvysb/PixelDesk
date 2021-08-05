using System.Net.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PixelDesk.Domain.Abstractions.Services;
using PixelDesk.Domain.Models;
using PixelDesk.Domain.Services;

namespace PixelDesk
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();

            var mqttConfig = new MQTTConfig();
            Configuration.GetSection("MQTT").Bind(mqttConfig);
            services.AddSingleton(provider => mqttConfig);

            var weatherApiConfig = new WeatherApiConfig();
            Configuration.GetSection("OpenWeatherApi").Bind(weatherApiConfig);
            services.AddSingleton(provider => weatherApiConfig);

            services.AddSingleton<IIntercomService, IntercomService>();
            services.AddSingleton<IWeatherService, WeatherService>();
            services.AddScoped(sp => new HttpClient());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}