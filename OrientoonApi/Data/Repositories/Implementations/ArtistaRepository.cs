using Microsoft.EntityFrameworkCore;
using OrientoonApi.Data.Contexts;
using OrientoonApi.Data.Repositories.Interfaces;
using OrientoonApi.Models.Entities;

namespace OrientoonApi.Data.Repositories.Implementations
{
    public class ArtistaRepository : IArtistaRepository
    {
        private readonly OrientoonContext _context;

        public ArtistaRepository(OrientoonContext context)
        {
            _context = context;
        }

        public async Task<ArtistaModel> FindByIdAsync(int id)
        {
            return await _context.Artista.FindAsync(id);
        }

        public async Task<ArtistaModel> FindByNomeAsync(string nome)
        {
            return await _context.Artista.FirstOrDefaultAsync(x => x.NomeArtista == nome);
        }

    }
}
