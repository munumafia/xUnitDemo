using System;
using Xunit;
using XUnitDemo.UnitTests.Example1;

namespace XUnitDemo.UnitTests.XUnitExample
{
    public class MathServiceFacts
    {
        public class TheAddMethod : IDisposable 
        {
            private MathService _uow;

            public TheAddMethod()
            {
                _uow = new MathService();
            }

            [Theory]
            [InlineData(3, 5)]
            [InlineData(4, 2)]
            [InlineData(1, 0)]
            [InlineData(0, 0)]
            public void ReturnsTheCorrectSum(int a, int b)
            {
                // Arrange
                var expected = a + b;

                // Act
                var actual = _uow.Add(a, b);

                // Assert
                Assert.Equal(expected, actual);
            }

            [Theory]
            [InlineData(3, 5)]
            [InlineData(4, 2)]
            [InlineData(1, 0)]
            [InlineData(0, 0)]
            public void IsCommutative(int a, int b)
            {
                // Arrange
                var expected = b + a;

                // Act
                var actual = _uow.Add(a, b);

                // Assert
                Assert.Equal(expected, actual);
            }

            public void Dispose()
            {
                _uow = null;
            }
        }
    }
}
