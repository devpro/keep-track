using System.Collections.Generic;
using System.Threading.Tasks;
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
    public class MovieController : KeepTrack.Api.Controllers.ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMovieRepository _movieRepository;

        /// <summary>
        /// Creates a new instance of <see cref="MovieController"/>.
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="movieRepository"></param>
        public MovieController(IMapper mapper, IMovieRepository movieRepository)
        {
            _mapper = mapper;
            _movieRepository = movieRepository;
        }

        /// <summary>
        /// Gets all the movies.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<MovieDto>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Get()
        {
            var models = await _movieRepository.FindAllAsync(GetUserId());
            return Ok(_mapper.Map<List<MovieDto>>(models));
        }

        /// <summary>
        /// Gets information from a single movie.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "GetMovieById")]
        [ProducesResponseType(200, Type = typeof(MovieDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetById(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest();
            }

            var model = await _movieRepository.FindOneAsync(id, GetUserId());
            if (model == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<MovieDto>(model));
        }

        /// <summary>
        /// Creates a new car history.
        /// </summary>
        /// <param name="dto"></param>
        [HttpPost]
        [ProducesResponseType(201)]
        public async Task<IActionResult> Post([FromBody] MovieDto dto)
        {
            var input = _mapper.Map<MovieModel>(dto);
            input.OwnerId = GetUserId();
            var model = await _movieRepository.CreateAsync(input);
            return CreatedAtRoute("GetMovieById", new { id = model.Id }, _mapper.Map<MovieDto>(model));
        }

        /// <summary>
        /// Updates a movie.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Put(string id, [FromBody] MovieDto dto)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest();
            }

            var input = _mapper.Map<MovieModel>(dto);
            input.OwnerId = GetUserId();
            await _movieRepository.UpdateAsync(id, input, GetUserId());
            return NoContent();
        }

        /// <summary>
        /// Deletes a movie.
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

            await _movieRepository.DeleteAsync(id, GetUserId());
            return NoContent();
        }
    }
}
