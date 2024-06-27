using OrientoonApi.Models.Request;
using OrientoonApi.Models.Response;

namespace OrientoonApi.Services.Interfaces
{
    public interface IStatusService
    {
        Task<StatusForm> CreateAsync(StatusDto statusDto);
        Task DeleteAsync(string id);
        Task<StatusForm> GetAsync(string id);
        Task<StatusForm> GetByNomeAsync(string nome);
        Task<List<StatusForm>> GetListAsync(int batchSize, int pageNumber);
        Task<StatusForm> UpdateAsync(string id, StatusDto statusDto);
    }
}
