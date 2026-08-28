using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Paqueteria.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class GuiasTrasnbordo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "guia_transbordos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    GuiaId = table.Column<Guid>(type: "uuid", nullable: false),
                    AsignacionId = table.Column<Guid>(type: "uuid", nullable: false),
                    SucursalTransbordoId = table.Column<Guid>(type: "uuid", nullable: false),
                    UsuarioRegistroId = table.Column<Guid>(type: "uuid", nullable: false),
                    FechaEscaneoIngreso = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FechaEscaneoSalida = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Observaciones = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_guia_transbordos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_guia_transbordos_asignaciones_AsignacionId",
                        column: x => x.AsignacionId,
                        principalTable: "asignaciones",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_guia_transbordos_guias_GuiaId",
                        column: x => x.GuiaId,
                        principalTable: "guias",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_guia_transbordos_sucursales_SucursalTransbordoId",
                        column: x => x.SucursalTransbordoId,
                        principalTable: "sucursales",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_guia_transbordos_AsignacionId",
                table: "guia_transbordos",
                column: "AsignacionId");

            migrationBuilder.CreateIndex(
                name: "IX_guia_transbordos_GuiaId",
                table: "guia_transbordos",
                column: "GuiaId");

            migrationBuilder.CreateIndex(
                name: "IX_guia_transbordos_GuiaId_FechaEscaneoIngreso",
                table: "guia_transbordos",
                columns: new[] { "GuiaId", "FechaEscaneoIngreso" });

            migrationBuilder.CreateIndex(
                name: "IX_guia_transbordos_SucursalTransbordoId",
                table: "guia_transbordos",
                column: "SucursalTransbordoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "guia_transbordos");
        }
    }
}
