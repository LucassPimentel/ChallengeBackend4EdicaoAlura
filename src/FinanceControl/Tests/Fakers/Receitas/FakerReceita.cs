using Bogus;
using ChallengeBackend4EdicaoAlura.Models;

namespace ChallengeBackend4EdicaoAlura.Tests.Fakers.Receitas
{
    public class FakerReceita
    {
        public static Faker<ReceitaModel> Faker = new Faker<ReceitaModel> ()
            .RuleFor(x => x.Id, y => y.Random.Int())
            .RuleFor(x => x.Data, DateTime.UtcNow)
            .RuleFor(x => x.Descricao, y => y.Lorem.Text())
            .RuleFor(x => x.Valor, y => y.Random.Decimal());

    }
}
