using OrientoonApi.Models.Entities;
using OrientoonApi.Models.Response;

namespace OrientoonApi.Data.Repositories.Interfaces
{
    public interface IOrientoonRepository
    {
        Task AddAsync(OrientoonModel orientoon);
        Task DeleteAsync(string id);
        Task<OrientoonForm> FindByIdAsync(string id);
        Task<List<OrientoonForm>> GetByAmountAsync(int batchSize, int pageNumber);
        Task UpdateAsync(OrientoonModel orientoonModel);
        Task<bool> ExistBtIdAsync(string id);
        Task<bool> ExistByTituloAsync(string titulo);
        Task<List<OrientoonForm>> FindByTituloAsync(string titulo);

    }
}
