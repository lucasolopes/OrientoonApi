using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using OrientoonApi.Data.Contexts;
using OrientoonApi.Data.Repositories.Interfaces;
using OrientoonApi.Models.Entities;
using OrientoonApi.Models.Request;
using OrientoonApi.Models.Response;
using System.IO;
using System.Linq;
using System.Threading.Channels;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace OrientoonApi.Data.Repositories.Implementations
{
    public class OrientoonRepository : GenericRepository<OrientoonModel>, IOrientoonRepository 
    {

        private readonly IHttpContextAccessor _httpContextAccessor;

        public OrientoonRepository(OrientoonContext context, IHttpContextAccessor httpContextAccessor) : base(context) {

            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<OrientoonForm> GetByIdAsync(string id)
        {

            var request = _httpContextAccessor.HttpContext.Request;

            var host = $"{request.Scheme}://{request.Host}/imagens";

            OrientoonForm orientoon = await _context.Orientoons.Where(x => x.Id == id).Select(o => new OrientoonForm
            {
                Id = o.Id,
                Titulo = o.nome,
                Descricao = o.Descricao,
                DataLancamento = o.DataLancamento,
                Avaliacao = o.Avaliacao,
                NomeArtista = o.Artista.nome,
                NomeAutor = o.Autor.nome,
                Status = o.Status.nome,
                AdultContent = o.AdultContent,
                Banner = host +"/"+ o.CBanner.Replace("\\", "/"),
                Generos = o.GeneroOrientoons.Select(g => new GeneroForm
                {
                    Id = g.Genero.Id,
                    Nome = g.Genero.nome
                }).ToList(),
                Tipos = o.TipoOrientoon.Select(t => new TipoForm
                {
                    Id = t.Tipo.Id,
                    Nome = t.Tipo.nome
                }
                ).ToList()
            }).FirstOrDefaultAsync();

            return orientoon;
        }

        public async Task<string> GetPathBannerById(string id)
        {
            return await _context.Orientoons.Where(x => x.Id == id).Select(x => x.CBanner).FirstOrDefaultAsync();
        }

        public async Task<List<OrientoonForm>> SearchAsync( int batchSize, int pageNumber,SearchDto? searchDto)
        {
            var request = _httpContextAccessor.HttpContext.Request;

            var host = $"{request.Scheme}://{request.Host}/imagens";

            var query = _context.Orientoons.AsQueryable();

            if(!string.IsNullOrEmpty(searchDto.titulo))
                query = query.Where(x => x.NormalizedTitulo.Contains(searchDto.titulo));

            if (searchDto.genero != null && searchDto.genero.Any())
                query = query.Where(x => x.GeneroOrientoons.Any(g => searchDto.genero.Contains(g.Genero.nome)));

            if(!string.IsNullOrEmpty(searchDto.tipo))
                query = query.Where(x => x.TipoOrientoon.Any(t => t.Tipo.nome == searchDto.tipo));

            if(!string.IsNullOrEmpty(searchDto.status))
                query = query.Where(x => x.Status.nome == searchDto.status);

            if(!string.IsNullOrEmpty(searchDto.autor))
                query = query.Where(x => x.Autor.nome == searchDto.autor);

            if(!string.IsNullOrEmpty(searchDto.artista))
                query = query.Where(x => x.Artista.nome == searchDto.artista);


            query = searchDto.orderBy switch
            {
                "titulo" => query.OrderBy(x => x.nome),
                "dataLancamento" => query.OrderBy(x => x.DataLancamento),
                "Artista" => query.OrderBy(x => x.Artista.nome),
                "Autor" => query.OrderBy(x => x.Autor.nome),
                "status" => query.OrderBy(x => x.Status.nome),
                _ => query.OrderBy(x => x.DataLancamento) // Default ordering
            };


            return await query.Skip((pageNumber - 1) * batchSize).Take(batchSize).Select(o => new OrientoonForm
            {
                Id = o.Id,
                Titulo = o.nome,
                Descricao = o.Descricao,
                DataLancamento = o.DataLancamento,
                NomeArtista = o.Artista.nome,
                NomeAutor = o.Autor.nome,
                Status = o.Status.nome,
                AdultContent = o.AdultContent,
                Banner = host + "/"+ o.CBanner.Replace("\\", "/"),
                Avaliacao = o.Avaliacao,
                Generos = o.GeneroOrientoons.Select(g => new GeneroForm
                {
                    Id = g.Genero.Id,
                    Nome = g.Genero.nome
                }).ToList(),
                Tipos = o.TipoOrientoon.Select(t => new TipoForm
                {
                    Id = t.Tipo.Id,
                    Nome = t.Tipo.nome
                }).ToList()

            }).ToListAsync();

        }

    }
}
