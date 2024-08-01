using OrientoonApi.Models.Entities;
using OrientoonApi.Models.Request;
using OrientoonApi.Models.Response;

namespace OrientoonApi.Services.Interfaces
{
    public interface ICapituloService
    {
        Task<CapituloForm> AddCapituloAsync(string mangaId, CapituloDto capituloDto);
        Task DeleteAsync(string id);
        Task<CapituloForm> GetCapituloByIdAsync(string id);
        Task<List<CapituloInfoForm>> GetCapituloFormsByOrientoonIdAsync(string orientoonId);
    }
}
