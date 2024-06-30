using Microsoft.AspNetCore.Mvc;
using OrientoonApi.Models.Entities;
using OrientoonApi.Models.Request;
using OrientoonApi.Models.Response;
using OrientoonApi.Services.Interfaces;

namespace OrientoonApi.Controllers
{
    [Route("api/[controller]")]
    public class StatusController : ControllerBase
    {
        private readonly IStatusService _statusService;
        public StatusController(IStatusService statusService)
        {
            _statusService = statusService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<StatusForm>> Post([FromBody] StatusDto statusDto)
        {
            try
            {
                StatusForm status = await _statusService.CreateAsync(statusDto);
                return CreatedAtAction(nameof(Get), new { id = status.Id }, status);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<StatusForm>> Get(string id)
        {
            try
            {
                StatusForm statuss = await _statusService.GetAsync(id);
                return Ok(statuss);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("list")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<StatusForm>>> GetList([FromQuery] int batchSize, [FromQuery] int pageNumber)
        {
            try
            {
                List<StatusForm> statuss = await _statusService.GetListAsync(batchSize, pageNumber);
                return Ok(statuss);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<StatusForm>> Put(string id, [FromBody] StatusDto statusDto)
        {
            try
            {
                StatusForm status = await _statusService.UpdateAsync(id, statusDto);
                return Ok(status);
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
                await _statusService.DeleteAsync(id);
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
        public async Task<ActionResult<StatusForm>> GetByNome(string nome)
        {
            try
            {
                StatusModel status = await _statusService.GetByNomeAsync(nome);
                return Ok(status.Converter());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
