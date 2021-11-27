using FlasherWebApi.DTO;
using FlasherWebApi.Models;

namespace FlasherWebApi.Services
{
    public interface IPushSubscriptionService
    {
        Task StoreSubscriptionAsync(PushSubscription subscription);
        
    }
}
