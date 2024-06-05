using OrientoonApi.Models.Request;
using OrientoonApi.Models.Response;

namespace OrientoonApi.Services.Interfaces
{
    public interface IStatusService
    {
        Task<StatusForm> CreateAsync(StatusDto statusDto);
        Task DeleteAsync(int id);
        Task<StatusForm> GetAsync(int id);
        Task<StatusForm> GetByNomeAsync(string nome);
        Task<List<StatusForm>> GetListAsync(int batchSize, int pageNumber);
        Task<StatusForm> UpdateAsync(int id, StatusDto statusDto);
    }
}
