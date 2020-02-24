using System.Threading.Tasks;
using FluentAssertions;
using KeepTrack.Api.Dto;
using KeepTrack.Api.IntegrationTests.TestingLogic.Resources;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using Xunit.Sdk;

namespace KeepTrack.Api.IntegrationTests.Localhost
{
    [Trait("Environment", "Localhost")]
    public class MovieResourceLocalhostTest : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly GenericResource<MovieDto> _movieResource;

        public MovieResourceLocalhostTest(WebApplicationFactory<Startup> factory)
        {
            var client = factory.CreateClient();
            _movieResource = new GenericResource<MovieDto>(client, "api/movies");
        }

        [Fact]
        public async Task MovieResourceLocalhostFullCycle_IsOk()
        {
            // check not authorized if not logged
            (await Assert.ThrowsAsync<XunitException>(async () => await _movieResource.FindAll()))
                .Message.Should().Be("Expected object to be OK, but found Unauthorized.");

            await _movieResource.Authenticate();

            var initialItems = await _movieResource.FindAll();
            initialItems.Count.Should().Be(0);

            var created = await _movieResource.Create();
            created.Id.Should().NotBeNullOrEmpty();

            created.Title = "New shiny title";
            await _movieResource.Update(created.Id, created);

            var updated = await _movieResource.FindOneById(created.Id, created);

            var finalItems = await _movieResource.FindAll();
            finalItems.Count.Should().Be(1);
            finalItems[0].Id.Should().Be(updated.Id);

            await _movieResource.Delete(updated.Id);
        }
    }
}
