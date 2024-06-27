using OrientoonApi.Models.Entities;

namespace OrientoonApi.Data.Repositories.Interfaces
{
    public interface IGeneroOrientoonRepository
    {
        Task AddAsync(string orientoonId, string generoId);
        Task DeleteAsync(string orientoonId, string generoId);
        Task<bool> ExistByGeneroIdAsync(string id, string nome);
    }
}
