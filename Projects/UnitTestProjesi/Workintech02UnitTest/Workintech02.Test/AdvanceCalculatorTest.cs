using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workintech02UnitTest;

namespace Workintech02.Test
{
    public class AdvanceCalculatorTest
    {
        private readonly AdvanceCalculator _advanceCalculator;

        public AdvanceCalculatorTest()
        {
            _advanceCalculator = new AdvanceCalculator();
        }

        [Fact]
        public void Modulus_Should_SameValueExpected()
        {
            //Arrange
            var a = 3;var b = 2;
            var expected = 1;

            //Act
            var result = _advanceCalculator.Modulus(a, b);

            //Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Modulus_ValidInput_SameExpected()
        {
            //Arrange
            var advanceCalculatorMock = new Mock<IAdvanceCalculator>();

            advanceCalculatorMock.Setup(x => x.Modulus(4, 2)).Returns(1);

            var advanceCalculatorFake = advanceCalculatorMock.Object;

            //Act
            var fakeResult = advanceCalculatorFake.Modulus(4, 2);
            var realResult = _advanceCalculator.Modulus(8, 3);

            //Assert
            Assert.Equal(1, fakeResult);
            Assert.Equal(2, realResult);
        }

        [Fact]
        public void Power_Should_SameValueExpected()
        {
            //Arrange
            var a = 3;var b = 2;
            var expected = 9;

            //Act
            var result = _advanceCalculator.Power(a, b);

            //Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void SquareRoot_Should_SameValueExpected()
        {
            //Arrange
            var a = 9;
            var expected = 3;

            //Act
            var result = _advanceCalculator.SquareRoot(a);

            //Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(0,false)]
        [InlineData(1,false)]
        [InlineData(2,true)]
        [InlineData(33,false)]
        [InlineData(7,true)]
        [InlineData(8,false)]
        [InlineData(20, false)]
        [InlineData(21, false)]
        [InlineData(22, false)]
        public void IsPrime_Should_SameValueExpected(int a,bool expected)
        {
            //Arrange

            //Act
            var result = _advanceCalculator.IsPrime(a);

            //Assert
            Assert.Equal(expected, result);
        }
    }
}
