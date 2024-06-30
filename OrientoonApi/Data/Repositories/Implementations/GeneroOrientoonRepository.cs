using Microsoft.EntityFrameworkCore;
using OrientoonApi.Data.Contexts;
using OrientoonApi.Data.Repositories.Interfaces;
using OrientoonApi.Models.Entities;

namespace OrientoonApi.Data.Repositories.Implementations
{
    public class GeneroOrientoonRepository : GenericRepository<GeneroOrientoonModel>, IGeneroOrientoonRepository
    {
        public GeneroOrientoonRepository(OrientoonContext context) : base(context)
        {
        }

        public async Task AddAsync(string orientoonId, string generoId)
        {
            await _context.GeneroOrientoons.AddAsync(new GeneroOrientoonModel
            {
                IdOrientoon = orientoonId,
                IdGenero = generoId
            });
        }

        public async Task DeleteAsync(string orientoonId, string generoId)
        {
            _context.GeneroOrientoons.Remove(await _context.GeneroOrientoons.FindAsync(orientoonId, generoId));
        }

        public Task<bool> ExistByGeneroIdAsync(string id, string nome)
        {
            return _context.GeneroOrientoons.AnyAsync(x => x.IdOrientoon == id && x.Genero.nome == nome);
        }
    }
}
