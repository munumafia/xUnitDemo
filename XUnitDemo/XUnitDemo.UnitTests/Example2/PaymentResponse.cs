namespace XUnitDemo.UnitTests.Example2
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
}
