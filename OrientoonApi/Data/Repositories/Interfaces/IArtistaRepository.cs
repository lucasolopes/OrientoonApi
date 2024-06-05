using OrientoonApi.Models.Entities;

namespace OrientoonApi.Data.Repositories.Interfaces
{
    public interface IArtistaRepository
    {
        Task<ArtistaModel> FindByIdAsync(int id);
        Task<ArtistaModel> FindByNomeAsync(string nome);
        Task AddAsync(ArtistaModel artista);
        Task DeleteAsync(int id);
        Task UpdateAsync(ArtistaModel artistaModel);
        Task<bool> ExistByNomeAsync(string nome);
        Task<bool> ExistByIdAsync(int id);
        Task<List<ArtistaModel>> GetByAmountAsync(int batchSize, int pageNumber);
    }
}
