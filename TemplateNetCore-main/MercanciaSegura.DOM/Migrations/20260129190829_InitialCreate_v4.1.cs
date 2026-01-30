using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MercanciaSegura.DOM.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate_v41 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Monto",
                table: "Tipo_Cuota");

            migrationBuilder.AddColumn<string>(
                name: "Cuota",
                table: "Tipo_Cuota",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cuota",
                table: "Tipo_Cuota");

            migrationBuilder.AddColumn<decimal>(
                name: "Monto",
                table: "Tipo_Cuota",
                type: "decimal(10,2)",
                nullable: true);
        }
    }
}
