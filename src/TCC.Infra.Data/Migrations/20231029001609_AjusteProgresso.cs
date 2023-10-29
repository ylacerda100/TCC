using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TCC.Infra.Data.Migrations
{
    public partial class AjusteProgresso : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RespostasAlunoExercicios_Progressos_ProgressoAulaId",
                table: "RespostasAlunoExercicios");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProgressoAulaId",
                table: "RespostasAlunoExercicios",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_RespostasAlunoExercicios_Progressos_ProgressoAulaId",
                table: "RespostasAlunoExercicios",
                column: "ProgressoAulaId",
                principalTable: "Progressos",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RespostasAlunoExercicios_Progressos_ProgressoAulaId",
                table: "RespostasAlunoExercicios");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProgressoAulaId",
                table: "RespostasAlunoExercicios",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_RespostasAlunoExercicios_Progressos_ProgressoAulaId",
                table: "RespostasAlunoExercicios",
                column: "ProgressoAulaId",
                principalTable: "Progressos",
                principalColumn: "Id");
        }
    }
}
