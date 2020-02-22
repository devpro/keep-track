using AutoMapper;
using MongoDB.Bson;
using Withywoods.Dal.MongoDb.MappingConverters;
using Xunit;

namespace KeepTrack.InventoryComponent.Infrastructure.MongoDb.UnitTests.MappingProfiles
{
    [Trait("Category", "UnitTests")]
    public class InventoryMappingProfileTest
    {
        [Fact]
        public void InventoryMappingProfileBuildAutoMapper_AssertConfigurationIsValid()
        {
            var mappingConfig = new MapperConfiguration(x =>
            {
                x.CreateMap<ObjectId, string>().ConvertUsing<ObjectIdToStringConverter>();
                x.CreateMap<string, ObjectId>().ConvertUsing<StringToObjectIdConverter>();
                x.AddProfile(new MongoDb.MappingProfiles.InventoryMappingProfile());
                x.AllowNullCollections = true;
            });
            var mapper = mappingConfig.CreateMapper();
            mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }
    }
}
