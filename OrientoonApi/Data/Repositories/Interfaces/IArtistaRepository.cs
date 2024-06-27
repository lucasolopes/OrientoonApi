using OrientoonApi.Models.Entities;

namespace OrientoonApi.Data.Repositories.Interfaces
{
    public interface IArtistaRepository
    {
        Task<ArtistaModel> FindByIdAsync(string id);
        Task<ArtistaModel> FindByNomeAsync(string nome);
        Task AddAsync(ArtistaModel artista);
        Task DeleteAsync(string id);
        Task UpdateAsync(ArtistaModel artistaModel);
        Task<bool> ExistByNomeAsync(string nome);
        Task<bool> ExistByIdAsync(string id);
        Task<List<ArtistaModel>> GetByAmountAsync(int batchSize, int pageNumber);
    }
}
