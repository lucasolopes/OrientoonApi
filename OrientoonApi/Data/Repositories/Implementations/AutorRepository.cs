using Microsoft.EntityFrameworkCore;
using OrientoonApi.Data.Contexts;
using OrientoonApi.Data.Repositories.Interfaces;
using OrientoonApi.Models.Entities;

namespace OrientoonApi.Data.Repositories.Implementations
{
    public class AutorRepository : IAutorRepository
    {
        private readonly OrientoonContext _context;

        public AutorRepository(OrientoonContext context)
        {
            _context = context;
        }
        public async Task<AutorModel> FindByIdAsync(string id)
        {
            return await _context.Autor.FindAsync(id);
        }

        public async Task<AutorModel> FindByNomeAsync(string nome)
        {
            return await _context.Autor.FirstOrDefaultAsync(x => x.NomeAutor == nome);
        }

        public async Task<bool> ExistByNomeAsync(string nome)
        {
            return await _context.Autor.AnyAsync(x => x.NomeAutor == nome);
        }

        public async Task<bool> ExistByIdAsync(string id)
        {
            return await _context.Autor.AnyAsync(x => x.Id == id);
        }


        public async Task AddAsync(AutorModel Autor)
        {
            await _context.Autor.AddAsync(Autor);
        }


        public async Task DeleteAsync(string id)
        {
            _context.Autor.Remove(await FindByIdAsync(id));
        }

        public async Task UpdateAsync(AutorModel AutorModel)
        {
            _context.Autor.Update(AutorModel);
        }

        public async Task<List<AutorModel>> GetByAmountAsync(int batchSize, int pageNumber)
        {
            return await _context.Autor.Skip((pageNumber - 1) * batchSize).Take(batchSize).ToListAsync();

        }

    }
}
