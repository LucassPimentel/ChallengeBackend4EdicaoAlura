using Microsoft.AspNetCore.Mvc;
using UserControl.Interfaces;
using UserControl.Services;

namespace UserControl.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LogoutController : ControllerBase
    {
        private ILogoutService _logoutService;

        public LogoutController(ILogoutService logoutService)
        {
            _logoutService = logoutService;
        }

        [HttpPost]
        public IActionResult LogoutUser()
        {
            var result = _logoutService.LogoutUser();
            return result.IsSuccess ? (Ok("Deslogado com sucesso!")) : (Unauthorized(result.Errors[0]));
        }
    }
}
