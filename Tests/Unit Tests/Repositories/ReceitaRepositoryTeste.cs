using Autofac.Extras.FakeItEasy;
using AutoMapper;
using ChallengeBackend4EdicaoAlura.Context;
using ChallengeBackend4EdicaoAlura.Dtos.Despesas;
using ChallengeBackend4EdicaoAlura.Dtos.Receitas;
using ChallengeBackend4EdicaoAlura.Interfaces;
using ChallengeBackend4EdicaoAlura.Profiles;
using ChallengeBackend4EdicaoAlura.Repositories;
using ChallengeBackend4EdicaoAlura.Tests.Fakers;
using ChallengeBackend4EdicaoAlura.Util;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace ChallengeBackend4EdicaoAlura.Tests.Unit_Tests.Repositories
{
    public class ReceitaRepositoryTeste
    {
        private readonly AutoFake _autoFake;
        private readonly IMapper _mapper;
        private readonly IValidacao _validacao;
        private readonly DataBaseContext dbContextMock;
        private readonly ReceitaRepository _receitaRepository;

        public ReceitaRepositoryTeste()
        {
            _autoFake = new AutoFake();
            _validacao = _autoFake.Resolve<IValidacao>();

            var mappingConfig = new MapperConfiguration(c =>
            c.AddProfile(new ReceitasProfle()));

            _mapper = mappingConfig.CreateMapper();

            var dbOptions = new DbContextOptionsBuilder<DataBaseContext>()
                .UseInMemoryDatabase(
                databaseName: Guid.NewGuid().ToString()
                );

            dbContextMock = new DataBaseContext(dbOptions.Options);

            _receitaRepository = new ReceitaRepository(dbContextMock, _mapper, _validacao);
        }

        [Fact]
        public void AddReceita_WhenReceitaWasAdded_ShouldReturnReceita()
        {
            var receita = FakerReceita.Faker.Generate();
            receita.Id = 1;

            var putReceitaDto = _mapper.Map<PostReceitaDto>(receita);

            var result = _receitaRepository.AddReceita(putReceitaDto);

            result.Should().BeEquivalentTo(receita);
        }

        [Fact]
        public void DeleteReceita_WhenReceitaWasDeleted_ShouldReturnNothing()
        {
            var receita = FakerReceita.Faker.Generate();
            dbContextMock.Receitas.Add(receita);
            dbContextMock.SaveChanges();

            _receitaRepository.DeleteReceita(receita.Id);

            var wasDespesaDeleted = dbContextMock.Despesas.Find(receita.Id);

            wasDespesaDeleted.Should().BeNull();
        }

        [Fact]
        public void GetReceitaByDate_WhenSuccefullyExecuted_ShouldReturnEquivalentsReceitas()
        {
            dbContextMock.Receitas.Add(FakerReceita.Faker.Generate());
            dbContextMock.Receitas.Add(FakerReceita.Faker.Generate());
            dbContextMock.Receitas.Add(FakerReceita.Faker.Generate());
            dbContextMock.SaveChanges();

            var result = _receitaRepository.GetReceitaByDate(DateTime.UtcNow.Year, DateTime.UtcNow.Month);

            result.Should().NotBeEmpty();
        }

        [Fact]
        public void GetReceitaById_WhenSuccefullyExecuted_ShouldReturnEquivalentReceita()
        {
            var receita = FakerReceita.Faker.Generate();
            dbContextMock.Receitas.Add(receita);
            dbContextMock.SaveChanges();

            var readDespesa = _mapper.Map<ReadReceitaDto>(receita);

            var result = _receitaRepository.GetReceitaById(receita.Id);

            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(readDespesa);
        }

        [Fact]
        public void GetReceitaByDescricao_WhenSuccefullyExecuted_ShouldReturnEquivalentsReceitas()
        {
            var receita = FakerReceita.Faker.Generate();
            dbContextMock.Receitas.Add(receita);
            dbContextMock.SaveChanges();

            var readDespesa = _mapper.Map<ReadReceitaDto>(receita);

            var result = _receitaRepository.GetReceitaByDescricao(receita.Descricao);

            result.Should().NotBeEmpty();
            result[0].Should().BeEquivalentTo(readDespesa);
        }

        [Fact]
        public void GetReceitas_WhenSuccefullyExecuted_ShouldReturnReceitas()
        {
            dbContextMock.Receitas.Add(FakerReceita.Faker.Generate());
            dbContextMock.Receitas.Add(FakerReceita.Faker.Generate());
            dbContextMock.Receitas.Add(FakerReceita.Faker.Generate());
            dbContextMock.SaveChanges();

            var result = _receitaRepository.GetReceitas();

            result.Should().NotBeEmpty().And.NotBeNull();
            result.Count.Should().Be(3);
        }

        [Fact]
        public void PutReceita_WhenDespesaWasUpdated_ShouldReturnNothing()
        {
            var receitaThatWilBelUpdate = FakerReceita.Faker.Generate();
            dbContextMock.Receitas.Add(receitaThatWilBelUpdate);
            dbContextMock.SaveChanges();


            var updatedReceita = FakerReceita.Faker.Generate();
            dbContextMock.Receitas.Add(updatedReceita);
            dbContextMock.SaveChanges();

            var putDespesaDto = _mapper.Map<PutReceitaDto>(updatedReceita);

            _receitaRepository.PutReceita(receitaThatWilBelUpdate.Id, putDespesaDto);

            receitaThatWilBelUpdate.Valor.Should().Be(updatedReceita.Valor);
            receitaThatWilBelUpdate.Descricao.Should().Be(updatedReceita.Descricao);
        }
    }
}
