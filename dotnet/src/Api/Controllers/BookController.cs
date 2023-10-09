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
    [Route("api/books")]
    public class BookController : DataCrudControllerBase<BookDto, BookModel>
    {
        /// <summary>
        /// Creates a new instance of <see cref="BookController"/>.
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="dataRepository"></param>
        public BookController(IMapper mapper, IBookRepository dataRepository)
            : base(mapper, dataRepository)
        {
        }
    }
}
