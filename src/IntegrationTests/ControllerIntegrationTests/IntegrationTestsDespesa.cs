using Bogus;
using ChallengeBackend4EdicaoAlura.Dtos.Despesas;
using ChallengeBackend4EdicaoAlura.Models;
using ChallengeBackend4EdicaoAlura.Tests.Fakers.Despesas;
using FluentAssertions;
using Microsoft.AspNetCore.Builder;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Json;

namespace IntegrationTests.ControllerIntegrationTests
{
    public class IntegrationTestsDespesa
    {
        private readonly ChallengeApiApplication _application;
        private readonly HttpClient _client;

        public IntegrationTestsDespesa()
        {
            _application = new ChallengeApiApplication();
            DataBaseMockData.MockData(_application);
            _client = _application.CreateClient();
        }
        [Fact]
        public async void GetDespesas_WhenSuccefullyExecuted_ShouldReturnStatusCodeOkAndDespesas()
        {
            var statusCodeResult = await _client.GetAsync("https://localhost:7135/Despesas");

            var returnedDespesas = await _client.GetFromJsonAsync<List<ReadDespesaDto>>("https://localhost:7135/Despesas");

            statusCodeResult.StatusCode.Should().Be(HttpStatusCode.OK);
            returnedDespesas.Should().NotBeEmpty();
        }

        [Fact]
        public async void CreateDespesa_ValidDespesa_ShouldReturnStatusCodeCreatedAndDespesa()
        {
            var despesa = FakerPostDespesaDto.Faker.Generate();

            var statusCodeResult = await _client.PostAsJsonAsync("https://localhost:7135/Despesas", despesa);

            var urlCreatedDespesa = statusCodeResult.Headers.Location;

            var returnedDespesa = await _client.GetFromJsonAsync<ReadDespesaDto>(urlCreatedDespesa);

            statusCodeResult.StatusCode.Should().Be(HttpStatusCode.Created);
            returnedDespesa.Should().BeEquivalentTo(despesa);
        }

        [Fact]
        public async void CreateDespesa_AlreadyExistsInDb_ShouldReturnStatusCodeBadRequestAndAnArgumentException()
        {
            var despesa = FakerPostDespesaDto.Faker.Generate();

            await _client.PostAsJsonAsync("https://localhost:7135/Despesas", despesa);

            var statusCodeResult = await _client.PostAsJsonAsync("https://localhost:7135/Despesas", despesa);

            var returnedMessage = await statusCodeResult.Content.ReadAsStringAsync();

            statusCodeResult.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            returnedMessage.Should().Be("Descrição e mês já existente.");
        }


        [Fact]
        public async void GetDespesaById_WhenReceiveAValidId_ShouldReturnStatusCodeOkAndReadDespesa()
        {
            var despesa = FakerPostDespesaDto.Faker.Generate();

            var createdDespesaForSearchById = await _client.PostAsJsonAsync("https://localhost:7135/Despesas", despesa);

            var urlCreatedDespesa = createdDespesaForSearchById.Headers.Location;

            var statusCodeResult = await _client.GetAsync(urlCreatedDespesa);

            var returnedDespesa = await _client.GetFromJsonAsync<ReadDespesaDto>(urlCreatedDespesa);

            statusCodeResult.StatusCode.Should().Be(HttpStatusCode.OK);
            returnedDespesa.Should().BeEquivalentTo(despesa);
        }

        [Fact]
        public async void GetDespesaById_WhenEntityIsNull_ShouldReturnThrowAKeyNotFoundExceptionAndStatusCodeNotFound()
        {
            var statusCodeResult = await _client.GetAsync("https://localhost:7135/Despesas/1000");

            var returnedMessage = await statusCodeResult.Content.ReadAsStringAsync();

            statusCodeResult.StatusCode.Should().Be(HttpStatusCode.NotFound);
            returnedMessage.Should().Be("Não encontrado...");
        }

        [Fact]
        public async void PutDespesa_WhenDespesaIsUpdated_ShouldReturnStatusCodeNoContent()
        {
            var despesaThatWillBeUpdated = FakerPostDespesaDto.Faker.Generate();

            var updatedDespesa = FakerPutDespesaDto.Faker.Generate();

            var createdDespesa = await _client.PostAsJsonAsync("https://localhost:7135/Despesas", despesaThatWillBeUpdated);

            var createdDespesaId = createdDespesa.Headers.Location.Segments[2];

            var statusCodeResult = await _client.PutAsJsonAsync($"https://localhost:7135/Despesas/{createdDespesaId}", updatedDespesa);

            var despesaThatHasBeenUpdated = await _client.GetFromJsonAsync<ReadDespesaDto>($"https://localhost:7135/Despesas/{createdDespesaId}");

            statusCodeResult.StatusCode.Should().Be(HttpStatusCode.NoContent);
            despesaThatHasBeenUpdated.Should().BeEquivalentTo(updatedDespesa);
        }

