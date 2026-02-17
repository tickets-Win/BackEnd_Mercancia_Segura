using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MercanciaSegura.DOM.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate_v81 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Poliza_Cobertura_Poliza_ID",
                table: "Poliza_Cobertura");

            migrationBuilder.DropColumn(
                name: "Estatus",
                table: "Poliza");

            migrationBuilder.AddColumn<int>(
                name: "Estatus_Poliza_ID",
                table: "Poliza",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Fecha_Baja",
                table: "Poliza",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Estatus_Poliza",
                columns: table => new
                {
                    Estatus_Poliza_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tipo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estatus_Poliza", x => x.Estatus_Poliza_ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Poliza_Cobertura_Poliza_ID",
                table: "Poliza_Cobertura",
                column: "Poliza_ID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Poliza_Estatus_Poliza_ID",
                table: "Poliza",
                column: "Estatus_Poliza_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Poliza_Estatus_Poliza_Estatus_Poliza_ID",
                table: "Poliza",
                column: "Estatus_Poliza_ID",
                principalTable: "Estatus_Poliza",
                principalColumn: "Estatus_Poliza_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Poliza_Estatus_Poliza_Estatus_Poliza_ID",
                table: "Poliza");

            migrationBuilder.DropTable(
                name: "Estatus_Poliza");

            migrationBuilder.DropIndex(
                name: "IX_Poliza_Cobertura_Poliza_ID",
                table: "Poliza_Cobertura");

            migrationBuilder.DropIndex(
                name: "IX_Poliza_Estatus_Poliza_ID",
                table: "Poliza");

            migrationBuilder.DropColumn(
                name: "Estatus_Poliza_ID",
                table: "Poliza");

            migrationBuilder.DropColumn(
                name: "Fecha_Baja",
                table: "Poliza");

            migrationBuilder.AddColumn<bool>(
                name: "Estatus",
                table: "Poliza",
                type: "bit",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Poliza_Cobertura_Poliza_ID",
                table: "Poliza_Cobertura",
                column: "Poliza_ID");
        }
    }
}
