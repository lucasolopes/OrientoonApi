using Microsoft.EntityFrameworkCore;
using OrientoonApi.Data.Contexts;
using OrientoonApi.Data.Repositories.Interfaces;
using OrientoonApi.Models.Entities;
using OrientoonApi.Models.Response;
using OrientoonApi.Services.Interfaces;

namespace OrientoonApi.Data.Repositories.Implementations
{
    public class CapituloRepository : ICapituloRepository
    {
        private readonly OrientoonContext _context;
        private readonly IUrlService _urlService;

        public CapituloRepository(OrientoonContext context, IUrlService urlService)
        {
            _context = context;
            _urlService = urlService;
        }

        public async Task<CapituloModel> AddAsync(CapituloModel capitulo)
        {
            _context.Capitulo.Add(capitulo);
            await _context.SaveChangesAsync();


            return new CapituloModel
            {
                Id = capitulo.Id,
                NumCapitulo = capitulo.NumCapitulo,
                OrientoonId = capitulo.OrientoonId,
                AvaliacaoCap = capitulo.AvaliacaoCap,
                Caminho = capitulo.Caminho,
                DataLancamento = capitulo.DataLancamento,
                Imagens = capitulo.Imagens.Select(i => new ImagemModel
                {
                    Id = i.Id,
                    Caminho = _urlService.getImagesBaseUrl() + i.Caminho.Replace("\\", "/"),
                    CapituloId = i.CapituloId,
                    Ordem = i.Ordem,
                    NomeArquivo = i.NomeArquivo
                }).ToList()
            };

        }

        public async Task<CapituloModel> GetByIdAsync(string id)
        {


            return await _context.Capitulo.Where(x => x.Id == id).Select(capitulo => new CapituloModel
            {
                Id = capitulo.Id,
                NumCapitulo = capitulo.NumCapitulo,
                OrientoonId = capitulo.OrientoonId,
                AvaliacaoCap = capitulo.AvaliacaoCap,
                Caminho = capitulo.Caminho,
                DataLancamento = capitulo.DataLancamento,
                Imagens = capitulo.Imagens.Select(i => new ImagemModel
                {
                    Id = i.Id,
                    Caminho = _urlService.getImagesBaseUrl() + i.Caminho.Replace("\\", "/"),
                    CapituloId = i.CapituloId,
                    Ordem = i.Ordem,
                    NomeArquivo = i.NomeArquivo
                }).ToList()
            }).FirstOrDefaultAsync();
        }

        public Task<List<CapituloModel>> GetCapituloByOrientoonIdAsync(string orientoonId)
        {
            return _context.Capitulo.Where(x => x.OrientoonId == orientoonId).ToListAsync();
        }

    }
}
