    using OrientoonApi.Models.Entities;

namespace OrientoonApi.Data.Repositories.Interfaces
{
    public interface ICapituloRepository 
    {
        Task<CapituloModel> AddAsync(CapituloModel capitulo);
        Task<CapituloModel> GetByIdAsync(string id);

    }
}
