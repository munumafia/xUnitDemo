using XUnitDemo.UnitTests.Example2;

namespace XUnitDemo.UnitTests.Example3
{
    public class FakeOrderRepository : IOrderRepository
    {
        public Order Order { get; set; }

        public FakeOrderRepository()
        {
            Order = new Order() { OrderStatus = OrderStatus.Pending };
        }
        
        public void Save(Order order)
        {
            Order = new Order()
            {
                OrderStatus = order.OrderStatus
            };
        }
    }
}