using Bogus;
using ChallengeBackend4EdicaoAlura.Dtos.Despesas;
using ChallengeBackend4EdicaoAlura.Enums;

namespace ChallengeBackend4EdicaoAlura.Tests.Fakers.Despesas
{
    public class FakerPostDespesaDto
    {
        public static Faker<PostDespesaDto> Faker = new Faker<PostDespesaDto>()
        {
            Locale = "pt_BR"
        }
            .RuleFor(x => x.Data, y => y.Date.Recent())
            .RuleFor(x => x.Descricao, y => y.Lorem.Text())
            .RuleFor(x => x.Categoria, y => y.PickRandom<CategoriaDespesa>())
            .RuleFor(x => x.Valor, y => y.Random.Decimal(100, 1000));
    }
}
