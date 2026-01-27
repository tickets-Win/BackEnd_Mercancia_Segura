using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MercanciaSegura.DOM.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate_v15 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tipo_Persona",
                table: "Cliente");

            migrationBuilder.AddColumn<int>(
                name: "Tipo_Persona_ID",
                table: "Cliente",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Tipo_Persona",
                columns: table => new
                {
                    Tipo_Persona_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tipo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tipo_Persona", x => x.Tipo_Persona_ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cliente_Tipo_Persona_ID",
                table: "Cliente",
                column: "Tipo_Persona_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Cliente_Tipo_Persona_Tipo_Persona_ID",
                table: "Cliente",
                column: "Tipo_Persona_ID",
                principalTable: "Tipo_Persona",
                principalColumn: "Tipo_Persona_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cliente_Tipo_Persona_Tipo_Persona_ID",
                table: "Cliente");

            migrationBuilder.DropTable(
                name: "Tipo_Persona");

            migrationBuilder.DropIndex(
                name: "IX_Cliente_Tipo_Persona_ID",
                table: "Cliente");

            migrationBuilder.DropColumn(
                name: "Tipo_Persona_ID",
                table: "Cliente");

            migrationBuilder.AddColumn<string>(
                name: "Tipo_Persona",
                table: "Cliente",
                type: "nvarchar(1)",
                maxLength: 1,
                nullable: true);
        }
    }
}
