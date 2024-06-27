using OrientoonApi.Models.Request;
using OrientoonApi.Models.Response;

namespace OrientoonApi.Services.Interfaces
{
    public interface IGeneroService
    {
        Task<GeneroForm> CreateAsync(GeneroDto generoDto);
        Task DeleteAsync(string id);
        Task<GeneroForm> GetAsync(string id);
        Task<GeneroForm> GetByNomeAsync(string nome);
        Task<List<GeneroForm>> GetListAsync(int batchSize, int pageNumber);
        Task<GeneroForm> UpdateAsync(string id, GeneroDto generoDto);
    }
}
