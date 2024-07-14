using OrientoonApi.Models.Request;
using OrientoonApi.Models.Response;

namespace OrientoonApi.Services.Interfaces
{
    public interface IOrientoonService
    {
         Task<OrientoonForm> CreateAsync(OrientoonDto orientoonDto,IFormFile banner);
         
         Task<OrientoonForm> GetAsync(string id);
        // Task<List<OrientoonForm>> GetListAsync(int batchSize, int pageNumber);
         Task CreateListAsync(List<OrientoonDto> orientoon);
         Task<OrientoonForm> UpdateAsync(string id, OrientoonPutDto orientoon);
        Task DeleteAsync(string id);
        Task<IEnumerable<OrientoonForm>> SearchAsync(int batchSize, int pageNumber, SearchDto? searchDto);
        Task AddGeneroAsync(string id, GeneroDto generoDtos);
        Task DeleteGeneroAsync(string id, GeneroDto generoDtos);
        Task AddTipoAsync(string id, TipoDto tipoDto);
        Task DeleteTipoAsync(string id, TipoDto tipoDto);
        Task<OrientoonAggregateForm> GetAggregateAsync(string id);
        Task<OrientoonForm> GetRandomAsync();
    }
}
