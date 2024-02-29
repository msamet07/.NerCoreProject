using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workintech02RestApiDemo.Business.Authentication;
using Workintech02RestApiDemo.Controllers;
using Workintech02RestApiDemo.Domain.Entities;

namespace Workintech02ResApiDemo.Test
{
    public class LoginControllerTest
    {
        private readonly Mock<IAuthenticationService> authenticationServiceMock;
        private readonly LoginController loginController;

        private readonly User fakeData = new User() { Id = 1, UserName = "TestUser", Password = "TestPassword" };
        private const string FAKE_TOKEN = "TestToken";

        public LoginControllerTest()
        {
            authenticationServiceMock = new Mock<IAuthenticationService>();
            loginController = new LoginController(authenticationServiceMock.Object);

            authenticationServiceMock.Setup(x => x.Login("TestUser", "TestPassword")).Returns(fakeData);
            authenticationServiceMock.Setup(x => x.GenerateToken(fakeData)).Returns(FAKE_TOKEN);
        }

        [Fact]
        public void Login_Should_Return_OkResult()
        {
            //Arrange
          
           
            //Act
            var result = loginController.Login("TestUser", "TestPassword");
            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<string>(okResult.Value);
            Assert.Equal(FAKE_TOKEN, returnValue);
            okResult.StatusCode.Should().Be(StatusCodes.Status200OK);
        }

        [Fact]
        public void Login_Should_Return_UnauthorizedResult()
        {
            //Arrange
            var fakeData = new User() { Id= 0, UserName = "TestUser", Password = "TestPassword"};
            authenticationServiceMock.Setup(x=>x.Login("TestUser", "TestPassword")).Returns((User)null);
            //Act
            var result = loginController.Login("TestUser", "TestPassword");
            //Assert
            var unauthorizedResult = Assert.IsType<UnauthorizedResult>(result);
            unauthorizedResult.StatusCode.Should().Be(StatusCodes.Status401Unauthorized);
        }

        [Fact]
        public void Register_Should_Return_OkResult()
        {
            //Arrange
            authenticationServiceMock.Setup(x=>x.Register(fakeData)).Returns(fakeData);
            //Act
            var result = loginController.Register(fakeData);
            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<string>(okResult.Value);
            Assert.Equal(FAKE_TOKEN, returnValue);
            okResult.StatusCode.Should().Be(StatusCodes.Status200OK);
        }
    }
}
