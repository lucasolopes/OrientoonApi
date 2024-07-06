using OrientoonApi.Models.Entities;

namespace OrientoonApi.Data.Repositories.Interfaces
{
    public interface ICapituloRepository 
    {
        Task<CapituloModel> AddCapituloAsync(CapituloModel capitulo);
        Task<CapituloModel> GetCapituloByIdAsync(string id);

    }
}
