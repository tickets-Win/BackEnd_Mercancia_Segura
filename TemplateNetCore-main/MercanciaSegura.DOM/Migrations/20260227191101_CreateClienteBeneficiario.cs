using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MercanciaSegura.DOM.Migrations
{
    /// <inheritdoc />
    public partial class CreateClienteBeneficiario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Beneficiario_Preferente_Cliente_Cliente_ID",
                table: "Beneficiario_Preferente");

            migrationBuilder.RenameColumn(
                name: "Cliente_ID",
                table: "Beneficiario_Preferente",
                newName: "Tipo_Persona_ID");

            migrationBuilder.RenameIndex(
                name: "IX_Beneficiario_Preferente_Cliente_ID",
                table: "Beneficiario_Preferente",
                newName: "IX_Beneficiario_Preferente_Tipo_Persona_ID");

            migrationBuilder.AddColumn<string>(
                name: "Apellido_Materno",
                table: "Beneficiario_Preferente",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Apellido_Paterno",
                table: "Beneficiario_Preferente",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CP",
                table: "Beneficiario_Preferente",
                type: "nvarchar(5)",
                maxLength: 5,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Calle",
                table: "Beneficiario_Preferente",
                type: "nvarchar(120)",
                maxLength: 120,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Clave",
                table: "Beneficiario_Preferente",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Colonia",
                table: "Beneficiario_Preferente",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Fecha_Actualizacion",
                table: "Beneficiario_Preferente",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Fecha_Registro",
                table: "Beneficiario_Preferente",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Nacionalidad",
                table: "Beneficiario_Preferente",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Nombre_Completo",
                table: "Beneficiario_Preferente",
                type: "nvarchar(120)",
                maxLength: 120,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Numero_Ext",
                table: "Beneficiario_Preferente",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Numero_Int",
                table: "Beneficiario_Preferente",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Pais",
                table: "Beneficiario_Preferente",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Poblacion",
                table: "Beneficiario_Preferente",
                type: "nvarchar(80)",
                maxLength: 80,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RfcGenerico_ID",
                table: "Beneficiario_Preferente",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Cliente_Beneficiario",
                columns: table => new
                {
                    Cliente_Beneficiario_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cliente_ID = table.Column<int>(type: "int", nullable: true),
                    Beneficiario_Preferente_ID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cliente_Beneficiario", x => x.Cliente_Beneficiario_ID);
                    table.ForeignKey(
                        name: "FK_Cliente_Beneficiario_Beneficiario_Preferente_Beneficiario_Preferente_ID",
                        column: x => x.Beneficiario_Preferente_ID,
                        principalTable: "Beneficiario_Preferente",
                        principalColumn: "Beneficiario_Preferente_ID");
                    table.ForeignKey(
                        name: "FK_Cliente_Beneficiario_Cliente_Cliente_ID",
                        column: x => x.Cliente_ID,
                        principalTable: "Cliente",
                        principalColumn: "Cliente_ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Beneficiario_Preferente_RfcGenerico_ID",
                table: "Beneficiario_Preferente",
                column: "RfcGenerico_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Cliente_Beneficiario_Beneficiario_Preferente_ID",
                table: "Cliente_Beneficiario",
                column: "Beneficiario_Preferente_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Cliente_Beneficiario_Cliente_ID",
                table: "Cliente_Beneficiario",
                column: "Cliente_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Beneficiario_Preferente_RfcGenerico_RfcGenerico_ID",
                table: "Beneficiario_Preferente",
                column: "RfcGenerico_ID",
                principalTable: "RfcGenerico",
                principalColumn: "RfcGenerico_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Beneficiario_Preferente_Tipo_Persona_Tipo_Persona_ID",
                table: "Beneficiario_Preferente",
                column: "Tipo_Persona_ID",
                principalTable: "Tipo_Persona",
                principalColumn: "Tipo_Persona_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Beneficiario_Preferente_RfcGenerico_RfcGenerico_ID",
                table: "Beneficiario_Preferente");

            migrationBuilder.DropForeignKey(
                name: "FK_Beneficiario_Preferente_Tipo_Persona_Tipo_Persona_ID",
                table: "Beneficiario_Preferente");

            migrationBuilder.DropTable(
                name: "Cliente_Beneficiario");

            migrationBuilder.DropIndex(
                name: "IX_Beneficiario_Preferente_RfcGenerico_ID",
                table: "Beneficiario_Preferente");

            migrationBuilder.DropColumn(
                name: "Apellido_Materno",
                table: "Beneficiario_Preferente");

            migrationBuilder.DropColumn(
                name: "Apellido_Paterno",
                table: "Beneficiario_Preferente");

            migrationBuilder.DropColumn(
                name: "CP",
                table: "Beneficiario_Preferente");

            migrationBuilder.DropColumn(
                name: "Calle",
                table: "Beneficiario_Preferente");

            migrationBuilder.DropColumn(
                name: "Clave",
                table: "Beneficiario_Preferente");

            migrationBuilder.DropColumn(
                name: "Colonia",
                table: "Beneficiario_Preferente");

            migrationBuilder.DropColumn(
                name: "Fecha_Actualizacion",
                table: "Beneficiario_Preferente");

            migrationBuilder.DropColumn(
                name: "Fecha_Registro",
                table: "Beneficiario_Preferente");

            migrationBuilder.DropColumn(
                name: "Nacionalidad",
                table: "Beneficiario_Preferente");

            migrationBuilder.DropColumn(
                name: "Nombre_Completo",
                table: "Beneficiario_Preferente");

            migrationBuilder.DropColumn(
                name: "Numero_Ext",
                table: "Beneficiario_Preferente");

            migrationBuilder.DropColumn(
                name: "Numero_Int",
                table: "Beneficiario_Preferente");

            migrationBuilder.DropColumn(
                name: "Pais",
                table: "Beneficiario_Preferente");

            migrationBuilder.DropColumn(
                name: "Poblacion",
                table: "Beneficiario_Preferente");

            migrationBuilder.DropColumn(
                name: "RfcGenerico_ID",
                table: "Beneficiario_Preferente");

            migrationBuilder.RenameColumn(
                name: "Tipo_Persona_ID",
                table: "Beneficiario_Preferente",
                newName: "Cliente_ID");

            migrationBuilder.RenameIndex(
                name: "IX_Beneficiario_Preferente_Tipo_Persona_ID",
                table: "Beneficiario_Preferente",
                newName: "IX_Beneficiario_Preferente_Cliente_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Beneficiario_Preferente_Cliente_Cliente_ID",
                table: "Beneficiario_Preferente",
                column: "Cliente_ID",
                principalTable: "Cliente",
                principalColumn: "Cliente_ID");
        }
    }
}
