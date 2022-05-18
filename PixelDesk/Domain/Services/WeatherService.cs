using PixelDesk.Domain.Abstractions.Services;
using PixelDesk.Domain.Models;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace PixelDesk.Domain.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly WeatherApiConfig weatherApiConfig;
        private readonly HttpClient httpClient;
        private readonly string openWeatherServiceUrl;
        private readonly string localWeatherServiceUrl;

        public WeatherService(WeatherApiConfig weatherApiConfig)
        {
            this.weatherApiConfig = weatherApiConfig ?? throw new ArgumentNullException(nameof(weatherApiConfig));
            httpClient = new HttpClient();
        }

        public async Task<OpenWeather> GetWeather()
        {
            var response = await httpClient.GetAsync($"{weatherApiConfig.Url}?lat={weatherApiConfig.Latitude}&lon={weatherApiConfig.Longitude}&units=metric&appid={weatherApiConfig.ApiKey}");
            if (response.StatusCode != System.Net.HttpStatusCode.OK) throw new HttpRequestException(response.ReasonPhrase, null, response.StatusCode);
            return JsonSerializer.Deserialize<OpenWeather>(await response.Content.ReadAsStringAsync());
        }

        public async Task<LocalWeather> GetLocalWeather()
        {
            var response = await httpClient.GetAsync($"{weatherApiConfig.LocalUrl}");
            if (response.StatusCode != System.Net.HttpStatusCode.OK) throw new HttpRequestException(response.ReasonPhrase, null, response.StatusCode);
            return JsonSerializer.Deserialize<LocalWeather>(await response.Content.ReadAsStringAsync());
        }
    }
}