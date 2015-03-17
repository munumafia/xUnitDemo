namespace XUnitDemo.UnitTests.Example2
{
    public class Order
    {
        public OrderStatus OrderStatus { get; set; }

        public Order()
        {
            OrderStatus = OrderStatus.Pending;
        }
    }

    public enum OrderStatus
    {
        Approved,
        Canceled,
        Pending
    }
}