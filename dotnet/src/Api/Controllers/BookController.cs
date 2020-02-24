using System.Collections.Generic;
using System.Threading.Tasks;
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
    [Route("api/books")]
    public class BookController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IBookRepository _bookRepository;

        /// <summary>
        /// Creates a new instance of <see cref="BookController"/>.
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="bookRepository"></param>
        public BookController(IMapper mapper, IBookRepository bookRepository)
        {
            _mapper = mapper;
            _bookRepository = bookRepository;
        }

        /// <summary>
        /// Gets all the books.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<BookDto>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Get()
        {
            var models = await _bookRepository.FindAllAsync(GetUserId());
            return Ok(_mapper.Map<List<BookDto>>(models));
        }

        /// <summary>
        /// Gets information from a single book.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "GetBookById")]
        [ProducesResponseType(200, Type = typeof(BookDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetById(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest();
            }

            var model = await _bookRepository.FindOneAsync(id, GetUserId());
            if (model == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<BookDto>(model));
        }

        /// <summary>
        /// Creates a new book.
        /// </summary>
        /// <param name="dto"></param>
        [HttpPost]
        [ProducesResponseType(201)]
        public async Task<IActionResult> Post([FromBody] BookDto dto)
        {
            var input = _mapper.Map<BookModel>(dto);
            input.OwnerId = GetUserId();
            var model = await _bookRepository.CreateAsync(input);
            return CreatedAtRoute("GetBookById", new { id = model.Id }, _mapper.Map<BookDto>(model));
        }

        /// <summary>
        /// Updates a book.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Put(string id, [FromBody] BookDto dto)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest();
            }

            var input = _mapper.Map<BookModel>(dto);
            input.OwnerId = GetUserId();
            await _bookRepository.UpdateAsync(id, input, GetUserId());
            return NoContent();
        }

        /// <summary>
        /// Deletes a book.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest();
            }

            await _bookRepository.DeleteAsync(id, GetUserId());
            return NoContent();
        }
    }
}
