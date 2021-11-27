using FlasherWebApi.Models;
using FlasherWebApi.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using WebPush;

namespace FlasherWebApi.Services.Imp
{

    public class PushNotificationService : IPushNotificationService
    {
        private readonly PushNotificationServiceOptions _options;
        private readonly WebPushClient _pushClient;
        private readonly DatabaseContext _db;
        public string PublicKey { get { return _options.PublicKey; } }

        public PushNotificationService(IOptions<PushNotificationServiceOptions> optionsAccessor, DatabaseContext db)
        {
            _options = optionsAccessor.Value;

            _pushClient = new WebPushClient();
            _pushClient.SetVapidDetails(_options.Subject, _options.PublicKey, _options.PrivateKey);
            _db = db;
        }

        public async Task SendNotification(string payload)
        {
            var subs = await _db.Subscriptors.ToListAsync();

            foreach (var sub in subs)
            {
                var webPushSubscription = new WebPush.PushSubscription(
                    sub.EndPoint,
                    sub.P256dh,
                    sub.Auth);

                _pushClient.SendNotification(webPushSubscription, payload);
            }

        }
    }
}
