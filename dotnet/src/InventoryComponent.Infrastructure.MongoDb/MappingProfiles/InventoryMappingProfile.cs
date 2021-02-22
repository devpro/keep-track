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

            CreateMap<Entities.TvShow, Domain.Models.TvShowModel>();
            CreateMap<Domain.Models.TvShowModel, Entities.TvShow>();

            CreateMap<Entities.VideoGame, Domain.Models.VideoGameModel>();
            CreateMap<Domain.Models.VideoGameModel, Entities.VideoGame>();
        }
    }
}
