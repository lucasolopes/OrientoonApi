using Microsoft.AspNetCore.Mvc;
using OrientoonApi.Models.Request;
using OrientoonApi.Services.Interfaces;

namespace OrientoonApi.Controllers
{
    [Route("api/[controller]")]
    public class CapituloController : ControllerBase
    {
        private readonly ICapituloService _capituloService;

        public CapituloController(ICapituloService capituloService)
        {
            _capituloService = capituloService;
        }

        /// <summary>
        /// Cria o Capitulo.
        /// </summary>
        [HttpPost("{orientoonId}")]
        public async Task<IActionResult> UploadCapitulo(string orientoonId,[FromForm] CapituloDto capituloDto)
        {
            if (capituloDto.files == null || capituloDto.files.Count == 0)
            {
                return BadRequest("No files provided.");
            }

            var capitulo = await _capituloService.AddCapituloAsync(orientoonId, capituloDto);
            return Ok(capitulo);
        }

        /// <summary>
        /// Pega o Capitulo.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCapitulo(string id)
        {
            var capitulo = await _capituloService.GetCapituloByIdAsync(id);
            if (capitulo == null)
            {
                return NotFound();
            }

            return Ok(capitulo);
        }
    }
}
