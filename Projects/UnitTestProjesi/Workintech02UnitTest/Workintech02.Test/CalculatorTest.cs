using FluentAssertions;
using Workintech02UnitTest;

namespace Workintech02.Test
{
    public class CalculatorTest
    {
        private readonly Calculator _calculator;

        public CalculatorTest()
        {
            _calculator = new Calculator();
        }

        [Fact]
        public void Add_Should_SameValueExpected()
        {
            //Arrange
            var a = 1;var b = 2;
            var expected = 3;

            //Act
            var result = _calculator.Add(a, b);

            //Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(1,2,3)]
        [InlineData(5,6,11)]
        [InlineData(32023,45672,77695)]
        [InlineData(30,20,50)]
        public void Add_Should_SameValueFromInlineData(int a,int b,int expected)
        {
            //Arrange
            //Act
            var result = _calculator.Add(a, b);

            //Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Subtract_Should_SameValueExpected()
        {
            //Arrange
            var a = 3;var b = 2;
            var expected = 1;

            //Act
            var result = _calculator.Subtract(a, b);

            //Assert
            Assert.NotNull(_calculator);
            Assert.IsAssignableFrom<Calculator>(_calculator);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Times_Should_SameValueExpected()
        {
            //Arrange
            var a = 3;var b = 2;
            var expected = 6;

            //Act
            var result = _calculator.Times(a, b);

            //Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Divide_Should_SameValueExpected()
        {
            //Arrange
            var a = 6;var b = 2;
            var expected = 3;

            //Act
            var result = _calculator.Divide(a, b);

            //Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Divide_Should_ThrowException()
        {
            //Arrange
            var a = 6;var b = 0;

            //Act
            var exception = Record.Exception(() => _calculator.Divide(a, b));

            //Assert
            Assert.NotNull(exception);
            Assert.IsType<System.DivideByZeroException>(exception);
            Assert.Equal("Cannot divide by zero", exception.Message);
        }

        [Fact]
        public void Divide_ShouldThrowDivideByZeroException()
        {
            //Arrange
            var a = 1;var b = 0;

            //Act
            
            Action act = ()=> _calculator.Divide(a,b);
            

            //Assert
            act.Should().Throw<DivideByZeroException>().WithMessage("Cannot divide by zero");
        }


    }
}