using FlasherWebApi.Models;

namespace FlasherWebApi.Services
{
    public interface IPushSubscriptionStore
    {
        Task StoreSubscriptionAsync(PushSubscription subscription);
    }
}
