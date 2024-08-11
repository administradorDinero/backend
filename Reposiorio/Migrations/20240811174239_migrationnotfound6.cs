using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repositorio.Migrations
{
    /// <inheritdoc />
    public partial class migrationnotfound6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransaccionContext_CuentaContext_CuentaId",
                table: "TransaccionContext");

            migrationBuilder.RenameColumn(
                name: "CuentaId",
                table: "TransaccionContext",
                newName: "cuentaid");

            migrationBuilder.RenameIndex(
                name: "IX_TransaccionContext_CuentaId",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransaccionContext_CuentaContext_cuentaid",
                table: "TransaccionContext");

            migrationBuilder.RenameColumn(
                name: "cuentaid",
                table: "TransaccionContext",
                newName: "CuentaId");

            migrationBuilder.RenameIndex(
                name: "IX_TransaccionContext_cuentaid",
                table: "TransaccionContext",
                newName: "IX_TransaccionContext_CuentaId");

            migrationBuilder.AddForeignKey(
                name: "FK_TransaccionContext_CuentaContext_CuentaId",
                table: "TransaccionContext",
                column: "CuentaId",
                principalTable: "CuentaContext",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
