using Bogus;
using ChallengeBackend4EdicaoAlura.Dtos.Resumos;

namespace ChallengeBackend4EdicaoAlura.Tests.Fakers.Resumo
{
    public class FakerReadResumoDto
    {
        public static Faker<ReadResumoDto> Faker = new Faker<ReadResumoDto>()
            .RuleFor(x => x.DespesaTotal, f => f.Random.Decimal(1000, 20000))
            .RuleFor(x => x.ReceitaTotal, f => f.Random.Decimal(1000, 10000))
            .RuleFor(x => x.GastoPorCategoria, new List<ReadGastoPorCategoriaDto>()
            {
                new ReadGastoPorCategoriaDto()
                {
                    Categoria = Enums.CategoriaDespesa.Outras,
                    Valor = 1000
                }
            })
            .RuleFor(x => x.SaldoFinal, f => f.Random.Decimal(1000, 10000));
    }
}
