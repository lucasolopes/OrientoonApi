﻿using Microsoft.AspNetCore.Mvc;
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

        [HttpPost("{orientoonId}")]
        public async Task<IActionResult> UploadCapitulo(string orientoonId, [FromForm] double numCap, [FromForm] IList<IFormFile> files)
        {
            if (files == null || files.Count == 0)
            {
                return BadRequest("No files provided.");
            }

            var capitulo = await _capituloService.AddCapituloAsync(orientoonId, numCap, files);
            return Ok(capitulo);
        }


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
