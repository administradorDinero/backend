using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repositorio.Migrations
{
    /// <inheritdoc />
    public partial class nameIdTransaccion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoriaContext_EstadoContext_EstadoId",
                table: "CategoriaContext");

            migrationBuilder.DropForeignKey(
                name: "FK_CategoriaContext_PersonaContext_PersonaId",
                table: "CategoriaContext");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "TransaccionContext",
                newName: "Id");

            migrationBuilder.AlterColumn<int>(
                name: "PersonaId",
                table: "CategoriaContext",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "EstadoId",
                table: "CategoriaContext",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoriaContext_EstadoContext_EstadoId",
                table: "CategoriaContext",
                column: "EstadoId",
                principalTable: "EstadoContext",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoriaContext_PersonaContext_PersonaId",
                table: "CategoriaContext",
                column: "PersonaId",
                principalTable: "PersonaContext",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoriaContext_EstadoContext_EstadoId",
                table: "CategoriaContext");

            migrationBuilder.DropForeignKey(
                name: "FK_CategoriaContext_PersonaContext_PersonaId",
                table: "CategoriaContext");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "TransaccionContext",
                newName: "id");

            migrationBuilder.AlterColumn<int>(
                name: "PersonaId",
                table: "CategoriaContext",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EstadoId",
                table: "CategoriaContext",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CategoriaContext_EstadoContext_EstadoId",
                table: "CategoriaContext",
                column: "EstadoId",
                principalTable: "EstadoContext",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CategoriaContext_PersonaContext_PersonaId",
                table: "CategoriaContext",
                column: "PersonaId",
                principalTable: "PersonaContext",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
