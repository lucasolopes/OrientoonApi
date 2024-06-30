namespace OrientoonApi.Data.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T : class

    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(string id);

        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(string id);
        Task<bool> ExistsByNameAsync(string name);
        Task<bool> ExistsByIdAsync(string id);
        Task<List<T>> GetByAmountAsync(int batchSize, int pageNumber);
        Task<T> GetByNomeAsync(string nome);
    }
}
