using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using PixelDesk.Domain.Abstractions.Services;
using PixelDesk.Domain.Models;

namespace PixelDesk.Domain.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly WeatherApiConfig weatherApiConfig;
        private readonly HttpClient httpClient;

        public WeatherService(WeatherApiConfig weatherApiConfig)
        {
            this.weatherApiConfig = weatherApiConfig ?? throw new ArgumentNullException(nameof(weatherApiConfig));
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(weatherApiConfig.Url);
        }

        public async Task<OpenWeather> getWeather()
        {
            var response = await httpClient.GetAsync($"?q={weatherApiConfig.City}&units=metric&appid={weatherApiConfig.ApiKey}");
            if (response.StatusCode != System.Net.HttpStatusCode.OK) throw new HttpRequestException(response.ReasonPhrase, null, response.StatusCode);
            return JsonSerializer.Deserialize<OpenWeather>(await response.Content.ReadAsStringAsync());
        }
    }
}