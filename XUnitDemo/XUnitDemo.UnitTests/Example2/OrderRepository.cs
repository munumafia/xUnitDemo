namespace XUnitDemo.UnitTests.Example2
{
    public interface IOrderRepository
    {
        void Save(Order order);
    }

    public class OrderRepository : IOrderRepository
    {
        public void Save(Order order)
        {
            // No-Op
        }
    }
}
