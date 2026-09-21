using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Paqueteria.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SucursalAsignacion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "SucursalDestinoId",
                table: "asignaciones",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "SucursalOrigenId",
                table: "asignaciones",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_asignaciones_SucursalDestinoId",
                table: "asignaciones",
                column: "SucursalDestinoId");

            migrationBuilder.CreateIndex(
                name: "IX_asignaciones_SucursalOrigenId",
                table: "asignaciones",
                column: "SucursalOrigenId");

            migrationBuilder.AddForeignKey(
                name: "FK_asignaciones_sucursales_SucursalDestinoId",
                table: "asignaciones",
                column: "SucursalDestinoId",
                principalTable: "sucursales",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_asignaciones_sucursales_SucursalOrigenId",
                table: "asignaciones",
                column: "SucursalOrigenId",
                principalTable: "sucursales",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_asignaciones_sucursales_SucursalDestinoId",
                table: "asignaciones");

            migrationBuilder.DropForeignKey(
                name: "FK_asignaciones_sucursales_SucursalOrigenId",
                table: "asignaciones");

            migrationBuilder.DropIndex(
                name: "IX_asignaciones_SucursalDestinoId",
                table: "asignaciones");

            migrationBuilder.DropIndex(
                name: "IX_asignaciones_SucursalOrigenId",
                table: "asignaciones");

            migrationBuilder.DropColumn(
                name: "SucursalDestinoId",
                table: "asignaciones");

            migrationBuilder.DropColumn(
                name: "SucursalOrigenId",
                table: "asignaciones");
        }
    }
}
