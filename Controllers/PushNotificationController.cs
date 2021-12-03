using FlasherWebApi.DTO;
using FlasherWebApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace FlasherWebApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class PushNotificationController : ControllerBase
{

    private readonly IPushSubscriptionService _pushSubscriptionSerivce;
    private readonly IPushNotificationService _pushNotificationService;
    private static List<PushSubscription> subs = new List<PushSubscription>();


    public PushNotificationController(IPushSubscriptionService pushSubscriptionService, IPushNotificationService pushNotificationService)
    {
        _pushSubscriptionSerivce = pushSubscriptionService;
        _pushNotificationService = pushNotificationService;
    }

    [HttpPost("subscriptions")]
    public async Task<IActionResult> StoreSubscription([FromBody] PushSubscription subscription)
    {
        await _pushSubscriptionSerivce.StoreSubscriptionAsync(subscription);
        return Ok();
    }
    [HttpGet("SendAll")]
    public async Task<IActionResult> SendAll()
    {
        await _pushNotificationService.SendNotification("first message from server side");

        return Ok();
    }

    [HttpGet("Clear")]
    public async Task<IActionResult> ClearSubs()
    {
        subs.Clear();
        return Ok();
    }



}
