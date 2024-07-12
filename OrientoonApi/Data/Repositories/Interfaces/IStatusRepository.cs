using OrientoonApi.Enum;
using OrientoonApi.Models.Entities;

namespace OrientoonApi.Data.Repositories.Interfaces
{
    public interface IStatusRepository
    {
        Task<StatusModel> GetByIdAsync(string statusId);
        Task<IEnumerable<StatusModel>> GetAllAsync();
        Task<bool> ExistsByIdAsync(string statusId);
        Task<StatusModel> GetByNameAsync(StatusEnum? status);
        Task<bool> ExistsByNameAsync(StatusEnum? status);
    }
}
