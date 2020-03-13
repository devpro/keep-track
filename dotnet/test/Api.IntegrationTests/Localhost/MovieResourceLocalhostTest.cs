﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using KeepTrack.Api.Dto;
using KeepTrack.Api.IntegrationTests.TestingLogic.Resources;
using Microsoft.AspNetCore.Mvc.Testing;
using Withywoods.Serialization.Json;
using Xunit;
using Xunit.Sdk;

namespace KeepTrack.Api.IntegrationTests.Localhost
{
    [Trait("Environment", "Localhost")]
    public class MovieResourceLocalhostTest : ResourceBase, IClassFixture<WebApplicationFactory<Startup>>
    {
        private const string ResourceEndpoint = "api/movies";

        public MovieResourceLocalhostTest(WebApplicationFactory<Startup> factory)
            : base(factory.CreateClient())
        {
        }

        [Fact]
        public async Task MovieResourceLocalhostFullCycle_IsOk()
        {
            // check not authorized if not logged
            (await Assert.ThrowsAsync<XunitException>(async () => await GetAsync<List<MovieDto>>($"/{ResourceEndpoint}")))
                .Message.Should().Be("Expected object to be OK, but found Unauthorized.");

            await Authenticate();

            var initialItems = await GetAsync<List<MovieDto>>($"/{ResourceEndpoint}");
            initialItems.Count.Should().Be(0);

            var input = Fixture.Create<MovieDto>();
            input.Id = null;
            var created = await PostAsync<MovieDto>($"/{ResourceEndpoint}", input.ToJson());
            created.Id.Should().NotBeNullOrEmpty();

            try
            {
                created.Title = "New shiny title";
                await PutAsync<MovieDto>($"/{ResourceEndpoint}/{created.Id}", created.ToJson());

                var updated = await GetAsync<MovieDto>($"/{ResourceEndpoint}/{created.Id}");
                updated.Should().BeEquivalentTo(created);

                var finalItems = await GetAsync<List<MovieDto>>($"/{ResourceEndpoint}");
                finalItems.Count.Should().Be(1);
                finalItems[0].Id.Should().Be(updated.Id);
                var firstItem = finalItems.FirstOrDefault(x => x.Id == updated.Id);
                firstItem.Should().NotBeNull();
                firstItem.Title.Should().Be(updated.Title);
            }
            finally
            {
                await DeleteAsync($"/{ResourceEndpoint}/{created.Id}");
            }
        }
    }
}
