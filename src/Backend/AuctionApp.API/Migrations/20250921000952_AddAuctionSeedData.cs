using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AuctionApp.API.Migrations
{
    /// <inheritdoc />
    public partial class AddAuctionSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Auctions",
                columns: new[] { "Id", "Description", "EndDate", "ImageUrl", "SellerId", "StartDate", "StartingPrice", "Title", "WinningBidId" },
                values: new object[,]
                {
                    { 1, "Teléfono en perfecto estado, solo 6 meses de uso. Incluye cargador original, auriculares y caja.", new DateTime(2025, 9, 27, 19, 9, 52, 194, DateTimeKind.Local).AddTicks(3478), "https://images.unsplash.com/photo-1632661674596-df8be070a5c5", 1, new DateTime(2025, 9, 20, 19, 9, 52, 194, DateTimeKind.Local).AddTicks(3468), 699.99m, "iPhone 13 Pro Max - Como nuevo", null },
                    { 2, "Consola PS5 Digital Edition en perfecto estado. Incluye 2 mandos DualSense y 3 juegos digitales.", new DateTime(2025, 9, 25, 19, 9, 52, 194, DateTimeKind.Local).AddTicks(3489), "https://images.unsplash.com/photo-1606813907291-d86efa9b94db", 1, new DateTime(2025, 9, 20, 19, 9, 52, 194, DateTimeKind.Local).AddTicks(3488), 450m, "PlayStation 5 Digital Edition", null },
                    { 3, "Bicicleta de montaña Trek Marlin 7 talla M. Poco uso, en excelentes condiciones. Ideal para rutas de montaña.", new DateTime(2025, 9, 30, 19, 9, 52, 194, DateTimeKind.Local).AddTicks(3491), "https://images.unsplash.com/photo-1485965120184-e220f721d03e", 2, new DateTime(2025, 9, 20, 19, 9, 52, 194, DateTimeKind.Local).AddTicks(3491), 850m, "Bicicleta de montaña Trek Marlin 7", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Auctions",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Auctions",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Auctions",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
