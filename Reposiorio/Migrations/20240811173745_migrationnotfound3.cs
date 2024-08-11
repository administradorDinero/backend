using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repositorio.Migrations
{
    /// <inheritdoc />
    public partial class migrationnotfound3 : Migration
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
                newName: "cuenta_id");

            migrationBuilder.RenameIndex(
                name: "IX_TransaccionContext_CuentaId",
                table: "TransaccionContext",
                newName: "IX_TransaccionContext_cuenta_id");

            migrationBuilder.AddForeignKey(
                name: "FK_TransaccionContext_CuentaContext_cuenta_id",
                table: "TransaccionContext",
                column: "cuenta_id",
                principalTable: "CuentaContext",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransaccionContext_CuentaContext_cuenta_id",
                table: "TransaccionContext");

            migrationBuilder.RenameColumn(
                name: "cuenta_id",
                table: "TransaccionContext",
                newName: "CuentaId");

            migrationBuilder.RenameIndex(
                name: "IX_TransaccionContext_cuenta_id",
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
