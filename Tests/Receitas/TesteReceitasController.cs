using ChallengeBackend4EdicaoAlura.Controllers;
using ChallengeBackend4EdicaoAlura.Dtos.Receitas;
using ChallengeBackend4EdicaoAlura.Interfaces;
using ChallengeBackend4EdicaoAlura.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TestesChallengeBackEnd4Edicao.Fakers;

namespace TestesChallengeBackEnd4Edicao.Receitas
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


        // When-Given-then
        // AAA
        [Fact]
        public void PostReceita_ReceitaIsValid_Executed_ReturnStatusCreated()
        {
            var postReceitaDto = new PostReceitaDto()
            {
                Descricao = "Descricao",
                Valor = 1000,
                Data = DateTime.Now
            };

            receitaRepository.Setup(x => x.AddReceita(It.IsAny<PostReceitaDto>())).Returns(new Receita());

            var result = receitaController.CreateReceita(postReceitaDto);

            var objectResult = result as ObjectResult;

            Assert.NotNull(objectResult);
            Assert.Equal(StatusCodes.Status201Created, objectResult.StatusCode);
        }

        [Fact]
        public void PostReceita_ReceitaIsInvalid_Executed_ThrowAnInvalidDataException()
        {
            receitaRepository.Setup(x => x.AddReceita(It.IsAny<PostReceitaDto>())).Throws<InvalidDataException>();

            var result = receitaController.CreateReceita(It.IsAny<PostReceitaDto>());
            var objectResult = result as ObjectResult;

            Assert.NotNull(objectResult);
            Assert.Equal(StatusCodes.Status400BadRequest, objectResult.StatusCode);
        }

        [Fact]
        public void PostReceita_ReceitaIsInvalid_Executed_ThrowAnException()
        {
            receitaRepository.Setup(x => x.AddReceita(It.IsAny<PostReceitaDto>())).Throws<Exception>();

            var result = receitaController.CreateReceita(It.IsAny<PostReceitaDto>());
            var objectResult = result as ObjectResult;

            Assert.NotNull(objectResult);
            Assert.Equal(StatusCodes.Status404NotFound, objectResult.StatusCode);
        }

        [Fact]
        public void PostReceita_ReceitaIsInvalid_Executed_ThrowAnArgumentException()
        {
            receitaRepository.Setup(x => x.AddReceita(It.IsAny<PostReceitaDto>())).Throws<ArgumentException>();

            var result = receitaController.CreateReceita(It.IsAny<PostReceitaDto>());
            var objectResult = result as ObjectResult;

            Assert.NotNull(objectResult);
            Assert.Equal(StatusCodes.Status400BadRequest, objectResult.StatusCode);
        }

        [Fact]
        public void PutReceita_ReceitaIsValid_Executed_ReturnStatusNoContent()
        {
            receitaRepository.Setup(x => x.PutReceita(It.IsAny<int>(), It.IsAny<PutReceitaDto>()));

            var result = receitaController.PutReceita(It.IsAny<int>(), It.IsAny<PutReceitaDto>());
            var statusCode = result as StatusCodeResult;

            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status204NoContent, statusCode.StatusCode);

        }

        [Fact]
        public void PutReceita_ReceitaIsNull_Executed_ThrowAnKeyNotFoundException()
        {
            receitaRepository.Setup(x => x.PutReceita(It.IsAny<int>(), It.IsAny<PutReceitaDto>())).Throws<KeyNotFoundException>();

            var result = receitaController.PutReceita(It.IsAny<int>(), It.IsAny<PutReceitaDto>());
            var objectResult = result as ObjectResult;

            Assert.NotNull(objectResult);
            Assert.Equal(StatusCodes.Status404NotFound, objectResult.StatusCode);
        }

        [Fact]
        public void PutReceita_TypeItIsNotReceita_Executed_ThrowAnInvalidDataException()
        {
            receitaRepository.Setup(x => x.PutReceita(It.IsAny<int>(), It.IsAny<PutReceitaDto>())).Throws<InvalidDataException>();

            var result = receitaController.PutReceita(It.IsAny<int>(), It.IsAny<PutReceitaDto>());
            var objectResult = result as ObjectResult;

            Assert.NotNull(objectResult);
            Assert.Equal(StatusCodes.Status400BadRequest, objectResult.StatusCode);

        }

        [Fact]
        public void PutReceita_ReceitaAlreadyExistsInDb_Executed_ThrowAnArgumentException()
        {
            receitaRepository.Setup(x => x.PutReceita(It.IsAny<int>(), It.IsAny<PutReceitaDto>())).Throws<ArgumentException>();

            var result = receitaController.PutReceita(It.IsAny<int>(), It.IsAny<PutReceitaDto>());
            var objectResult = result as ObjectResult;

            Assert.NotNull(objectResult);
            Assert.Equal(StatusCodes.Status400BadRequest, objectResult.StatusCode);
        }

        [Fact]
        public void DeleteReceita_ReceitaIsExcluded_ReturnStatusNoContent()
        {
            receitaRepository.Setup(x => x.DeleteReceita(It.IsAny<int>()));

            var result = receitaController.DeleteReceita(It.IsAny<int>());

            var statusCode = result as StatusCodeResult;

            Assert.NotNull(statusCode);
            Assert.Equal(StatusCodes.Status204NoContent, statusCode.StatusCode);
        }

        [Fact]
        public void DeleteReceita_TypeItIsNotReceita_ThrowAnInvalidDataException()
        {
            receitaRepository.Setup(x => x.DeleteReceita(It.IsAny<int>())).Throws<InvalidDataException>();

            var result = receitaController.DeleteReceita(It.IsAny<int>());
            var objectResult = result as ObjectResult;

            Assert.NotNull(objectResult);
            Assert.Equal(StatusCodes.Status400BadRequest, objectResult.StatusCode);
        }

        [Fact]
        public void DeleteReceita_ReceitaIsNull_ThrowAnKeyNotFoundException()
        {
            receitaRepository.Setup(x => x.DeleteReceita(It.IsAny<int>())).Throws<KeyNotFoundException>();

            var result = receitaController.DeleteReceita(It.IsAny<int>());
            var objectResult = result as ObjectResult;

            Assert.NotNull(objectResult);
            Assert.Equal(StatusCodes.Status404NotFound, objectResult.StatusCode);
        }

        [Fact]
        public void GetReceitas_ReceitasAreValid_Executed_ReturnStatusOkAndAllReceitas()
        {
            var fakeListReadReceitaDto = FakerReadReceitaDto.Faker.Generate(5);
            receitaRepository.Setup(x => x.GetReceitas()).Returns(fakeListReadReceitaDto);


            var result = receitaController.GetReceitas();
            var objectResult = result as ObjectResult;

            Assert.NotNull(objectResult);
            Assert.Equal(StatusCodes.Status200OK, objectResult.StatusCode);
            Assert.Equal(fakeListReadReceitaDto, objectResult.Value);
        }

        [Fact]
        public void GetReceitaById_IdIsValid_Executed_ReturnStatusOkAndOneReceita()
        {
            var resultReadReceitaDto = FakerReadReceitaDto.Faker.Generate();

            receitaRepository.Setup(x => x.GetReceitaById(It.IsAny<int>())).Returns(resultReadReceitaDto);

            var result = receitaController.GetReceitaById(It.IsAny<int>());
            var objectResult = result as ObjectResult;

            Assert.NotNull(objectResult);
            Assert.Equal(StatusCodes.Status200OK, objectResult.StatusCode);
            Assert.Equal(resultReadReceitaDto, objectResult.Value);
        }

        [Fact]
        public void GetReceitaById_EntityFoundIsNull_Executed_ThrowAnKeyNotFoundException()
        {
            receitaRepository.Setup(x => x.GetReceitaById(It.IsAny<int>())).Throws(new KeyNotFoundException("Não encontrado..."));

            var result = receitaController.GetReceitaById(It.IsAny<int>());
            var objectResult = result as ObjectResult;

            Assert.NotNull(objectResult);
            Assert.Equal(StatusCodes.Status404NotFound, objectResult.StatusCode);
            Assert.Equal("Não encontrado...", objectResult.Value);
        }

        [Fact]
        public void GetReceitaById_IdIsInvalid_Executed_ThrowAnInvalidDataException()
        {
            receitaRepository.Setup(x => x.GetReceitaById(It.IsAny<int>())).Throws<InvalidDataException>();

            var result = receitaController.GetReceitaById(It.IsAny<int>());
            var objectResult = result as ObjectResult;

            Assert.NotNull(objectResult);
            Assert.Equal(StatusCodes.Status400BadRequest, objectResult.StatusCode);
        }

        [Fact]
        public void GetReceitaByDescricao_ReceitaFoundByDescricao_Executed_ReturnStatusOkAndListOfReadReceitasDtoWithThatKeyWord()
        {
            var listReadReceitaDto = FakerReadReceitaDto.Faker.Generate(3);

            receitaRepository.Setup(x => x.GetReceitaByDescricao(It.IsAny<string>())).Returns(listReadReceitaDto);

            var result = receitaController.GetReceitaByDescricao(It.IsAny<string>());
            var objectResult = result as ObjectResult;

            Assert.NotNull(objectResult);
            Assert.Equal(StatusCodes.Status200OK, objectResult.StatusCode);
            Assert.Equal(listReadReceitaDto, objectResult.Value);
        }

        [Fact]
        public void GetReceitaByDescricao_ReceitaNotFound_ThrowAnArgumentException()
        {
            receitaRepository.Setup(x => x.GetReceitaByDescricao(It.IsAny<string>())).Throws<ArgumentException>();

            var result = receitaController.GetReceitaByDescricao(It.IsAny<string>());
            var objectResult = result as ObjectResult;

            Assert.NotNull(objectResult);
            Assert.Equal(StatusCodes.Status404NotFound, objectResult.StatusCode);
        }

        [Fact]
        public void GetReceitaByDescricao_SomethingIsFail_Executed_ThrowAnException()
        {
            receitaRepository.Setup(x => x.GetReceitaByDescricao(It.IsAny<string>())).Throws<Exception>();

            var result = receitaController.GetReceitaByDescricao(It.IsAny<string>());
            var objectResult = result as ObjectResult;

            Assert.NotNull(objectResult);
            Assert.Equal(StatusCodes.Status404NotFound, objectResult.StatusCode);
        }

        [Fact]
        public void GetReceitaByDate_ReceitaFoundByDate_Executed_ReturnStatusOkAndListOfReadReceita()
        {
            var listReadReceitaDto = FakerReadReceitaDto.Faker.Generate(5);

            receitaRepository.Setup(x => x.GetReceitaByDate(It.IsAny<int>(), It.IsAny<int>())).Returns(listReadReceitaDto);

            var result = receitaController.GetReceitaByDate(It.IsAny<int>(), It.IsAny<int>());
            var objectResult = result as ObjectResult;

            Assert.NotNull(objectResult);
            Assert.Equal(StatusCodes.Status200OK, objectResult.StatusCode);
            Assert.Equal(listReadReceitaDto, objectResult.Value);
        }

        [Fact]
        public void GetReceitaByDate_ReturnedListIsEmpty_Executed_ThrowAnArgumentException()
        {
            receitaRepository.Setup(x => x.GetReceitaByDate(It.IsAny<int>(), It.IsAny<int>())).Throws<ArgumentException>();

            var result = receitaController.GetReceitaByDate(It.IsAny<int>(), It.IsAny<int>());
            var objectResult = result as ObjectResult;

            Assert.NotNull(objectResult);
            Assert.Equal(StatusCodes.Status404NotFound, objectResult.StatusCode);
        }

        [Fact]
        public void GetReceitaByDate_SomethingIsFail_Executed_ThrowAnException()
        {
            receitaRepository.Setup(x => x.GetReceitaByDate(It.IsAny<int>(), It.IsAny<int>())).Throws<Exception>();

            var result = receitaController.GetReceitaByDate(It.IsAny<int>(), It.IsAny<int>());
            var objectResult = result as ObjectResult;

            Assert.NotNull(objectResult);
            Assert.Equal(StatusCodes.Status404NotFound, objectResult.StatusCode);
        }
    }
}
