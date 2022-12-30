using FluentAssertions;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using Moq;
using UserControl.Controllers;
using UserControl.Dtos;
using UserControl.Dtos.Requests;
using UserControl.Interfaces;
using Xunit;

namespace UserControl.Unit_Tests.Controllers
{
    public class RegisterUserControllerTests
    {
        Mock<IRegisterUser> _registerUser;
        RegisterUserController registerUserController;
        public RegisterUserControllerTests()
        {
            _registerUser = new Mock<IRegisterUser>();
            registerUserController = new RegisterUserController(_registerUser.Object);
        }

        [Fact]
        public void RegisterUser_WhenRegisterNewUser_ShouldReturnStatusCodeOkAndAMessage()
        {
            var newUser = new PostUserDto() { Email = "email@email.com", Password = "Teste123!", RePassword = "Teste123!", Username = "Nome" };

            _registerUser.Setup(x => x.RegisterUser(It.IsAny<PostUserDto>())).Returns(Result.Ok().WithSuccess("Token que deve ser retornado..."));

            var result = registerUserController.RegisterUser(newUser);

            var objectResult = result as ObjectResult;

            var statusCode = objectResult.StatusCode;

            var returnedMessage = objectResult.Value;

            statusCode.Should().Be(StatusCodes.Status200OK);
            returnedMessage.Should().Be("Usuário registrado com sucesso!\nCódigo de ativação: Token que deve ser retornado...");
        }

        [Fact]
        public void RegisterUser_WhenTheNewUserIsInvalid_ShouldReturnStatusCodeInternalServerError()
        {
            var newUser = new PostUserDto() { Email = "email@email.com", Password = "Teste123!", RePassword = "senhainvalida", Username = "senhainvalida" };

            _registerUser.Setup(x => x.RegisterUser(It.IsAny<PostUserDto>())).Returns(Result.Fail("Invalid User."));

            var result = registerUserController.RegisterUser(newUser);

            var objectResult = result as ObjectResult;

            var statusCode = objectResult.StatusCode;

            var returnedMessage = objectResult.Value.ToString();

            statusCode.Should().Be(StatusCodes.Status500InternalServerError);
            returnedMessage.Should().Contain("Invalid User.");
        }

        [Fact]
        public void ActivateUserAccount_WhenAccountIsSuccefullyActivated_ShouldReturnStatusCodeOkAndAMessage()
        {
            var activateAccountRequest = new ActivateAccountRequest() { UserId = 1, ActivationCode = "codigoAtivacao" };

            _registerUser.Setup(x => x.ActivateUserAccount(It.IsAny<ActivateAccountRequest>())).Returns(Result.Ok().WithSuccess("Conta ativada com sucesso!"));

            var result = registerUserController.ActivateUserAccount(activateAccountRequest);

            var objectResult = result as ObjectResult;

            var statusCode = objectResult.StatusCode;

            var returnedMessage = objectResult.Value;

            statusCode.Should().Be(StatusCodes.Status200OK);
            returnedMessage.Should().Be("Conta ativada com sucesso!");
        }

        [Fact]
        public void ActivateUserAccount_WhenAccountIsNotActivated_ShouldReturnStatusCodeInternalServerErrorAndAMessage()
        {
            var activateAccountRequest = new ActivateAccountRequest() { UserId = 1, ActivationCode = "codigoAtivacao" };

            _registerUser.Setup(x => x.ActivateUserAccount(It.IsAny<ActivateAccountRequest>())).Returns(Result.Fail("Ocorreu um erro, conta não ativada!"));

            var result = registerUserController.ActivateUserAccount(activateAccountRequest);

            var objectResult = result as ObjectResult;

            var statusCode = objectResult.StatusCode;

            var returnedMessage = objectResult.Value.ToString();
            
            statusCode.Should().Be(StatusCodes.Status500InternalServerError);
            returnedMessage.Should().Contain("Ocorreu um erro, conta não ativada!");
        }
    }
}
