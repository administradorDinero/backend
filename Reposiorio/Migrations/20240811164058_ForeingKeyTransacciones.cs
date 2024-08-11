using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Repositorio.Migrations
{
    /// <inheritdoc />
    public partial class ForeingKeyTransacciones : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransaccionContext_CuentaContext_id",
                table: "TransaccionContext");

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "TransaccionContext",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "CuentaId",
                table: "TransaccionContext",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TransaccionContext_CuentaId",
                table: "TransaccionContext",
                column: "CuentaId");

            migrationBuilder.AddForeignKey(
                name: "FK_TransaccionContext_CuentaContext_CuentaId",
                table: "TransaccionContext",
                column: "CuentaId",
                principalTable: "CuentaContext",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransaccionContext_CuentaContext_CuentaId",
                table: "TransaccionContext");

            migrationBuilder.DropIndex(
                name: "IX_TransaccionContext_CuentaId",
                table: "TransaccionContext");

            migrationBuilder.DropColumn(
                name: "CuentaId",
                table: "TransaccionContext");

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "TransaccionContext",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddForeignKey(
                name: "FK_TransaccionContext_CuentaContext_id",
                table: "TransaccionContext",
                column: "id",
                principalTable: "CuentaContext",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
