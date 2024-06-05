using Microsoft.EntityFrameworkCore;
using OrientoonApi.Data.Contexts;
using OrientoonApi.Data.Repositories.Interfaces;
using OrientoonApi.Models.Entities;
using OrientoonApi.Models.Response;

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
                AdultContent = o.AdultContent
            }).FirstOrDefaultAsync();
            return orientoon;
        }

        public async Task<List<OrientoonForm>> GetByAmountAsync(int batchSize, int pageNumber)
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
                AdultContent = o.AdultContent
            }).ToListAsync();
            return orientoonForms;
        }

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

        public async Task<List<OrientoonForm>> FindByTituloAsync(string titulo)
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
        }

    }
}
