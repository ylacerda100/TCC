using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TCC.Infra.Data.Migrations
{
    public partial class AjusteItemLoja3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Multiplicador",
                table: "Itens",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<long>(
                name: "QtdXp",
                table: "Itens",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "TipoItem",
                table: "Itens",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "MultiplicadorXp",
                table: "AspNetUsers",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Multiplicador",
                table: "Itens");

            migrationBuilder.DropColumn(
                name: "QtdXp",
                table: "Itens");

            migrationBuilder.DropColumn(
                name: "TipoItem",
                table: "Itens");

            migrationBuilder.DropColumn(
                name: "MultiplicadorXp",
                table: "AspNetUsers");
        }
    }
}
