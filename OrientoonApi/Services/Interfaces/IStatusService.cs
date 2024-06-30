using OrientoonApi.Models.Entities;
using OrientoonApi.Models.Request;
using OrientoonApi.Models.Response;

namespace OrientoonApi.Services.Interfaces
{
    public interface IStatusService
    {
        Task<StatusForm> CreateAsync(StatusDto statusDto);
        Task DeleteAsync(string id);
        Task<StatusModel> GetByIdAsync(string statusId);
        Task<StatusModel> GetByNomeAsync(string status);
        Task<StatusForm> GetAsync(string id);
        Task<List<StatusForm>> GetListAsync(int batchSize, int pageNumber);
        Task<StatusForm> UpdateAsync(string id, StatusDto statusDto);
    }
}
