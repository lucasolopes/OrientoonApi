using Microsoft.EntityFrameworkCore;
using OrientoonApi.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrientoonApi.Models.Entities;
using OrientoonApi.Data.Repositories.Interfaces;
using OrientoonApi.Services.Interfaces;
using OrientoonApi.Data.Repositories.Implementations;
using OrientoonApi.Services.Implementations;
namespace TestOrientoon.Helpers.Fixtures
{
    public class DataBaseFixture : IDisposable
    {
        public OrientoonContext Context { get; private set; }
        public IArtistaRepository ArtistaRepository { get; private set; }
        public IArtistaService ArtistaService { get; private set; }
        public IContextRepository ContextRepository { get; private set; }

        public DataBaseFixture()
        {
            var options = new DbContextOptionsBuilder<OrientoonContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            
            Context  = new OrientoonContext(options);
            ArtistaRepository = new ArtistaRepository(Context);
            ContextRepository =new ContextRepository(Context);
            ArtistaService = new ArtistaService(ArtistaRepository, ContextRepository);

            SeedDatabase();

        }

        private void SeedDatabase()
        {
            Context.Artista.AddRange(
                new ArtistaModel { Id = "1", nome = "Oda" },
                new ArtistaModel { Id = "2", nome = "Echiro" },
                new ArtistaModel { Id = "3", nome = "Echiro Oda" }
            );
            Context.Autor.AddRange(
                new AutorModel { Id = "1", nome = "Oda" },
                new AutorModel { Id = "2", nome = "Echiro" },
                new AutorModel { Id = "3", nome = "Echiro Oda" }
            );
            Context.Genero.AddRange(
                new GeneroModel { Id = "1", nome = "Acao" },
                new GeneroModel { Id = "2", nome = "Aventura" },
                new GeneroModel { Id = "3", nome = "Drama" }
            );
            Context.Tipo.AddRange(
                new TipoModel { Id = "1", nome = "Manga" },
                new TipoModel { Id = "2", nome = "Anime" },
                new TipoModel { Id = "3", nome = "Filme" }
            );
            Context.GeneroOrientoons.AddRange(
                new GeneroOrientoonModel { IdGenero = "1", IdOrientoon = "1" },
                new GeneroOrientoonModel { IdGenero = "2", IdOrientoon = "2" },
                new GeneroOrientoonModel { IdGenero = "3", IdOrientoon = "3" }
             );
            Context.TipoOrientoons.AddRange(
                new TipoOrientoonModel { IdTipo = "1", IdOrientoon = "1" },
                new TipoOrientoonModel { IdTipo = "2", IdOrientoon = "2" },
                new TipoOrientoonModel { IdTipo = "3", IdOrientoon = "3" }
            );
            Context.SaveChanges();

        }

        public void Dispose()
        {
            Context.Database.EnsureDeleted();
            Context.Dispose();
        }
    }
}
