using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using KeepTrack.Api.Dto;
using KeepTrack.Api.IntegrationTests.Firebase;
using Microsoft.AspNetCore.Mvc.Testing;
using Withywoods.WebTesting.Rest;
using Xunit;

namespace KeepTrack.Api.IntegrationTests.Localhost
{
    [Trait("Environment", "Localhost")]
    public class BookResourceLocalTest : IClassFixture<WebApplicationFactory<Startup>>
    {
        private const string _ResourceEndpoint = "api/books";

        private readonly Fixture _fixture;
        private readonly HttpClient _client;
        private readonly RestRunner _restRunner;
        private readonly AccountRepository _accountRepository;

        public BookResourceLocalTest(WebApplicationFactory<Startup> factory)
        {
            _fixture = new Fixture();
            _client = factory.CreateClient();
            _restRunner = new RestRunner { ResourceEndpoint = _ResourceEndpoint };
            _accountRepository = new AccountRepository(new FirebaseConfiguration());
        }

        [Fact]
        public async Task AspNetCoreApiSampleTaskResourceFullCycle_IsOk()
        {
            // Arrange
            var token = await _accountRepository.Authenticate();
            SetBearerToken(_client, token);

            // Act
            var initialTasks = await _restRunner.GetResources<BookDto>(_client);

            // Assert
            initialTasks.Count.Should().Be(0);
        }

        private void SetBearerToken(HttpClient client, string token)
        {
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
    }
}
