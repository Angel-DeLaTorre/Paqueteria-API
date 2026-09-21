using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Paqueteria.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SucursalAsignacion1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_asignaciones_sucursales_SucursalDestinoId",
                table: "asignaciones");

            migrationBuilder.DropForeignKey(
                name: "FK_asignaciones_sucursales_SucursalOrigenId",
                table: "asignaciones");

            migrationBuilder.RenameColumn(
                name: "SucursalOrigenId",
                table: "asignaciones",
                newName: "sucursal_origen_id");

            migrationBuilder.RenameColumn(
                name: "SucursalDestinoId",
                table: "asignaciones",
                newName: "sucursal_destino_id");

            migrationBuilder.RenameIndex(
                name: "IX_asignaciones_SucursalOrigenId",
                table: "asignaciones",
                newName: "IX_asignaciones_sucursal_origen_id");

            migrationBuilder.RenameIndex(
                name: "IX_asignaciones_SucursalDestinoId",
                table: "asignaciones",
                newName: "IX_asignaciones_sucursal_destino_id");

            migrationBuilder.AddForeignKey(
                name: "FK_asignaciones_sucursales_sucursal_destino_id",
                table: "asignaciones",
                column: "sucursal_destino_id",
                principalTable: "sucursales",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_asignaciones_sucursales_sucursal_origen_id",
                table: "asignaciones",
                column: "sucursal_origen_id",
                principalTable: "sucursales",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_asignaciones_sucursales_sucursal_destino_id",
                table: "asignaciones");

            migrationBuilder.DropForeignKey(
                name: "FK_asignaciones_sucursales_sucursal_origen_id",
                table: "asignaciones");

            migrationBuilder.RenameColumn(
                name: "sucursal_origen_id",
                table: "asignaciones",
                newName: "SucursalOrigenId");

            migrationBuilder.RenameColumn(
                name: "sucursal_destino_id",
                table: "asignaciones",
                newName: "SucursalDestinoId");

            migrationBuilder.RenameIndex(
                name: "IX_asignaciones_sucursal_origen_id",
                table: "asignaciones",
                newName: "IX_asignaciones_SucursalOrigenId");

            migrationBuilder.RenameIndex(
                name: "IX_asignaciones_sucursal_destino_id",
                table: "asignaciones",
                newName: "IX_asignaciones_SucursalDestinoId");

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
    }
}
