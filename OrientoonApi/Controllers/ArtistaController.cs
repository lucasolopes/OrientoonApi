﻿using Microsoft.AspNetCore.Mvc;
using OrientoonApi.Models.Entities;
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

        /// <summary>
        /// Cria o Artista.
        /// </summary>
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

        /// <summary>
        /// Pega o Artista.
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ArtistaForm>> Get(string id)
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


        /// <summary>
        /// Pega a lista de Artistas.
        /// </summary>
        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<ArtistaForm>>> GetList([FromQuery] PageableDto pageable)
        {
            try
            {
                List<ArtistaForm> artistas = await _artistaService.GetListAsync(pageable.batchSize, pageable.pageNumber);
                return Ok(artistas);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Atualiza o Artista.
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ArtistaForm>> Put(string id, [FromBody] ArtistaDto artistaDto)
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

        /// <summary>
        /// Deleta o Artista.
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(string id)
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
    }
}
