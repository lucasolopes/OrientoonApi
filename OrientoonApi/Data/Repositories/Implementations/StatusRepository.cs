using Microsoft.EntityFrameworkCore;
using OrientoonApi.Data.Contexts;
using OrientoonApi.Data.Repositories.Interfaces;
using OrientoonApi.Enum;
using OrientoonApi.Models.Entities;

namespace OrientoonApi.Data.Repositories.Implementations
{
    public class StatusRepository : IStatusRepository
    {
        private readonly OrientoonContext _context;
        public StatusRepository(OrientoonContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<StatusModel>> GetAllAsync()
        {
            return await _context.Status.ToListAsync();
        }

        public async Task<StatusModel> GetByIdAsync(string statusId)
        {
            return await _context.Status.FirstOrDefaultAsync(x => x.Id == statusId);
        }

        public async Task<bool> ExistsByIdAsync(string statusId)
        {
            return await _context.Status.AnyAsync(x => x.Id == statusId);
        }

        public async Task<StatusModel> GetByNameAsync(StatusEnum? status)
        {
            return await _context.Status.FirstOrDefaultAsync(x => x.nome == status);
        }

        public Task<bool> ExistsByNameAsync(StatusEnum? status)
        {
            return Task.FromResult(_context.Status.Any(x => x.nome == status));
        }
    }
}
