using Bogus;
using ChallengeBackend4EdicaoAlura.Dtos.Despesas;
using ChallengeBackend4EdicaoAlura.Enums;

namespace ChallengeBackend4EdicaoAlura.Tests.Fakers.Despesas
{
    public class FakerPutDespesaDto
    {
        public static Faker<PutDespesaDto> Faker = new Faker<PutDespesaDto>()
            .RuleFor(x => x.Data, DateTime.UtcNow)
            .RuleFor(x => x.Descricao, y => y.Lorem.Text())
            .RuleFor(x => x.Categoria, y => y.PickRandom<CategoriaDespesa>())
            .RuleFor(x => x.Valor, y => y.Random.Decimal(100, 1000));


    }
}
