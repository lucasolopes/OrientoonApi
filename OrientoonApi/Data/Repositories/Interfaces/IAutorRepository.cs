using OrientoonApi.Models.Entities;

namespace OrientoonApi.Data.Repositories.Interfaces
{
    public interface IAutorRepository
    {

        Task<AutorModel> FindByIdAsync(int id);
        Task<AutorModel> FindByNomeAsync(string nome);
        Task<bool> ExistByNomeAsync(string nome);
        Task<bool> ExistByIdAsync(int id);

        Task AddAsync(AutorModel Autor);
        Task DeleteAsync(int id);
        Task UpdateAsync(AutorModel AutorModel);
        Task<List<AutorModel>> GetByAmountAsync(int batchSize, int pageNumber);
    }
}
