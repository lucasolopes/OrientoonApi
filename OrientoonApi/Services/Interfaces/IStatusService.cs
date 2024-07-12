using OrientoonApi.Enum;
using OrientoonApi.Models.Entities;
using OrientoonApi.Models.Request;
using OrientoonApi.Models.Response;

namespace OrientoonApi.Services.Interfaces
{
    public interface IStatusService
    {
        Task<StatusModel> GetByIdAsync(string statusId);
        Task<IEnumerable<StatusModel>> GetAllAsync();
        Task<StatusModel?> GetByNameAsync(StatusEnum? status);
    }
}
