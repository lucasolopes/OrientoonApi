using Microsoft.EntityFrameworkCore;
using OrientoonApi.Data.Contexts;
using OrientoonApi.Data.Repositories.Interfaces;
using OrientoonApi.Models.Entities;
using OrientoonApi.Models.Response;

namespace OrientoonApi.Data.Repositories.Implementations
{
    public class ImagemRepository : IImagemRepository
    {

        private readonly OrientoonContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ImagemRepository(OrientoonContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task AddAsync(ImagemModel imagem)
        {
            await _context.Imagem.AddAsync(imagem);
        }

        public async Task<List<ImagemForm>> GetByCapituloIdAsync(string capituloId)
        {
            var request = _httpContextAccessor.HttpContext.Request;

            var host = $"{request.Scheme}://{request.Host}/imagens";


            return await _context.Imagem.Where(x => x.CapituloId == capituloId).Select(i => new ImagemForm
            {
                Id = i.Id,
                Caminho =  host + "/" + i.Caminho.Replace("\\", "/"),
                CapituloId = i.CapituloId,
                Ordem = i.Ordem,
            }).ToListAsync();
        }

        public async Task<List<ImagemModel>> GetByOrientoonIdAsync(string orientoonId)
        {
            return await _context.Imagem.Where(x => x.Capitulo.OrientoonId == orientoonId).ToListAsync();
        }
    }
}
