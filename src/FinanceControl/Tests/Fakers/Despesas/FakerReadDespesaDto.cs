using Bogus;
using ChallengeBackend4EdicaoAlura.Dtos.Despesas;

namespace ChallengeBackend4EdicaoAlura.Tests.Fakers.Despesas
{
    public class FakerReadDespesaDto
    {
        public static readonly Faker<ReadDespesaDto> Faker = new Faker<ReadDespesaDto>()
            .RuleFor(x => x.Descricao, f => f.Lorem.Sentence(5))
            .RuleFor(x => x.Categoria, f => Enums.CategoriaDespesa.Transporte)
            .RuleFor(x => x.Data, f => f.Date.Recent())
            .RuleFor(x => x.Valor, f => f.Random.Decimal(10, 999));
    }
}
