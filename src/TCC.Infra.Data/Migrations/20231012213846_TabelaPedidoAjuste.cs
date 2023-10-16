using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TCC.Infra.Data.Migrations
{
    public partial class TabelaPedidoAjuste : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PedidoLoja_AspNetUsers_UsuarioId",
                table: "PedidoLoja");

            migrationBuilder.DropForeignKey(
                name: "FK_PedidoLoja_Itens_ItemCompradoId",
                table: "PedidoLoja");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PedidoLoja",
                table: "PedidoLoja");

            migrationBuilder.RenameTable(
                name: "PedidoLoja",
                newName: "Pedidos");

            migrationBuilder.RenameIndex(
                name: "IX_PedidoLoja_UsuarioId",
                table: "Pedidos",
                newName: "IX_Pedidos_UsuarioId");

            migrationBuilder.RenameIndex(
                name: "IX_PedidoLoja_ItemCompradoId",
                table: "Pedidos",
                newName: "IX_Pedidos_ItemCompradoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pedidos",
                table: "Pedidos",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Pedidos_AspNetUsers_UsuarioId",
                table: "Pedidos",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Pedidos_Itens_ItemCompradoId",
                table: "Pedidos",
                column: "ItemCompradoId",
                principalTable: "Itens",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pedidos_AspNetUsers_UsuarioId",
                table: "Pedidos");

            migrationBuilder.DropForeignKey(
                name: "FK_Pedidos_Itens_ItemCompradoId",
                table: "Pedidos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Pedidos",
                table: "Pedidos");

            migrationBuilder.RenameTable(
                name: "Pedidos",
                newName: "PedidoLoja");

            migrationBuilder.RenameIndex(
                name: "IX_Pedidos_UsuarioId",
                table: "PedidoLoja",
                newName: "IX_PedidoLoja_UsuarioId");

            migrationBuilder.RenameIndex(
                name: "IX_Pedidos_ItemCompradoId",
                table: "PedidoLoja",
                newName: "IX_PedidoLoja_ItemCompradoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PedidoLoja",
                table: "PedidoLoja",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PedidoLoja_AspNetUsers_UsuarioId",
                table: "PedidoLoja",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PedidoLoja_Itens_ItemCompradoId",
                table: "PedidoLoja",
                column: "ItemCompradoId",
                principalTable: "Itens",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
