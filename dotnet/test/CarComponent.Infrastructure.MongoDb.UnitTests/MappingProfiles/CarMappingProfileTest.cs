using AutoMapper;
using MongoDB.Bson;
using Withywoods.Dal.MongoDb.MappingConverters;
using Xunit;

namespace KeepTrack.CarComponent.Infrastructure.MongoDb.UnitTests.MappingProfiles
{
    [Trait("Category", "UnitTests")]
    public class CarMappingProfileTest
    {
        [Fact]
        public void CarMappingProfileBuildAutoMapper_AssertConfigurationIsValid()
        {
            var mappingConfig = new MapperConfiguration(x =>
            {
                x.CreateMap<ObjectId, string>().ConvertUsing<ObjectIdToStringConverter>();
                x.CreateMap<string, ObjectId>().ConvertUsing<StringToObjectIdConverter>();
                x.AddProfile(new MongoDb.MappingProfiles.CarMappingProfile());
                x.AllowNullCollections = true;
            });
            var mapper = mappingConfig.CreateMapper();
            mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }
    }
}
