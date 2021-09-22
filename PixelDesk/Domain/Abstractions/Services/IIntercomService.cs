using System;
using System.Threading.Tasks;
using PixelDesk.Domain.Models;

namespace PixelDesk.Domain.Abstractions.Services
{
    public interface IIntercomService
    {
        Task Subscribe(Action<DeviceData> receiveMessage);

        Task Unsubscribe();
    }
}