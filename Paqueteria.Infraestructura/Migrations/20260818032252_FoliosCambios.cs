using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Paqueteria.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FoliosCambios : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_folios_sucursales",
                table: "folios_sucursales");

            migrationBuilder.AddColumn<Guid>(
                name: "SucursalActualId",
                table: "guias",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "tipo",
                table: "folios_sucursales",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Clave",
                table: "asignaciones",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_folios_sucursales",
                table: "folios_sucursales",
                columns: new[] { "sucursal_id", "tipo" });

            migrationBuilder.CreateIndex(
                name: "IX_guias_SucursalActualId",
                table: "guias",
                column: "SucursalActualId");

            migrationBuilder.CreateIndex(
                name: "IX_folios_sucursales_sucursal_id",
                table: "folios_sucursales",
                column: "sucursal_id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_guias_sucursales_SucursalActualId",
                table: "guias",
                column: "SucursalActualId",
                principalTable: "sucursales",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_guias_sucursales_SucursalActualId",
                table: "guias");

            migrationBuilder.DropIndex(
                name: "IX_guias_SucursalActualId",
                table: "guias");

            migrationBuilder.DropPrimaryKey(
                name: "PK_folios_sucursales",
                table: "folios_sucursales");

            migrationBuilder.DropIndex(
                name: "IX_folios_sucursales_sucursal_id",
                table: "folios_sucursales");

            migrationBuilder.DropColumn(
                name: "SucursalActualId",
                table: "guias");

            migrationBuilder.DropColumn(
                name: "tipo",
                table: "folios_sucursales");

            migrationBuilder.DropColumn(
                name: "Clave",
                table: "asignaciones");

            migrationBuilder.AddPrimaryKey(
                name: "PK_folios_sucursales",
                table: "folios_sucursales",
                column: "sucursal_id");
        }
    }
}
