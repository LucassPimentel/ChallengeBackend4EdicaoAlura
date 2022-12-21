using Autofac.Extras.FakeItEasy;
using AutoMapper;
using ChallengeBackend4EdicaoAlura.Context;
using ChallengeBackend4EdicaoAlura.Dtos.Resumos;
using ChallengeBackend4EdicaoAlura.Interfaces;
using ChallengeBackend4EdicaoAlura.Profiles;
using ChallengeBackend4EdicaoAlura.Repositories;
using ChallengeBackend4EdicaoAlura.Tests.Fakers;
using ChallengeBackend4EdicaoAlura.Util;
using FakeItEasy;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace ChallengeBackend4EdicaoAlura.Tests.Unit_Tests.Repositories
{
    public class ResumoRepositoryTeste
    {

        protected readonly Mock<IDespesaRepository> _despesaRepository;
        protected readonly Mock<IReceitaRepository> _receitaRepository;
        private readonly Mock<IValidacao> _validacao;
        private readonly ResumoRepository _resumoRepository;
        protected readonly DataBaseContext dbContextMock;
        private readonly IMapper _mapper;


        public ResumoRepositoryTeste()
        {

            var mappingConfig = new MapperConfiguration(c =>
            c.AddProfile(new ReceitasProfle()));

            _mapper = mappingConfig.CreateMapper();

            var dbOptions = new DbContextOptionsBuilder<DataBaseContext>()
                .UseInMemoryDatabase(
                databaseName: Guid.NewGuid().ToString()
                );

            dbContextMock = new DataBaseContext(dbOptions.Options);

            _receitaRepository = new Mock<IReceitaRepository>();

            _despesaRepository = new Mock<IDespesaRepository>();

            _validacao = new Mock<IValidacao>();

            _resumoRepository = new ResumoRepository(_receitaRepository.Object, _despesaRepository.Object, dbContextMock, _validacao.Object);
        }

        [Theory]
        [InlineData(1500, 2000)]
        [InlineData(11000, 6000)]
        public void CalcularSaldoTotal_WhenSuccefullyExecuted_ShouldReturnSaldoTotal(int receitaTotal, int despesaTotal)
        {
            var expectedResult = receitaTotal - despesaTotal;

            var result = _resumoRepository.CalcularSaldoTotal(receitaTotal, despesaTotal);

            result.Should().Be(expectedResult);
        }

        [Fact]
        public void GerarDespesaTotal_WhenSuccefullyExecuted_ShouldReturnDespesaTotal()
        {
            var despesas = FakerReadDespesaDto.Faker.Generate(3);

            decimal expectedDespesaTotal = 0;

            var resumo = new ReadResumoDto();

            _despesaRepository.Setup(x => x.GetDespesaByDate(It.IsAny<int>(), It.IsAny<int>())).Returns(despesas);

            var result = _resumoRepository.GerarDespesaTotal(resumo, DateTime.UtcNow.Year, DateTime.UtcNow.Month);

            foreach (var despesa in despesas)
            {
                expectedDespesaTotal += despesa.Valor;
            }

            result.Should().Be(expectedDespesaTotal);
        }
        // VERIFICAR NOS TESTES DE DESPESA E RECEITA REPOSITORY SE NAO TEM COMO USAR O MOCK PARA SIMULAR O RETORNO DO BANCO DE DADOS...
    }
}
