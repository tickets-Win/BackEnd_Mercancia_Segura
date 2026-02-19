using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MercanciaSegura.DOM.Migrations
{
    /// <inheritdoc />
    public partial class v14_Poliza : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bien_Poliza_Poliza_ID",
                table: "Bien");

            migrationBuilder.DropForeignKey(
                name: "FK_Bien_Tipo_Bien_Tipo_Bien",
                table: "Bien");

            migrationBuilder.DropForeignKey(
                name: "FK_Poliza_Contenedor_Poliza_Poliza_ID",
                table: "Poliza_Contenedor");

            migrationBuilder.DropForeignKey(
                name: "FK_Poliza_Mercancia_Poliza_Poliza_ID",
                table: "Poliza_Mercancia");

            migrationBuilder.DropIndex(
                name: "IX_Poliza_Contenedor_Poliza_ID",
                table: "Poliza_Contenedor");

            migrationBuilder.AlterColumn<int>(
                name: "Poliza_ID",
                table: "Poliza_Mercancia",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Poliza_ID",
                table: "Poliza_Contenedor",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Medio_Transporte",
                table: "Poliza_Contenedor",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldMaxLength: 250,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Cobertura",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Tipo_Bien",
                table: "Bien",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Poliza_ID",
                table: "Bien",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Administracion_Bien",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Poliza_Contenedor_Poliza_ID",
                table: "Poliza_Contenedor",
                column: "Poliza_ID",
                unique: true,
                filter: "[Poliza_ID] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Bien_Poliza_Poliza_ID",
                table: "Bien",
                column: "Poliza_ID",
                principalTable: "Poliza",
                principalColumn: "Poliza_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Bien_Tipo_Bien_Tipo_Bien",
                table: "Bien",
                column: "Tipo_Bien",
                principalTable: "Tipo_Bien",
                principalColumn: "Tipo_Bien_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Poliza_Contenedor_Poliza_Poliza_ID",
                table: "Poliza_Contenedor",
                column: "Poliza_ID",
                principalTable: "Poliza",
                principalColumn: "Poliza_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Poliza_Mercancia_Poliza_Poliza_ID",
                table: "Poliza_Mercancia",
                column: "Poliza_ID",
                principalTable: "Poliza",
                principalColumn: "Poliza_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bien_Poliza_Poliza_ID",
                table: "Bien");

            migrationBuilder.DropForeignKey(
                name: "FK_Bien_Tipo_Bien_Tipo_Bien",
                table: "Bien");

            migrationBuilder.DropForeignKey(
                name: "FK_Poliza_Contenedor_Poliza_Poliza_ID",
                table: "Poliza_Contenedor");

            migrationBuilder.DropForeignKey(
                name: "FK_Poliza_Mercancia_Poliza_Poliza_ID",
                table: "Poliza_Mercancia");

            migrationBuilder.DropIndex(
                name: "IX_Poliza_Contenedor_Poliza_ID",
                table: "Poliza_Contenedor");

            migrationBuilder.AlterColumn<int>(
                name: "Poliza_ID",
                table: "Poliza_Mercancia",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Poliza_ID",
                table: "Poliza_Contenedor",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Medio_Transporte",
                table: "Poliza_Contenedor",
                type: "decimal(18,2)",
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Cobertura",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<int>(
                name: "Tipo_Bien",
                table: "Bien",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Poliza_ID",
                table: "Bien",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Administracion_Bien",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.CreateIndex(
                name: "IX_Poliza_Contenedor_Poliza_ID",
                table: "Poliza_Contenedor",
                column: "Poliza_ID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Bien_Poliza_Poliza_ID",
                table: "Bien",
                column: "Poliza_ID",
                principalTable: "Poliza",
                principalColumn: "Poliza_ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bien_Tipo_Bien_Tipo_Bien",
                table: "Bien",
                column: "Tipo_Bien",
                principalTable: "Tipo_Bien",
                principalColumn: "Tipo_Bien_ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Poliza_Contenedor_Poliza_Poliza_ID",
                table: "Poliza_Contenedor",
                column: "Poliza_ID",
                principalTable: "Poliza",
                principalColumn: "Poliza_ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Poliza_Mercancia_Poliza_Poliza_ID",
                table: "Poliza_Mercancia",
                column: "Poliza_ID",
                principalTable: "Poliza",
                principalColumn: "Poliza_ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
