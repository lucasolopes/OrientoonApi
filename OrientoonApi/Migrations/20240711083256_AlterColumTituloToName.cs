using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrientoonApi.Migrations
{
    public partial class AlterColumTituloToName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NormalizedTitulo",
                table: "Orientoon",
                newName: "NormalizedName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NormalizedName",
                table: "Orientoon",
                newName: "NormalizedTitulo");
        }
    }
}
