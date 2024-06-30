using Microsoft.EntityFrameworkCore;
using OrientoonApi.Data.Contexts;
using OrientoonApi.Data.Repositories.Interfaces;
using OrientoonApi.Models.Entities;

namespace OrientoonApi.Data.Repositories.Implementations
{
    public class ArtistaRepository : GenericRepository<ArtistaModel>, IArtistaRepository
    {
        public ArtistaRepository(OrientoonContext context) : base(context)
        {
        }
    }
}
