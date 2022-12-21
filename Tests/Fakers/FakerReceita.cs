using Bogus;
using ChallengeBackend4EdicaoAlura.Models;

namespace ChallengeBackend4EdicaoAlura.Tests.Fakers
{
    public class FakerReceita
    {
        public static Faker<Receita> Faker = new Faker<Receita>()
            .RuleFor(x => x.Id, y => y.Random.Int())
            .RuleFor(x => x.Data, DateTime.UtcNow)
            .RuleFor(x => x.Descricao, y => y.Lorem.Text())
            .RuleFor(x => x.Valor, y => y.Random.Decimal());

    }
}
