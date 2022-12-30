using Microsoft.AspNetCore.Mvc;
using UserControl.Dtos.Requests;
using UserControl.Interfaces;
using UserControl.Services;

namespace UserControl.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost]
        public IActionResult LoginUser(LoginRequest loginRequest)
        {
            var result = _loginService.LoginUser(loginRequest);
            return result.IsSuccess ? (Ok($"Logado com sucesso!\nToken: {result.Successes[0].Message}")) : (Unauthorized(result.Errors[0]));
        }
    }
}
