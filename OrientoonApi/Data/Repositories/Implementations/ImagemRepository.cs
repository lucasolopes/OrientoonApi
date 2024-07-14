using Microsoft.EntityFrameworkCore;
using OrientoonApi.Data.Contexts;
using OrientoonApi.Data.Repositories.Interfaces;
using OrientoonApi.Models.Entities;
using OrientoonApi.Models.Response;
using OrientoonApi.Services.Interfaces;

namespace OrientoonApi.Data.Repositories.Implementations
{
    public class ImagemRepository : IImagemRepository
    {

        private readonly OrientoonContext _context;

        private readonly IUrlService _urlService;

        public ImagemRepository(OrientoonContext context,IUrlService urlService )
        {
            _context = context;
            _urlService = urlService;
        }

        public async Task AddAsync(ImagemModel imagem)
        {
            await _context.Imagem.AddAsync(imagem);
        }

        public async Task<List<ImagemModel>> GetByCapituloIdAsync(string capituloId)
        {
            return await _context.Imagem.Where(x => x.CapituloId == capituloId).Select(i => new ImagemModel
            {
                Id = i.Id,
                Caminho =  _urlService.getImagesBaseUrl() + i.Caminho.Replace("\\", "/"),
                CapituloId = i.CapituloId,
                Ordem = i.Ordem,
                Capitulo = i.Capitulo,
                NomeArquivo = i.NomeArquivo
            }).ToListAsync();
        }

        public async Task<List<ImagemModel>> GetByOrientoonIdAsync(string orientoonId)
        {
            return await _context.Imagem.Where(x => x.Capitulo.OrientoonId == orientoonId).ToListAsync();
        }
    }
}
