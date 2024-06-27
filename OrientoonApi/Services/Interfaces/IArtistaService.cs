using OrientoonApi.Models.Request;
using OrientoonApi.Models.Response;

namespace OrientoonApi.Services.Interfaces
{
    public interface IArtistaService
    {
        Task<ArtistaForm> CreateAsync(ArtistaDto artistaDto);
        Task<ArtistaForm> GetAsync(string id);
        Task<List<ArtistaForm>> GetListAsync(int batchSize, int pageNumber);
        Task CreateListAsync(List<ArtistaDto> artistaDto);
        Task<ArtistaForm> UpdateAsync(string id, ArtistaDto artistaDto);
        Task DeleteAsync(string id);
        Task<ArtistaForm> GetByNomeAsync(string nome);
    }
}
