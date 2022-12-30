using ChallengeBackend4EdicaoAlura.Dtos.Receitas;
using FluentAssertions;
using System.Net.Http.Json;
using System.Net;
using ChallengeBackend4EdicaoAlura.Tests.Fakers.Receita;

namespace IntegrationTests.Tests
{
    public class IntegrationTestsReceita
    {
        private readonly ChallengeApiApplication _application;
        private readonly HttpClient _client;

        public IntegrationTestsReceita()
        {
            _application = new ChallengeApiApplication();
            DataBaseMockData.MockData(_application);
            _client = _application.CreateClient();
        }
        [Fact]
        public async void GetReceitas_WhenSuccefullyExecuted_ShouldReturnStatusCodeOkAndReceitas()
        {
            var statusCodeResult = await _client.GetAsync("https://localhost:7135/Receitas");

            var returnedReceitas = await _client.GetFromJsonAsync<List<ReadReceitaDto>>("https://localhost:7135/Receitas");

            statusCodeResult.StatusCode.Should().Be(HttpStatusCode.OK);
            returnedReceitas.Should().NotBeEmpty();
        }

        [Fact]
        public async void CreateReceita_ValidReceita_ShouldReturnStatusCodeCreatedAndReceita()
        {
            var despesa = FakerPostReceitaDto.Faker.Generate();

            var statusCodeResult = await _client.PostAsJsonAsync("https://localhost:7135/Receitas", despesa);

            var urlCreatedReceita = statusCodeResult.Headers.Location;

            var returnedReceita = await _client.GetFromJsonAsync<ReadReceitaDto>(urlCreatedReceita);

            statusCodeResult.StatusCode.Should().Be(HttpStatusCode.Created);
            returnedReceita.Should().BeEquivalentTo(despesa);
        }

        [Fact]
        public async void CreateReceita_AlreadyExistsInDb_ShouldReturnStatusCodeBadRequestAndAnArgumentException()
        {
            var receita = FakerPostReceitaDto.Faker.Generate();

            var a = await _client.PostAsJsonAsync("https://localhost:7135/Receitas", receita);

            var statusCodeResult = await _client.PostAsJsonAsync("https://localhost:7135/Receitas", receita);

            var returnedMessage = await statusCodeResult.Content.ReadAsStringAsync();

            statusCodeResult.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            returnedMessage.Should().Be("Descrição e mês já existente.");
        }


        [Fact]
        public async void GetReceitaById_WhenReceiveAValidId_ShouldReturnStatusCodeOkAndReadReceita()
        {
            var despesa = FakerPostReceitaDto.Faker.Generate();

            var createdReceitaForSearchById = await _client.PostAsJsonAsync("https://localhost:7135/Receitas", despesa);

            var urlCreatedReceita = createdReceitaForSearchById.Headers.Location;

            var statusCodeResult = await _client.GetAsync(urlCreatedReceita);

            var returnedReceita = await _client.GetFromJsonAsync<ReadReceitaDto>(urlCreatedReceita);

            statusCodeResult.StatusCode.Should().Be(HttpStatusCode.OK);
            returnedReceita.Should().BeEquivalentTo(despesa);
        }

        [Fact]
        public async void GetReceitaById_WhenEntityIsNull_ShouldReturnThrowAKeyNotFoundExceptionAndStatusCodeNotFound()
        {
            var statusCodeResult = await _client.GetAsync("https://localhost:7135/Receitas/1000");

            var returnedMessage = await statusCodeResult.Content.ReadAsStringAsync();

            statusCodeResult.StatusCode.Should().Be(HttpStatusCode.NotFound);
            returnedMessage.Should().Be("Não encontrado...");
        }

        [Fact]
        public async void PutReceita_WhenReceitaIsUpdated_ShouldReturnStatusCodeNoContent()
        {
            var receitaThatWillBeUpdated = FakerPostReceitaDto.Faker.Generate();

            var updatedReceita = FakerPutReceitaDto.Faker.Generate();

            var createdReceita = await _client.PostAsJsonAsync("https://localhost:7135/Receitas", receitaThatWillBeUpdated);

            var createdReceitaId = createdReceita.Headers.Location.Segments[2];

            var statusCodeResult = await _client.PutAsJsonAsync($"https://localhost:7135/Receitas/{createdReceitaId}", updatedReceita);

            var despesaThatHasBeenUpdated = await _client.GetFromJsonAsync<ReadReceitaDto>($"https://localhost:7135/Receitas/{createdReceitaId}");

            statusCodeResult.StatusCode.Should().Be(HttpStatusCode.NoContent);
            despesaThatHasBeenUpdated.Should().BeEquivalentTo(updatedReceita);
        }

