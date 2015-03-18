using FakeItEasy;
using Xunit;
using XUnitDemo.UnitTests.Example2;

namespace XUnitDemo.UnitTests.Example3
{
    public class OrderServiceFacts
    {
        public class ThePaymentMethod
        {
            [Fact]
            public void SavesTheOrderWhenPaymentIsApproved()
            {
                // Arrange
                var order = new Order();
                var paymentInfo = new PaymentInfo();

                var paymentServiceStub = CreatePaymentServiceStub(PaymentStatus.Approved);
                var orderRepoMock = A.Fake<IOrderRepository>();
                var uow = new OrderService(orderRepoMock, paymentServiceStub);
                
                // Act 
                uow.Process(order, paymentInfo);

                // Assert
                A.CallTo(() => orderRepoMock.Save(A<Order>.Ignored)).MustHaveHappened();
            }

            [Fact]
            public void ApprovesTheOrderBeforeSaving()
            {
                // Arrange
                var order = new Order();
                var paymentInfo = new PaymentInfo();

                var paymentServiceStub = CreatePaymentServiceStub(PaymentStatus.Approved);
                var orderRepoFake = new FakeOrderRepository();
                var uow = new OrderService(orderRepoFake, paymentServiceStub);

                // Act
                uow.Process(order, paymentInfo);

                // Assert
                Assert.Equal(OrderStatus.Approved, orderRepoFake.Order.OrderStatus);
            }

            private IPaymentService CreatePaymentServiceStub(PaymentStatus statusToUse)
            {
                var paymentServiceStub = A.Fake<IPaymentService>();
                
                A.CallTo(() => paymentServiceStub.Process(A<PaymentInfo>.Ignored))
                    .Returns(new PaymentResponse() {PaymentStatus = statusToUse});

                return paymentServiceStub;
            }
        }
    }
}
