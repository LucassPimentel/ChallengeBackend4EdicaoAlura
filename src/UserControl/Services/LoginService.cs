using FluentResults;
using Microsoft.AspNetCore.Identity;
using UserControl.Dtos.Requests;
using UserControl.Interfaces;

namespace UserControl.Services
{
    public class LoginService : ILoginService
    {
        private SignInManager<IdentityUser<int>> _signInManager;
        private TokenService _tokenService;

        public LoginService(SignInManager<IdentityUser<int>> signInManager, TokenService tokenService)
        {
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        public Result LoginUser(LoginRequest loginRequest)
        {
            var requestResult = _signInManager.PasswordSignInAsync(loginRequest.Username, loginRequest.Password, false, false);

            if (requestResult.Result.Succeeded)
            {
                var identityUser = _signInManager
                    .UserManager
                    .Users
                    .FirstOrDefault(user => user.NormalizedUserName == loginRequest.Username.ToUpper());

                var token = _tokenService.CreateToken(identityUser);

                return Result.Ok().WithSuccess(token.Value);
            }
            return Result.Fail("login falhou");
        }
    }
}
