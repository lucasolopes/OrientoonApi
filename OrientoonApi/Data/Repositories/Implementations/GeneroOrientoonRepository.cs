using Microsoft.EntityFrameworkCore;
using OrientoonApi.Data.Contexts;
using OrientoonApi.Data.Repositories.Interfaces;
using OrientoonApi.Models.Entities;

namespace OrientoonApi.Data.Repositories.Implementations
{
    public class GeneroOrientoonRepository : IGeneroOrientoonRepository
    {
        private readonly OrientoonContext _context;

        public GeneroOrientoonRepository(OrientoonContext context)
        {
            _context = context;
        }

        public async Task AddAsync(string orientoonId, int generoId)
        {
            await _context.GeneroOrientoons.AddAsync(new GeneroOrientoonModel
            {
                IdOrientoon = orientoonId,
                IdGenero = generoId
            });
        }

        public async Task DeleteAsync(string orientoonId, int generoId)
        {
            _context.GeneroOrientoons.Remove(await _context.GeneroOrientoons.FindAsync(orientoonId, generoId));
        }

        public Task<bool> ExistByGeneroIdAsync(string id, string nome)
        {
            return _context.GeneroOrientoons.AnyAsync(x => x.IdOrientoon == id && x.Genero.NomeGenero == nome);
        }
    }
}
