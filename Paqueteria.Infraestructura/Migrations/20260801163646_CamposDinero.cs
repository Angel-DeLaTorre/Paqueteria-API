using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Paqueteria.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CamposDinero : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "costo_flete",
                table: "guias",
                newName: "recoleccion");

            migrationBuilder.RenameColumn(
                name: "MaterialPeligrosoHistorico",
                table: "articulos_guia",
                newName: "ClaveUnidadSat");

            migrationBuilder.RenameColumn(
                name: "ClaveSatHistorico",
                table: "articulos_guia",
                newName: "ClaveProdServSat");

            migrationBuilder.AddColumn<bool>(
                name: "condona_iva",
                table: "guias",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "entrega_a",
                table: "guias",
                type: "numeric(15,2)",
                precision: 15,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "esta_asegurado",
                table: "guias",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "flete",
                table: "guias",
                type: "numeric(15,2)",
                precision: 15,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "lineas",
                table: "guias",
                type: "numeric(15,2)",
                precision: 15,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "maniobras",
                table: "guias",
                type: "numeric(15,2)",
                precision: 15,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "peaje",
                table: "guias",
                type: "numeric(15,2)",
                precision: 15,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "ClaveMaterialPeligrosoSat",
                table: "articulos_guia",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ClaveTipoEmbalajeSat",
                table: "articulos_guia",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "EsMaterialPeligroso",
                table: "articulos_guia",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "condona_iva",
                table: "guias");

            migrationBuilder.DropColumn(
                name: "entrega_a",
                table: "guias");

            migrationBuilder.DropColumn(
                name: "esta_asegurado",
                table: "guias");

            migrationBuilder.DropColumn(
                name: "flete",
                table: "guias");

            migrationBuilder.DropColumn(
                name: "lineas",
                table: "guias");

            migrationBuilder.DropColumn(
                name: "maniobras",
                table: "guias");

            migrationBuilder.DropColumn(
                name: "peaje",
                table: "guias");

            migrationBuilder.DropColumn(
                name: "ClaveMaterialPeligrosoSat",
                table: "articulos_guia");

            migrationBuilder.DropColumn(
                name: "ClaveTipoEmbalajeSat",
                table: "articulos_guia");

            migrationBuilder.DropColumn(
                name: "EsMaterialPeligroso",
                table: "articulos_guia");

            migrationBuilder.RenameColumn(
                name: "recoleccion",
                table: "guias",
                newName: "costo_flete");

            migrationBuilder.RenameColumn(
                name: "ClaveUnidadSat",
                table: "articulos_guia",
                newName: "MaterialPeligrosoHistorico");

            migrationBuilder.RenameColumn(
                name: "ClaveProdServSat",
                table: "articulos_guia",
                newName: "ClaveSatHistorico");
        }
    }
}
