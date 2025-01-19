namespace DontNetCore_DI
{
    public class EmailService : IEmailService
    {
        public void SendOrderConfirmation(Order order)
        {
            Console.WriteLine($"Email has been sent for order {order.Id}");
        }
    }

    public interface IEmailService
    {
        void SendOrderConfirmation(Order order);
    }   
}
