using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TCC.Infra.Data.Migrations
{
    public partial class Melhoria_Progresso_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Aulas_Cursos_CursoId",
                table: "Aulas");

            migrationBuilder.AddColumn<Guid>(
                name: "CursoId",
                table: "Progressos",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<Guid>(
                name: "CursoId",
                table: "Aulas",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "RespostasAlunoExercicios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExercicioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Resposta = table.Column<string>(type: "varchar(max)", nullable: false),
                    ProgressoAulaId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RespostasAlunoExercicios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RespostasAlunoExercicios_AspNetUsers_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RespostasAlunoExercicios_Exercicios_ExercicioId",
                        column: x => x.ExercicioId,
                        principalTable: "Exercicios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RespostasAlunoExercicios_Progressos_ProgressoAulaId",
                        column: x => x.ProgressoAulaId,
                        principalTable: "Progressos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Progressos_CursoId",
                table: "Progressos",
                column: "CursoId");

            migrationBuilder.CreateIndex(
                name: "IX_RespostasAlunoExercicios_ExercicioId",
                table: "RespostasAlunoExercicios",
                column: "ExercicioId");

            migrationBuilder.CreateIndex(
                name: "IX_RespostasAlunoExercicios_ProgressoAulaId",
                table: "RespostasAlunoExercicios",
                column: "ProgressoAulaId");

            migrationBuilder.CreateIndex(
                name: "IX_RespostasAlunoExercicios_UsuarioId",
                table: "RespostasAlunoExercicios",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Aulas_Cursos_CursoId",
                table: "Aulas",
                column: "CursoId",
                principalTable: "Cursos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Progressos_Cursos_CursoId",
                table: "Progressos",
                column: "CursoId",
                principalTable: "Cursos",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Aulas_Cursos_CursoId",
                table: "Aulas");

            migrationBuilder.DropForeignKey(
                name: "FK_Progressos_Cursos_CursoId",
                table: "Progressos");

            migrationBuilder.DropTable(
                name: "RespostasAlunoExercicios");

            migrationBuilder.DropIndex(
                name: "IX_Progressos_CursoId",
                table: "Progressos");

            migrationBuilder.DropColumn(
                name: "CursoId",
                table: "Progressos");

            migrationBuilder.AlterColumn<Guid>(
                name: "CursoId",
                table: "Aulas",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Aulas_Cursos_CursoId",
                table: "Aulas",
                column: "CursoId",
                principalTable: "Cursos",
                principalColumn: "Id");
        }
    }
}
