using OrientoonApi.Models.Request;
using OrientoonApi.Models.Response;

namespace OrientoonApi.Services.Interfaces
{
    public interface IArtistaService
    {
        Task<ArtistaForm> CreateAsync(ArtistaDto artistaDto);
        Task<ArtistaForm> GetAsync(int id);
        Task<List<ArtistaForm>> GetListAsync(int batchSize, int pageNumber);
        Task CreateListAsync(List<ArtistaDto> artistaDto);
        Task<ArtistaForm> UpdateAsync(int id, ArtistaDto artistaDto);
        Task DeleteAsync(int id);
        Task<ArtistaForm> GetByNomeAsync(string nome);
    }
}
