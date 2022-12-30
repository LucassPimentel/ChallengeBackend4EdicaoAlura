using FluentAssertions;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Moq;
using UserControl.Controllers;
using UserControl.Dtos.Requests;
using UserControl.Interfaces;
using UserControl.Services;
using Xunit;

namespace UserControl.Unit_Tests.Controllers
{
    public class LoginControllerTests
    {
        LoginController loginController;
        Mock<ILoginService> _loginService;

        public LoginControllerTests()
        {

            _loginService = new Mock<ILoginService>();
            loginController = new LoginController(_loginService.Object);
        }

        [Fact]
        public void LoginUser_WhenUserLogin_ShouldReturnStatusCodeOkAndMessage()
        {
            var loginRequest = new LoginRequest()
            {
                Username = "Lucas",
                Password = "Teste123!"
            };

            _loginService.Setup(x => x.LoginUser(It.IsAny<LoginRequest>())).Returns(Result.Ok().WithSuccess("Token que deve retornar..."));

            var result = loginController.LoginUser(loginRequest);

            var objectResult = result as ObjectResult;

            var statusCode = objectResult.StatusCode;

            var returnedMessage = objectResult.Value;

            statusCode.Should().Be(StatusCodes.Status200OK);
            returnedMessage.Should().Be("Logado com sucesso!\nToken: Token que deve retornar...");
        }

        [Fact]
        public void LoginUser_WhenLoginIsFailed_ShouldReturnStatusCodeUnauthorizedAndAMessage()
        {
            var loginRequest = new LoginRequest()
            {
                Username = "Lucas",
                Password = "Teste123!"
            };

            _loginService.Setup(x => x.LoginUser(It.IsAny<LoginRequest>())).Returns(Result.Fail("login falhou"));

            var result = loginController.LoginUser(loginRequest);

            var objectResult = result as ObjectResult;

            var statusCode = objectResult.StatusCode;

            var returnedMessage = objectResult.Value.ToString();

            statusCode.Should().Be(StatusCodes.Status401Unauthorized);
            returnedMessage.Should().Contain("login falhou");
        }
    }
}
