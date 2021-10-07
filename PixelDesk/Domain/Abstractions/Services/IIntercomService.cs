using System;
using System.Threading.Tasks;

namespace PixelDesk.Domain.Abstractions.Services
{
    public interface IIntercomService
    {
        Task Subscribe(Action<bool> receiveMessage);

        Task Unsubscribe();
    }
}