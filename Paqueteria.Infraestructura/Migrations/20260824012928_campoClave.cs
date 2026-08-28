using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Paqueteria.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class campoClave : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Clave",
                table: "asignaciones",
                newName: "clave");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "clave",
                table: "asignaciones",
                newName: "Clave");
        }
    }
}
