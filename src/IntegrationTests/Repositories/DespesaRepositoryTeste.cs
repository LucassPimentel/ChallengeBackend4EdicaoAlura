using Autofac.Extras.FakeItEasy;
using AutoMapper;
using ChallengeBackend4EdicaoAlura.Context;
using ChallengeBackend4EdicaoAlura.Dtos.Despesas;
using ChallengeBackend4EdicaoAlura.Profiles;
using ChallengeBackend4EdicaoAlura.Repositories;
using ChallengeBackend4EdicaoAlura.Tests.Fakers.Despesas;
using ChallengeBackend4EdicaoAlura.Util;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace IntegrationTests.Repositories
{
    public class DespesaRepositoryTeste
    {
        private readonly AutoFake _autoFake;
        private readonly IMapper _mapper;
        private readonly IValidacao _validacao;
        private readonly DataBaseContext dbContextMock;
        private readonly DespesaRepository _despesaRepository;

        public DespesaRepositoryTeste()
        {
            _autoFake = new AutoFake();
            _validacao = _autoFake.Resolve<IValidacao>();

            var mappingConfig = new MapperConfiguration(c =>
            c.AddProfile(new DespesaProfile()));

            _mapper = mappingConfig.CreateMapper();

            var dbOptions = new DbContextOptionsBuilder<DataBaseContext>()
                .UseInMemoryDatabase(
                databaseName: Guid.NewGuid().ToString()
                );

            dbContextMock = new DataBaseContext(dbOptions.Options);

            _despesaRepository = new DespesaRepository(dbContextMock, _mapper, _validacao);
        }
        [Fact]
        public void GetDespesas_WhenSuccessfullyExecuted_ShouldReturnAllDespesas()
        {
            dbContextMock.Despesas.Add(FakerDespesa.Faker.Generate());
            dbContextMock.Despesas.Add(FakerDespesa.Faker.Generate());
            dbContextMock.Despesas.Add(FakerDespesa.Faker.Generate());
            dbContextMock.SaveChanges();

            var result = _despesaRepository.GetDespesas();

            result.Should().NotBeEmpty().And.NotBeNull();
            result.Count.Should().Be(3);
        }

        [Fact]
        public void CreateDespesa_WhenDespesaWasCreated_ShouldReturnDespesa()
        {
            var despesa = FakerPostDespesaDto.Faker.Generate();
            var result = _despesaRepository.CreateDespesa(despesa);

            var itWasCreated = dbContextMock.Despesas.Find(result.Id);

            itWasCreated.Should().NotBeNull().And.BeEquivalentTo(despesa);
            result.Should().BeEquivalentTo(despesa);
        }

        [Fact]
        public void GetDespesaByDate_WhenSuccessfullyExecuted_ShouldReturnAnEquivalentDespesa()
        {
            dbContextMock.Despesas.Add(FakerDespesa.Faker.Generate());
            dbContextMock.Despesas.Add(FakerDespesa.Faker.Generate());
            dbContextMock.Despesas.Add(FakerDespesa.Faker.Generate());
            dbContextMock.SaveChanges();

            var result = _despesaRepository.GetDespesaByDate(DateTime.UtcNow.Year, DateTime.UtcNow.Month);

            result.Should().NotBeNull().And.NotBeEmpty();
        }

        [Fact]
        public void GetDespesaByDescricao_WhenSuccessfullyExecuted_ShouldReturnAnEquivalentDespesa()
        {
            var despesa = FakerDespesa.Faker.Generate();
            dbContextMock.Despesas.Add(despesa);
            dbContextMock.SaveChanges();

            var readDespesa = _mapper.Map<ReadDespesaDto>(despesa);

            var result = _despesaRepository.GetDespesaByDescricao(despesa.Descricao);

            result.Should().NotBeEmpty();
            result[0].Should().BeEquivalentTo(readDespesa);
        }

        [Fact]
        public void GetDespesaById_WhenSuccessfullyExecuted_ShouldReturnAnEquivalentDespesa()
        {
            var despesa = FakerDespesa.Faker.Generate();
            dbContextMock.Despesas.Add(despesa);
            dbContextMock.SaveChanges();

            var readDespesa = _mapper.Map<ReadDespesaDto>(despesa);

            var result = _despesaRepository.GetDespesaById(despesa.Id);

            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(readDespesa);
        }

        [Fact]
        public void PutDespesa_WhenDespesaWasUpdated_ShouldReturnNothing()
        {
            var despesaThatWilBelUpdate = FakerDespesa.Faker.Generate();
            dbContextMock.Despesas.Add(despesaThatWilBelUpdate);
            dbContextMock.SaveChanges();


            var updatedDespesa = FakerDespesa.Faker.Generate();
            dbContextMock.Despesas.Add(updatedDespesa);
            dbContextMock.SaveChanges();

            var putDespesaDto = _mapper.Map<PutDespesaDto>(updatedDespesa);

            _despesaRepository.PutDespsa(despesaThatWilBelUpdate.Id, putDespesaDto);

            despesaThatWilBelUpdate.Categoria.Should().Be(updatedDespesa.Categoria);
            despesaThatWilBelUpdate.Descricao.Should().Be(updatedDespesa.Descricao);

        }

        [Fact]
        public void DeleteDespesa_WhenDespesaWasDeleted_ShouldReturnNothing()
        {
            var despesa = FakerDespesa.Faker.Generate();
            dbContextMock.Despesas.Add(despesa);
            dbContextMock.SaveChanges();

            _despesaRepository.DeleteDespesa(despesa.Id);

            var wasDespesaDeleted = dbContextMock.Despesas.Find(despesa.Id);

            wasDespesaDeleted.Should().BeNull();
        }
    }
}
