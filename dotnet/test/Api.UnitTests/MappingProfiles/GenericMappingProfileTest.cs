using AutoMapper;
using Xunit;

namespace KeepTrack.Api.UnitTests.MappingProfiles
{
    [Trait("Category", "UnitTests")]
    public class GenericMappingProfileTest
    {
        [Fact]
        public void GenericMappingProfileBuildAutoMapper_AssertConfigurationIsValid()
        {
            var mappingConfig = new MapperConfiguration(x =>
            {
                x.AddProfile(new Api.MappingProfiles.GenericMappingProfile());
                x.AllowNullCollections = true;
            });
            var mapper = mappingConfig.CreateMapper();
            mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }
    }
}
