using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrientoonApi.Migrations
{
    public partial class StandardizeColumnNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NomeTipo",
                table: "Tipo",
                newName: "nome");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Status",
                newName: "nome");

            migrationBuilder.RenameColumn(
                name: "Titulo",
                table: "Orientoon",
                newName: "nome");

            migrationBuilder.RenameColumn(
                name: "NomeGenero",
                table: "Genero",
                newName: "nome");

            migrationBuilder.RenameColumn(
                name: "NomeAutor",
                table: "Autor",
                newName: "nome");

            migrationBuilder.RenameColumn(
                name: "NomeArtista",
                table: "Artista",
                newName: "nome");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "nome",
                table: "Tipo",
                newName: "NomeTipo");

            migrationBuilder.RenameColumn(
                name: "nome",
                table: "Status",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "nome",
                table: "Orientoon",
                newName: "Titulo");

            migrationBuilder.RenameColumn(
                name: "nome",
                table: "Genero",
                newName: "NomeGenero");

            migrationBuilder.RenameColumn(
                name: "nome",
                table: "Autor",
                newName: "NomeAutor");

            migrationBuilder.RenameColumn(
                name: "nome",
                table: "Artista",
                newName: "NomeArtista");
        }
    }
}
