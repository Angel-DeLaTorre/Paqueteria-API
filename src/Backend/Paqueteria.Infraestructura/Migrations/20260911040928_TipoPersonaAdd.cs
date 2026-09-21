using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Paqueteria.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class TipoPersonaAdd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TipoPersona",
                table: "clientes",
                newName: "tipo_persona");

            migrationBuilder.RenameColumn(
                name: "NumContenedor2",
                table: "choferes",
                newName: "num_contenedor2");

            migrationBuilder.RenameColumn(
                name: "NumContenedor",
                table: "choferes",
                newName: "num_contenedor");

            migrationBuilder.RenameColumn(
                name: "NumCamion",
                table: "choferes",
                newName: "num_camion");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "tipo_persona",
                table: "clientes",
                newName: "TipoPersona");

            migrationBuilder.RenameColumn(
                name: "num_contenedor2",
                table: "choferes",
                newName: "NumContenedor2");

            migrationBuilder.RenameColumn(
                name: "num_contenedor",
                table: "choferes",
                newName: "NumContenedor");

            migrationBuilder.RenameColumn(
                name: "num_camion",
                table: "choferes",
                newName: "NumCamion");
        }
    }
}
