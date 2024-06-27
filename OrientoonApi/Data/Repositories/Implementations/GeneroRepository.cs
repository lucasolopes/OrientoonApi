using Microsoft.EntityFrameworkCore;
using OrientoonApi.Data.Contexts;
using OrientoonApi.Data.Repositories.Interfaces;
using OrientoonApi.Models.Entities;

namespace OrientoonApi.Data.Repositories.Implementations
{
    public class GeneroRepository : IGeneroRepository
    {
        private readonly OrientoonContext _context;

        public GeneroRepository(OrientoonContext contextRepository)
        {
            _context = contextRepository;
        }

        public async Task AddAsync(GeneroModel genero)
        {
            await _context.Genero.AddAsync(genero);
        }

        public async Task<GeneroModel> FindByIdAsync(string id)
        {
            return await _context.Genero.FindAsync(id);
        }

        public async Task<GeneroModel> FindByNomeAsync(string nome)
        {
            return await _context.Genero.FirstOrDefaultAsync(x => x.NomeGenero == nome);
        }

        public async Task DeleteAsync(string id)
        {
            _context.Genero.Remove(await FindByIdAsync(id));
        }

        public async Task UpdateAsync(GeneroModel generoModel)
        {
            _context.Genero.Update(generoModel);
        }

        public async Task<bool> ExistByNomeAsync(string nome)
        {
            return await _context.Genero.AnyAsync(x => x.NomeGenero == nome);
        }

        public async Task<bool> ExistByIdAsync(string id)
        {
            return await _context.Genero.AnyAsync(x => x.Id == id);
        }

        public async Task<List<GeneroModel>> GetByAmountAsync(int batchSize, int pageNumber)
        {
            return await _context.Genero.Skip((pageNumber - 1) * batchSize).Take(batchSize).ToListAsync();

        }

    }
}
