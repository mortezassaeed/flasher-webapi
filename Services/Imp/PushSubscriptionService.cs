using FlasherWebApi.DTO;
using FlasherWebApi.Models;

namespace FlasherWebApi.Services.Imp
{
    public class PushSubscriptionService : IPushSubscriptionService
    {
        public readonly DatabaseContext _databaseContext;

        public PushSubscriptionService(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task StoreSubscriptionAsync(PushSubscription subscription)
        {
            try
            {
                var ent = await _databaseContext.Subscriptors.AddAsync(new Subscriptor()
                {
                    Auth = subscription.Keys.FirstOrDefault(k => k.Key == "auth").Value,
                    EndPoint = subscription.Endpoint,
                    P256dh = subscription.Keys.FirstOrDefault(k => k.Key == "p256dh").Value
                });

                await _databaseContext.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
