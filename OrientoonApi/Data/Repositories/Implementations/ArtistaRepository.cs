using Microsoft.EntityFrameworkCore;
using OrientoonApi.Data.Contexts;
using OrientoonApi.Data.Repositories.Interfaces;
using OrientoonApi.Models.Entities;

namespace OrientoonApi.Data.Repositories.Implementations
{
    public class ArtistaRepository : IArtistaRepository
    {
        private readonly OrientoonContext _context;

        public ArtistaRepository(OrientoonContext context)
        {
            _context = context;
        }

        public async Task AddAsync(ArtistaModel artista)
        {
           await _context.Artista.AddAsync(artista);
        }

        public async Task<ArtistaModel> FindByIdAsync(string id)
        {
            return await _context.Artista.FindAsync(id);
        }

        public async Task<ArtistaModel> FindByNomeAsync(string nome)
        {
            return await _context.Artista.FirstOrDefaultAsync(x => x.NomeArtista == nome);
        }

        public async Task DeleteAsync(string id)
        {
            _context.Artista.Remove(await FindByIdAsync(id));
        }

        public async Task UpdateAsync(ArtistaModel artistaModel)
        {
            _context.Artista.Update(artistaModel);
        }

        public async Task<bool> ExistByNomeAsync(string nome)
        {
            return await _context.Artista.AnyAsync(x => x.NomeArtista == nome);
        }

        public async Task<bool> ExistByIdAsync(string id)
        {
            return await _context.Artista.AnyAsync(x => x.Id == id);
        }

        public async Task<List<ArtistaModel>> GetByAmountAsync(int batchSize, int pageNumber)
        {
            return await _context.Artista.Skip((pageNumber - 1) * batchSize).Take(batchSize).ToListAsync();

        }
    }
}
