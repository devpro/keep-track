using AutoMapper;
using KeepTrack.Api.Dto;
using KeepTrack.InventoryComponent.Domain.Models;
using KeepTrack.InventoryComponent.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KeepTrack.Api.Controllers
{
    /// <summary>
    /// Video Games controller.
    /// </summary>
    [ApiController]
    [Authorize]
    [Route("api/video-games")]
    public class VideoGameController : DataCrudController<VideoGameDto, VideoGameModel>
    {
        /// <summary>
        /// Creates a new instance of <see cref="TvShowController"/>.
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="dataRepository"></param>
        public VideoGameController(IMapper mapper, IVideoGameRepository dataRepository)
            : base(mapper, dataRepository)
        {
        }
    }
}
