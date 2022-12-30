using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using UserControl.Dtos;
using UserControl.Dtos.Requests;
using UserControl.Interfaces;

namespace UserControl.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RegisterUserController : ControllerBase
    {
        private readonly IRegisterUser _cadastroService;

        public RegisterUserController(IRegisterUser cadastroService)
        {
            _cadastroService = cadastroService;
        }

        [HttpPost]
        public IActionResult RegisterUser(PostUserDto user)
        {
            var result = _cadastroService.RegisterUser(user);
            return result.IsSuccess ? (Ok($"Usuário registrado com sucesso!\nCódigo de ativação: {result.Successes[0].Message}")) : StatusCode(500, result.Errors[0]);
        }

        [HttpGet("ActivateUserAccount")]
        public IActionResult ActivateUserAccount([FromQuery] ActivateAccountRequest activateAccountRequest)
        {
            var result = _cadastroService.ActivateUserAccount(activateAccountRequest);

            return result.IsSuccess ? (Ok("Conta ativada com sucesso!")) : (StatusCode(500, result.Errors[0]));
        }
    }
}
