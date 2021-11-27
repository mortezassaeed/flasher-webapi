using FlasherWebApi.DTO;
using FlasherWebApi.Models;

namespace FlasherWebApi.Services
{
    public interface IPushNotificationService
    {
        Task SendNotification(string payload);
    }
}
