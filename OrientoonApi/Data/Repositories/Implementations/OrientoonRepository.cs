using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using OrientoonApi.Data.Contexts;
using OrientoonApi.Data.Repositories.Interfaces;
using OrientoonApi.Models.Entities;
using OrientoonApi.Models.Request;
using OrientoonApi.Models.Response;
using OrientoonApi.Services.Interfaces;
using System.IO;
using System.Linq;
using System.Threading.Channels;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace OrientoonApi.Data.Repositories.Implementations
{
    public class OrientoonRepository : GenericRepository<OrientoonModel>, IOrientoonRepository 
    {

        private readonly IUrlService _urlService;

        public OrientoonRepository(OrientoonContext context, IUrlService urlService) : base(context) {

            _urlService = urlService;

        }

        public async Task<OrientoonModel> GetByIdAsync(string id)
        {

            return await _context.Orientoons.Where(x => x.Id == id).Select(o => new OrientoonModel
            {
                Id = o.Id,
                nome = o.nome,
                Descricao = o.Descricao,
                DataLancamento = o.DataLancamento,
                Artista = o.Artista,
                Autor = o.Autor,
                Status = o.Status,
                Avaliacao = o.Avaliacao,
                AdultContent = o.AdultContent,
               GeneroOrientoons = o.GeneroOrientoons,
               TipoOrientoon = o.TipoOrientoon,
               CBanner = _urlService.getImagesBaseUrl() + o.CBanner.Replace("\\", "/"),
               ArtistaId = o.ArtistaId,
               AutorId = o.AutorId,
               StatusId = o.StatusId,
               NormalizedName = o.NormalizedName,
               Capitulos = o.Capitulos
            }).FirstOrDefaultAsync();
            
        }

        public async Task<string> GetPathBannerById(string id)
        {
            return await _context.Orientoons.Where(x => x.Id == id).Select(x => x.CBanner).FirstOrDefaultAsync();
        }

        public async Task<OrientoonModel> GetRandomAsync()
        {
            var count = await _context.Orientoons.CountAsync();
            var randomindex =  new Random().Next(count);
            return await _context.Orientoons.Skip(randomindex).Select(o => new OrientoonModel
            {
                Id = o.Id,
                nome = o.nome,
                Descricao = o.Descricao,
                DataLancamento = o.DataLancamento,
                Artista = o.Artista,
                Autor = o.Autor,
                Status = o.Status,
                Avaliacao = o.Avaliacao,
                AdultContent = o.AdultContent,
                GeneroOrientoons = o.GeneroOrientoons,
                TipoOrientoon = o.TipoOrientoon,
                CBanner = _urlService.getImagesBaseUrl() + o.CBanner.Replace("\\", "/"),
                ArtistaId = o.ArtistaId,
                AutorId = o.AutorId,
                StatusId = o.StatusId,
                NormalizedName = o.NormalizedName,
                Capitulos = o.Capitulos
            }).FirstOrDefaultAsync();
        }

        public async Task<List<OrientoonModel>> SearchAsync( int batchSize, int pageNumber,SearchDto? searchDto)
        {

            var query = _context.Orientoons.AsQueryable();

            if(!string.IsNullOrEmpty(searchDto.titulo))
                query = query.Where(x => x.NormalizedName.Contains(searchDto.titulo));

            if (searchDto.genero != null && searchDto.genero.Any())
                query = query.Where(x => x.GeneroOrientoons.Any(g => searchDto.genero.Contains(g.Genero.nome)));

            if(!string.IsNullOrEmpty(searchDto.tipo))
                query = query.Where(x => x.TipoOrientoon.Any(t => t.Tipo.nome == searchDto.tipo));

            if(searchDto.status != null )
                query = query.Where(x => x.Status.nome == searchDto.status);

            if(!string.IsNullOrEmpty(searchDto.autorId))
                query = query.Where(x => x.Autor.Id == searchDto.autorId);

            if(!string.IsNullOrEmpty(searchDto.artistaId))
                query = query.Where(x => x.Artista.Id == searchDto.artistaId);


            query = searchDto.orderBy switch
            {
                "titulo" => query.OrderBy(x => x.nome),
                "dataLancamento" => query.OrderBy(x => x.DataLancamento),
                "Artista" => query.OrderBy(x => x.Artista.nome),
                "Autor" => query.OrderBy(x => x.Autor.nome),
                "status" => query.OrderBy(x => x.Status.nome),
                _ => query.OrderBy(x => x.DataLancamento) // Default ordering
            };


            return await query.Skip((pageNumber - 1) * batchSize).Take(batchSize).Select(o => new OrientoonModel
            {
                Id = o.Id,
                nome = o.nome,
                Descricao = o.Descricao,
                DataLancamento = o.DataLancamento,
                ArtistaId = o.ArtistaId,
                Artista = o.Artista,
                AutorId = o.AutorId,
                Autor = o.Autor,
                Status = o.Status,
                StatusId = o.StatusId,
                AdultContent = o.AdultContent,
                CBanner = _urlService.getImagesBaseUrl() + o.CBanner.Replace("\\", "/"),
                Avaliacao = o.Avaliacao,
                GeneroOrientoons = o.GeneroOrientoons,
                TipoOrientoon = o.TipoOrientoon,
                Capitulos = o.Capitulos,
                NormalizedName = o.NormalizedName
            }).ToListAsync();

        }

    }
}
