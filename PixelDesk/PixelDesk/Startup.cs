using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PixelDesk.Domain.Abstractions.Services;
using PixelDesk.Domain.Models;
using PixelDesk.Domain.Services;
using System.Net.Http;

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

            services.AddSingleton(provider => new MQTTConfig
            {
                DeviceId = Configuration["MQTT_DEVICE_ID"],
                IntercomTopic = Configuration["MQTT_INTERCOM_TOPIC"],
                Server = Configuration["MQTT_SERVER"],
                Port = int.Parse(Configuration["MQTT_PORT"]),
                User = Configuration["MQTT_USER"],
                Password = Configuration["MQTT_PASSWORD"],
                ResultJPath = Configuration["MQTT_VALUE_JPATH"]
            });

            services.AddSingleton(provider => new WeatherApiConfig
            {
                Url = Configuration["OW_URL"],
                ApiKey = Configuration["OW_APIKEY"],
                Latitude = Configuration["OW_LATITUDE"],
                Longitude = Configuration["OW_LONGITUDE"]
            });

            services.AddTransient<IIntercomService, IntercomService>();
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