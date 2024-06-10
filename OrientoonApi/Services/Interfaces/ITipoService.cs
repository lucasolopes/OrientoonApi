using OrientoonApi.Models.Request;
using OrientoonApi.Models.Response;

namespace OrientoonApi.Services.Interfaces
{
    public interface ITipoService
    {
        Task<TipoForm> CreateAsync(TipoDto tipoDto);
        Task DeleteAsync(int id);
        Task<TipoForm> GetAsync(int id);
        Task<TipoForm> GetByNomeAsync(string nome);
        Task<List<TipoForm>> GetListAsync(int batchSize, int pageNumber);
        Task<TipoForm> UpdateAsync(int id, TipoDto tipoDto);
    }
}
