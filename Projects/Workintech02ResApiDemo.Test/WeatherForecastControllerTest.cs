using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workintech02RestApiDemo.Controllers;
using Workintech02RestApiDemo.Domain;

namespace Workintech02ResApiDemo.Test
{
    public class WeatherForecastControllerTest
    {
        private readonly Mock<ILogger<WeatherForecastController>> loggerMock;
        private readonly WeatherForecastController weatherForecastController;

        public WeatherForecastControllerTest()
        {
            loggerMock = new Mock<ILogger<WeatherForecastController>>();
            weatherForecastController = new WeatherForecastController(loggerMock.Object);
        }

        [Fact]
        public void Get_Should_Return_WeatherForecast()
        {
            //Arrange
            //Act
            var result = weatherForecastController.Get();
            //Assert
            Assert.NotEmpty(result);
            Assert.Equal(5, result.Count());
        }

        [Fact]
        public void GetDateTime_Should_Return_DateTime()
        {
            //Arrange
            var fakeData = DateTime.Now;
            //Act
            var result = weatherForecastController.GetDateTime();
            //Assert
            Assert.Equal(fakeData.Year, result.Year);
        }
    }
}
