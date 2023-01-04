using ChallengeBackend4EdicaoAlura.Controllers;
using ChallengeBackend4EdicaoAlura.Dtos.Receitas;
using ChallengeBackend4EdicaoAlura.Interfaces;
using ChallengeBackend4EdicaoAlura.Models;
using ChallengeBackend4EdicaoAlura.Tests.Fakers.Receitas;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace ChallengeBackend4EdicaoAlura.Tests.Receitas
{
    public class TesteReceitasController
    {
        Mock<IReceitaRepository> receitaRepository;
        ReceitasController receitaController;

        public TesteReceitasController()
        {
            receitaRepository = new Mock<IReceitaRepository>();
            receitaController = new ReceitasController(receitaRepository.Object);
        }

        [Fact]
        public void PostReceita_ReceitaWasAdded_Executed_ReturnStatusCreated()
        {
            var postReceitaDto = new PostReceitaDto()
            {
                Descricao = "Descricao",
                Valor = 1000,
                Data = DateTime.Now
            };

            receitaRepository.Setup(x => x.AddReceita(It.IsAny<PostReceitaDto>())).Returns(new ReceitaModel());

            var result = receitaController.CreateReceita(postReceitaDto);

            var objectResult = result as ObjectResult;

            objectResult.Should().NotBeNull();
            objectResult.StatusCode.Should().Be(StatusCodes.Status201Created);
        }

        [Fact]
        public void PostReceita_TypeItWasNotReceita_ThrowAnInvalidDataException()
        {
            receitaRepository.Setup(x => x.AddReceita(It.IsAny<PostReceitaDto>())).Throws<InvalidDataException>();

            var result = receitaController.CreateReceita(It.IsAny<PostReceitaDto>());
            var objectResult = result as ObjectResult;

            objectResult.Should().NotBeNull();
            objectResult.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        }

        [Fact]
        public void PostReceita_ReceitaAlreadyExistedInDb_ThrowAnArgumentException()
        {
            receitaRepository.Setup(x => x.AddReceita(It.IsAny<PostReceitaDto>())).Throws<ArgumentException>();

            var result = receitaController.CreateReceita(It.IsAny<PostReceitaDto>());
            var objectResult = result as ObjectResult;

            objectResult.Should().NotBeNull();
            objectResult.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        }

        [Fact]
        public void PutReceita_ReceitaWasModified_ReturnStatusNoContent()
        {
            receitaRepository.Setup(x => x.PutReceita(It.IsAny<int>(), It.IsAny<PutReceitaDto>()));

            var result = receitaController.PutReceita(It.IsAny<int>(), It.IsAny<PutReceitaDto>());
            var statusCode = result as StatusCodeResult;

            result.Should().NotBeNull();
            statusCode.StatusCode.Should().Be(StatusCodes.Status204NoContent);

        }

        [Fact]
        public void PutReceita_ReceitaWasNull_ThrowAKeyNotFoundException()
        {
            receitaRepository.Setup(x => x.PutReceita(It.IsAny<int>(), It.IsAny<PutReceitaDto>())).Throws<KeyNotFoundException>();

            var result = receitaController.PutReceita(It.IsAny<int>(), It.IsAny<PutReceitaDto>());
            var objectResult = result as ObjectResult;

            objectResult.Should().NotBeNull();
            objectResult.StatusCode.Should().Be(StatusCodes.Status404NotFound);
        }

        [Fact]
        public void PutReceita_TypeItWasNotReceita_ThrowAnInvalidDataException()
        {
            receitaRepository.Setup(x => x.PutReceita(It.IsAny<int>(), It.IsAny<PutReceitaDto>())).Throws<InvalidDataException>();

            var result = receitaController.PutReceita(It.IsAny<int>(), It.IsAny<PutReceitaDto>());
            var objectResult = result as ObjectResult;

            objectResult.Should().NotBeNull();
            objectResult.StatusCode.Should().Be(StatusCodes.Status400BadRequest);

        }

        [Fact]
        public void PutReceita_ReceitaAlreadyExistsInDb_ThrowAnArgumentException()
        {
            receitaRepository.Setup(x => x.PutReceita(It.IsAny<int>(), It.IsAny<PutReceitaDto>())).Throws<ArgumentException>();

            var result = receitaController.PutReceita(It.IsAny<int>(), It.IsAny<PutReceitaDto>());
            var objectResult = result as ObjectResult;

            objectResult.Should().NotBeNull();
            objectResult.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        }

        [Fact]
        public void DeleteReceita_ReceitaWasDeleted_ReturnStatusNoContent()
        {
            receitaRepository.Setup(x => x.DeleteReceita(It.IsAny<int>()));

            var result = receitaController.DeleteReceita(It.IsAny<int>());

            var statusCode = result as StatusCodeResult;

            statusCode.Should().NotBeNull();
            statusCode.StatusCode.Should().Be(StatusCodes.Status204NoContent);
        }

        [Fact]
        public void DeleteReceita_TypeItWasNotReceita_ThrowAnInvalidDataException()
        {
            receitaRepository.Setup(x => x.DeleteReceita(It.IsAny<int>())).Throws<InvalidDataException>();

            var result = receitaController.DeleteReceita(It.IsAny<int>());
            var objectResult = result as ObjectResult;

            objectResult.Should().NotBeNull();
            objectResult.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        }

        [Fact]
        public void DeleteReceita_ReceitaWasNull_ThrowAKeyNotFoundException()
        {
            receitaRepository.Setup(x => x.DeleteReceita(It.IsAny<int>())).Throws<KeyNotFoundException>();

            var result = receitaController.DeleteReceita(It.IsAny<int>());
            var objectResult = result as ObjectResult;

            objectResult.Should().NotBeNull();
            objectResult.StatusCode.Should().Be(StatusCodes.Status404NotFound);
        }

        [Fact]
        public void GetReceitas_ReturnStatusOkAndAllReceitas()
        {
            var fakeListReadReceitaDto = FakerReadReceitaDto.Faker.Generate(5);
            receitaRepository.Setup(x => x.GetReceitas()).Returns(fakeListReadReceitaDto);


            var result = receitaController.GetReceitas();
            var objectResult = result as ObjectResult;

            objectResult.Should().NotBeNull();
            objectResult.StatusCode.Should().Be(StatusCodes.Status200OK);
            objectResult.Value.Should().BeEquivalentTo(fakeListReadReceitaDto);
        }

        [Fact]
        public void GetReceitaById_ReceitaWasReturned_ReturnStatusOkAndReceita()
        {
            var resultReadReceitaDto = FakerReadReceitaDto.Faker.Generate();

            receitaRepository.Setup(x => x.GetReceitaById(It.IsAny<int>())).Returns(resultReadReceitaDto);

            var result = receitaController.GetReceitaById(It.IsAny<int>());
            var objectResult = result as ObjectResult;

            objectResult.Should().NotBeNull();
            objectResult.StatusCode.Should().Be(StatusCodes.Status200OK);
            objectResult.Value.Should().BeEquivalentTo(resultReadReceitaDto);
        }

        [Fact]
        public void GetReceitaById_ReceitaFoundWasNull_Executed_ThrowAnKeyNotFoundException()
        {
            receitaRepository.Setup(x => x.GetReceitaById(It.IsAny<int>())).Throws(new KeyNotFoundException("Não encontrado..."));

            var result = receitaController.GetReceitaById(It.IsAny<int>());
            var objectResult = result as ObjectResult;

            objectResult.Should().NotBeNull();
            objectResult.StatusCode.Should().Be(StatusCodes.Status404NotFound);
            objectResult.Value.Should().Be("Não encontrado...");
        }

        [Fact]
        public void GetReceitaById_IdWasInvalid_ThrowAnInvalidDataException()
        {
            receitaRepository.Setup(x => x.GetReceitaById(It.IsAny<int>())).Throws<InvalidDataException>();

            var result = receitaController.GetReceitaById(It.IsAny<int>());
            var objectResult = result as ObjectResult;

            objectResult.Should().NotBeNull();
            objectResult.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        }

        [Fact]
        public void GetReceitaByDescricao_ReceitaWasFound_ReturnStatusOkAndReadReceitasWithThatKeyWord()
        {
            var listReadReceitaDto = FakerReadReceitaDto.Faker.Generate(3);

            receitaRepository.Setup(x => x.GetReceitaByDescricao(It.IsAny<string>())).Returns(listReadReceitaDto);

            var result = receitaController.GetReceitaByDescricao(It.IsAny<string>());
            var objectResult = result as ObjectResult;

            objectResult.Should().NotBeNull();
            objectResult.StatusCode.Should().Be(StatusCodes.Status200OK);
            objectResult.Value.Should().Be(listReadReceitaDto);
        }

        [Fact]
        public void GetReceitaByDescricao_ReceitaNotFound_ThrowAnArgumentException()
        {
            receitaRepository.Setup(x => x.GetReceitaByDescricao(It.IsAny<string>())).Throws<ArgumentException>();

            var result = receitaController.GetReceitaByDescricao(It.IsAny<string>());
            var objectResult = result as ObjectResult;

            objectResult.Should().NotBeNull();
            objectResult.StatusCode.Should().Be(StatusCodes.Status404NotFound);
        }

        [Fact]
        public void GetReceitaByDescricao_SomethingWasWrong_ThrowAnException()
        {
            receitaRepository.Setup(x => x.GetReceitaByDescricao(It.IsAny<string>())).Throws<Exception>();

            var result = receitaController.GetReceitaByDescricao(It.IsAny<string>());
            var objectResult = result as ObjectResult;

            objectResult.Should().NotBeNull();
            objectResult.StatusCode.Should().Be(StatusCodes.Status404NotFound);
        }

        [Fact]
        public void GetReceitaByDate_ReceitaWasFound_ReturnStatusOkAndListReadReceitaWithThatDate()
        {
            var listReadReceitaDto = FakerReadReceitaDto.Faker.Generate(5);

            receitaRepository.Setup(x => x.GetReceitaByDate(It.IsAny<int>(), It.IsAny<int>())).Returns(listReadReceitaDto);

            var result = receitaController.GetReceitaByDate(It.IsAny<int>(), It.IsAny<int>());
            var objectResult = result as ObjectResult;

            objectResult.Should().NotBeNull();
            objectResult.StatusCode.Should().Be(StatusCodes.Status200OK);
            objectResult.Value.Should().Be(listReadReceitaDto);
        }

        [Fact]
        public void GetReceitaByDate_ReceitaNotFound_ThrowAnArgumentException()
        {
            receitaRepository.Setup(x => x.GetReceitaByDate(It.IsAny<int>(), It.IsAny<int>())).Throws<ArgumentException>();

            var result = receitaController.GetReceitaByDate(It.IsAny<int>(), It.IsAny<int>());
            var objectResult = result as ObjectResult;

            objectResult.Should().NotBeNull();
            objectResult.StatusCode.Should().Be(StatusCodes.Status404NotFound);
        }

        [Fact]
        public void GetReceitaByDate_SomethingWasWrong_ThrowAnException()
        {
            receitaRepository.Setup(x => x.GetReceitaByDate(It.IsAny<int>(), It.IsAny<int>())).Throws<Exception>();

            var result = receitaController.GetReceitaByDate(It.IsAny<int>(), It.IsAny<int>());
            var objectResult = result as ObjectResult;

            objectResult.Should().NotBeNull();
            objectResult.StatusCode.Should().Be(StatusCodes.Status404NotFound);
        }
    }
}
