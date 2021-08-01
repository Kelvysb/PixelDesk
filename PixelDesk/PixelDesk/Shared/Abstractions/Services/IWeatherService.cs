using System.Threading.Tasks;
using PixelDesk.Shared.Models;

namespace PixelDesk.Shared.Abstractions.Services
{
    public interface IWeatherService
    {
        Task<OpenWeather> getWeather();
    }
}