using Microsoft.EntityFrameworkCore;
using OrientoonApi.Data.Contexts;
using OrientoonApi.Data.Repositories.Interfaces;
using OrientoonApi.Models.Entities;

namespace OrientoonApi.Data.Repositories.Implementations
{
    public class TipoOrientoonRepository : GenericRepository<TipoOrientoonModel>, ITipoOrientoonRepository
    {
        public TipoOrientoonRepository(OrientoonContext context) : base(context)
        {
        }

        public async Task AddAsync(string orientoonId, string tipoId)
        {
            await _context.TipoOrientoons.AddAsync(new TipoOrientoonModel { IdOrientoon = orientoonId, IdTipo = tipoId });
        }

        public Task<bool> ExistByTipoIdAsync(string id, string nome)
        {
            return _context.TipoOrientoons.AnyAsync(x => x.IdOrientoon == id && x.Tipo.nome == nome);
        }

        public async Task DeleteAsync(string orientoonId, string tipoId)
        {
            _context.TipoOrientoons.Remove(await _context.TipoOrientoons.FindAsync(orientoonId, tipoId));
        }
    }
}
