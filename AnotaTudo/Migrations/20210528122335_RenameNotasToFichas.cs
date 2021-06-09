using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AnotaTudo.Migrations
{
    public partial class RenameNotasToFichas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Compras_Notas_NotaId",
                table: "Compras");

            migrationBuilder.DropTable(
                name: "Notas");

            migrationBuilder.DropIndex(
                name: "IX_Compras_NotaId",
                table: "Compras");

            migrationBuilder.AddColumn<int>(
                name: "FichaId",
                table: "Compras",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Fichas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ValorTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fichas", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Compras_FichaId",
                table: "Compras",
                column: "FichaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Compras_Fichas_FichaId",
                table: "Compras",
                column: "FichaId",
                principalTable: "Fichas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Compras_Fichas_FichaId",
                table: "Compras");

            migrationBuilder.DropTable(
                name: "Fichas");

            migrationBuilder.DropIndex(
                name: "IX_Compras_FichaId",
                table: "Compras");

            migrationBuilder.DropColumn(
                name: "FichaId",
                table: "Compras");

            migrationBuilder.CreateTable(
                name: "Notas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ValorTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notas", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Compras_NotaId",
                table: "Compras",
                column: "NotaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Compras_Notas_NotaId",
                table: "Compras",
                column: "NotaId",
                principalTable: "Notas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
