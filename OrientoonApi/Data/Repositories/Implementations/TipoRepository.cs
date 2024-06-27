using Microsoft.EntityFrameworkCore;
using OrientoonApi.Data.Contexts;
using OrientoonApi.Data.Repositories.Interfaces;
using OrientoonApi.Models.Entities;

namespace OrientoonApi.Data.Repositories.Implementations
{
    public class TipoRepository : ITipoRepository
    {
        private readonly OrientoonContext _context;

        public TipoRepository(OrientoonContext orientoonContext)
        {
            _context = orientoonContext;
        }

        public async Task AddAsync(TipoModel tipo)
        {
            await _context.Tipo.AddAsync(tipo);
        }

        public async Task<TipoModel> FindByIdAsync(string id)
        {
            return await _context.Tipo.FindAsync(id);
        }

        public async Task<TipoModel> FindByNomeAsync(string nome)
        {
            return await _context.Tipo.FirstOrDefaultAsync(x => x.NomeTipo == nome);
        }

        public async Task DeleteAsync(string id)
        {
            _context.Tipo.Remove(await FindByIdAsync(id));
        }

        public async Task UpdateAsync(TipoModel tipoModel)
        {
            _context.Tipo.Update(tipoModel);
        }

        public async Task<bool> ExistByNomeAsync(string nome)
        {
            return await _context.Tipo.AnyAsync(x => x.NomeTipo == nome);
        }

        public async Task<bool> ExistByIdAsync(string id)
        {
            return await _context.Tipo.AnyAsync(x => x.Id == id);
        }

        public async Task<List<TipoModel>> GetByAmountAsync(int batchSize, int pageNumber)
        {
            return await _context.Tipo.Skip((pageNumber - 1) * batchSize).Take(batchSize).ToListAsync();

        }
        
    }
}
