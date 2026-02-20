using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MercanciaSegura.DOM.Migrations
{
    /// <inheritdoc />
    public partial class v1_Bien : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Administracion_Bien_ID",
                table: "Bien",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bien_Administracion_Bien_ID",
                table: "Bien",
                column: "Administracion_Bien_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Bien_Administracion_Bien_Administracion_Bien_ID",
                table: "Bien",
                column: "Administracion_Bien_ID",
                principalTable: "Administracion_Bien",
                principalColumn: "Administracion_Bien_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bien_Administracion_Bien_Administracion_Bien_ID",
                table: "Bien");

            migrationBuilder.DropIndex(
                name: "IX_Bien_Administracion_Bien_ID",
                table: "Bien");

            migrationBuilder.DropColumn(
                name: "Administracion_Bien_ID",
                table: "Bien");
        }
    }
}
