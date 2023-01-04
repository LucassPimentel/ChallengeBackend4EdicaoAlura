using Bogus;
using ChallengeBackend4EdicaoAlura.Dtos.Receitas;

namespace ChallengeBackend4EdicaoAlura.Tests.Fakers.Receitas
{
    public class FakerPostReceitaDto
    {
        public static Faker<PostReceitaDto> Faker = new Faker<PostReceitaDto>()
            .RuleFor(x => x.Data, y => DateTime.UtcNow)
            .RuleFor(x => x.Descricao, y => y.Lorem.Text())
            .RuleFor(x => x.Valor, y => y.Random.Decimal(100, 1000));

    }
}
