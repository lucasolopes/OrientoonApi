using Microsoft.AspNetCore.Mvc;
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

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AutorForm>> Get(int id)
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

        [HttpGet("list")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<AutorForm>>> GetList([FromQuery] int batchSize, [FromQuery] int pageNumber)
        {
            try
            {
                List<AutorForm> autors = await _autorService.GetListAsync(batchSize, pageNumber);
                return Ok(autors);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AutorForm>> Put(int id, [FromBody] AutorDto autorDto)
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

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(int id)
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

        [HttpGet("nome/{nome}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AutorForm>> GetByNome(string nome)
        {
            try
            {
                AutorForm autor = await _autorService.GetByNomeAsync(nome);
                return Ok(autor);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
