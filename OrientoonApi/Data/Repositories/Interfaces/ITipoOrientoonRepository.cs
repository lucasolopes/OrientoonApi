namespace OrientoonApi.Data.Repositories.Interfaces
{
    public interface ITipoOrientoonRepository
    {
        Task AddAsync(string orientoonId, string tipoId);
        Task DeleteAsync(string orientoonId, string tipoId);
        Task<bool> ExistByTipoIdAsync(string id, string nome);
    }
}
