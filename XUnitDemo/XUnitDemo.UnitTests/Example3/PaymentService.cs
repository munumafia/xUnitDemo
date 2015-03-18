using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using XUnitDemo.UnitTests.Example2;

namespace XUnitDemo.UnitTests.Example3
{
    public interface IPaymentService
    {
        Task<PaymentResponse> Process(PaymentInfo paymentInfo);
    }

    public class PaymentService : IPaymentService
    {
        private readonly string _ApiEndpoint;

        public PaymentService(string apiEndpoint)
        {
            _ApiEndpoint = apiEndpoint;
        }

        public async Task<PaymentResponse> Process(PaymentInfo paymentInfo)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_ApiEndpoint);
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
