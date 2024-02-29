using Castle.Core.Logging;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using Workintech02RestApiDemo.Business.Currency;
using Workintech02RestApiDemo.Domain;
using Workintech02RestApiDemo.Domain.Helper;

namespace Workintech02RestApiDemo.Business.Test
{
    public class CurrencyServiceTest
    {
        private readonly CurrencyService currencyService;
       
    
        private readonly List<CurrencyResponse> fakeData = new List<CurrencyResponse>()
        {
            new CurrencyResponse()
            {
                CAD= 1.5,
                EUR= 2.5,
                TRY= 3.5,
                USD= 4.5
            }
        };

        private readonly Mock<ILogger<CurrencyService>> loggerMock;
        private readonly Mock<IHttpClientHandler> httpClientMock;

        public CurrencyServiceTest()
        {
            loggerMock = new Mock<ILogger<CurrencyService>>();
            httpClientMock = new Mock<IHttpClientHandler>();
            currencyService = new CurrencyService(loggerMock.Object,httpClientMock.Object);
          
           
        }
        [Fact]
        public async Task GetCurrencySymbol_Should_Return_CurrencySymbol()
        {
            //Arrange
            var currencyCode = "USD";
            //Act
            var result = await currencyService.GetCurrencySymbol(currencyCode);
            //Assert
            Assert.Equal("$", result);
        }

        [Fact]
        public async Task GetCurrencySymbol_Shoul_ArgumentException_InvalidCall()
        {
            //Arrange
            var currencyCode = "XXX";
            //Act
            Action action = () => currencyService.GetCurrencySymbol(currencyCode).GetAwaiter().GetResult();
            //Assert
            action.Should().Throw<ArgumentException>();
            await Assert.ThrowsAsync<ArgumentException>(() => currencyService.GetCurrencySymbol(currencyCode));
        }

        [Fact]
        public async Task GetCurrency_Should_Return_CurrencyResponse()
        {
            //Arrange
            var fakeData = new CurrencyResponse()
            {
                CAD = 1.5,
                EUR = 2.5,
                TRY = 3.5,
                USD = 4.5
            };
            var fakeCurrencyRoot = new CurrencyRoot()
            {
                data = fakeData
            };
            
            httpClientMock.Setup(x => x.GetStringAsync(It.IsAny<string>())).ReturnsAsync(JsonConvert.SerializeObject(fakeCurrencyRoot));
            //Act
            var result = await currencyService.GetCurrency();

            //Assert
            Assert.NotNull(result);
            Assert.Equal(result.USD,fakeData.USD);
        }   

    }
}
