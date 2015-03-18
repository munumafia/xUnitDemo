using System;
using XUnitDemo.UnitTests.Example2;

namespace XUnitDemo.UnitTests.XUnitExample
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

        public async void Refund(Order order)
        {
            var response = await _PaymentService.Refund(order.PaymentAuthCode);
            if (response.PaymentStatus == PaymentStatus.Refunded)
            {
                order.OrderStatus = OrderStatus.Canceled;
                _OrderRepo.Save(order);
            }
            else
            {
                var message = string.Format("Couldn't refund payment: {0}", response.ErrorMessage);
                throw new InvalidOperationException(message);
            }
        }
    }
}
