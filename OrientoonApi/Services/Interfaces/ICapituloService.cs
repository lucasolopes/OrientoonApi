using OrientoonApi.Models.Entities;

namespace OrientoonApi.Services.Interfaces
{
    public interface ICapituloService
    {
        Task<CapituloModel> AddCapituloAsync(string mangaId, double numCap, IList<IFormFile> files);
        Task<CapituloModel> GetCapituloByIdAsync(string id);
    }
}
