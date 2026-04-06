using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MercanciaSegura.DOM.Migrations
{
    /// <inheritdoc />
    public partial class UpdateNameTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Endosos_TipoEndoso_Tipo_Endoso_ID",
                table: "Endosos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TipoEndoso",
                table: "TipoEndoso");

            migrationBuilder.RenameTable(
                name: "TipoEndoso",
                newName: "Tipo_Endoso");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tipo_Endoso",
                table: "Tipo_Endoso",
                column: "Tipo_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Endosos_Tipo_Endoso_Tipo_Endoso_ID",
                table: "Endosos",
                column: "Tipo_Endoso_ID",
                principalTable: "Tipo_Endoso",
                principalColumn: "Tipo_ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Endosos_Tipo_Endoso_Tipo_Endoso_ID",
                table: "Endosos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tipo_Endoso",
                table: "Tipo_Endoso");

            migrationBuilder.RenameTable(
                name: "Tipo_Endoso",
                newName: "TipoEndoso");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TipoEndoso",
                table: "TipoEndoso",
                column: "Tipo_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Endosos_TipoEndoso_Tipo_Endoso_ID",
                table: "Endosos",
                column: "Tipo_Endoso_ID",
                principalTable: "TipoEndoso",
                principalColumn: "Tipo_ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
