using System.Linq;
using Microsoft.AspNetCore.Components;
using PixelDesk.Shared.Models;

namespace PixelDesk.Client.Shared
{
    public class WeatherBase : ComponentBase
    {
        [Parameter]
        public OpenWeather OpenWeather { get; set; }

        public string Icon => $"http://openweathermap.org/img/wn/{OpenWeather?.Weather.First().Icon}@2x.png";
    }
}