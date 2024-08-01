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


        /// <summary>
        /// Atualiza o Capitulo.
        /// </summary>
        [HttpPut("{id}")]
        [ApiExplorerSettings(IgnoreApi = true)]
        //implementar depois
        public async Task<IActionResult> UpdateCapitulo(string id, [FromBody] CapituloDto capituloDto)
        {
            throw new NotImplementedException();
        }



        /// <summary>
        /// Deleta o Capitulo.
        /// </summary>
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCapitulo(string id)
        {
            try
            {
                await _capituloService.DeleteAsync(id);
                return Ok();
            }catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
    }
}
