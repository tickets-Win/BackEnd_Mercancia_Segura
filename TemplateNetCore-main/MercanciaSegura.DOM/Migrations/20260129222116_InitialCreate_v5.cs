using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MercanciaSegura.DOM.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate_v5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cliente_Creditos",
                columns: table => new
                {
                    Cliente_Creditos_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cliente_ID = table.Column<int>(type: "int", nullable: true),
                    Dias_De_Credito = table.Column<int>(type: "int", nullable: true),
                    Metodo_De_Pago = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Numero_Cuenta = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Limite_De_Credito = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    Dias_De_Pago = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Dias_De_Revision = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Saldo = table.Column<decimal>(type: "decimal(10,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cliente_Creditos", x => x.Cliente_Creditos_ID);
                    table.ForeignKey(
                        name: "FK_Cliente_Creditos_Cliente_Cliente_ID",
                        column: x => x.Cliente_ID,
                        principalTable: "Cliente",
                        principalColumn: "Cliente_ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cliente_Creditos_Cliente_ID",
                table: "Cliente_Creditos",
                column: "Cliente_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cliente_Creditos");
        }
    }
}
