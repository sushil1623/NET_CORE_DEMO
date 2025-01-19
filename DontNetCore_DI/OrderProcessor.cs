namespace DontNetCore_DI
{
    public class OrderProcessor
    {
        private readonly IEmailService _emailService;
        private readonly IPaymentService _paymentService;

        public OrderProcessor(IEmailService emailService,IPaymentService paymentService)
        {
            _emailService = emailService;
            _paymentService = paymentService;
        }

        public void ProcessOrder(Order order)
        {
            _paymentService.ProcessPayment(order);
            _emailService.SendOrderConfirmation(order);
        }
    }
}
