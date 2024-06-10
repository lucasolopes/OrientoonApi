using OrientoonApi.Models.Entities;

namespace OrientoonApi.Data.Repositories.Interfaces
{
    public interface ITipoRepository
    {
        Task AddAsync(TipoModel tipoModel);
        Task DeleteAsync(int id);
        Task<bool> ExistByIdAsync(int id);
        Task<bool> ExistByNomeAsync(string nome);
        Task<TipoModel> FindByIdAsync(int id);
        Task<TipoModel> FindByNomeAsync(string nome);
        Task<List<TipoModel>> GetByAmountAsync(int batchSize, int pageNumber);
        Task UpdateAsync(TipoModel tipoModel);
    }
}
