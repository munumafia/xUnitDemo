using Xunit;

namespace XUnitDemo.UnitTests.Example1
{
    public class MathServiceFacts
    {
        public class TheAddMethod
        {
            [Fact]
            public void ReturnsTheCorrectAnswer()
            {
                // Arrange
                var sut = new MathService();
                const int expected = 8;

                // Act
                var actual = sut.Add(3, 5);

                // Assert
                Assert.Equal(expected, actual);
            }

            [Fact]
            public void IsCommutative()
            {
                // Arrange
                var sut = new MathService();

                // Act
                var result1 = sut.Add(3, 5);
                var result2 = sut.Add(5, 3);

                // Assert
                Assert.True(result1 == result2);
            }
        }
    }
}
