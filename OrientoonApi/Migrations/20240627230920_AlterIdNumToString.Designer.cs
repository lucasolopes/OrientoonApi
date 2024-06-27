﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OrientoonApi.Data.Contexts;

#nullable disable

namespace OrientoonApi.Migrations
{
    [DbContext(typeof(OrientoonContext))]
    [Migration("20240627230920_AlterIdNumToString")]
    partial class AlterIdNumToString
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.31")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("OrientoonApi.Models.Entities.ArtistaModel", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("NomeArtista")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Artista");
                });

            modelBuilder.Entity("OrientoonApi.Models.Entities.AutorModel", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("NomeAutor")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Autor");
                });

            modelBuilder.Entity("OrientoonApi.Models.Entities.CapituloModel", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<double>("AvaliacaoCap")
                        .HasColumnType("double");

                    b.Property<string>("Caminho")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<DateOnly>("DataLancamento")
                        .HasColumnType("date");

                    b.Property<string>("Link")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<int>("NumCapitulo")
                        .HasColumnType("int");

                    b.Property<string>("OrientoonId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("OrientoonId");

                    b.ToTable("Capitulo");
                });

            modelBuilder.Entity("OrientoonApi.Models.Entities.GeneroModel", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("NomeGenero")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Genero");
                });

            modelBuilder.Entity("OrientoonApi.Models.Entities.GeneroOrientoonModel", b =>
                {
                    b.Property<string>("IdOrientoon")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("IdGenero")
                        .HasColumnType("varchar(255)");

                    b.HasKey("IdOrientoon", "IdGenero");

                    b.HasIndex("IdGenero");

                    b.ToTable("GeneroOrientoon");
                });

            modelBuilder.Entity("OrientoonApi.Models.Entities.OrientoonModel", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<bool>("AdultContent")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("ArtistaId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("AutorId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<double>("Avaliacao")
                        .HasColumnType("double");

                    b.Property<string>("CBanner")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)");

                    b.Property<DateTime?>("DataLancamento")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)");

                    b.Property<string>("NormalizedTitulo")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("StatusId")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("ArtistaId");

                    b.HasIndex("AutorId");

                    b.HasIndex("StatusId");

                    b.ToTable("Orientoon");
                });

            modelBuilder.Entity("OrientoonApi.Models.Entities.StatusModel", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Status");
                });

            modelBuilder.Entity("OrientoonApi.Models.Entities.TipoModel", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("NomeTipo")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Tipo");
                });

            modelBuilder.Entity("OrientoonApi.Models.Entities.TipoOrientoonModel", b =>
                {
                    b.Property<string>("IdOrientoon")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("IdTipo")
                        .HasColumnType("varchar(255)");

                    b.HasKey("IdOrientoon", "IdTipo");

                    b.HasIndex("IdTipo");

                    b.ToTable("TipoOrientoon");
                });

            modelBuilder.Entity("OrientoonApi.Models.Entities.CapituloModel", b =>
                {
                    b.HasOne("OrientoonApi.Models.Entities.OrientoonModel", "Orientoon")
                        .WithMany("Capitulos")
                        .HasForeignKey("OrientoonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Orientoon");
                });

            modelBuilder.Entity("OrientoonApi.Models.Entities.GeneroOrientoonModel", b =>
                {
                    b.HasOne("OrientoonApi.Models.Entities.GeneroModel", "Genero")
                        .WithMany("GeneroOrientoons")
                        .HasForeignKey("IdGenero")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OrientoonApi.Models.Entities.OrientoonModel", "Orientoon")
                        .WithMany("GeneroOrientoons")
                        .HasForeignKey("IdOrientoon")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Genero");

                    b.Navigation("Orientoon");
                });

            modelBuilder.Entity("OrientoonApi.Models.Entities.OrientoonModel", b =>
                {
                    b.HasOne("OrientoonApi.Models.Entities.ArtistaModel", "Artista")
                        .WithMany("Orientoons")
                        .HasForeignKey("ArtistaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OrientoonApi.Models.Entities.AutorModel", "Autor")
                        .WithMany("Orientoons")
                        .HasForeignKey("AutorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OrientoonApi.Models.Entities.StatusModel", "Status")
                        .WithMany("Orientoons")
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Artista");

                    b.Navigation("Autor");

                    b.Navigation("Status");
                });

            modelBuilder.Entity("OrientoonApi.Models.Entities.TipoOrientoonModel", b =>
                {
                    b.HasOne("OrientoonApi.Models.Entities.OrientoonModel", "Orientoon")
                        .WithMany("TipoOrientoon")
                        .HasForeignKey("IdOrientoon")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OrientoonApi.Models.Entities.TipoModel", "Tipo")
                        .WithMany("TipoOrientoon")
                        .HasForeignKey("IdTipo")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Orientoon");

                    b.Navigation("Tipo");
                });

            modelBuilder.Entity("OrientoonApi.Models.Entities.ArtistaModel", b =>
                {
                    b.Navigation("Orientoons");
                });

            modelBuilder.Entity("OrientoonApi.Models.Entities.AutorModel", b =>
                {
                    b.Navigation("Orientoons");
                });

            modelBuilder.Entity("OrientoonApi.Models.Entities.GeneroModel", b =>
                {
                    b.Navigation("GeneroOrientoons");
                });

            modelBuilder.Entity("OrientoonApi.Models.Entities.OrientoonModel", b =>
                {
                    b.Navigation("Capitulos");

                    b.Navigation("GeneroOrientoons");

                    b.Navigation("TipoOrientoon");
                });

            modelBuilder.Entity("OrientoonApi.Models.Entities.StatusModel", b =>
                {
                    b.Navigation("Orientoons");
                });

            modelBuilder.Entity("OrientoonApi.Models.Entities.TipoModel", b =>
                {
                    b.Navigation("TipoOrientoon");
                });
#pragma warning restore 612, 618
        }
    }
}
