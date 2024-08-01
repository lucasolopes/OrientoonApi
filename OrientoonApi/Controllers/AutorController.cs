using Microsoft.AspNetCore.Mvc;
using OrientoonApi.Models.Entities;
using OrientoonApi.Models.Request;
using OrientoonApi.Models.Response;
using OrientoonApi.Services.Interfaces;

namespace OrientoonApi.Controllers
{
    [Route("api/[controller]")]
    public class AutorController : ControllerBase
    {
        private readonly IAutorService _autorService;
        public AutorController(IAutorService autorService)
        {
            _autorService = autorService;
        }

        /// <summary>
        /// Cria o Autor.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AutorForm>> Post([FromBody] AutorDto autorDto)
        {
            try
            {
                AutorForm autor = await _autorService.CreateAsync(autorDto);
                return CreatedAtAction(nameof(Get), new { id = autor.Id }, autor);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Pega o Autor.
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AutorForm>> Get(string id)
        {
            try
            {
                AutorForm autors = await _autorService.GetAsync(id);
                return Ok(autors);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Pega a lista de Autores.
        /// </summary>
        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<AutorForm>>> GetList([FromQuery] PageableDto pageable)
        {
            try
            {
                List<AutorForm> autors = await _autorService.GetListAsync(pageable.batchSize, pageable.pageNumber);
                return Ok(autors);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Atualiza o Autor.
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AutorForm>> Put(string id, [FromBody] AutorDto autorDto)
        {
            try
            {
                AutorForm autor = await _autorService.UpdateAsync(id, autorDto);
                return Ok(autor);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Deleta o Autor.
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(string id)
        {
            try
            {
                await _autorService.DeleteAsync(id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
