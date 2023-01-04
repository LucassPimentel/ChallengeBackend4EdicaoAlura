using Autofac.Extras.FakeItEasy;
using AutoMapper;
using ChallengeBackend4EdicaoAlura.Context;
using ChallengeBackend4EdicaoAlura.Dtos.Resumos;
using ChallengeBackend4EdicaoAlura.Enums;
using ChallengeBackend4EdicaoAlura.Interfaces;
using ChallengeBackend4EdicaoAlura.Profiles;
using ChallengeBackend4EdicaoAlura.Repositories;
using ChallengeBackend4EdicaoAlura.Tests.Fakers.Despesas;
using ChallengeBackend4EdicaoAlura.Tests.Fakers.Receitas;
using ChallengeBackend4EdicaoAlura.Util;
using FakeItEasy;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace IntegrationTests.Repositories
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

        [Fact]
        public void GerarReceitaTotal_WhenSuccefullyExecuted_ShouldReturnReceitaTotal()
        {
            var receitas = FakerReadReceitaDto.Faker.Generate(3);

            decimal expectedReceitaTotal = 0;

            var resumo = new ReadResumoDto();

            _receitaRepository.Setup(x => x.GetReceitaByDate(It.IsAny<int>(), It.IsAny<int>())).Returns(receitas);

            var result = _resumoRepository.GerarReceitaTotal(resumo, DateTime.UtcNow.Year, DateTime.UtcNow.Month);

            foreach (var receita in receitas)
            {
                expectedReceitaTotal += receita.Valor;
            }

            result.Should().Be(expectedReceitaTotal);
        }

        [Fact]
        public void AdicionarGastosPorCategorias_WhenSuccefullyExecuted_ShouldAddSpentByCategory()
        {
            var despesas = FakerReadDespesaDto.Faker.Generate(3);

            var resumo = new ReadResumoDto();

            _despesaRepository.Setup(x => x.GetDespesaByDate(It.IsAny<int>(), It.IsAny<int>())).Returns(despesas);

            var categorias = new List<CategoriaDespesa>() { CategoriaDespesa.Transporte, CategoriaDespesa.Lazer };

            _resumoRepository.AdicionarGastosPorCategorias(resumo, categorias, DateTime.UtcNow.Year, DateTime.UtcNow.Month);

            resumo.GastoPorCategoria.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void IdentificarCategorias_WhenSuccefullyExecuted_ShouldReturnCategoriasDespesas()
        {
            var despesas = FakerReadDespesaDto.Faker.Generate(3);

            _despesaRepository.Setup(x => x.GetDespesaByDate(It.IsAny<int>(), It.IsAny<int>())).Returns(despesas);

            var categorias = _resumoRepository.IdentificarCategorias(DateTime.UtcNow.Year, DateTime.UtcNow.Month);

            categorias.Count().Should().BeGreaterThan(0);
        }

        [Fact]
        public void GerarResumo_WhenSuccefullyExecuted_ShouldReturnReadResumoDto()
        {
            var despesas = FakerReadDespesaDto.Faker.Generate(3);

            _despesaRepository.Setup(x => x.GetDespesaByDate(It.IsAny<int>(), It.IsAny<int>())).Returns(despesas);

            var receitas = FakerReadReceitaDto.Faker.Generate(3);

            _receitaRepository.Setup(x => x.GetReceitaByDate(It.IsAny<int>(), It.IsAny<int>())).Returns(receitas);

            var result = _resumoRepository.GerarResumo(DateTime.UtcNow.Year, DateTime.UtcNow.Month);

            result.Should().NotBeNull();
        }
    }
}
