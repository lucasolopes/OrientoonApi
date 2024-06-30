using Microsoft.EntityFrameworkCore;
using OrientoonApi.Data.Contexts;
using OrientoonApi.Data.Repositories.Interfaces;
using OrientoonApi.Models.Entities;

namespace OrientoonApi.Data.Repositories.Implementations
{
    public class TipoRepository : GenericRepository<TipoModel>, ITipoRepository
    {
        public TipoRepository(OrientoonContext context) : base(context)
        {
        }
    }
}
