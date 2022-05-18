using Microsoft.AspNetCore.Components;
using PixelDesk.Domain.Models;
using System.Linq;

namespace PixelDesk.Shared
{
    public class WeatherBase : ComponentBase
    {
        [Parameter]
        public OpenWeather OpenWeather { get; set; }

        [Parameter]
        public LocalWeather LocalWeather { get; set; }

        public string Icon => $"http://openweathermap.org/img/wn/{OpenWeather?.Weather.First().Icon}@2x.png";
    }
}