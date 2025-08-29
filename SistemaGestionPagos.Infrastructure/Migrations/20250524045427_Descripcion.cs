using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaGestionPagos.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Descripcion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Descripcion",
                table: "Pagos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Descripcion",
                table: "Pagos");
        }
    }
}
