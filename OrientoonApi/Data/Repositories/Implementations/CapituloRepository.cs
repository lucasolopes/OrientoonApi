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

        public Task<CapituloModel> AddCapituloAsync(CapituloModel capitulo)
        {
            throw new NotImplementedException();
        }

        public Task<CapituloModel> GetCapituloByIdAsync(string id)
        {
            throw new NotImplementedException();
        }
    }
}
