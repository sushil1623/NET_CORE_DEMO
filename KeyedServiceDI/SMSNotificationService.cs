namespace KeyedServiceDI
{
    public class SMSNotificationService : INotificationService
    {
        public void SendNotification(string notificationData)
        {
           Console.WriteLine(notificationData);
        }
    }
}
