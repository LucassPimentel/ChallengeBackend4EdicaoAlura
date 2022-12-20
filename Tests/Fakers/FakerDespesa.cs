﻿using Bogus;
using ChallengeBackend4EdicaoAlura.Enums;
using ChallengeBackend4EdicaoAlura.Models;

namespace ChallengeBackend4EdicaoAlura.Tests.Fakers
{
    public class FakerDespesa
    {
        public static readonly Faker<Despesa> Faker = new Faker<Despesa>()
        {
            Locale = "pt_BR"
        }
            .RuleFor(x => x.Data, y => y.Date.Recent())
            .RuleFor(x => x.Descricao, y => y.Lorem.Text())
            .RuleFor(x => x.Categoria, y => y.PickRandom<CategoriaDespesa>())
            .RuleFor(x => x.Valor, y => y.Random.Decimal(100, 1000))
            .RuleFor(x => x.Id, y => y.Random.Int());
    }
}
