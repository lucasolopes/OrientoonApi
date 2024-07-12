    using OrientoonApi.Models.Entities;
using OrientoonApi.Models.Response;

namespace OrientoonApi.Data.Repositories.Interfaces
{
    public interface ICapituloRepository 
    {
        Task<CapituloForm> AddAsync(CapituloModel capitulo);
        Task<CapituloForm> GetByIdAsync(string id);
        Task<List<CapituloModel>> GetCapituloByOrientoonIdAsync(string orientoonId);
    }
}
