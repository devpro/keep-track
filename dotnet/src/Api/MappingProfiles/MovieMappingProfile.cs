using AutoMapper;

namespace KeepTrack.Api.MappingProfiles
{
    /// <summary>
    /// Movie mapping profile.
    /// </summary>
    public class MovieMappingProfile : Profile
    {
        /// <summary>
        /// Profile name.
        /// </summary>
        public override string ProfileName
        {
            get { return "KeepTrackApiMovieMappingProfile"; }
        }

        /// <summary>
        /// Create a new instance of <see cref="MovieMappingProfile"/>.
        /// </summary>
        public MovieMappingProfile()
        {
            CreateMap<Dto.MovieDto, MovieComponent.Domain.MovieModel>()
                .ForMember(x => x.OwnerId, opt => opt.Ignore());
            CreateMap<MovieComponent.Domain.MovieModel, Dto.MovieDto>();
        }
    }
}
