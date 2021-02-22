using AutoMapper;
using KeepTrack.Api.Dto;
using KeepTrack.InventoryComponent.Domain.Models;
using KeepTrack.InventoryComponent.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KeepTrack.Api.Controllers
{
    /// <summary>
    /// Book controller.
    /// </summary>
    [ApiController]
    [Authorize]
    [Route("api/tv-shows")]
    public class TvShowController : DataCrudController<TvShowDto, TvShowModel>
    {
        /// <summary>
        /// Creates a new instance of <see cref="TvShowController"/>.
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="dataRepository"></param>
        public TvShowController(IMapper mapper, ITvShowRepository dataRepository)
            : base(mapper, dataRepository)
        {
        }
    }
}
