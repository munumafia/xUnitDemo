using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using XUnitDemo.UnitTests.Example2;

namespace XUnitDemo.UnitTests.XUnitExample
{
    public interface IPaymentService
    {
        Task<PaymentResponse> Process(PaymentInfo paymentInfo);
        Task<PaymentResponse> Refund(string authCode);
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
            using (var client = CreateClientForUri(_ApiEndpoint))
            {
                const string authorizeUri = "/payment";
                var result = await client.PostAsJsonAsync(authorizeUri, paymentInfo);
                result.EnsureSuccessStatusCode();

                return await result.Content.ReadAsAsync<PaymentResponse>();
            }
        }

        public async Task<PaymentResponse> Refund(string authCode)
        {
            using (var client = CreateClientForUri(_ApiEndpoint))
            {
                const string authorizeUri = "/refund";
                var result = await client.PostAsJsonAsync(authorizeUri, authCode);
                result.EnsureSuccessStatusCode();

                return await result.Content.ReadAsAsync<PaymentResponse>();
            }
        }

        private HttpClient CreateClientForUri(string uri)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(uri);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return client;
        }
    }
}
