using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Repositorio.Migrations
{
    /// <inheritdoc />
    public partial class AddEstadoTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EstadoId",
                table: "CategoriaContext",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "EstadoContext",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstadoContext", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoriaContext_EstadoId",
                table: "CategoriaContext",
                column: "EstadoId");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoriaContext_EstadoContext_EstadoId",
                table: "CategoriaContext",
                column: "EstadoId",
                principalTable: "EstadoContext",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoriaContext_EstadoContext_EstadoId",
                table: "CategoriaContext");

            migrationBuilder.DropTable(
                name: "EstadoContext");

            migrationBuilder.DropIndex(
                name: "IX_CategoriaContext_EstadoId",
                table: "CategoriaContext");

            migrationBuilder.DropColumn(
                name: "EstadoId",
                table: "CategoriaContext");
        }
    }
}
