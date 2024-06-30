using OrientoonApi.Models.Entities;

namespace OrientoonApi.Data.Repositories.Interfaces
{
    public interface ITipoOrientoonRepository : IGenericRepository<TipoOrientoonModel>
    {
        Task AddAsync(string orientoonId, string tipoId);
        Task<bool> ExistByTipoIdAsync(string id, string nome);
        Task DeleteAsync(string orientoonId, string tipoId);
    }
}
