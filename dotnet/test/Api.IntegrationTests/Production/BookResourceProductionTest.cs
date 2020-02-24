using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using KeepTrack.Api.Dto;
using KeepTrack.Api.IntegrationTests.TestingLogic.Resources;
using Xunit;
using Xunit.Sdk;

namespace KeepTrack.Api.IntegrationTests.Deployed
{
    [Trait("Environment", "Production")]
    public class BookResourceProductionTest
    {
        private readonly GenericResource<BookDto> _bookResource;

        public BookResourceProductionTest()
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(Environment.GetEnvironmentVariable("Production_Url"))
            };
            _bookResource = new GenericResource<BookDto>(client, "api/books");
        }

        [Fact]
        public async Task BookResourceProductionFullCycle_IsOk()
        {
            // check not authorized if not logged
            (await Assert.ThrowsAsync<XunitException>(async () => await _bookResource.FindAll()))
                .Message.Should().Be("Expected object to be OK, but found Unauthorized.");

            await _bookResource.Authenticate();

            var created = await _bookResource.Create();

            try
            {
                created.Title = "New shiny title";
                await _bookResource.Update(created.Id, created);

                var updated = await _bookResource.FindOneById(created.Id, created);
                updated.Should().BeEquivalentTo(created);

                var finalItems = await _bookResource.FindAll();
                finalItems.Count.Should().BeGreaterOrEqualTo(1);
                var firstItem = finalItems.FirstOrDefault(x => x.Id == updated.Id);
                firstItem.Should().NotBeNull();
                firstItem.Title.Should().Be(updated.Title);
            }
            finally
            {
                await _bookResource.Delete(created.Id);
            }
        }
    }
}
