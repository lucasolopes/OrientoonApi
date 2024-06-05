using Microsoft.EntityFrameworkCore;
using OrientoonApi.Data.Contexts;
using OrientoonApi.Data.Repositories.Interfaces;
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

        public async Task<StatusModel> FindByIdAsync(int statusId)
        {
            return await _context.Status.FindAsync(statusId);
        }

        public async Task<StatusModel> FindByStatusAsync(string status)
        {
            return await _context.Status.FirstOrDefaultAsync(s => s.Status == status);
        }

        public async Task<bool> ExistByIdAsync(int statusId)
        {
            return await _context.Status.AnyAsync(s => s.Id == statusId);
        }
        public async Task<bool> ExistByStatusAsync(string status)
        {
            return await _context.Status.AnyAsync(s => s.Status == status);
        }

        public async Task AddAsync(StatusModel statusModel)
        {
            await _context.Status.AddAsync(statusModel);
        }

        public async Task<List<StatusModel>> GetByAmountAsync(int batchSize, int pageNumber)
        {
            return await _context.Status.Skip((pageNumber - 1) * batchSize).Take(batchSize).ToListAsync();
        }

        public async Task UpdateAsync(StatusModel statusModel)
        {
             _context.Status.Update(statusModel);
        }

        public async Task DeleteAsync(int id)
        {
            _context.Status.Remove(await FindByIdAsync(id));
        }
    }
}
