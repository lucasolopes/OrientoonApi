using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using OrientoonApi.Data.Contexts;
using OrientoonApi.Data.Repositories.Interfaces;
using OrientoonApi.Models.Entities;
using OrientoonApi.Models.Request;
using OrientoonApi.Models.Response;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace OrientoonApi.Data.Repositories.Implementations
{
    public class OrientoonRepository : IOrientoonRepository
    {
        private readonly OrientoonContext _context;

        public OrientoonRepository(OrientoonContext context)
        {
            _context = context;
        }

        public async Task AddAsync(OrientoonModel orientoonModel)
        {
            await _context.Orientoons.AddAsync(orientoonModel);
        }



        public async Task<OrientoonForm> FindByIdAsync(string id)
        {
            OrientoonForm orientoon = await _context.Orientoons.Where(x => x.Id == id).Select(o => new OrientoonForm
            {
                Id = o.Id,
                Titulo = o.Titulo,
                Descricao = o.Descricao,
                DataLancamento = o.DataLancamento,
                NomeArtista = o.Artista.NomeArtista,
                NomeAutor = o.Autor.NomeAutor,
                Status = o.Status.Status,
                AdultContent = o.AdultContent,
                Generos = o.GeneroOrientoons.Select(g => new GeneroForm
                {
                    Id = g.Genero.Id,
                    Nome = g.Genero.NomeGenero
                }).ToList(),
                Tipos = o.TipoOrientoon.Select(t => new TipoForm
                {
                    Id = t.Tipo.Id,
                    Nome = t.Tipo.NomeTipo
                }).ToList()
            }).FirstOrDefaultAsync();

            return orientoon;
        }

        public async Task teste(string id)
        {
            //get orientoon by id and  all generoOrientoon by orientoonId
            OrientoonModel orientoon = await _context.Orientoons.Include(x => x.GeneroOrientoons).FirstOrDefaultAsync(x => x.Id == id);

    
        }

       /* public async Task<List<OrientoonForm>> GetByAmountAsync(int batchSize, int pageNumber)
        {
            //get orientoons quantity
            List<OrientoonForm> orientoonForms = await _context.Orientoons.Skip((pageNumber - 1) * batchSize).Take(batchSize).Select(o => new OrientoonForm
            {

                Id = o.Id,
                Titulo = o.Titulo,
                Descricao = o.Descricao,
                DataLancamento = o.DataLancamento,
                NomeArtista = o.Artista.NomeArtista,
                NomeAutor = o.Autor.NomeAutor,
                Status = o.Status.Status,
                AdultContent = o.AdultContent,
                Generos = o.GeneroOrientoons.Select(g => new GeneroForm
                {
                    Id = g.Genero.Id,
                    Nome = g.Genero.NomeGenero
                }).ToList(),
                Tipos = o.TipoOrientoon.Select(t => new TipoForm
                {
                    Id = t.Tipo.Id,
                    Nome = t.Tipo.NomeTipo
                }).ToList()
            }).ToListAsync();
            return orientoonForms;
        }*/

        //Put by id
        public async Task UpdateAsync(OrientoonModel orientoonModel)
        {
            //convert OrientoonForm to OrientoonModel
            


            _context.Orientoons.Update(orientoonModel);
        }

        public async Task DeleteAsync(string id)
        {
            //delete orientoon by id
            OrientoonModel orientoon = await _context.Orientoons.FirstOrDefaultAsync(x => x.Id == id);
            _context.Orientoons.Remove(orientoon);
        }

        public async Task<bool> ExistBtIdAsync(string id)
        {
            return await _context.Orientoons.AnyAsync(x => x.Id == id);
        }

        public Task<bool> ExistByTituloAsync(string titulo)
        {
            return _context.Orientoons.AnyAsync(x => x.NormalizedTitulo == titulo.ToUpper());
        }

        public async Task<List<OrientoonForm>> SearchAsync( int batchSize, int pageNumber,SearchDto? searchDto)
        {

            var query = _context.Orientoons.AsQueryable();

            if(!string.IsNullOrEmpty(searchDto.titulo))
                query = query.Where(x => x.NormalizedTitulo.Contains(searchDto.titulo));

            if (searchDto.genero != null && searchDto.genero.Any())
                query = query.Where(x => x.GeneroOrientoons.Any(g => searchDto.genero.Contains(g.Genero.NomeGenero)));

            if(!string.IsNullOrEmpty(searchDto.tipo))
                query = query.Where(x => x.TipoOrientoon.Any(t => t.Tipo.NomeTipo == searchDto.tipo));

            if(!string.IsNullOrEmpty(searchDto.status))
                query = query.Where(x => x.Status.Status == searchDto.status);

            if(!string.IsNullOrEmpty(searchDto.autor))
                query = query.Where(x => x.Autor.NomeAutor == searchDto.autor);

            if(!string.IsNullOrEmpty(searchDto.artista))
                query = query.Where(x => x.Artista.NomeArtista == searchDto.artista);


            query = searchDto.orderBy switch
            {
                "titulo" => query.OrderBy(x => x.Titulo),
                "dataLancamento" => query.OrderBy(x => x.DataLancamento),
                "Artista" => query.OrderBy(x => x.Artista.NomeArtista),
                "Autor" => query.OrderBy(x => x.Autor.NomeAutor),
                "status" => query.OrderBy(x => x.Status.Status),
                _ => query.OrderBy(x => x.DataLancamento) // Default ordering
            };


            return await query.Skip((pageNumber - 1) * batchSize).Take(batchSize).Select(o => new OrientoonForm
            {
                Id = o.Id,
                Titulo = o.Titulo,
                Descricao = o.Descricao,
                DataLancamento = o.DataLancamento,
                NomeArtista = o.Artista.NomeArtista,
                NomeAutor = o.Autor.NomeAutor,
                Status = o.Status.Status,
                AdultContent = o.AdultContent,
                Generos = o.GeneroOrientoons.Select(g => new GeneroForm
                {
                    Id = g.Genero.Id,
                    Nome = g.Genero.NomeGenero
                }).ToList(),
                Tipos = o.TipoOrientoon.Select(t => new TipoForm
                {
                    Id = t.Tipo.Id,
                    Nome = t.Tipo.NomeTipo
                }).ToList()

            }).ToListAsync();

        }

        /* public async Task<List<OrientoonForm>> FindByTituloAsync(string titulo)
         {
             List<OrientoonForm> orientoon = await _context.Orientoons.Where(x => x.NormalizedTitulo == titulo.ToUpper()).Select(o => new OrientoonForm
             {

                 Id = o.Id,
                 Titulo = o.Titulo,
                 Descricao = o.Descricao,
                 DataLancamento = o.DataLancamento,
                 NomeArtista = o.Artista.NomeArtista,
                 NomeAutor = o.Autor.NomeAutor,
                 Status = o.Status.Status,
                 AdultContent = o.AdultContent
             }).ToListAsync();
             //return await _context.Orientoons.FirstOrDefaultAsync(x => x.NormalizedTitulo == titulo.ToUpper());
             return orientoon;
         }*/

    }
}
