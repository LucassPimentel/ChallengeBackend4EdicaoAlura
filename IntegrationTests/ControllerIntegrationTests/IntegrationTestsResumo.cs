using ChallengeBackend4EdicaoAlura.Dtos.Resumos;
using FluentAssertions;
using System.Net;
using System.Net.Http.Json;

namespace IntegrationTests.ControllerIntegrationTests
{
    public class IntegrationTestsResumo
    {
        private readonly ChallengeApiApplication _application;
        private readonly HttpClient _client;

        public IntegrationTestsResumo()
        {
            _application = new ChallengeApiApplication();
           DataBaseMockData.MockData(_application);
            _client = _application.CreateClient();
        }

        [Fact]
        public async void GetResumoByDate()
        {
            var statusCodeResult = await _client.GetAsync($"https://localhost:7135/Resumo/{DateTime.UtcNow.Year}/{DateTime.UtcNow.Month}");

            var returnedResumo = await _client.GetFromJsonAsync<ReadResumoDto>($"https://localhost:7135/Resumo/{DateTime.UtcNow.Year}/{DateTime.UtcNow.Month}");

            statusCodeResult.StatusCode.Should().Be(HttpStatusCode.OK);
            returnedResumo.Should().NotBeNull();
        }
    }
}
