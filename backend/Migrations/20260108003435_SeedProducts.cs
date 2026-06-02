using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TreatTraderApi.Migrations
{
    /// <inheritdoc />
    public partial class SeedProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Price = table.Column<int>(type: "INTEGER", nullable: false),
                    Category = table.Column<string>(type: "TEXT", nullable: false),
                    Desc = table.Column<string>(type: "TEXT", nullable: false),
                    Nutrition = table.Column<string>(type: "TEXT", nullable: false),
                    Img = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false),
                    Phone = table.Column<string>(type: "TEXT", nullable: true),
                    Address = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProductId = table.Column<int>(type: "INTEGER", nullable: false),
                    UserId = table.Column<int>(type: "INTEGER", nullable: true),
                    UserName = table.Column<string>(type: "TEXT", nullable: false),
                    Rating = table.Column<int>(type: "INTEGER", nullable: false),
                    Content = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviews_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reviews_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Category", "Desc", "Img", "Name", "Nutrition", "Price" },
                values: new object[,]
                {
                    { 1, "japan", "來自靜岡的濃郁抹茶香，口感綿密。", "Matcha.jpg", "日本靜岡抹茶蛋糕", "熱量: 350大卡 | 蛋白質: 5g", 350 },
                    { 2, "usa", "美式經典，外酥內軟，巧克力豆爆漿。", "Cookie.jpg", "美國重磅巧克力餅乾", "熱量: 450大卡 | 糖: 30g", 150 },
                    { 3, "france", "法式浪漫，甜而不膩，少女心爆發。", "Macaron.jpg", "法國巴黎馬卡龍", "熱量: 200大卡 | 糖: 40g", 450 },
                    { 4, "uk", "下午茶首選，優雅茶香。", "TeaBiscuit.jpg", "英國皇家伯爵茶餅", "熱量: 150大卡 | 脂肪: 10g", 280 },
                    { 5, "korea", "鹹甜交織，起司控最愛。", "Cheese.jpg", "韓國起司夾心餅", "熱量: 300大卡 | 鈉: 200mg", 120 },
                    { 6, "japan", "北海道經典伴手禮。", "WhiteLover.jpg", "日本白色戀人", "熱量: 250大卡 | 糖: 15g", 480 },
                    { 7, "usa", "重乳酪風味，濃郁化口。", "CheeseCake.jpg", "紐約起司蛋糕", "熱量: 400大卡 | 脂肪: 25g", 320 },
                    { 8, "france", "酸甜清爽，法式甜點代表。", "LemonTart.jpg", "法國檸檬塔", "熱量: 220大卡 | 糖: 18g", 160 },
                    { 9, "germany", "酒漬櫻桃與巧克力的完美結合。", "Cake.jpg", "德國黑森林蛋糕", "熱量: 450大卡 | 酒精: 微量", 500 },
                    { 10, "italy", "帶我走，享受微醺的咖啡香。", "Tiramisu.jpg", "義大利提拉米蘇", "熱量: 380大卡 | 咖啡因: 有", 380 },
                    { 11, "belgium", "頂級可可，絲滑柔順。", "Choco.jpg", "比利時巧克力", "熱量: 550大卡 | 可可脂: 高", 250 },
                    { 12, "taiwan", "在地土鳳梨，酸甜適中。", "Pineapple.jpg", "台灣鳳梨酥", "熱量: 180大卡 | 纖維: 2g", 200 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ProductId",
                table: "Reviews",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_UserId",
                table: "Reviews",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
