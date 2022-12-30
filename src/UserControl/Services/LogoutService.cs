using FluentResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UserControl.Interfaces;

namespace UserControl.Services
{
    public class LogoutService : ILogoutService
    {
        private SignInManager<IdentityUser<int>> _signInManager;
        public LogoutService(SignInManager<IdentityUser<int>> signInManager)
        {
            _signInManager = signInManager;
        }

        public Result LogoutUser()
        {
            var identityResult = _signInManager.SignOutAsync();
            return identityResult.IsCompletedSuccessfully ? (Result.Ok()) : (Result.Fail("Logout falhou!"));
        }
    }
}
