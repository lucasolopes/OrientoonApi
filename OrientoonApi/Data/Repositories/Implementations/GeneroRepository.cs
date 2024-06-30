using Microsoft.EntityFrameworkCore;
using OrientoonApi.Data.Contexts;
using OrientoonApi.Data.Repositories.Interfaces;
using OrientoonApi.Models.Entities;

namespace OrientoonApi.Data.Repositories.Implementations
{
    public class GeneroRepository : GenericRepository<GeneroModel>, IGeneroRepository
    {
        public GeneroRepository(OrientoonContext context) : base(context)
        {
        }
    }
}
