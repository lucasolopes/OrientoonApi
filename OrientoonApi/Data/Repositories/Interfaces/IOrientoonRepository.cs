using OrientoonApi.Models.Entities;

namespace OrientoonApi.Data.Repositories.Interfaces
{
    public interface IOrientoonRepository
    {
        Task AddAsync(OrientoonModel orientoon);
        Task DeleteAsync(string id);
        Task<OrientoonModel> FindByIdAsync(string id);
        Task<List<OrientoonModel>> GetByAmountAsync(int batchSize, int pageNumber);
        Task UpdateAsync(OrientoonModel orientoon);
    }
}
