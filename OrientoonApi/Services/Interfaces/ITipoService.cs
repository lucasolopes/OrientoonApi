using OrientoonApi.Models.Entities;
using OrientoonApi.Models.Request;
using OrientoonApi.Models.Response;

namespace OrientoonApi.Services.Interfaces
{
    public interface ITipoService
    {
        Task<TipoForm> CreateAsync(TipoDto tipoDto);
        Task DeleteAsync(string id);
        Task<bool> ExistsByNameAsync(string nome);
        Task<TipoModel> GetByNomeAsync(string nome);
        Task<TipoForm> GetAsync(string id);
        Task<List<TipoForm>> GetListAsync(int batchSize, int pageNumber);
        Task<TipoForm> UpdateAsync(string id, TipoDto tipoDto);
    }
}
