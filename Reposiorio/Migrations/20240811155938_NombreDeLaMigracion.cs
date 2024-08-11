using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Repositorio.Migrations
{
    /// <inheritdoc />
    public partial class NombreDeLaMigracion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CategoriaContext",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CategoriaNo = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoriaContext", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PersonaContext",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "text", nullable: false),
                    clave = table.Column<string>(type: "text", nullable: true),
                    correo = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonaContext", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoContext",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoContext", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CuentaContext",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    valor = table.Column<double>(type: "double precision", nullable: true),
                    descripcion = table.Column<string>(type: "text", nullable: true),
                    personaId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CuentaContext", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CuentaContext_PersonaContext_personaId",
                        column: x => x.personaId,
                        principalTable: "PersonaContext",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TransaccionContext",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false),
                    cantidad = table.Column<double>(type: "double precision", nullable: false),
                    fecha = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    descripcion = table.Column<string>(type: "text", nullable: true),
                    categoriaId = table.Column<int>(type: "integer", nullable: true),
                    tipoId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransaccionContext", x => x.id);
                    table.ForeignKey(
                        name: "FK_TransaccionContext_CategoriaContext_categoriaId",
                        column: x => x.categoriaId,
                        principalTable: "CategoriaContext",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TransaccionContext_CuentaContext_id",
                        column: x => x.id,
                        principalTable: "CuentaContext",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TransaccionContext_TipoContext_tipoId",
                        column: x => x.tipoId,
                        principalTable: "TipoContext",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CuentaContext_personaId",
                table: "CuentaContext",
                column: "personaId");

            migrationBuilder.CreateIndex(
                name: "IX_TransaccionContext_categoriaId",
                table: "TransaccionContext",
                column: "categoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_TransaccionContext_tipoId",
                table: "TransaccionContext",
                column: "tipoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TransaccionContext");

            migrationBuilder.DropTable(
                name: "CategoriaContext");

            migrationBuilder.DropTable(
                name: "CuentaContext");

            migrationBuilder.DropTable(
                name: "TipoContext");

            migrationBuilder.DropTable(
                name: "PersonaContext");
        }
    }
}
