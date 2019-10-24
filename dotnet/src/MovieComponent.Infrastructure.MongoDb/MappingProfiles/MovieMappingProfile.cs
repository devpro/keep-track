using AutoMapper;

namespace KeepTrack.MovieComponent.Infrastructure.MongoDb.MappingProfiles
{
    public class MovieMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "KeepTrackMovieInfrastructureMongoDbMappingProfile"; }
        }

        public MovieMappingProfile()
        {
            CreateMap<Entities.Movie, Domain.MovieModel>();
            CreateMap<Domain.MovieModel, Entities.Movie>();
        }
    }
}
