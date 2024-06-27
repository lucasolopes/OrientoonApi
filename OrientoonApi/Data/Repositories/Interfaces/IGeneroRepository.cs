using OrientoonApi.Models.Entities;
using OrientoonApi.Models.Request;

namespace OrientoonApi.Data.Repositories.Interfaces
{
    public interface IGeneroRepository
    {
        Task AddAsync(GeneroModel generoModel);
        Task DeleteAsync(string id);
        Task<bool> ExistByIdAsync(string id);
        Task<bool> ExistByNomeAsync(string nome);
        Task<GeneroModel> FindByIdAsync(string id);
        Task<GeneroModel> FindByNomeAsync(string nome);
        Task<List<GeneroModel>> GetByAmountAsync(int batchSize, int pageNumber);
        Task UpdateAsync(GeneroModel generoModel);
    }
}
