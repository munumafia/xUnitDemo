using FakeItEasy;
using Xunit;
using XUnitDemo.UnitTests.Example2;
using XUnitDemo.UnitTests.Example3;

namespace XUnitDemo.UnitTests.XUnitExample
{
    public class OrderServiceFixture 
    {
        public IPaymentService CreateProcessPaymentStub(PaymentStatus statusToUse)
        {
            var status = new PaymentResponse() {PaymentStatus = statusToUse};

            var stub = A.Fake<IPaymentService>();
            A.CallTo(() => stub.Process(A<PaymentInfo>.Ignored)).Returns(status);

            return stub;
        }

        public IPaymentService CreateRefundPaymentStub(PaymentStatus statusToUse)
        {
            var status = new PaymentResponse() { PaymentStatus = statusToUse };

            var stub = A.Fake<IPaymentService>();
            A.CallTo(() => stub.Refund(A<string>.Ignored)).Returns(status);

            return stub;
        }
    }

    public class OrderServiceFacts 
    {
        public class TheProcessMethod : IClassFixture<OrderServiceFixture>
        {
            private readonly OrderServiceFixture _Fixture;

            public TheProcessMethod(OrderServiceFixture fixture)
            {
                _Fixture = fixture;
            }

            [Fact]
            public void SavesTheOrderWhenPaymentIsApproved()
            {
                // Arrange
                var order = new Order();
                var paymentInfo = new PaymentInfo();

                var paymentServiceStub = _Fixture.CreateProcessPaymentStub(PaymentStatus.Approved);
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
                var paymentServiceStub = _Fixture.CreateProcessPaymentStub(PaymentStatus.Approved);
                var orderRepoFake = new FakeOrderRepository();
                var uow = new OrderService(orderRepoFake, paymentServiceStub);

                // Act
                uow.Process(order, paymentInfo);

                // Assert
                Assert.Equal(OrderStatus.Approved, orderRepoFake.Order.OrderStatus);
            }
        }

        public class TheCancelMethod : IClassFixture<OrderServiceFixture>
        {
            private readonly OrderServiceFixture _Fixture;

            public TheCancelMethod(OrderServiceFixture fixture)
            {
                _Fixture = fixture;
            }

            [Fact]
            public void CancelsTheOrder()
            {
                // Arrange
                var order = new Order() {PaymentAuthCode = "12345"};
                var paymentServiceStub = _Fixture.CreateRefundPaymentStub(PaymentStatus.Refunded);
                var orderRepoFake = new FakeOrderRepository();
                var uow = new OrderService(orderRepoFake, paymentServiceStub);

                // Act
                uow.Refund(order);

                // Assert
                Assert.Equal(OrderStatus.Canceled, orderRepoFake.Order.OrderStatus);
            }
        }
    }
}
