using AutoMapper;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workintech02RestApiDemo.Business.City;
using Workintech02RestApiDemo.Domain.Dto;

namespace Workintech02RestApiDemo.Business.Test
{
    public class CityServiceTest
    {
        private readonly CityService cityService;
        private readonly Mock<IMapper> mapperMock;

        public CityServiceTest()
        {
            var mockDb = TestHelper.GetInMemoryDbContext();
            mapperMock = new Mock<IMapper>();
            cityService = new CityService(mockDb, mapperMock.Object);
        }


        [Fact]

        public void AddCity_Should_ReturnValue()
        {
            //Arrange
            var fakeCityName = "Test City";
            mapperMock.Setup(x => x.Map<CityDto>(It.IsAny<Domain.Entities.City>())).Returns(new CityDto() { CityId=1, CityName = fakeCityName});

            //Act
            var result = cityService.AddCity(fakeCityName);
            //Assert

            Assert.NotNull(result);
            Assert.Equal(fakeCityName, result.CityName);
        }

        [Fact]
        public void GetCity_Should_Exception_WhenCityNotFound()
        {
            //Arrange
            var fakeCityName = "Test City";
            mapperMock.Setup(x => x.Map<CityDto>(It.IsAny<Domain.Entities.City>())).Returns(new CityDto() { CityId = 1, CityName = fakeCityName });

            //Act
            Action act = () => cityService.GetById(1);

            //Assert
            act.Should().Throw<ArgumentException>().WithMessage("City not found");
        }
    }
}
