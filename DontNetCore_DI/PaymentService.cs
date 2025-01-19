namespace DontNetCore_DI
{
    public class PaymentService1: IPaymentService
    {
        public PaymentService1(string customerName,int customerId)
        {
            
        }
        public void ProcessPayment(Order order)
        {
            // Code to process payment.
            Console.WriteLine($"Payment processed for order {order.Id}");
        }
    }

    public interface IPaymentService
    {
        void ProcessPayment(Order order);
    }
}
