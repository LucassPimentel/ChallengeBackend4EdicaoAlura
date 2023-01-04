using ChallengeBackend4EdicaoAlura.Context;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace IntegrationTests
{
    public class ChallengeApiApplication : WebApplicationFactory<Program>
    {
        protected override IHost CreateHost(IHostBuilder builder)
        {
            var root = new InMemoryDatabaseRoot();

            builder.ConfigureServices(services =>
            {
                services.RemoveAll(typeof(DbContextOptions<DataBaseContext>));

                services.AddDbContext<DataBaseContext>(options =>
                    options.UseInMemoryDatabase("TesteDataBase", root));
            });

            return base.CreateHost(builder);
        }
    }
}
