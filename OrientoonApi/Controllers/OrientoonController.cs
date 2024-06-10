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

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<OrientoonForm>> PostOrientoon(OrientoonDto orientoon)
        {
            /* try
             {
                 if (!ModelState.IsValid)
                 {
                     var errors = new
                     {
                         Message = "Ocorreu um erro de validação",
                         Errors = ModelState.Values.SelectMany(v => v.Errors)
                         .Select(e => e.ErrorMessage)
                     };
                     // Serializa os erros usando o contratante de camelCase (opcional)
                     var settings = new JsonSerializerSettings
                     {
                         ContractResolver = new CamelCasePropertyNamesContractResolver()
                     };
                     var json = JsonConvert.SerializeObject(errors, settings);

                     // Retorna um BadRequest com a mensagem de erro personalizada
                     return BadRequest(json);
                 }
            */
            try {

                OrientoonForm orientoonForm = await _orientoonContext.CreateAsync(orientoon);
                return CreatedAtAction(nameof(GetOrientoon), new { id = orientoonForm.Id }, orientoonForm);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        //post: api/Orientoon/List
        [HttpPost("List")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> PostListOrientoon(List<OrientoonDto> orientoon)
        {
            try
            {
                await _orientoonContext.CreateListAsync(orientoon);

                return Ok("Cadastro realizado com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        //get: api/Orientoon/{id}
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<OrientoonForm>> GetOrientoon(string id)
        {
            OrientoonForm orientoonForm = await _orientoonContext.GetAsync(id);

            return Ok(orientoonForm);

        }

        //get: api/Orientoon/{batchSize}/{oageNumber}
       /* [HttpGet("{batchSize}/{pageNumber}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<OrientoonForm>>> GetOrientoon(int batchSize, int pageNumber)
        {
            List<OrientoonForm> orientoonForm = await _orientoonContext.GetListAsync(batchSize, pageNumber);

            return Ok(orientoonForm);
        }*/

        //put: api/Orientoon/{id}
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

        //delete: api/Orientoon/{id}
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


        [HttpGet("Search")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<OrientoonForm>>> SearchOrientoon([FromQuery] int? batchSize, [FromQuery] int? pageNumber,[FromQuery] SearchDto? searchDto)
        {
            /*
                OrderBy Options:
                "titulo" 
                "dataLancamento" 
                "Artista" 
                "Autor"
                "status" 
             */
            int batchSizeValue = batchSize ?? 10;
            int pageNumberValue = pageNumber ?? 1;

            IEnumerable<OrientoonForm> orientoonForm = await _orientoonContext.SearchAsync(batchSizeValue, pageNumberValue, searchDto);

            return Ok(orientoonForm);
        }


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



    }
}
