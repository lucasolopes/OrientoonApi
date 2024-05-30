using Microsoft.EntityFrameworkCore;
using OrientoonApi.Models;
using System.Reflection;

namespace OrientoonApi.Data
{
    public class OrientoonContext : DbContext
    {
        public OrientoonContext(DbContextOptions<OrientoonContext> options) : base(options)
        {
        }

        public DbSet<OrientoonModel> Orientoons { get; set; }
        public DbSet<GeneroOrientoonModel> GeneroOrientoons { get; set; }
        public DbSet<ArtistaModel> Artista { get; set; }
        public DbSet<AutorModel> Autor { get; set; }
        public DbSet<TipoOrientoonModel> TipoOrientoons { get; set; }
        public DbSet<CapituloModel> Capitulo { get; set; }
        public DbSet<GeneroModel> Genero { get; set; }
        public DbSet<TipoModel> Tipo { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.Entity<GeneroOrientoonModel>()
                .HasKey(us => new { us.IdOrientoon, us.IdGenero });

            modelBuilder.Entity<GeneroOrientoonModel>()
                .HasOne(us => us.Orientoon)
                .WithMany(u => u.GeneroOrientoons)
                .HasForeignKey(us => us.IdOrientoon);

            modelBuilder.Entity<GeneroOrientoonModel>()
                .HasOne(us => us.Genero)
                .WithMany(s => s.GeneroOrientoons)
                .HasForeignKey(us => us.IdGenero);


            modelBuilder.Entity<TipoOrientoonModel>()
                .HasKey(us => new { us.IdOrientoon, us.IdTipo });

            modelBuilder.Entity<TipoOrientoonModel>()
                .HasOne(us => us.Orientoon)
                .WithMany(u => u.TipoOrientoon)
                .HasForeignKey(us => us.IdOrientoon);

            modelBuilder.Entity<TipoOrientoonModel>()
                .HasOne(us => us.Tipo)
                .WithMany(s => s.TipoOrientoon)
                .HasForeignKey(us => us.IdTipo);

        }
    }
}
