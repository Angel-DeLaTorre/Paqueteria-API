using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Paqueteria.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CamposHistoricos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Alto",
                table: "articulos_guia",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Ancho",
                table: "articulos_guia",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "ClaveSatHistorico",
                table: "articulos_guia",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "Largo",
                table: "articulos_guia",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "MaterialPeligrosoHistorico",
                table: "articulos_guia",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Alto",
                table: "articulos_guia");

            migrationBuilder.DropColumn(
                name: "Ancho",
                table: "articulos_guia");

            migrationBuilder.DropColumn(
                name: "ClaveSatHistorico",
                table: "articulos_guia");

            migrationBuilder.DropColumn(
                name: "Largo",
                table: "articulos_guia");

            migrationBuilder.DropColumn(
                name: "MaterialPeligrosoHistorico",
                table: "articulos_guia");
        }
    }
}
