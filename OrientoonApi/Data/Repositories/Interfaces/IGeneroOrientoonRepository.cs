using OrientoonApi.Models.Entities;

namespace OrientoonApi.Data.Repositories.Interfaces
{
    public interface IGeneroOrientoonRepository
    {
        Task AddAsync(string orientoonId, int generoId);
        Task DeleteAsync(string orientoonId, int generoId);
        Task<bool> ExistByGeneroIdAsync(string id, string nome);
    }
}
