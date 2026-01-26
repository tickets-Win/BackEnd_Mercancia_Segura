using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MercanciaSegura.DOM.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate_v3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Aseguradora",
                columns: table => new
                {
                    Aseguradora_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aseguradora", x => x.Aseguradora_ID);
                });

            migrationBuilder.CreateTable(
                name: "Contratante",
                columns: table => new
                {
                    Contratante_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contratante", x => x.Contratante_ID);
                });

            migrationBuilder.CreateTable(
                name: "FormaPago",
                columns: table => new
                {
                    FormaPago_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormaPago", x => x.FormaPago_ID);
                });

            migrationBuilder.CreateTable(
                name: "Moneda",
                columns: table => new
                {
                    Moneda_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Tipo_Cambio = table.Column<decimal>(type: "decimal(10,2)", maxLength: 100, nullable: true),
                    Tipo_Cambio_Ventanilla = table.Column<decimal>(type: "decimal(10,2)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Moneda", x => x.Moneda_ID);
                });

            migrationBuilder.CreateTable(
                name: "Poliza",
                columns: table => new
                {
                    Poliza_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Producto_ID = table.Column<int>(type: "int", nullable: true),
                    Contratante_ID = table.Column<int>(type: "int", nullable: true),
                    Aseguradora_ID = table.Column<int>(type: "int", nullable: true),
                    SubRamo_ID = table.Column<int>(type: "int", nullable: true),
                    Moneda_ID = table.Column<int>(type: "int", nullable: true),
                    Forma_Pago_ID = table.Column<int>(type: "int", nullable: true),
                    Estatus = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Tipo_Poliza = table.Column<int>(type: "int", maxLength: 50, nullable: false),
                    Numero_Poliza = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Vigencia_Del = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Vigencia_Hasta = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Otros_Poliza = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Prima_Neta = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Derecho_Poliza = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Otros = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    IVA = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Prima_Total = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Poliza", x => x.Poliza_ID);
                });

            migrationBuilder.CreateTable(
                name: "Producto",
                columns: table => new
                {
                    Producto_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Pantalla = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Producto", x => x.Producto_ID);
                });

            migrationBuilder.CreateTable(
                name: "SubRamo",
                columns: table => new
                {
                    SubRamo_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubRamo", x => x.SubRamo_ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Aseguradora");

            migrationBuilder.DropTable(
                name: "Contratante");

            migrationBuilder.DropTable(
                name: "FormaPago");

            migrationBuilder.DropTable(
                name: "Moneda");

            migrationBuilder.DropTable(
                name: "Poliza");

            migrationBuilder.DropTable(
                name: "Producto");

            migrationBuilder.DropTable(
                name: "SubRamo");
        }
    }
}
