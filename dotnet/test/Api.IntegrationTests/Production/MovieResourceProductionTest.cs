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
    public class MovieResourceProductionTest
    {
        private readonly GenericResource<MovieDto> _movieResource;

        public MovieResourceProductionTest()
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(Environment.GetEnvironmentVariable("Production_Url"))
            };
            _movieResource = new GenericResource<MovieDto>(client, "api/movies");
        }

        [Fact]
        public async Task MovieResourceProductionFullCycle_IsOk()
        {
            // check not authorized if not logged
            (await Assert.ThrowsAsync<XunitException>(async () => await _movieResource.FindAll()))
                .Message.Should().Be("Expected object to be OK, but found Unauthorized.");

            await _movieResource.Authenticate();

            var created = await _movieResource.Create();
            created.Id.Should().NotBeNullOrEmpty();

            created.Title = "New shiny title";
            await _movieResource.Update(created.Id, created);

            var updated = await _movieResource.FindOneById(created.Id, created);

            var finalItems = await _movieResource.FindAll();
            finalItems.Count.Should().BeGreaterOrEqualTo(1);
            finalItems.FirstOrDefault(x => x.Id == updated.Id).Should().NotBeNull();
            finalItems.First().Title.Should().Be(updated.Title);

            await _movieResource.Delete(updated.Id);
        }
    }
}
