using Microsoft.EntityFrameworkCore;
using OrientoonApi.Data.Contexts;
using OrientoonApi.Data.Repositories.Interfaces;
using OrientoonApi.Models.Entities;

namespace OrientoonApi.Data.Repositories.Implementations
{
    public class ImagemRepository : IImagemRepository
    {

        private readonly OrientoonContext _context;

        public ImagemRepository(OrientoonContext context)
        {
            _context = context;
        }

        public async Task AddAsync(ImagemModel imagem)
        {
            await _context.Imagem.AddAsync(imagem);
        }

        public Task<List<ImagemModel>> GetByCapituloIdAsync(string capituloId)
        {
            return _context.Imagem.Where(x => x.CapituloId == capituloId).ToListAsync();
        }
    }
}
