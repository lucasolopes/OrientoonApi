using OrientoonApi.Data.Contexts;
using OrientoonApi.Data.Repositories.Interfaces;

namespace OrientoonApi.Data.Repositories.Implementations
{
    public class ContextRepository : IContextRepository
    {
        private readonly OrientoonContext _context;

        public ContextRepository(OrientoonContext context)
        {
            _context = context;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
