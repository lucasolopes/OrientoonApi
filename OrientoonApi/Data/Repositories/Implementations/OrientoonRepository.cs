using Microsoft.EntityFrameworkCore;
using OrientoonApi.Data.Contexts;
using OrientoonApi.Data.Repositories.Interfaces;
using OrientoonApi.Models.Entities;

namespace OrientoonApi.Data.Repositories.Implementations
{
    public class OrientoonRepository : IOrientoonRepository
    {
        private readonly OrientoonContext _context;

        public OrientoonRepository(OrientoonContext context)
        {
            _context = context;
        }

        public async Task AddAsync(OrientoonModel orientoonModel)
        {
            await _context.Orientoons.AddAsync(orientoonModel);
        }



        public async Task<OrientoonModel> FindByIdAsync(string id)
        {
            return await _context.Orientoons.FindAsync(id);
        }

        public async Task<List<OrientoonModel>> GetByAmountAsync(int batchSize, int pageNumber)
        {
            //get orientoons quantity
            return await _context.Orientoons.Skip((pageNumber - 1) * batchSize).Take(batchSize).ToListAsync();
        }

        //Put by id
        public async Task UpdateAsync(OrientoonModel orientoonModel)
        {
            _context.Orientoons.Update(orientoonModel);
        }

        public async Task DeleteAsync(string id)
        {
            _context.Orientoons.Remove(await FindByIdAsync(id));
        }
    }
}
