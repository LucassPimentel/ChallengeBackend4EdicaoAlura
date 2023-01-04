using Bogus;
using ChallengeBackend4EdicaoAlura.Dtos.Receitas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChallengeBackend4EdicaoAlura.Tests.Fakers.Receitas
{
    public class FakerReadReceitaDto
    {

        public static readonly Faker<ReadReceitaDto> Faker = new Faker<ReadReceitaDto>()
            .RuleFor(x => x.Descricao, y => y.Lorem.Sentence(5))
            .RuleFor(x => x.Data, y => y.Date.Recent())
            .RuleFor(x => x.Valor, y => y.Random.Decimal());

    }
}
