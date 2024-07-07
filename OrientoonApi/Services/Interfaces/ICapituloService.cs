using OrientoonApi.Models.Entities;
using OrientoonApi.Models.Response;

namespace OrientoonApi.Services.Interfaces
{
    public interface ICapituloService
    {
        Task<CapituloForm> AddCapituloAsync(string mangaId, double numCap, IList<IFormFile> files);
        Task<CapituloForm> GetCapituloByIdAsync(string id);
    }
}
