using Microsoft.EntityFrameworkCore;
using OrientoonApi.Data.Contexts;
using OrientoonApi.Data.Repositories.Interfaces;
using OrientoonApi.Models.Entities;

namespace OrientoonApi.Data.Repositories.Implementations
{
    public class TipoOrientoonRepository : ITipoOrientoonRepository
    {
        private readonly OrientoonContext _context;

        public TipoOrientoonRepository(OrientoonContext context)
        {
            _context = context;
        }

        public async Task AddAsync(string orientoonId, int tipoId)
        {
            await _context.TipoOrientoons.AddAsync(new TipoOrientoonModel { IdOrientoon = orientoonId, IdTipo = tipoId });
        }

        public Task<bool> ExistByTipoIdAsync(string id, string nome)
        {
            return _context.TipoOrientoons.AnyAsync(x => x.IdOrientoon == id && x.Tipo.NomeTipo == nome);
        }

        public async Task DeleteAsync(string orientoonId, int tipoId)
        {
            _context.TipoOrientoons.Remove(await _context.TipoOrientoons.FindAsync(orientoonId, tipoId));
        }
    }
}
