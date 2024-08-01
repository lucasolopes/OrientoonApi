using Microsoft.AspNetCore.Mvc;

using OrientoonApi.Models;
using OrientoonApi.Models.Request;
using OrientoonApi.Models.Response;
using OrientoonApi.Services;
using OrientoonApi.Services.Interfaces;
using Microsoft.AspNetCore.Diagnostics;
using System.Text.Json;
using OrientoonApi.Data.Repositories.Interfaces;

namespace OrientoonApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrientoonController : ControllerBase
    {
        private readonly IOrientoonService _orientoonContext;
        private readonly IOrientoonRepository _orientoonRepository;

        public OrientoonController(IOrientoonService context, IOrientoonRepository orientoonRepository)
        {
            _orientoonContext = context;
            _orientoonRepository = orientoonRepository;

        }

        /// <summary>
        /// Cria um novo Orientoon.
        /// </summary>
        [HttpPost]
        [Consumes("multipart/form-data")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<OrientoonForm>> PostOrientoon([FromForm] OrientoonDto orientoon,IFormFile banner)
        {
            try {
                if(banner == null || banner.Length == 0)
                    return BadRequest("Banner não informado.");

                OrientoonForm orientoonForm = await _orientoonContext.CreateAsync(orientoon,banner);
                return CreatedAtAction(nameof(GetOrientoon), new { id = orientoonForm.Id }, orientoonForm);
            } catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }


        /// <summary>
        /// Pega o Orientoon.
        /// </summary>
        [HttpGet("{id}")]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<OrientoonForm>> GetOrientoon(string id)
        {
            OrientoonForm orientoonForm = await _orientoonContext.GetAsync(id);
            return Ok(orientoonForm);

        }


        /// <summary>
        /// Atualiza o Orientoon.
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<OrientoonForm>> PutOrientoon(string id, OrientoonPutDto orientoon)
        {
            try
            {
                OrientoonForm orientoonForm = await _orientoonContext.UpdateAsync(id, orientoon);
                return Ok(orientoonForm);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Deleta o Orientoon.
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteOrientoon(string id)
        {
            try
            {
                await _orientoonContext.DeleteAsync(id);

                return Ok("Deletado com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Busca a lista de Orientoons.
        /// </summary>
        [HttpGet("Search")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<OrientoonForm>>> SearchOrientoon([FromQuery] PageableDto pageable, [FromQuery] SearchDto? searchDto)
        {


            IEnumerable<OrientoonForm> orientoonForm = await _orientoonContext.SearchAsync(pageable.batchSize, pageable.pageNumber, searchDto);

            return Ok(orientoonForm);
        }

        /// <summary>
        /// Adiciona um Genero ao Orientoon.
        /// </summary>
        [HttpPost("genero")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult> AddGeneroInOrientoon(string id, GeneroDto generoDto)
        {
            try
            {
                await _orientoonContext.AddGeneroAsync(id, generoDto);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Deleta um Genero ao Orientoon.
        /// </summary>
        [HttpDelete("genero")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteGeneroInOrientoon(string id, GeneroDto generoDto)
        {
            try
            {
                await _orientoonContext.DeleteGeneroAsync(id, generoDto);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Adiciona um Tipo ao Orientoon.
        /// </summary>
        [HttpPost("tipo")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult> AddTipoInOrientoon(string id, TipoDto tipoDto)
        {
            try
            {
                await _orientoonContext.AddTipoAsync(id, tipoDto);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Deleta um Tipo ao Orientoon.
        /// </summary>
        [HttpDelete("tipo")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteTipoInOrientoon(string id, TipoDto tipoDto)
        {
            try
            {
                await _orientoonContext.DeleteTipoAsync(id, tipoDto);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Pega o Orientoon com os capitulos.
        /// </summary>
        [HttpGet("{id}/aggregate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<OrientoonAggregateForm>> GetOrientoonAggregate(string id)
        {
            OrientoonAggregateForm orientoonAggregate = await _orientoonContext.GetAggregateAsync(id);
            return Ok(orientoonAggregate);
        }

        /// <summary>
        /// Pega um Orientoon aleatório.
        /// </summary>
        [HttpGet("random")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<OrientoonForm>> GetRandomOrientoon()
        {
            OrientoonForm orientoonForm = await _orientoonContext.GetRandomAsync();
            return Ok(orientoonForm);
        }
    }
}
