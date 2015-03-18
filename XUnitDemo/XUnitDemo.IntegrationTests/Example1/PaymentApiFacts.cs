using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xunit;

namespace XUnitDemo.IntegrationTests.Example1
{
    public class PaymentApiFacts
    {
        public class WhenPostingAPayment
        {
            private const string ApiEndpoint = "http://localhost:62288/api/";

            [Fact]
            public async void ThePaymentStatusShouldBeIncludedInTheResponse()
            {
                // Arrange
                var paymentInfo = CreateTestPaymentInfo();
                var client = CreateClientForUri(ApiEndpoint);

                // Act
                var sut = await GetJsonDictionary(client, paymentInfo, "payment");

                // Assert
                Assert.True(sut.ContainsKey("PaymentStatus"));
            }

            [Fact]
            public async void TheAuthorizationCodeShouldBeIncludedInTheResponse()
            {
                // Arrange
                var paymentInfo = CreateTestPaymentInfo();
                var client = CreateClientForUri(ApiEndpoint);

                // Act
                var sut = await GetJsonDictionary(client, paymentInfo, "payment");

                // Assert
                Assert.True(sut.ContainsKey("AuthorizationCode"));
            }

            [Fact]
            public async void TheErrorMessageShouldBeIncludedInTheResponse()
            {
                // Arrange
                var paymentInfo = CreateTestPaymentInfo();
                var client = CreateClientForUri(ApiEndpoint);

                // Act
                var sut = await GetJsonDictionary(client, paymentInfo, "payment");

                // Assert
                Assert.True(sut.ContainsKey("ErrorMessage"));
            }

            private static PaymentInfo CreateTestPaymentInfo()
            {
                return new PaymentInfo()
                       {
                           BillingAddress = "330 Marshall Street Suite 800",
                           BillingCity = "Shreveport",
                           BillingState = "LA",
                           CardNumber = "1111111111111111",
                           Cvv2 = "123",
                           Expiration = new DateTime(2018, 1, 1)
                       };
            }

            private static HttpClient CreateClientForUri(string uri)
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(uri);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                return client;
            }

            private async static Task<Dictionary<string, string>> GetJsonDictionary(HttpClient client, PaymentInfo paymentInfo,
                string uri)
            {
                var response = await client.PostAsJsonAsync(uri, paymentInfo);
                var json = await response.Content.ReadAsStringAsync();
                
                return JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
            }
        }
    }
}
