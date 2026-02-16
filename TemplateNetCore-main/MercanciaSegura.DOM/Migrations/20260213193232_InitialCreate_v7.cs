using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MercanciaSegura.DOM.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate_v7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Beneficiario_Preferente_Cliente_ID",
                table: "Beneficiario_Preferente");

            migrationBuilder.CreateIndex(
                name: "IX_Beneficiario_Preferente_Cliente_ID",
                table: "Beneficiario_Preferente",
                column: "Cliente_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Beneficiario_Preferente_Cliente_ID",
                table: "Beneficiario_Preferente");

            migrationBuilder.CreateIndex(
                name: "IX_Beneficiario_Preferente_Cliente_ID",
                table: "Beneficiario_Preferente",
                column: "Cliente_ID",
                unique: true,
                filter: "[Cliente_ID] IS NOT NULL");
        }
    }
}
