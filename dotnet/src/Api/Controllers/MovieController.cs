using AutoMapper;
using KeepTrack.Api.Dto;
using KeepTrack.MovieComponent.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KeepTrack.Api.Controllers
{
    /// <summary>
    /// Movie controller.
    /// </summary>
    [ApiController]
    [Authorize]
    [Route("api/movies")]
    public class MovieController : DataCrudControllerBase<MovieDto, MovieModel>
    {
        /// <summary>
        /// Creates a new instance of <see cref="MovieController"/>.
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="dataRepository"></param>
        public MovieController(IMapper mapper, IMovieRepository dataRepository)
            : base(mapper, dataRepository)
        {
        }
    }
}
