using OrientoonApi.Models.Request;
using OrientoonApi.Models.Response;

namespace OrientoonApi.Services.Interfaces
{
    public interface IGeneroService
    {
        Task<GeneroForm> CreateAsync(GeneroDto generoDto);
        Task DeleteAsync(int id);
        Task<GeneroForm> GetAsync(int id);
        Task<GeneroForm> GetByNomeAsync(string nome);
        Task<List<GeneroForm>> GetListAsync(int batchSize, int pageNumber);
        Task<GeneroForm> UpdateAsync(int id, GeneroDto generoDto);
    }
}
