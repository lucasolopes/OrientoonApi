using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrientoonApi.Migrations
{
    public partial class RemoveForeignKeyConstraint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
           name: "FK_GeneroOrientoon_Genero_IdGenero",
           table: "GeneroOrientoon");

            migrationBuilder.DropIndex(
                name: "IX_GeneroOrientoon_IdGenero",
                table: "GeneroOrientoon");

            migrationBuilder.DropForeignKey(
          name: "FK_TipoOrientoon_Tipo_IdTipo",
          table: "TipoOrientoon");

            migrationBuilder.DropIndex(
                name: "IX_TipoOrientoon_IdTipo",
                table: "TipoOrientoon");

            migrationBuilder.DropForeignKey(
         name: "FK_Orientoon_Status_StatusId",
         table: "orientoon");

            migrationBuilder.DropIndex(
                name: "IX_Orientoon_StatusId",
                table: "orientoon");






            migrationBuilder.DropForeignKey(
         name: "FK_Orientoon_Artista_ArtistaId",
         table: "orientoon");

            migrationBuilder.DropIndex(
                name: "IX_Orientoon_ArtistaId",
                table: "orientoon");


            migrationBuilder.DropForeignKey(
         name: "FK_Orientoon_Autor_AutorId",
         table: "orientoon");

            migrationBuilder.DropIndex(
                name: "IX_Orientoon_AutorId",
                table: "orientoon");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Recrie a constraint de chave estrangeira e o índice correspondente no Down
            migrationBuilder.CreateIndex(
                name: "IX_GeneroOrientoon_IdGenero",
                table: "GeneroOrientoon",
                column: "IdGenero");

            migrationBuilder.AddForeignKey(
                name: "FK_GeneroOrientoon_Genero_IdGenero",
                table: "GeneroOrientoon",
                column: "IdGenero",
                principalTable: "Genero",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.CreateIndex(
                name: "IX_TipoOrientoon_IdTipo",
                table: "TipoOrientoon",
                column: "IdTipo");

            migrationBuilder.AddForeignKey(
                name: "FK_TipoOrientoon_Tipo_IdTipo",
                table: "TipoOrientoon",
                column: "IdTipo",
                principalTable: "Tipo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.CreateIndex(
               name: "IX_Orientoon_StatusId",
               table: "orientoon",
               column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orientoon_Status_StatusId",
                table: "orientoon",
                column: "StatusId",
                principalTable: "status",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);


            migrationBuilder.CreateIndex(
               name: "IX_Orientoon_ArtistaId",
               table: "orientoon",
               column: "ArtistaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orientoon_Artista_ArtistaId",
                table: "orientoon",
                column: "ArtistaId",
                principalTable: "artista",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.CreateIndex(
               name: "IX_Orientoon_AutorId",
               table: "orientoon",
               column: "AutorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orientoon_Autor_AutorId",
                table: "orientoon",
                column: "AutorId",
                principalTable: "autor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

        }
    }
}
