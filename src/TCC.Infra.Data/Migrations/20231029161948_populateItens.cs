using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TCC.Infra.Data.Migrations
{
    public partial class populateItens : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Itens",
                columns: new[] { "Id", "Descricao", "Duracao", "ImagemUrl", "Multiplicador", "Nome", "Preco", "QtdXp", "TipoItem" },
                values: new object[,]
                {
                    { new Guid("40e76097-4f16-4015-bd96-fbd261420721"), "Boost de XP de 50%", 2592000000000L, "50-XP.png", 0.50m, "Boost de XP", 400, 0L, 1 },
                    { new Guid("6a12b8ec-b131-4646-a696-cc450543cacb"), "Receba 1000 em XP", 0L, "1000-XP.png", 0m, "Pacote de XP", 1800, 1000L, 2 },
                    { new Guid("9b111811-8bfb-45c7-b5c4-ac4be98d4459"), "Receba 250 em XP", 0L, "250-XP.png", 0m, "Pacote de XP", 450, 250L, 2 },
                    { new Guid("b1270d2c-b633-4ae4-bdfc-bc45521dd099"), "Boost de XP de 25%", 2592000000000L, "25-XP.png", 0.25m, "Boost de XP", 300, 0L, 1 },
                    { new Guid("b84bf61a-d7a9-44b2-91b0-53c75baa815a"), "Boost de XP de 75%", 2592000000000L, "75-XP.png", 0.75m, "Boost de XP", 500, 0L, 1 },
                    { new Guid("d7cb2122-27d9-4cca-92f5-3247ab268811"), "Receba 500 em XP", 0L, "500-XP.png", 0m, "Pacote de XP", 900, 500L, 2 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Itens",
                keyColumn: "Id",
                keyValue: new Guid("40e76097-4f16-4015-bd96-fbd261420721"));

            migrationBuilder.DeleteData(
                table: "Itens",
                keyColumn: "Id",
                keyValue: new Guid("6a12b8ec-b131-4646-a696-cc450543cacb"));

            migrationBuilder.DeleteData(
                table: "Itens",
                keyColumn: "Id",
                keyValue: new Guid("9b111811-8bfb-45c7-b5c4-ac4be98d4459"));

            migrationBuilder.DeleteData(
                table: "Itens",
                keyColumn: "Id",
                keyValue: new Guid("b1270d2c-b633-4ae4-bdfc-bc45521dd099"));

            migrationBuilder.DeleteData(
                table: "Itens",
                keyColumn: "Id",
                keyValue: new Guid("b84bf61a-d7a9-44b2-91b0-53c75baa815a"));

            migrationBuilder.DeleteData(
                table: "Itens",
                keyColumn: "Id",
                keyValue: new Guid("d7cb2122-27d9-4cca-92f5-3247ab268811"));
        }
    }
}
