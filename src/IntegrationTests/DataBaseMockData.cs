using ChallengeBackend4EdicaoAlura.Context;
using ChallengeBackend4EdicaoAlura.Tests.Fakers.Despesas;
using ChallengeBackend4EdicaoAlura.Tests.Fakers.Receitas;
using Microsoft.Extensions.DependencyInjection;

namespace IntegrationTests
{
    public class DataBaseMockData
    {
        public static async Task MockData(ChallengeApiApplication application)
        {
            using (var scope = application.Services.CreateScope())
            {
                var provider = scope.ServiceProvider;
                using (var dbContext = provider.GetRequiredService<DataBaseContext>())
                {
                    await dbContext.Database.EnsureCreatedAsync();

                    var idx = 0;

                    while (idx < 5)
                    {
                        await dbContext.Despesas.AddAsync(FakerDespesa.Faker.Generate());
                        await dbContext.Receitas.AddAsync(FakerReceita.Faker.Generate());
                        idx++;
                    }
                    await dbContext.SaveChangesAsync();
                }
            }

        }
    }
}
