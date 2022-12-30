using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using UserControl.Services;
using Xunit;

namespace UserControl.Unit_Tests.Services
{
    public class TokenServiceTests
    {
        [Fact]
        public void CreateToken_WhenCreateToken_ShouldReturnToken()
        {
            var identityUser = new IdentityUser<int>() { Id = 1, UserName = "Teste"};

            var tokenService = new TokenService();

            var token = tokenService.CreateToken(identityUser);

            token.Should().NotBeNull();
        }
    }
}
