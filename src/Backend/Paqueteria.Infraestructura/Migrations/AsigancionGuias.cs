using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Paqueteria.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AsigancionGuias : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_guias_asignaciones_AsignacionId1",
                table: "guias");

            migrationBuilder.DropIndex(
                name: "IX_guias_AsignacionId1",
                table: "guias");

            migrationBuilder.DropColumn(
                name: "AsignacionId1",
                table: "guias");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AsignacionId1",
                table: "guias",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_guias_AsignacionId1",
                table: "guias",
                column: "AsignacionId1");

            migrationBuilder.AddForeignKey(
                name: "FK_guias_asignaciones_AsignacionId1",
                table: "guias",
                column: "AsignacionId1",
                principalTable: "asignaciones",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
