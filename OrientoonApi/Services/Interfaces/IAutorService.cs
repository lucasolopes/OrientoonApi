using OrientoonApi.Models.Entities;
using OrientoonApi.Models.Request;
using OrientoonApi.Models.Response;

namespace OrientoonApi.Services.Interfaces
{
    public interface IAutorService
    {
        Task<AutorForm> CreateAsync(AutorDto autorDto);
        Task<AutorForm> GetAsync(string id);
        Task<List<AutorForm>> GetListAsync(int batchSize, int pageNumber);
        Task CreateListAsync(List<AutorDto> autorDto);
        Task<AutorForm> UpdateAsync(string id, AutorDto autorDto);
        Task DeleteAsync(string id);
        Task<AutorModel> GetByNomeAsync(string nomeAutor);
        Task<AutorModel> GetByIdAsync(string autorId);
    }
}
