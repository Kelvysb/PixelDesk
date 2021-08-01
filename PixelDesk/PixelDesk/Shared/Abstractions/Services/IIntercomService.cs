using System.Threading.Tasks;
using PixelDesk.Shared.Models;

namespace PixelDesk.Shared.Abstractions.Services
{
    public interface IIntercomService
    {
        Task<IntercomModule> getIntercomData();
    }
}