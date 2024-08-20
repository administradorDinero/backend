using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repositorio.Migrations
{
    /// <inheritdoc />
    public partial class migrationnotfound7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransaccionContext_CuentaContext_cuentaid",
                table: "TransaccionContext");

            migrationBuilder.RenameColumn(
                name: "cuentaid",
                table: "TransaccionContext",
                newName: "cuentaId");

            migrationBuilder.RenameIndex(
                name: "IX_TransaccionContext_cuentaid",
                table: "TransaccionContext",
                newName: "IX_TransaccionContext_cuentaId");

            migrationBuilder.AddForeignKey(
                name: "FK_TransaccionContext_CuentaContext_cuentaId",
                table: "TransaccionContext",
                column: "cuentaId",
                principalTable: "CuentaContext",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransaccionContext_CuentaContext_cuentaId",
                table: "TransaccionContext");

            migrationBuilder.RenameColumn(
                name: "cuentaId",
                table: "TransaccionContext",
                newName: "cuentaid");

            migrationBuilder.RenameIndex(
                name: "IX_TransaccionContext_cuentaId",
                table: "TransaccionContext",
                newName: "IX_TransaccionContext_cuentaid");

            migrationBuilder.AddForeignKey(
                name: "FK_TransaccionContext_CuentaContext_cuentaid",
                table: "TransaccionContext",
                column: "cuentaid",
                principalTable: "CuentaContext",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
