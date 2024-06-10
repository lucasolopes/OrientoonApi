namespace OrientoonApi.Data.Repositories.Interfaces
{
    public interface ITipoOrientoonRepository
    {
        Task AddAsync(string orientoonId, int tipoId);
        Task DeleteAsync(string orientoonId, int tipoId);
        Task<bool> ExistByTipoIdAsync(string id, string nome);
    }
}
