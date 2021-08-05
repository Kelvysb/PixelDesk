using System.Threading.Tasks;
using PixelDesk.Domain.Models;

namespace PixelDesk.Domain.Abstractions.Services
{
    public interface IWeatherService
    {
        Task<OpenWeather> getWeather();
    }
}