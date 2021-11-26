using FlasherWebApi.Models;

namespace FlasherWebApi.Services
{
    public interface IPushNotificationService
    {
        void SendNotification(PushSubscription subscription, string payload);
    }
}
