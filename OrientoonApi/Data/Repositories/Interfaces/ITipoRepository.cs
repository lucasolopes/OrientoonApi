using OrientoonApi.Models.Entities;

namespace OrientoonApi.Data.Repositories.Interfaces
{
    public interface ITipoRepository
    {
        Task AddAsync(TipoModel tipoModel);
        Task DeleteAsync(string id);
        Task<bool> ExistByIdAsync(string id);
        Task<bool> ExistByNomeAsync(string nome);
        Task<TipoModel> FindByIdAsync(string id);
        Task<TipoModel> FindByNomeAsync(string nome);
        Task<List<TipoModel>> GetByAmountAsync(int batchSize, int pageNumber);
        Task UpdateAsync(TipoModel tipoModel);
    }
}
