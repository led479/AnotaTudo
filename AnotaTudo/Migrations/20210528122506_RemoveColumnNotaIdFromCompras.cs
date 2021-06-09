using Microsoft.EntityFrameworkCore.Migrations;

namespace AnotaTudo.Migrations
{
    public partial class RemoveColumnNotaIdFromCompras : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Compras_Fichas_FichaId",
                table: "Compras");

            migrationBuilder.DropColumn(
                name: "NotaId",
                table: "Compras");

            migrationBuilder.AlterColumn<int>(
                name: "FichaId",
                table: "Compras",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Compras_Fichas_FichaId",
                table: "Compras",
                column: "FichaId",
                principalTable: "Fichas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Compras_Fichas_FichaId",
                table: "Compras");

            migrationBuilder.AlterColumn<int>(
                name: "FichaId",
                table: "Compras",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "NotaId",
                table: "Compras",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Compras_Fichas_FichaId",
                table: "Compras",
                column: "FichaId",
                principalTable: "Fichas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
