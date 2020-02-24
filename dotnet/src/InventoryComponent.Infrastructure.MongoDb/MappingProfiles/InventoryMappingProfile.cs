using AutoMapper;

namespace KeepTrack.InventoryComponent.Infrastructure.MongoDb.MappingProfiles
{
    public class InventoryMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "KeepTrackInventoryInfrastructureMongoDbMappingProfile"; }
        }

        public InventoryMappingProfile()
        {
            CreateMap<Entities.Book, Domain.Models.BookModel>();
            CreateMap<Domain.Models.BookModel, Entities.Book>();
        }
    }
}
