using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace XUnitDemo.UnitTests.Example2
{
    public class OrderService
    {
        private readonly OrderRepository _OrderRepo = new OrderRepository();
        private const string PaymentGatewayEndpoint = @"http://example.com/payment/api";

        public async void Process(Order order, PaymentInfo paymentInfo)
        {
            var payment = await ProcessPayment(paymentInfo);
            if (payment.PaymentStatus == PaymentStatus.Approved)
            {
                order.OrderStatus = OrderStatus.Approved;
                _OrderRepo.Save(order);
            }
        }

        private static async Task<PaymentResponse> ProcessPayment(PaymentInfo paymentInfo)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(PaymentGatewayEndpoint);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                const string authorizeUri = "/payment";
                var result = await client.PostAsJsonAsync(authorizeUri, paymentInfo);
                result.EnsureSuccessStatusCode();

                return await result.Content.ReadAsAsync<PaymentResponse>();
            }
        }
    }
}
