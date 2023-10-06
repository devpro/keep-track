using KeepTrack.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace KeepTrack.Api.Controllers
{
    /// <summary>
    /// Data CRUD (Create, Request, Update, Delete) Controller abstract class.
    /// </summary>
    /// <typeparam name="T">Data Transfer Object</typeparam>
    /// <typeparam name="U">Domain Model</typeparam>
    public abstract class DataCrudControllerBase<T, U> : ControllerBase
        where U : Domain.Models.IDataModel
    {
        private readonly IMapper _mapper;
        private readonly IDataRepository<U> _dataRepository;

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="dataRepository"></param>
        protected DataCrudControllerBase(IMapper mapper, IDataRepository<U> dataRepository)
        {
            _mapper = mapper;
            _dataRepository = dataRepository;
        }

        /// <summary>
        /// Gets all models.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<List<T>>> Get(int? page, int? pageSize)
        {
            var models = await _dataRepository.FindAllAsync(GetUserId());
            return Ok(_mapper.Map<List<T>>(models));
        }

        /// <summary>
        /// Gets information from a model.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<T>> GetById(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest();
            }

            var model = await _dataRepository.FindOneAsync(id, GetUserId());
            if (model == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<T>(model));
        }

        /// <summary>
        /// Creates a new model.
        /// </summary>
        /// <param name="dto"></param>
        [HttpPost]
        [ProducesResponseType(201)]
        public async Task<IActionResult> Post([FromBody] T dto)
        {
            var input = _mapper.Map<U>(dto);
            input.OwnerId = GetUserId();
            var model = await _dataRepository.CreateAsync(input);
            return CreatedAtAction(nameof(GetById), new { id = model.Id }, _mapper.Map<T>(model));
        }

        /// <summary>
        /// Updates a model.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Put(string id, [FromBody] T dto)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest();
            }

            var input = _mapper.Map<U>(dto);
            input.OwnerId = GetUserId();
            await _dataRepository.UpdateAsync(id, input, GetUserId());
            return NoContent();
        }

        /// <summary>
        /// Deletes a model.
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

            await _dataRepository.DeleteAsync(id, GetUserId());
            return NoContent();
        }
    }
}
