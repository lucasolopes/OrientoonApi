using Microsoft.EntityFrameworkCore;
using OrientoonApi.Data.Contexts;
using OrientoonApi.Data.Repositories.Interfaces;
using OrientoonApi.Models.Entities;

namespace OrientoonApi.Data.Repositories.Implementations
{
    public class AutorRepository : GenericRepository<AutorModel>, IAutorRepository
    {
        public AutorRepository(OrientoonContext context) : base(context)
        {
        }
    }
}
