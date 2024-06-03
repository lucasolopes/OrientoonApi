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
        public async Task<AutorModel> FindByIdAsync(int id)
        {
            return await _context.Autor.FindAsync(id);
        }

        public async Task<AutorModel> FindByNomeAsync(string nome)
        {
            return await _context.Autor.FirstOrDefaultAsync(x => x.NomeAutor == nome);
        }

    }
}
