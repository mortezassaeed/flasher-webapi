using FlasherWebApi.Models;
using FlasherWebApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlasherWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PushNotificationController : ControllerBase
    {

        private readonly IPushNotificationService _notificationService;
        private static List<PushSubscription> subs = new List<PushSubscription>();


        public PushNotificationController(IPushNotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpPost("subscriptions")]
        public async Task<IActionResult> StoreSubscription([FromBody] PushSubscription subscription)
        {
            subs.Add(subscription);
            //await _subscriptionStore.StoreSubscriptionAsync(subscription);
            return NoContent();
        }
        [HttpGet("send")]
        public async Task<IActionResult> Send()
        {
            //await _subscriptionStore.StoreSubscriptionAsync(subscription);
            foreach (var item in subs)
            {
                _notificationService.SendNotification(item, "hiiiii from srver side");

            }
            return Ok();
        }

        [HttpGet("Clear")]
        public async Task<IActionResult> ClearSubs()
        {
            subs.Clear();
            return Ok();
        }



    }
}
