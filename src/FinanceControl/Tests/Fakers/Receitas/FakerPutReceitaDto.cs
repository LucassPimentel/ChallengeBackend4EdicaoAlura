using Bogus;
using ChallengeBackend4EdicaoAlura.Dtos.Receitas;
using ChallengeBackend4EdicaoAlura.Models;

namespace ChallengeBackend4EdicaoAlura.Tests.Fakers.Receitas
{
    public class FakerPutReceitaDto
    {
        public static Faker<PutReceitaDto> Faker = new Faker<PutReceitaDto>()
            .RuleFor(x => x.Data, DateTime.UtcNow)
            .RuleFor(x => x.Descricao, y => y.Lorem.Text())
            .RuleFor(x => x.Valor, y => y.Random.Decimal(100, 1000));
    }
}
