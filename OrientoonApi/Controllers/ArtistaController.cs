using Microsoft.AspNetCore.Mvc;
using OrientoonApi.Models.Request;
using OrientoonApi.Models.Response;
using OrientoonApi.Services.Interfaces;

namespace OrientoonApi.Controllers
{
    [Route("api/[controller]")]
    public class ArtistaController : ControllerBase
    {
        private readonly IArtistaService _artistaService;

        public ArtistaController(IArtistaService context)
        {
            _artistaService = context;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ArtistaForm>> Post([FromBody] ArtistaDto artistaDto)
        {
            try
            {
                ArtistaForm artista = await _artistaService.CreateAsync(artistaDto);
                return CreatedAtAction(nameof(Get), new { id = artista.Id }, artista);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ArtistaForm>> Get(int id)
        {
            try
            {
                ArtistaForm artistas = await _artistaService.GetAsync(id);
                return Ok(artistas);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("list")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<ArtistaForm>>> GetList([FromQuery] int batchSize, [FromQuery] int pageNumber)
        {
            try
            {
                List<ArtistaForm> artistas = await _artistaService.GetListAsync(batchSize, pageNumber);
                return Ok(artistas);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ArtistaForm>> Put(int id, [FromBody] ArtistaDto artistaDto)
        {
            try
            {
                ArtistaForm artista = await _artistaService.UpdateAsync(id, artistaDto);
                return Ok(artista);
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
                await _artistaService.DeleteAsync(id);
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
        public async Task<ActionResult<ArtistaForm>> GetByNome(string nome)
        {
            try
            {
                ArtistaForm artista = await _artistaService.GetByNomeAsync(nome);
                return Ok(artista);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
