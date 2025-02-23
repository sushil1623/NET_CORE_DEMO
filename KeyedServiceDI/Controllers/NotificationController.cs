using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KeyedServiceDI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        INotificationService EmailService;
        INotificationService PushService;
        INotificationService SMSService;
        public NotificationController([FromKeyedServices("Email")] INotificationService emailService,
            [FromKeyedServices("PushNotification")] INotificationService pushService,
            [FromKeyedServices("SMS")] INotificationService smsService
            )
        {
            EmailService= emailService;
            PushService= pushService;
            SMSService= smsService;
        }

        [HttpGet]
        public IActionResult Get(string notificationData) 
        { 
            
            PushService?.SendNotification(notificationData);
            EmailService?.SendNotification(notificationData);
            SMSService?.SendNotification(notificationData);
            return Ok("Notification has been send successfully");
        }
    }
}
