namespace KeyedServiceDI
{
    public class EmailNotificationService : INotificationService
    {
        public void SendNotification(string notificationData)
        {
            Console.WriteLine(notificationData);
        }
    }
}
