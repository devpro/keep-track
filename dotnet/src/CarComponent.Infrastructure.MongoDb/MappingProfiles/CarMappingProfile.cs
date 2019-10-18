using AutoMapper;

namespace KeepTrack.CarComponent.Infrastructure.MongoDb.MappingProfiles
{
    public class CarMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "KeepTrackCarInfrastructureMongoDbMappingProfile"; }
        }

        public CarMappingProfile()
        {
            CreateMap<Entities.Car, Domain.CarModel>();
            CreateMap<Entities.CarHistory, Domain.CarHistoryModel>();
        }
    }
}
