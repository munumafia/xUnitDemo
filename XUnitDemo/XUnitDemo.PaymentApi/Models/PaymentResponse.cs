using System;

namespace XUnitDemo.PaymentApi.Models
{
    public class PaymentResponse
    {
        public PaymentStatus PaymentStatus { get; set; }
        public string AuthorizationCode { get; set; }
        public string ErrorMessage { get; set; }
    }

    public enum PaymentStatus
    {
        Approved,
        Declined
    }

    public class PaymentRequest
    {
        public string BillingAddress { get; set; }
        public string BillingCity { get; set; }
        public string BillingState { get; set; }
        public string CardNumber { get; set; }
        public string Cvv2 { get; set; }
        public DateTime Expiration { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}