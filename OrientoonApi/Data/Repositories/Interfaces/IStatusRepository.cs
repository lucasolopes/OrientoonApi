using OrientoonApi.Models.Entities;

namespace OrientoonApi.Data.Repositories.Interfaces
{
    public interface IStatusRepository
    {
        Task<StatusModel> FindByIdAsync(int statusId);
        Task<StatusModel> FindByStatusAsync(string status);
        Task<bool> ExistByIdAsync(int statusId);
        Task<bool> ExistByStatusAsync(string status);
        Task AddAsync(StatusModel statusModel);
        Task<List<StatusModel>> GetByAmountAsync(int batchSize, int pageNumber);
        Task UpdateAsync(StatusModel statusModel);
        Task DeleteAsync(int id);
    }
}
