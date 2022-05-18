using PixelDesk.Domain.Models;
using System.Threading.Tasks;

namespace PixelDesk.Domain.Abstractions.Services
{
    public interface IWeatherService
    {
        Task<OpenWeather> GetWeather();

        Task<LocalWeather> GetLocalWeather();
    }
}