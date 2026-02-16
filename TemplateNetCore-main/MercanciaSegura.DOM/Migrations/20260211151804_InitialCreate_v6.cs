using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MercanciaSegura.DOM.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate_v6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Cliente_Creditos_Cliente_ID",
                table: "Cliente_Creditos");

            migrationBuilder.DropIndex(
                name: "IX_Beneficiario_Preferente_Cliente_ID",
                table: "Beneficiario_Preferente");

            migrationBuilder.DropColumn(
                name: "Tipo_Poliza",
                table: "Poliza");

            migrationBuilder.AlterColumn<bool>(
                name: "Estatus",
                table: "Poliza",
                type: "bit",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cliente_Creditos_Cliente_ID",
                table: "Cliente_Creditos",
                column: "Cliente_ID",
                unique: true,
                filter: "[Cliente_ID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Beneficiario_Preferente_Cliente_ID",
                table: "Beneficiario_Preferente",
                column: "Cliente_ID",
                unique: true,
                filter: "[Cliente_ID] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Cliente_Creditos_Cliente_ID",
                table: "Cliente_Creditos");

            migrationBuilder.DropIndex(
                name: "IX_Beneficiario_Preferente_Cliente_ID",
                table: "Beneficiario_Preferente");

            migrationBuilder.AlterColumn<string>(
                name: "Estatus",
                table: "Poliza",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Tipo_Poliza",
                table: "Poliza",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cliente_Creditos_Cliente_ID",
                table: "Cliente_Creditos",
                column: "Cliente_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Beneficiario_Preferente_Cliente_ID",
                table: "Beneficiario_Preferente",
                column: "Cliente_ID");
        }
    }
}
