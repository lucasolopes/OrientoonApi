using OrientoonApi.Models.Request;
using OrientoonApi.Models.Response;

namespace OrientoonApi.Services.Interfaces
{
    public interface IOrientoonService
    {
         Task<OrientoonForm> CreateAsync(OrientoonDto orientoonDto);
         
         Task<OrientoonForm> GetAsync(string id);
         Task<List<OrientoonForm>> GetListAsync(int batchSize, int pageNumber);
         Task CreateListAsync(List<OrientoonDto> orientoon);
         Task<OrientoonForm> UpdateAsync(string id, OrientoonPutDto orientoon);
        Task DeleteAsync(string id);
    }
}
