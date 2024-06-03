using OrientoonApi.Models.Entities;

namespace OrientoonApi.Data.Repositories.Interfaces
{
    public interface IAutorRepository
    {
        Task<AutorModel> FindByIdAsync(int id);
        Task<AutorModel> FindByNomeAsync(string nome);
    }
}