        [Fact]
        public async void PutDespesa_WhenEntityIsNull_ShouldThrowAKeyNotFoundExceptionAndStatusCodeNotFound()
        {
            var updatedDespesa = FakerPutDespesaDto.Faker.Generate();

            var statusCodeResult = await _client.PutAsJsonAsync($"https://localhost:7135/Despesas/1000", updatedDespesa);

            var returneMessage = await statusCodeResult.Content.ReadAsStringAsync();

            statusCodeResult.StatusCode.Should().Be(HttpStatusCode.NotFound);
            returneMessage.Should().Be("Não encontrado...");
        }

        [Fact]
        public async void PutDespesa_WhenDespesaAlreadyExistInDb_ShouldThrowAnArgumentExceptionAndStatusCodeBadRequest()
        {
            var despesaThatWillBeUpdated = FakerPostDespesaDto.Faker.Generate();

            var createdDespesa = await _client.PostAsJsonAsync("https://localhost:7135/Despesas", despesaThatWillBeUpdated);

            var updatedDespesa = FakerPutDespesaDto.Faker.Generate();
            updatedDespesa.Descricao = despesaThatWillBeUpdated.Descricao;
            updatedDespesa.Data = despesaThatWillBeUpdated.Data;

            var createdDespesaId = createdDespesa.Headers.Location.Segments[2];

            var statusCodeResult = await _client.PutAsJsonAsync($"https://localhost:7135/Despesas/{createdDespesaId}", updatedDespesa);

            var returnedMessage = await statusCodeResult.Content.ReadAsStringAsync();

            statusCodeResult.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            returnedMessage.Should().Be("Descrição e mês já existente.");
        }

        [Fact]
        public async void DeleteDespesa_WhenEntityIsNull_ShouldThrowAKeyNotFoundExceptionAndStatusCodeNotFound()
        {
            var statusCodeResult = await _client.DeleteAsync("https://localhost:7135/Despesas/1000");

            var returnedMessage = await statusCodeResult.Content.ReadAsStringAsync();

            statusCodeResult.StatusCode.Should().Be(HttpStatusCode.NotFound);
            returnedMessage.Should().Be("Não encontrado...");
        }

        [Fact]
        public async void GetDespesaByDescricao_WhenReceiveAExistingDescricao_ShouldReturnStatusCodeOkAndEquivalentsDespesas()
        {
            var despesa = FakerPostDespesaDto.Faker.Generate();
            despesa.Descricao = "Descricao igual";

            var anotherDespesa = FakerPostDespesaDto.Faker.Generate();
            anotherDespesa.Data = DateTime.UtcNow.AddMonths(2);
            anotherDespesa.Descricao = "Descricao igual";

            await _client.PostAsJsonAsync("https://localhost:7135/Despesas", despesa);
            await _client.PostAsJsonAsync("https://localhost:7135/Despesas", anotherDespesa);

            var statusCodeResult = await _client.GetAsync("https://localhost:7135/Despesas/descricao?descricao=Descricao%20igual");

            var returnedDespesasByDescricao = await _client.GetFromJsonAsync<List<ReadDespesaDto>>("https://localhost:7135/Despesas/descricao?descricao=Descricao%20igual");

            statusCodeResult.StatusCode.Should().Be(HttpStatusCode.OK);
            returnedDespesasByDescricao.Count.Should().Be(2);
        }

        [Fact]
        public async void GetDespesaByDate_WhenReceiveAnExistingDate_ShouldReturnStatusCodeOkAndEquivalentsDespesas()
        {
            var statusCodeResult = await _client.GetAsync($"https://localhost:7135/Despesas/{DateTime.UtcNow.Year}/{DateTime.UtcNow.Month}");

            var returnedDespesasByDate = await _client.GetFromJsonAsync<List<ReadDespesaDto>>($"https://localhost:7135/Despesas/{DateTime.UtcNow.Year}/{DateTime.UtcNow.Month}");

            statusCodeResult.StatusCode.Should().Be(HttpStatusCode.OK);
            returnedDespesasByDate.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public async void DeleteDespesa_WhenReceiveAValidId_ShouldReturnStatusCodeNoContent()
        {
            var despesa = FakerPostDespesaDto.Faker.Generate();

            var createdDespesa = await _client.PostAsJsonAsync("https://localhost:7135/Despesas", despesa);

            var createdDespesaId = createdDespesa.Headers.Location.Segments[2];

            var result = await _client.DeleteAsync(createdDespesa.Headers.Location);

            var wasDespesaDeleted = await _client.GetAsync($"https://localhost:7135/Despesas/{createdDespesa}");

            result.StatusCode.Should().Be(HttpStatusCode.NoContent);
            wasDespesaDeleted.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}