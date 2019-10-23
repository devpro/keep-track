using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using KeepTrack.Api.Dto;
using KeepTrack.CarComponent.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    /// <summary>
    /// Car history controller.
    /// </summary>
    [ApiController]
    // [Authorize()]
    [Route("api/car-history")]
    public class CarHistoryController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICarHistoryRepository _carHistoryRepository;

        /// <summary>
        /// Creates a new instance of <see cref="CarHistoryController"/>.
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="carHistoryRepository"></param>
        public CarHistoryController(IMapper mapper, ICarHistoryRepository carHistoryRepository)
        {
            _mapper = mapper;
            _carHistoryRepository = carHistoryRepository;
        }

        /// <summary>
        /// Gets all the history for a given car.
        /// </summary>
        /// <param name="carId">Car ID</param>
        /// <returns></returns>
        // GET: api/CarHistory
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<CarHistoryDto>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Get(string carId)
        {
            if (string.IsNullOrEmpty(carId))
            {
                return BadRequest();
            }

            var models = await _carHistoryRepository.FindAllAsync(carId);
            return Ok(_mapper.Map<List<CarHistoryDto>>(models));
        }

        /// <summary>
        /// Gets information from a single car history.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "GetCarHistoryById")]
        [ProducesResponseType(200, Type = typeof(CarHistoryDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetById(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest();
            }

            var model = await _carHistoryRepository.FindOneAsync(id);
            if (model == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<CarHistoryDto>(model));
        }

        /// <summary>
        /// Creates a new car history.
        /// </summary>
        /// <param name="dto"></param>
        [HttpPost]
        [ProducesResponseType(201)]
        public async Task<IActionResult> Post([FromBody] CarHistoryDto dto)
        {
            var input = _mapper.Map<CarHistoryModel>(dto);
            var model = await _carHistoryRepository.CreateAsync(input);
            return CreatedAtRoute("GetCarHistoryById", new { id = model.Id }, _mapper.Map<CarHistoryDto>(model));
        }

        /// <summary>
        /// Updates a car history.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        [HttpPut("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public IActionResult Put(string id, [FromBody] string value)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest();
            }

            throw new NotImplementedException();
        }

        /// <summary>
        /// Deletes a car history.
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

            await _carHistoryRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
