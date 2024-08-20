using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repositorio.Migrations
{
    /// <inheritdoc />
    public partial class AddCategoriasTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PersonaId",
                table: "CategoriaContext",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CategoriaContext_PersonaId",
                table: "CategoriaContext",
                column: "PersonaId");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoriaContext_PersonaContext_PersonaId",
                table: "CategoriaContext",
                column: "PersonaId",
                principalTable: "PersonaContext",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoriaContext_PersonaContext_PersonaId",
                table: "CategoriaContext");

            migrationBuilder.DropIndex(
                name: "IX_CategoriaContext_PersonaId",
                table: "CategoriaContext");

            migrationBuilder.DropColumn(
                name: "PersonaId",
                table: "CategoriaContext");
        }
    }
}
