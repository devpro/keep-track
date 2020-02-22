using AutoMapper;

namespace KeepTrack.Api.MappingProfiles
{
    /// <summary>
    /// Generic mapping profile.
    /// </summary>
    public class GenericMappingProfile : Profile
    {
        /// <summary>
        /// Profile name.
        /// </summary>
        public override string ProfileName
        {
            get { return "KeepTrackApiGenericMappingProfile"; }
        }

        /// <summary>
        /// Create a new instance of <see cref="GenericMappingProfile"/>.
        /// </summary>
        public GenericMappingProfile()
        {
            CreateMap<Dto.MovieDto, MovieComponent.Domain.MovieModel>()
                .ForMember(x => x.OwnerId, opt => opt.Ignore());
            CreateMap<MovieComponent.Domain.MovieModel, Dto.MovieDto>();

            CreateMap<Dto.CarHistoryDto, CarComponent.Domain.CarHistoryModel>()
                .ForMember(x => x.OwnerId, opt => opt.Ignore());
            CreateMap<CarComponent.Domain.CarHistoryModel, Dto.CarHistoryDto>();

            CreateMap<Dto.BookDto, InventoryComponent.Domain.Models.BookModel>()
                .ForMember(x => x.OwnerId, opt => opt.Ignore());
            CreateMap<InventoryComponent.Domain.Models.BookModel, Dto.BookDto>();
        }
    }
}
