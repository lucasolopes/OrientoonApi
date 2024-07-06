using Microsoft.EntityFrameworkCore;
using OrientoonApi.Data.Contexts;
using OrientoonApi.Data.Repositories.Interfaces;
using OrientoonApi.Models.Entities;

namespace OrientoonApi.Data.Repositories.Implementations
{
    public class CapituloRepository : ICapituloRepository
    {
        private readonly OrientoonContext _context;

        public CapituloRepository(OrientoonContext context)
        {
            _context = context;
        }

        public async Task<CapituloModel> AddCapituloAsync(CapituloModel capitulo)
        {
            _context.Capitulo.Add(capitulo);
            await _context.SaveChangesAsync();
            return capitulo;
        }

        public async Task<CapituloModel> GetCapituloByIdAsync(string id)
        {
            return await _context.Capitulo.FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
