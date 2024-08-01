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

        /// <summary>
        /// Pega o Status.
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<StatusForm>> Get(string id)
        {
            try
            {
                StatusForm statuss = (await _statusService.GetByIdAsync(id)).Converter();
                return Ok(statuss);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Pega todos Status.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<StatusForm>>> GetAll()
        {
            try
            {
                IEnumerable<StatusForm> statuss = (await _statusService.GetAllAsync()).Select(x => x.Converter());
                return Ok(statuss);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
      
    }
}
