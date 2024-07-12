using Microsoft.EntityFrameworkCore;
using OrientoonApi.Data.Contexts;
using OrientoonApi.Data.Repositories.Interfaces;

namespace OrientoonApi.Data.Repositories.Implementations
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly OrientoonContext _context;
        protected readonly DbSet<T> _dbSet;

        public GenericRepository(OrientoonContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }


        public virtual async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public virtual async Task DeleteAsync(string id)
        {
             _dbSet.Remove(await _dbSet.FindAsync(id));
        }

        public virtual async Task<bool> ExistsByIdAsync(string id)
        {
            //vificar se existe um registro com o id passado

            return await _dbSet.AnyAsync(e => EF.Property<string>(e, "Id") == id);
        }

        public virtual async Task<bool> ExistsByNameAsync(string name)
        {
            var property = typeof(T).GetProperty("nome");
            if (property == null)
            {
                throw new Exception($"A entidade {typeof(T).Name} não possui uma propriedade 'nome'.");
            }
            
            return await _dbSet.AnyAsync(e => EF.Property<string>(e, "nome") == name);
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public virtual async Task<T> GetByIdAsync(string id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual async Task UpdateAsync(T entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public virtual Task<List<T>> GetByAmountAsync(int batchSize, int pageNumber)
        {
            return _dbSet.Skip((pageNumber - 1) * batchSize).Take(batchSize).ToListAsync();
        }

        public virtual Task<T> GetByNomeAsync(string nome)
        {
            return _dbSet.FirstOrDefaultAsync(e => EF.Property<string>(e, "nome") == nome);
        }
    }
}
