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
    public class BookResourceLocalhostTest : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly GenericResource<BookDto> _bookResource;

        public BookResourceLocalhostTest(WebApplicationFactory<Startup> factory)
        {
            var client = factory.CreateClient();
            _bookResource = new GenericResource<BookDto>(client, "api/books");
        }

        [Fact]
        public async Task BookResourceLocalhostFullCycle_IsOk()
        {
            // check not authorized if not logged
            (await Assert.ThrowsAsync<XunitException>(async () => await _bookResource.FindAll()))
                .Message.Should().Be("Expected object to be OK, but found Unauthorized.");

            await _bookResource.Authenticate();

            var initialItems = await _bookResource.FindAll();
            initialItems.Count.Should().Be(0);

            var created = await _bookResource.Create();
            created.Id.Should().NotBeNullOrEmpty();

            created.Title = "New shiny title";
            await _bookResource.Update(created.Id, created);

            var updated = await _bookResource.FindOneById(created.Id, created);

            var finalItems = await _bookResource.FindAll();
            finalItems.Count.Should().Be(1);
            finalItems[0].Id.Should().Be(updated.Id);

            await _bookResource.Delete(updated.Id);
        }
    }
}
