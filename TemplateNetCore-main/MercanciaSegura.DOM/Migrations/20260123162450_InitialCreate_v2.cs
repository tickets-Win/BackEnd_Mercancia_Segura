using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MercanciaSegura.DOM.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate_v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RFC_Generico",
                table: "Cliente");

            migrationBuilder.AddColumn<int>(
                name: "RfcGenerico_ID",
                table: "Cliente",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "RfcGenerico",
                columns: table => new
                {
                    RfcGenerico_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tipo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RfcGenerico", x => x.RfcGenerico_ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cliente_RfcGenerico_ID",
                table: "Cliente",
                column: "RfcGenerico_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Cliente_RfcGenerico_RfcGenerico_ID",
                table: "Cliente",
                column: "RfcGenerico_ID",
                principalTable: "RfcGenerico",
                principalColumn: "RfcGenerico_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cliente_RfcGenerico_RfcGenerico_ID",
                table: "Cliente");

            migrationBuilder.DropTable(
                name: "RfcGenerico");

            migrationBuilder.DropIndex(
                name: "IX_Cliente_RfcGenerico_ID",
                table: "Cliente");

            migrationBuilder.DropColumn(
                name: "RfcGenerico_ID",
                table: "Cliente");

            migrationBuilder.AddColumn<string>(
                name: "RFC_Generico",
                table: "Cliente",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: true);
        }
    }
}
