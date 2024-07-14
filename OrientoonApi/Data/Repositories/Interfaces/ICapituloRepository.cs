    using OrientoonApi.Models.Entities;
using OrientoonApi.Models.Response;

namespace OrientoonApi.Data.Repositories.Interfaces
{
    public interface ICapituloRepository 
    {
        Task<CapituloModel> AddAsync(CapituloModel capitulo);
        Task<CapituloModel> GetByIdAsync(string id);
        Task<List<CapituloModel>> GetCapituloByOrientoonIdAsync(string orientoonId);
    }
}
