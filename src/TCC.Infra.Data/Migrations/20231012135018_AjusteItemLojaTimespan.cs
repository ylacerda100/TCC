using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TCC.Infra.Data.Migrations
{
    public partial class AjusteItemLojaTimespan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Duracao",
                table: "Itens");

            migrationBuilder.AddColumn<long>(
                name: "Duracao",
                table: "Itens",
                type: "bigint",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<TimeSpan>(
                name: "Duracao",
                table: "Itens",
                type: "time",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");
        }
    }
}
