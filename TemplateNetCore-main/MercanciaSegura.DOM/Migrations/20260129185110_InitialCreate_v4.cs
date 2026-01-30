using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MercanciaSegura.DOM.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate_v4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Tipo_Poliza",
                table: "Poliza",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 50);

            migrationBuilder.AddColumn<string>(
                name: "Genero",
                table: "Cliente",
                type: "nvarchar(1)",
                maxLength: 1,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Cliente_Vendedor",
                columns: table => new
                {
                    Cliente_Vendedor_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Vendedor_ID = table.Column<int>(type: "int", nullable: true),
                    Comision = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    Cliente_ID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cliente_Vendedor", x => x.Cliente_Vendedor_ID);
                    table.ForeignKey(
                        name: "FK_Cliente_Vendedor_Cliente_Cliente_ID",
                        column: x => x.Cliente_ID,
                        principalTable: "Cliente",
                        principalColumn: "Cliente_ID");
                    table.ForeignKey(
                        name: "FK_Cliente_Vendedor_Vendedor_Vendedor_ID",
                        column: x => x.Vendedor_ID,
                        principalTable: "Vendedor",
                        principalColumn: "Vendedor_ID");
                });

            migrationBuilder.CreateTable(
                name: "Poliza_Contenedor",
                columns: table => new
                {
                    Poliza_ID = table.Column<int>(type: "int", nullable: false),
                    Bienes_Asegurados = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Por_Contenedor = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    Ferrocarril = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    Terrestre = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    Cuota_Aplicable = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    Danio_Material = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    Robo = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    Perdida_Parcial = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    Perdida_Total = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    Trayectos_Asegurados = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Medio_Transporte = table.Column<decimal>(type: "decimal(10,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Poliza_Contenedor", x => x.Poliza_ID);
                    table.ForeignKey(
                        name: "FK_Poliza_Contenedor_Poliza_Poliza_ID",
                        column: x => x.Poliza_ID,
                        principalTable: "Poliza",
                        principalColumn: "Poliza_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Poliza_Mercancia",
                columns: table => new
                {
                    Poliza_ID = table.Column<int>(type: "int", nullable: false),
                    Nombre_Interno_Poliza = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    Folio_Poliza = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Bienes_Asegurados = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Bienes_Excluidos = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Bienes_Sujetos_A_Consulta = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Terrestre_Aereo = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    Maritimo = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    Paqueteria_Mensajeria = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    Deducibles = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: true),
                    Compras = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Ventas = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Maquila = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Bienes_Usados = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Embarque_Filiales = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Indemnizacion_Otros = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Medicamentos = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    Cobre_Aluminio_Acero = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    Medicamentos_Controlados = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    EQ_Contratistas = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    Cuota_General_Poliza = table.Column<decimal>(type: "decimal(10,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Poliza_Mercancia", x => x.Poliza_ID);
                    table.ForeignKey(
                        name: "FK_Poliza_Mercancia_Poliza_Poliza_ID",
                        column: x => x.Poliza_ID,
                        principalTable: "Poliza",
                        principalColumn: "Poliza_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tipo_Correo",
                columns: table => new
                {
                    Tipo_Correo_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tipo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tipo_Correo", x => x.Tipo_Correo_ID);
                });

            migrationBuilder.CreateTable(
                name: "Tipo_Cuota",
                columns: table => new
                {
                    Tipo_Cuota_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Monto = table.Column<decimal>(type: "decimal(10,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tipo_Cuota", x => x.Tipo_Cuota_ID);
                });

            migrationBuilder.CreateTable(
                name: "Tipo_Tarifa",
                columns: table => new
                {
                    Tipo_Tarifa_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tarifa = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tipo_Tarifa", x => x.Tipo_Tarifa_ID);
                });

            migrationBuilder.CreateTable(
                name: "Correos",
                columns: table => new
                {
                    Correo_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Correo = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Tipo_Correo_ID = table.Column<int>(type: "int", nullable: true),
                    Cliente_ID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Correos", x => x.Correo_ID);
                    table.ForeignKey(
                        name: "FK_Correos_Cliente_Cliente_ID",
                        column: x => x.Cliente_ID,
                        principalTable: "Cliente",
                        principalColumn: "Cliente_ID");
                    table.ForeignKey(
                        name: "FK_Correos_Tipo_Correo_Tipo_Correo_ID",
                        column: x => x.Tipo_Correo_ID,
                        principalTable: "Tipo_Correo",
                        principalColumn: "Tipo_Correo_ID");
                });

            migrationBuilder.CreateTable(
                name: "Cuota",
                columns: table => new
                {
                    Cuota_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tipo_Cuota_ID = table.Column<int>(type: "int", nullable: true),
                    Monto = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    Cliente_ID = table.Column<int>(type: "int", nullable: true),
                    Tipo_Tarifa_ID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cuota", x => x.Cuota_ID);
                    table.ForeignKey(
                        name: "FK_Cuota_Cliente_Cliente_ID",
                        column: x => x.Cliente_ID,
                        principalTable: "Cliente",
                        principalColumn: "Cliente_ID");
                    table.ForeignKey(
                        name: "FK_Cuota_Tipo_Cuota_Tipo_Cuota_ID",
                        column: x => x.Tipo_Cuota_ID,
                        principalTable: "Tipo_Cuota",
                        principalColumn: "Tipo_Cuota_ID");
                    table.ForeignKey(
                        name: "FK_Cuota_Tipo_Tarifa_Tipo_Tarifa_ID",
                        column: x => x.Tipo_Tarifa_ID,
                        principalTable: "Tipo_Tarifa",
                        principalColumn: "Tipo_Tarifa_ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cliente_Vendedor_Cliente_ID",
                table: "Cliente_Vendedor",
                column: "Cliente_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Cliente_Vendedor_Vendedor_ID",
                table: "Cliente_Vendedor",
                column: "Vendedor_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Correos_Cliente_ID",
                table: "Correos",
                column: "Cliente_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Correos_Tipo_Correo_ID",
                table: "Correos",
                column: "Tipo_Correo_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Cuota_Cliente_ID",
                table: "Cuota",
                column: "Cliente_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Cuota_Tipo_Cuota_ID",
                table: "Cuota",
                column: "Tipo_Cuota_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Cuota_Tipo_Tarifa_ID",
                table: "Cuota",
                column: "Tipo_Tarifa_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cliente_Vendedor");

            migrationBuilder.DropTable(
                name: "Correos");

            migrationBuilder.DropTable(
                name: "Cuota");

            migrationBuilder.DropTable(
                name: "Poliza_Contenedor");

            migrationBuilder.DropTable(
                name: "Poliza_Mercancia");

            migrationBuilder.DropTable(
                name: "Tipo_Correo");

            migrationBuilder.DropTable(
                name: "Tipo_Cuota");

            migrationBuilder.DropTable(
                name: "Tipo_Tarifa");

            migrationBuilder.DropColumn(
                name: "Genero",
                table: "Cliente");

            migrationBuilder.AlterColumn<int>(
                name: "Tipo_Poliza",
                table: "Poliza",
                type: "int",
                maxLength: 50,
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);
        }
    }
}
