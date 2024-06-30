using Microsoft.AspNetCore.Mvc;
using OrientoonApi.Models.Entities;
using OrientoonApi.Models.Request;
using OrientoonApi.Models.Response;
using OrientoonApi.Services.Implementations;
using OrientoonApi.Services.Interfaces;

namespace OrientoonApi.Controllers
{
    [Route("api/[controller]")]
    public class TipoController : ControllerBase
    {
        private readonly ITipoService _tipoService;
        public TipoController(ITipoService tipoService)
        {
            _tipoService = tipoService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TipoForm>> Post([FromBody] TipoDto tipoDto)
        {
            try
            {
                TipoForm tipo = await _tipoService.CreateAsync(tipoDto);
                return CreatedAtAction(nameof(Get), new { id = tipo.Id }, tipo);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TipoForm>> Get(string id)
        {
            try
            {
                TipoForm tipos = await _tipoService.GetAsync(id);
                return Ok(tipos);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("list")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<TipoForm>>> GetList([FromQuery] int batchSize, [FromQuery] int pageNumber)
        {
            try
            {
                List<TipoForm> tipos = await _tipoService.GetListAsync(batchSize, pageNumber);
                return Ok(tipos);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TipoForm>> Put(string id, [FromBody] TipoDto tipoDto)
        {
            try
            {
                TipoForm tipo = await _tipoService.UpdateAsync(id, tipoDto);
                return AcceptedAtAction(nameof(Get), new { id = tipo.Id }, tipo);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(string id)
        {
            try
            {
                await _tipoService.DeleteAsync(id);
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
        public async Task<ActionResult<TipoForm>> GetByNome(string nome)
        {
            try
            {
                TipoModel tipo = await _tipoService.GetByNomeAsync(nome);
                return Ok(tipo.Converter());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
