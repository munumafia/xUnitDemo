using XUnitDemo.UnitTests.Example2;

namespace XUnitDemo.UnitTests.Example3
{
    public class OrderService
    {
        private readonly IOrderRepository _OrderRepo;
        private readonly IPaymentService _PaymentService;

        public OrderService(IOrderRepository orderRepo, IPaymentService paymentService)
        {
            _OrderRepo = orderRepo;
            _PaymentService = paymentService;
        }

        public async void Process(Order order, PaymentInfo paymentInfo)
        {
            var payment = await _PaymentService.Process(paymentInfo);
            if (payment.PaymentStatus == PaymentStatus.Approved)
            {
                order.OrderStatus = OrderStatus.Approved;
                _OrderRepo.Save(order);
            }
        }
    }
}
