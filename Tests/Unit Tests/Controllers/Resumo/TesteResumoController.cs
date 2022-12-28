using ChallengeBackend4EdicaoAlura.Controllers;
using ChallengeBackend4EdicaoAlura.Interfaces;
using ChallengeBackend4EdicaoAlura.Tests.Fakers.Resumo;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace ChallengeBackend4EdicaoAlura.Tests.Resumo
{
    public class TesteResumoController
    {
        Mock<IResumoRepository> resumoRepository;

        ResumoController resumoController;
        public TesteResumoController()
        {
            resumoRepository = new Mock<IResumoRepository>();

            resumoController = new ResumoController(resumoRepository.Object);
        }

        [Fact]
        public void GetResumoByDate_ResumoWasReturned_ReturnStatusOkAndResumo()
        {
            var resumo = FakerReadResumoDto.Faker.Generate();

            resumoRepository.Setup(x => x.GerarResumo(It.IsAny<int>(), It.IsAny<int>())).Returns(resumo);

            var result = resumoController.GetResumoByDate(It.IsAny<int>(), It.IsAny<int>());
            var objectResult = result as ObjectResult;

            objectResult.Should().NotBeNull();
            objectResult.StatusCode.Should().Be(StatusCodes.Status200OK);
            objectResult.Value.Should().Be(resumo);
        }
    }
}