        [Fact]
        public async void PutReceita_WhenEntityIsNull_ShouldThrowAKeyNotFoundExceptionAndStatusCodeNotFound()
        {
            var updatedReceita = FakerPutReceitaDto.Faker.Generate();

            var statusCodeResult = await _client.PutAsJsonAsync($"https://localhost:7135/Receitas/1000", updatedReceita);

            var returneMessage = await statusCodeResult.Content.ReadAsStringAsync();

            statusCodeResult.StatusCode.Should().Be(HttpStatusCode.NotFound);
            returneMessage.Should().Be("Não encontrado...");
        }

        [Fact]
        public async void PutReceita_WhenReceitaAlreadyExistInDb_ShouldThrowAnArgumentExceptionAndStatusCodeBadRequest()
        {
            var despesaThatWillBeUpdated = FakerPostReceitaDto.Faker.Generate();

            var createdReceita = await _client.PostAsJsonAsync("https://localhost:7135/Receitas", despesaThatWillBeUpdated);

            var updatedReceita = FakerPutReceitaDto.Faker.Generate();
            updatedReceita.Descricao = despesaThatWillBeUpdated.Descricao;
            updatedReceita.Data = despesaThatWillBeUpdated.Data;

            var createdReceitaId = createdReceita.Headers.Location.Segments[2];

            var statusCodeResult = await _client.PutAsJsonAsync($"https://localhost:7135/Receitas/{createdReceitaId}", updatedReceita);

            var returnedMessage = await statusCodeResult.Content.ReadAsStringAsync();

            statusCodeResult.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            returnedMessage.Should().Be("Descrição e mês já existente.");
        }

        [Fact]
        public async void DeleteReceita_WhenEntityIsNull_ShouldThrowAKeyNotFoundExceptionAndStatusCodeNotFound()
        {
            var statusCodeResult = await _client.DeleteAsync("https://localhost:7135/Receitas/1000");

            var returnedMessage = await statusCodeResult.Content.ReadAsStringAsync();

            statusCodeResult.StatusCode.Should().Be(HttpStatusCode.NotFound);
            returnedMessage.Should().Be("Não encontrado...");
        }

        [Fact]
        public async void GetReceitaByDescricao_WhenReceiveAExistingDescricao_ShouldReturnStatusCodeOkAndEquivalentsReceitas()
        {
            var despesa = FakerPostReceitaDto.Faker.Generate();
            despesa.Descricao = "Descricao igual";

            var anotherReceita = FakerPostReceitaDto.Faker.Generate();
            anotherReceita.Data = DateTime.UtcNow.AddMonths(2);
            anotherReceita.Descricao = "Descricao igual";

            await _client.PostAsJsonAsync("https://localhost:7135/Receitas", despesa);
            await _client.PostAsJsonAsync("https://localhost:7135/Receitas", anotherReceita);

            var statusCodeResult = await _client.GetAsync("https://localhost:7135/Receitas/descricao?descricao=Descricao%20igual");

            var returnedReceitasByDescricao = await _client.GetFromJsonAsync<List<ReadReceitaDto>>("https://localhost:7135/Receitas/descricao?descricao=Descricao%20igual");

            statusCodeResult.StatusCode.Should().Be(HttpStatusCode.OK);
            returnedReceitasByDescricao.Count.Should().Be(2);
        }

        [Fact]
        public async void GetReceitaByDate_WhenReceiveAnExistingDate_ShouldReturnStatusCodeOkAndEquivalentsReceitas()
        {
            var statusCodeResult = await _client.GetAsync($"https://localhost:7135/Receitas/{DateTime.UtcNow.Year}/{DateTime.UtcNow.Month}");

            var returnedReceitasByDate = await _client.GetFromJsonAsync<List<ReadReceitaDto>>($"https://localhost:7135/Receitas/{DateTime.UtcNow.Year}/{DateTime.UtcNow.Month}");

            statusCodeResult.StatusCode.Should().Be(HttpStatusCode.OK);
            returnedReceitasByDate.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public async void DeleteReceita_WhenReceiveAValidId_ShouldReturnStatusCodeNoContent()
        {
            var despesa = FakerPostReceitaDto.Faker.Generate();

            var createdReceita = await _client.PostAsJsonAsync("https://localhost:7135/Receitas", despesa);

            var createdReceitaId = createdReceita.Headers.Location.Segments[2];

            var result = await _client.DeleteAsync(createdReceita.Headers.Location);

            var wasReceitaDeleted = await _client.GetAsync($"https://localhost:7135/Receitas/{createdReceita}");

            result.StatusCode.Should().Be(HttpStatusCode.NoContent);
            wasReceitaDeleted.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
