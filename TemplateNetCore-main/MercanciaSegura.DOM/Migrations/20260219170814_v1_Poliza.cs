using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MercanciaSegura.DOM.Migrations
{
    /// <inheritdoc />
    public partial class v1_Poliza : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cobertura_Poliza_Cobertura_Poliza_Cobertura_ID",
                table: "Cobertura");

            migrationBuilder.DropForeignKey(
                name: "FK_Cobertura_Riesgo_Riesgo_ID",
                table: "Cobertura");

            migrationBuilder.DropTable(
                name: "Poliza_Cobertura");

            migrationBuilder.DropTable(
                name: "Riesgo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Poliza_Mercancia",
                table: "Poliza_Mercancia");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Poliza_Contenedor",
                table: "Poliza_Contenedor");

            migrationBuilder.DropIndex(
                name: "IX_Cobertura_Poliza_Cobertura_ID",
                table: "Cobertura");

            migrationBuilder.DropColumn(
                name: "Bienes_Asegurados",
                table: "Poliza_Mercancia");

            migrationBuilder.DropColumn(
                name: "Bienes_Excluidos",
                table: "Poliza_Mercancia");

            migrationBuilder.DropColumn(
                name: "Bienes_Sujetos_A_Consulta",
                table: "Poliza_Mercancia");

            migrationBuilder.DropColumn(
                name: "Folio_Poliza",
                table: "Poliza_Mercancia");

            migrationBuilder.DropColumn(
                name: "Bienes_Asegurados",
                table: "Poliza_Contenedor");

            migrationBuilder.DropColumn(
                name: "Poliza_Cobertura_ID",
                table: "Cobertura");

            migrationBuilder.RenameColumn(
                name: "Riesgo_ID",
                table: "Cobertura",
                newName: "Poliza_Contenedor_ID");

            migrationBuilder.RenameIndex(
                name: "IX_Cobertura_Riesgo_ID",
                table: "Cobertura",
                newName: "IX_Cobertura_Poliza_Contenedor_ID");

            migrationBuilder.AddColumn<int>(
                name: "Poliza_Mercancia_ID",
                table: "Poliza_Mercancia",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<decimal>(
                name: "Medio_Transporte",
                table: "Poliza_Contenedor",
                type: "decimal(18,2)",
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Poliza_Contenedor_ID",
                table: "Poliza_Contenedor",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "Nombre_Interno_Poliza",
                table: "Poliza_Contenedor",
                type: "nvarchar(80)",
                maxLength: 80,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Tipo_Poliza",
                table: "Poliza",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Poliza_Mercancia",
                table: "Poliza_Mercancia",
                column: "Poliza_Mercancia_ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Poliza_Contenedor",
                table: "Poliza_Contenedor",
                column: "Poliza_Contenedor_ID");

            migrationBuilder.CreateTable(
                name: "Riesgo_Cubierto",
                columns: table => new
                {
                    Riesgo_Cubierto_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Tipo_Riesgo_ID = table.Column<int>(type: "int", nullable: false),
                    Poliza_Mercancia_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Riesgo_Cubierto", x => x.Riesgo_Cubierto_ID);
                });

            migrationBuilder.CreateTable(
                name: "Tipo_Bien",
                columns: table => new
                {
                    Tipo_Bien_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tipo_Bien", x => x.Tipo_Bien_ID);
                });

            migrationBuilder.CreateTable(
                name: "Tipo_Riesgo",
                columns: table => new
                {
                    Tipo_Riesgo_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tipo_Riesgo", x => x.Tipo_Riesgo_ID);
                });

            migrationBuilder.CreateTable(
                name: "Bien",
                columns: table => new
                {
                    Bien_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Poliza_ID = table.Column<int>(type: "int", nullable: false),
                    Tipo_Bien = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bien", x => x.Bien_ID);
                    table.ForeignKey(
                        name: "FK_Bien_Poliza_Poliza_ID",
                        column: x => x.Poliza_ID,
                        principalTable: "Poliza",
                        principalColumn: "Poliza_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bien_Tipo_Bien_Tipo_Bien",
                        column: x => x.Tipo_Bien,
                        principalTable: "Tipo_Bien",
                        principalColumn: "Tipo_Bien_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Poliza_Mercancia_Poliza_ID",
                table: "Poliza_Mercancia",
                column: "Poliza_ID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Poliza_Contenedor_Poliza_ID",
                table: "Poliza_Contenedor",
                column: "Poliza_ID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bien_Poliza_ID",
                table: "Bien",
                column: "Poliza_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Bien_Tipo_Bien",
                table: "Bien",
                column: "Tipo_Bien");

            migrationBuilder.AddForeignKey(
                name: "FK_Cobertura_Poliza_Contenedor_Poliza_Contenedor_ID",
                table: "Cobertura",
                column: "Poliza_Contenedor_ID",
                principalTable: "Poliza_Contenedor",
                principalColumn: "Poliza_Contenedor_ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cobertura_Poliza_Contenedor_Poliza_Contenedor_ID",
                table: "Cobertura");

            migrationBuilder.DropTable(
                name: "Bien");

            migrationBuilder.DropTable(
                name: "Riesgo_Cubierto");

            migrationBuilder.DropTable(
                name: "Tipo_Riesgo");

            migrationBuilder.DropTable(
                name: "Tipo_Bien");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Poliza_Mercancia",
                table: "Poliza_Mercancia");

            migrationBuilder.DropIndex(
                name: "IX_Poliza_Mercancia_Poliza_ID",
                table: "Poliza_Mercancia");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Poliza_Contenedor",
                table: "Poliza_Contenedor");

            migrationBuilder.DropIndex(
                name: "IX_Poliza_Contenedor_Poliza_ID",
                table: "Poliza_Contenedor");

            migrationBuilder.DropColumn(
                name: "Poliza_Mercancia_ID",
                table: "Poliza_Mercancia");

            migrationBuilder.DropColumn(
                name: "Poliza_Contenedor_ID",
                table: "Poliza_Contenedor");

            migrationBuilder.DropColumn(
                name: "Nombre_Interno_Poliza",
                table: "Poliza_Contenedor");

            migrationBuilder.DropColumn(
                name: "Tipo_Poliza",
                table: "Poliza");

            migrationBuilder.RenameColumn(
                name: "Poliza_Contenedor_ID",
                table: "Cobertura",
                newName: "Riesgo_ID");

            migrationBuilder.RenameIndex(
                name: "IX_Cobertura_Poliza_Contenedor_ID",
                table: "Cobertura",
                newName: "IX_Cobertura_Riesgo_ID");

            migrationBuilder.AddColumn<string>(
                name: "Bienes_Asegurados",
                table: "Poliza_Mercancia",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Bienes_Excluidos",
                table: "Poliza_Mercancia",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Bienes_Sujetos_A_Consulta",
                table: "Poliza_Mercancia",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Folio_Poliza",
                table: "Poliza_Mercancia",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Medio_Transporte",
                table: "Poliza_Contenedor",
                type: "decimal(10,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldMaxLength: 250,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Bienes_Asegurados",
                table: "Poliza_Contenedor",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Poliza_Cobertura_ID",
                table: "Cobertura",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Poliza_Mercancia",
                table: "Poliza_Mercancia",
                column: "Poliza_ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Poliza_Contenedor",
                table: "Poliza_Contenedor",
                column: "Poliza_ID");

            migrationBuilder.CreateTable(
                name: "Poliza_Cobertura",
                columns: table => new
                {
                    Poliza_Cobertura_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Poliza_ID = table.Column<int>(type: "int", nullable: false),
                    Deducible = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: true),
                    Prima = table.Column<decimal>(type: "decimal(12,2)", nullable: true),
                    Suma_Asegurada = table.Column<decimal>(type: "decimal(14,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Poliza_Cobertura", x => x.Poliza_Cobertura_ID);
                    table.ForeignKey(
                        name: "FK_Poliza_Cobertura_Poliza_Poliza_ID",
                        column: x => x.Poliza_ID,
                        principalTable: "Poliza",
                        principalColumn: "Poliza_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Riesgo",
                columns: table => new
                {
                    Riesgo_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Riesgo", x => x.Riesgo_ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cobertura_Poliza_Cobertura_ID",
                table: "Cobertura",
                column: "Poliza_Cobertura_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Poliza_Cobertura_Poliza_ID",
                table: "Poliza_Cobertura",
                column: "Poliza_ID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Cobertura_Poliza_Cobertura_Poliza_Cobertura_ID",
                table: "Cobertura",
                column: "Poliza_Cobertura_ID",
                principalTable: "Poliza_Cobertura",
                principalColumn: "Poliza_Cobertura_ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cobertura_Riesgo_Riesgo_ID",
                table: "Cobertura",
                column: "Riesgo_ID",
                principalTable: "Riesgo",
                principalColumn: "Riesgo_ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
