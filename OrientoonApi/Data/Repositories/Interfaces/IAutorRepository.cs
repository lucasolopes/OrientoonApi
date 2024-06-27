using OrientoonApi.Models.Entities;

namespace OrientoonApi.Data.Repositories.Interfaces
{
    public interface IAutorRepository
    {

        Task<AutorModel> FindByIdAsync(string id);
        Task<AutorModel> FindByNomeAsync(string nome);
        Task<bool> ExistByNomeAsync(string nome);
        Task<bool> ExistByIdAsync(string id);

        Task AddAsync(AutorModel Autor);
        Task DeleteAsync(string id);
        Task UpdateAsync(AutorModel AutorModel);
        Task<List<AutorModel>> GetByAmountAsync(int batchSize, int pageNumber);
    }
}
