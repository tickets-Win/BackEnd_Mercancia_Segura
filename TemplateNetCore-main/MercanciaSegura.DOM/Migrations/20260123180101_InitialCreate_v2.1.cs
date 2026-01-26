using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MercanciaSegura.DOM.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate_v21 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tipo_Persona",
                table: "Vendedor");

            migrationBuilder.AddColumn<int>(
                name: "Tipo_Persona_ID",
                table: "Vendedor",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vendedor_Tipo_Persona_ID",
                table: "Vendedor",
                column: "Tipo_Persona_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Vendedor_Tipo_Persona_Tipo_Persona_ID",
                table: "Vendedor",
                column: "Tipo_Persona_ID",
                principalTable: "Tipo_Persona",
                principalColumn: "Tipo_Persona_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vendedor_Tipo_Persona_Tipo_Persona_ID",
                table: "Vendedor");

            migrationBuilder.DropIndex(
                name: "IX_Vendedor_Tipo_Persona_ID",
                table: "Vendedor");

            migrationBuilder.DropColumn(
                name: "Tipo_Persona_ID",
                table: "Vendedor");

            migrationBuilder.AddColumn<string>(
                name: "Tipo_Persona",
                table: "Vendedor",
                type: "nvarchar(1)",
                maxLength: 1,
                nullable: true);
        }
    }
}
