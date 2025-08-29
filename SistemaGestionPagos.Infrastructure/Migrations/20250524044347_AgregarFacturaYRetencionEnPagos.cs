using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaGestionPagos.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AgregarFacturaYRetencionEnPagos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NombreArchivo",
                table: "Pagos",
                newName: "NombreRetencion");

            migrationBuilder.RenameColumn(
                name: "Documento",
                table: "Pagos",
                newName: "Retencion");

            migrationBuilder.AddColumn<byte[]>(
                name: "Factura",
                table: "Pagos",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NombreFactura",
                table: "Pagos",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Factura",
                table: "Pagos");

            migrationBuilder.DropColumn(
                name: "NombreFactura",
                table: "Pagos");

            migrationBuilder.RenameColumn(
                name: "Retencion",
                table: "Pagos",
                newName: "Documento");

            migrationBuilder.RenameColumn(
                name: "NombreRetencion",
                table: "Pagos",
                newName: "NombreArchivo");
        }
    }
}
