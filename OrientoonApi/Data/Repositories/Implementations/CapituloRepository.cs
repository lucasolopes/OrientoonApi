using Microsoft.EntityFrameworkCore;
using OrientoonApi.Data.Contexts;
using OrientoonApi.Data.Repositories.Interfaces;
using OrientoonApi.Models.Entities;
using OrientoonApi.Models.Response;

namespace OrientoonApi.Data.Repositories.Implementations
{
    public class CapituloRepository : ICapituloRepository
    {
        private readonly OrientoonContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CapituloRepository(OrientoonContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<CapituloForm> AddAsync(CapituloModel capitulo)
        {
            _context.Capitulo.Add(capitulo);
            await _context.SaveChangesAsync();
            var request = _httpContextAccessor.HttpContext.Request;

            var host = $"{request.Scheme}://{request.Host}/imagens";

            CapituloForm capituloForm = new CapituloForm
            {
                Id = capitulo.Id,
                NumCapitulo = capitulo.NumCapitulo,
                OrientoonId = capitulo.OrientoonId,
                Imagens = capitulo.Imagens.Select(i => new ImagemForm
                {
                    Id = i.Id,
                    Caminho = host + "/" + i.Caminho.Replace("\\", "/"),
                    CapituloId = i.CapituloId,
                    Ordem = i.Ordem,
                }).ToList()
            };

            return capituloForm;
        }

        public async Task<CapituloForm> GetByIdAsync(string id)
        {
            var request = _httpContextAccessor.HttpContext.Request;

            var host = $"{request.Scheme}://{request.Host}/imagens";

            return await _context.Capitulo.Where(x => x.Id == id).Select(c => new CapituloForm
            {
                Id = c.Id,
                NumCapitulo = c.NumCapitulo,
                OrientoonId = c.OrientoonId,
                Imagens = c.Imagens.Select(i => new ImagemForm
                {
                    Id = i.Id,
                    Caminho = host + "/" + i.Caminho.Replace("\\", "/"),
                    CapituloId = i.CapituloId,
                    Ordem = i.Ordem,
                }).ToList()
            }).FirstOrDefaultAsync();
        }

        public Task<List<CapituloModel>> GetCapituloByOrientoonIdAsync(string orientoonId)
        {
            return _context.Capitulo.Where(x => x.OrientoonId == orientoonId).ToListAsync();
        }

    }
}
