namespace KeyedServiceDI
{
    public class PushNotificationServices : INotificationService
    {
        public void SendNotification(string notificationData)
        {
            Console.WriteLine(notificationData);
        }
    }
}
