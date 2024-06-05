using OrientoonApi.Models.Request;
using OrientoonApi.Models.Response;

namespace OrientoonApi.Services.Interfaces
{
    public interface IAutorService
    {
        Task<AutorForm> CreateAsync(AutorDto autorDto);
        Task<AutorForm> GetAsync(int id);
        Task<List<AutorForm>> GetListAsync(int batchSize, int pageNumber);
        Task CreateListAsync(List<AutorDto> autorDto);
        Task<AutorForm> UpdateAsync(int id, AutorDto autorDto);
        Task DeleteAsync(int id);
        Task<AutorForm> GetByNomeAsync(string nome);
    }
}
