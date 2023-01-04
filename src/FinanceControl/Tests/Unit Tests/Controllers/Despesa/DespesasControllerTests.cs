using ChallengeBackend4EdicaoAlura.Controllers;
using ChallengeBackend4EdicaoAlura.Dtos.Despesas;
using ChallengeBackend4EdicaoAlura.Dtos.Receitas;
using ChallengeBackend4EdicaoAlura.Interfaces;
using ChallengeBackend4EdicaoAlura.Tests.Fakers.Despesas;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace ChallengeBackend4EdicaoAlura.Tests
{
    public class DespesasControllerTests
    {
        Mock<IDespesaRepository> despesaRepository;
        DespesasController despesaController;

        public DespesasControllerTests()
        {
            despesaRepository = new Mock<IDespesaRepository>();
            despesaController = new DespesasController(despesaRepository.Object);
        }

        [Fact]
        public void PostDespesa_DespesaWasAdded_ReturnStatusCreated()
        {
            var createdDespesa = FakerDespesa.Faker.Generate();

            despesaRepository.Setup(x => x.CreateDespesa(It.IsAny<PostDespesaDto>())).Returns(createdDespesa);

            var result = despesaController.CreateDespesa(It.IsAny<PostDespesaDto>());
            var objectResult = result as ObjectResult;

            objectResult.Should().NotBeNull();
            objectResult.StatusCode.Should().Be(StatusCodes.Status201Created);
            createdDespesa.Should().BeEquivalentTo(objectResult.Value);

        }

        [Fact]
        public void PostDespesa_TypeItWasNotDespesa_ThrowAnKeyNotFoundException()
        {
            despesaRepository.Setup(x => x.CreateDespesa(It.IsAny<PostDespesaDto>())).Throws<KeyNotFoundException>();

            var result = despesaController.CreateDespesa(It.IsAny<PostDespesaDto>());
            var objectResult = result as ObjectResult;

            objectResult.Should().NotBeNull();
            objectResult.StatusCode.Should().Be(StatusCodes.Status404NotFound);

        }

        [Fact]
        public void PostDespesa_DespesaAlreadyExistedInDb_ThrowAnArgumentException()
        {
            despesaRepository.Setup(x => x.CreateDespesa(It.IsAny<PostDespesaDto>())).Throws<ArgumentException>();

            var result = despesaController.CreateDespesa(It.IsAny<PostDespesaDto>());
            var objectResult = result as ObjectResult;

            objectResult.Should().NotBeNull();
            objectResult.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        }

        [Fact]
        public void PutDespesa_DespesaWasModified_ReturnStatusNoContent()
        {
            despesaRepository.Setup(x => x.PutDespsa(It.IsAny<int>(), It.IsAny<PutDespesaDto>()));

            var result = despesaController.PutDespesa(It.IsAny<int>(), It.IsAny<PutDespesaDto>());
            var statusCode = result as StatusCodeResult;

            statusCode.Should().NotBeNull();
            statusCode.StatusCode.Should().Be(StatusCodes.Status204NoContent);
        }

        [Fact]
        public void PutDespesa_DespesaWasNull_ThrowAKeyNotFoundException()
        {
            despesaRepository.Setup(x => x.PutDespsa(It.IsAny<int>(), It.IsAny<PutDespesaDto>())).Throws<KeyNotFoundException>();

            var result = despesaController.PutDespesa(It.IsAny<int>(), new PutDespesaDto());
            var objectResult = result as ObjectResult;

            objectResult.Should().NotBeNull();
            objectResult.StatusCode.Should().Be(StatusCodes.Status404NotFound);
        }

        [Fact]
        public void PutDespesa_TypeWasNotDespesa_ThrowKeyNotFoundException()
        {
            despesaRepository.Setup(x => x.PutDespsa(It.IsAny<int>(), It.IsAny<PutDespesaDto>())).Throws<KeyNotFoundException>();

            var result = despesaController.PutDespesa(It.IsAny<int>(), It.IsAny<PutDespesaDto>());
            var objectResult = result as ObjectResult;

            objectResult.Should().NotBeNull();
            objectResult.StatusCode.Should().Be(StatusCodes.Status404NotFound);
        }

        [Fact]
        public void PutDespesa_DespesaAlreadyExistedIsDb_ThrowAnArgumentException()
        {
            despesaRepository.Setup(x => x.PutDespsa(It.IsAny<int>(), It.IsAny<PutDespesaDto>())).Throws<ArgumentException>();

            var result = despesaController.PutDespesa(It.IsAny<int>(), It.IsAny<PutDespesaDto>());
            var objectResult = result as ObjectResult;

            objectResult.Should().NotBeNull();
            objectResult.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        }

        [Fact]
        public void DeleteDespesa_DespesaWasDeleted_ReturnStatusNoContet()
        {
            despesaRepository.Setup(x => x.DeleteDespesa(It.IsAny<int>()));

            var result = despesaController.DeleteDespesa(It.IsAny<int>());
            var statusCode = result as StatusCodeResult;

            statusCode.Should().NotBeNull();
            statusCode.StatusCode.Should().Be(StatusCodes.Status204NoContent);
        }

        [Fact]
        public void DeleteDespesa_TypeItWasNotDespesa_ThrowAnKeyNotFoundException()
        {
            despesaRepository.Setup(x => x.DeleteDespesa(It.IsAny<int>())).Throws<KeyNotFoundException>();

            var result = despesaController.DeleteDespesa(It.IsAny<int>());
            var objectResult = result as ObjectResult;

            objectResult.Should().NotBeNull();
            objectResult.StatusCode.Should().Be(StatusCodes.Status404NotFound);
        }

        [Fact]
        public void DeleteDespesa_DespesaWasNull_ThrowAKeyNotFoundException()
        {
            despesaRepository.Setup(x => x.DeleteDespesa(It.IsAny<int>())).Throws<KeyNotFoundException>();

            var result = despesaController.DeleteDespesa(It.IsAny<int>());
            var objectResult = result as ObjectResult;

            objectResult.Should().NotBeNull();
            objectResult.StatusCode.Should().Be(StatusCodes.Status404NotFound);
        }

        [Fact]
        public void GetDespesaById_DespesaWasReturned_ReturnStatusOkAndDespesa()
        {
            var readDespesaReturned = new ReadReceitaDto()
            {
                Data = DateTime.Now,
                Descricao = "Teste",
                Valor = 2300
            };

            despesaRepository.Setup(x => x.GetDespesaById(It.IsAny<int>())).Returns(It.IsAny<ReadDespesaDto>());

            var result = despesaController.GetDespesaById(It.IsAny<int>());
            var objectResult = result as ObjectResult;

            objectResult.Should().NotBeNull();
            objectResult.StatusCode.Should().Be(StatusCodes.Status200OK);

        }

        [Fact]
        public void GetDespesaById_IdWasInvalid_ThrowAnKeyNotFoundException()
        {
            despesaRepository.Setup(x => x.GetDespesaById(It.IsAny<int>())).Throws<KeyNotFoundException>();

            var result = despesaController.GetDespesaById(It.IsAny<int>());
            var objectResult = result as ObjectResult;

            objectResult.Should().NotBeNull();
            objectResult.StatusCode.Should().Be(StatusCodes.Status404NotFound);
        }

        [Fact]
        public void GetDespesaById_DespesaFoundWasNull_ThrowAKeyNotFoundException()
        {
            despesaRepository.Setup(x => x.GetDespesaById(It.IsAny<int>())).Throws<KeyNotFoundException>();

            var result = despesaController.GetDespesaById(It.IsAny<int>());
            var objectResult = result as ObjectResult;

            objectResult.Should().NotBeNull();
            objectResult.StatusCode.Should().Be(StatusCodes.Status404NotFound);
        }

        [Fact]
        public void GetDespesas_ReturnStatusOkAndAllDespesas()
        {
            var listReadDespesa = FakerReadDespesaDto.Faker.Generate(5);

            despesaRepository.Setup(x => x.GetDespesas()).Returns(listReadDespesa);

            var result = despesaController.GetDespesas();
            var objectResult = result as ObjectResult;

            objectResult.Should().NotBeNull();
            objectResult.Value.Should().BeEquivalentTo(listReadDespesa);
            objectResult.StatusCode.Should().Be(StatusCodes.Status200OK);
        }

        [Fact]
        public void GetDespesaByDescricao_DespesaWasFound_ReturnStatusOkAndListDespesaWithThatKeyWord()
        {
            var listReadDespesa = FakerReadDespesaDto.Faker.Generate(3);

            despesaRepository.Setup(x => x.GetDespesaByDescricao(It.IsAny<string>())).Returns(listReadDespesa);

            var result = despesaController.GetDespesaByDescricao(It.IsAny<string>());
            var objectResult = result as ObjectResult;

            objectResult.Should().NotBeNull();
            objectResult.StatusCode.Should().Be(StatusCodes.Status200OK);
            objectResult.Value.Should().BeEquivalentTo(listReadDespesa);

        }

        [Fact]
        public void GetDespesaByDescricao_DespesaNotFound_ThrowAnArgumentException()
        {
            despesaRepository.Setup(x => x.GetDespesaByDescricao(It.IsAny<string>())).Throws<ArgumentException>();

            var result = despesaController.GetDespesaByDescricao(It.IsAny<string>());
            var objectResult = result as ObjectResult;

            objectResult.Should().NotBeNull();
            objectResult.StatusCode.Should().Be(StatusCodes.Status404NotFound);
        }

        [Fact]
        public void GetDespesaByDescricao_SomethingWasWrong_ThrowAnException()
        {
            despesaRepository.Setup(x => x.GetDespesaByDescricao(It.IsAny<string>())).Throws<Exception>();

            var result = despesaController.GetDespesaByDescricao(It.IsAny<string>());
            var objectResult = result as ObjectResult;

            objectResult.Should().NotBeNull();
            objectResult.StatusCode.Should().Be(StatusCodes.Status404NotFound);
        }

        [Fact]
        public void GetDespesaByDate_DespesaWasFound_ReturnStatusOkAndListDespesaWithThatDate()
        {
            var listReadDespesa = FakerReadDespesaDto.Faker.Generate(4);

            despesaRepository.Setup(x => x.GetDespesaByDate(It.IsAny<int>(), It.IsAny<int>())).Returns(listReadDespesa);

            var result = despesaController.GetDespesaByDate(It.IsAny<int>(), It.IsAny<int>());
            var objectResult = result as ObjectResult;

            objectResult.Should().NotBeNull();
            objectResult.StatusCode.Should().Be(StatusCodes.Status200OK);
            objectResult.Value.Should().BeEquivalentTo(listReadDespesa);
        }

        [Fact]
        public void GetDespesaByDate_DespesaNotFound_ThrowAnArgumentException()
        {
            despesaRepository.Setup(x => x.GetDespesaByDate(It.IsAny<int>(), It.IsAny<int>())).Throws<ArgumentException>();

            var result = despesaController.GetDespesaByDate(It.IsAny<int>(), It.IsAny<int>());
            var objectResult = result as ObjectResult;

            objectResult.Should().NotBeNull();
            objectResult.StatusCode.Should().Be(StatusCodes.Status404NotFound);
        }

        [Fact]
        public void GetDespesaByDate_SomethingWasWrong_ThrowAnException()
        {
            despesaRepository.Setup(x => x.GetDespesaByDate(It.IsAny<int>(), It.IsAny<int>())).Throws<Exception>();

            var result = despesaController.GetDespesaByDate(It.IsAny<int>(), It.IsAny<int>());
            var objectResult = result as ObjectResult;

            objectResult.Should().NotBeNull();
            objectResult.StatusCode.Should().Be(StatusCodes.Status404NotFound);
        }

    }
}
