using AutoMapper;

namespace KeepTrack.Api.MappingProfiles
{
    /// <summary>
    /// Car mapping profile.
    /// </summary>
    public class CarMappingProfile : Profile
    {
        /// <summary>
        /// Profile name.
        /// </summary>
        public override string ProfileName
        {
            get { return "KeepTrackApiMappingProfile"; }
        }

        /// <summary>
        /// Create a new instance of <see cref="CarMappingProfile"/>.
        /// </summary>
        public CarMappingProfile()
        {
            CreateMap<Dto.CarHistoryDto, CarComponent.Domain.CarHistoryModel>()
                .ForMember(x => x.OwnerId, opt => opt.Ignore());
            CreateMap<CarComponent.Domain.CarHistoryModel, Dto.CarHistoryDto>();
        }
    }
}
