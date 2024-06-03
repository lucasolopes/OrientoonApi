using OrientoonApi.Models.Entities;

namespace OrientoonApi.Data.Repositories.Interfaces
{
    public interface IArtistaRepository
    {
        Task<ArtistaModel> FindByIdAsync(int id);
        Task<ArtistaModel> FindByNomeAsync(string nome);
    }
}
