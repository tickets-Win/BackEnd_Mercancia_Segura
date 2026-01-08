using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Template.DOM.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Beneficiario_Preferente",
                columns: table => new
                {
                    Beneficiario_Preferente_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Domicilio = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RFC = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Beneficiario_Preferente", x => x.Beneficiario_Preferente_ID);
                });

            migrationBuilder.CreateTable(
                name: "Origen_Cliente",
                columns: table => new
                {
                    Origen_Cliente_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tipo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Origen_Cliente", x => x.Origen_Cliente_ID);
                });

            migrationBuilder.CreateTable(
                name: "Regimen_Fiscal",
                columns: table => new
                {
                    Regimen_Fiscal_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Tipo_Persona = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regimen_Fiscal", x => x.Regimen_Fiscal_ID);
                });

            migrationBuilder.CreateTable(
                name: "Tipo_Cuenta",
                columns: table => new
                {
                    Tipo_Cuenta_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tipo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tipo_Cuenta", x => x.Tipo_Cuenta_ID);
                });

            migrationBuilder.CreateTable(
                name: "Tipo_Sector",
                columns: table => new
                {
                    Tipo_Sector_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tipo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tipo_Sector", x => x.Tipo_Sector_ID);
                });

            migrationBuilder.CreateTable(
                name: "Tipo_Seguro",
                columns: table => new
                {
                    Tipo_Seguro_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tipo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tipo_Seguro", x => x.Tipo_Seguro_ID);
                });

            migrationBuilder.CreateTable(
                name: "Tipo_Vendedor",
                columns: table => new
                {
                    Tipo_Vendedor_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tipo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tipo_Vendedor", x => x.Tipo_Vendedor_ID);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Usuario_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Usuario = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Correo = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Fecha_Registro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Estatus = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Usuario_ID);
                });

            migrationBuilder.CreateTable(
                name: "Cliente",
                columns: table => new
                {
                    Cliente_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tipo_Seguro_ID = table.Column<int>(type: "int", nullable: true),
                    Origen_Cliente_ID = table.Column<int>(type: "int", nullable: true),
                    Tipo_Cuenta_ID = table.Column<int>(type: "int", nullable: true),
                    Tipo_Sector_ID = table.Column<int>(type: "int", nullable: true),
                    Regimen_Fiscal_ID = table.Column<int>(type: "int", nullable: true),
                    Beneficiario_Preferente_ID = table.Column<int>(type: "int", nullable: true),
                    Tipo_Persona = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true),
                    RFC = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: true),
                    RFC_Generico = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: true),
                    Telefono = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: true),
                    Correo_Electronico = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Nacionalidad = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Calle = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: true),
                    Numero_Int = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Numero_Ext = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Pais = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Municipio = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    Poblacion = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    Colonia = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    Estado = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    CP = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true),
                    Apellido_Paterno = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    Apellido_Materno = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    Nombres = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    Nombre_Completo = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: true),
                    Cuota_Minima_Internacional = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    Cuota_Minima_Nacional = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    Cuota_Aplicable_Internacional = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    Cuota_Aplicable_Nacional = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    Fecha_Actualizacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Fecha_Registro = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cliente", x => x.Cliente_ID);
                    table.ForeignKey(
                        name: "FK_Cliente_Beneficiario_Preferente_Beneficiario_Preferente_ID",
                        column: x => x.Beneficiario_Preferente_ID,
                        principalTable: "Beneficiario_Preferente",
                        principalColumn: "Beneficiario_Preferente_ID");
                    table.ForeignKey(
                        name: "FK_Cliente_Origen_Cliente_Origen_Cliente_ID",
                        column: x => x.Origen_Cliente_ID,
                        principalTable: "Origen_Cliente",
                        principalColumn: "Origen_Cliente_ID");
                    table.ForeignKey(
                        name: "FK_Cliente_Regimen_Fiscal_Regimen_Fiscal_ID",
                        column: x => x.Regimen_Fiscal_ID,
                        principalTable: "Regimen_Fiscal",
                        principalColumn: "Regimen_Fiscal_ID");
                    table.ForeignKey(
                        name: "FK_Cliente_Tipo_Cuenta_Tipo_Cuenta_ID",
                        column: x => x.Tipo_Cuenta_ID,
                        principalTable: "Tipo_Cuenta",
                        principalColumn: "Tipo_Cuenta_ID");
                    table.ForeignKey(
                        name: "FK_Cliente_Tipo_Sector_Tipo_Sector_ID",
                        column: x => x.Tipo_Sector_ID,
                        principalTable: "Tipo_Sector",
                        principalColumn: "Tipo_Sector_ID");
                    table.ForeignKey(
                        name: "FK_Cliente_Tipo_Seguro_Tipo_Seguro_ID",
                        column: x => x.Tipo_Seguro_ID,
                        principalTable: "Tipo_Seguro",
                        principalColumn: "Tipo_Seguro_ID");
                });

            migrationBuilder.CreateTable(
                name: "Vendedor",
                columns: table => new
                {
                    Vendedor_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Apellido_Paterno = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    Apellido_Materno = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    Nombres = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    Nombre_Completo = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: true),
                    Tipo_Persona = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true),
                    Tipo_Vendedor_ID = table.Column<int>(type: "int", nullable: true),
                    Estatus = table.Column<bool>(type: "bit", nullable: false),
                    Clave = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    RFC = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: true),
                    Domicilio = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    CP = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true),
                    Colonia = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    Estado = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Genero = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true),
                    Telefono = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: true),
                    Correo_Electronico = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Observaciones = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Comision = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    Fecha_Actualizacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Fecha_Registro = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendedor", x => x.Vendedor_ID);
                    table.ForeignKey(
                        name: "FK_Vendedor_Tipo_Vendedor_Tipo_Vendedor_ID",
                        column: x => x.Tipo_Vendedor_ID,
                        principalTable: "Tipo_Vendedor",
                        principalColumn: "Tipo_Vendedor_ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cliente_Beneficiario_Preferente_ID",
                table: "Cliente",
                column: "Beneficiario_Preferente_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Cliente_Origen_Cliente_ID",
                table: "Cliente",
                column: "Origen_Cliente_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Cliente_Regimen_Fiscal_ID",
                table: "Cliente",
                column: "Regimen_Fiscal_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Cliente_Tipo_Cuenta_ID",
                table: "Cliente",
                column: "Tipo_Cuenta_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Cliente_Tipo_Sector_ID",
                table: "Cliente",
                column: "Tipo_Sector_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Cliente_Tipo_Seguro_ID",
                table: "Cliente",
                column: "Tipo_Seguro_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Vendedor_Tipo_Vendedor_ID",
                table: "Vendedor",
                column: "Tipo_Vendedor_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cliente");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "Vendedor");

            migrationBuilder.DropTable(
                name: "Beneficiario_Preferente");

            migrationBuilder.DropTable(
                name: "Origen_Cliente");

            migrationBuilder.DropTable(
                name: "Regimen_Fiscal");

            migrationBuilder.DropTable(
                name: "Tipo_Cuenta");

            migrationBuilder.DropTable(
                name: "Tipo_Sector");

            migrationBuilder.DropTable(
                name: "Tipo_Seguro");

            migrationBuilder.DropTable(
                name: "Tipo_Vendedor");
        }
    }
}
