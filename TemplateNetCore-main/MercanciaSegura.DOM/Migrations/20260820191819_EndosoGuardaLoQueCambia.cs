using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MercanciaSegura.DOM.Migrations
{
    /// <inheritdoc />
    public partial class EndosoGuardaLoQueCambia : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Prima_Servicio_De_Aseguramiento",
                table: "Endosos",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Suma_Asegurada",
                table: "Endosos",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Vigencia_Del",
                table: "Endosos",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Vigencia_Hasta",
                table: "Endosos",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Prima_Servicio_De_Aseguramiento",
                table: "Endosos");

            migrationBuilder.DropColumn(
                name: "Suma_Asegurada",
                table: "Endosos");

            migrationBuilder.DropColumn(
                name: "Vigencia_Del",
                table: "Endosos");

            migrationBuilder.DropColumn(
                name: "Vigencia_Hasta",
                table: "Endosos");
        }
    }
}
