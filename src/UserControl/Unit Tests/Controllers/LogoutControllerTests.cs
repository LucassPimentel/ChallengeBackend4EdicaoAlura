using FluentAssertions;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using Moq;
using UserControl.Controllers;
using UserControl.Interfaces;
using Xunit;

namespace UserControl.Unit_Tests.Controllers
{
    public class LogoutControllerTests
    {
        Mock<ILogoutService> _logoutService;
        LogoutController logoutController;
        public LogoutControllerTests()
        {
            _logoutService = new Mock<ILogoutService>();
            logoutController = new LogoutController(_logoutService.Object);
        }

        [Fact]
        public void LogoutUser_WhenUserLogout_ShouldSignOutAndReturnStatusCodeOkAndAMessage()
        {
            _logoutService.Setup(x => x.LogoutUser()).Returns(Result.Ok());

            var result = logoutController.LogoutUser();

            var objectResult = result as ObjectResult;

            var statusCode = objectResult.StatusCode;

            var returnedMessage = objectResult.Value;

            statusCode.Should().Be(StatusCodes.Status200OK);
            returnedMessage.Should().Be("Deslogado com sucesso!");
        }

        [Fact]
        public void LogoutUser_WhenLogoutIsFailed_ShouldReturnStatusCodeUnauthorized()
        {
            _logoutService.Setup(x => x.LogoutUser()).Returns(Result.Fail("Logout falhou!"));

            var result = logoutController.LogoutUser();

            var objectResult = result as ObjectResult;

            var statusCode = objectResult.StatusCode;

            var returnedMessage = objectResult.Value.ToString();

            statusCode.Should().Be(StatusCodes.Status401Unauthorized);
            returnedMessage.Should().Contain("Logout falhou!");
        }
    }
}
