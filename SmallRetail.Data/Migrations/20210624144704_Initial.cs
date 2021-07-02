using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SmallRetail.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Barcode = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table => table.PrimaryKey("PK_Products", x => x.Id));

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table => table.PrimaryKey("PK_Transactions", x => x.Id));

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Username = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Type = table.Column<string>(type: "varchar(10)", nullable: false, defaultValue: "User"),
                    DateCreated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table => table.PrimaryKey("PK_Users", x => x.Id));

            migrationBuilder.CreateTable(
                name: "TransactionProducts",
                columns: table => new
                {
                    TransactionId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionProducts", x => new { x.TransactionId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_TransactionProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TransactionProducts_Transactions_TransactionId",
                        column: x => x.TransactionId,
                        principalTable: "Transactions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Barcode", "DateCreated", "DateUpdated", "Name", "Price" },
                values: new object[,]
                {
                    { new Guid("23b610d0-3dfc-411b-9760-c496fc186568"), "016343470", new DateTime(2019, 6, 11, 1, 21, 21, 263, DateTimeKind.Local).AddTicks(8314), new DateTime(2019, 11, 24, 22, 4, 44, 637, DateTimeKind.Local).AddTicks(6381), "possimus", 6200m },
                    { new Guid("e05d1bee-fd95-4fd8-8d60-5d27a7eca451"), "980273337", new DateTime(2020, 7, 20, 7, 28, 49, 981, DateTimeKind.Local).AddTicks(1506), new DateTime(2020, 1, 30, 20, 43, 45, 520, DateTimeKind.Local).AddTicks(7011), "autem", 200m },
                    { new Guid("ba7b0648-9227-4ebf-9e64-7c5a6f7bdac6"), "697819469", new DateTime(2020, 11, 13, 1, 58, 45, 573, DateTimeKind.Local).AddTicks(9172), new DateTime(2020, 10, 24, 3, 0, 45, 165, DateTimeKind.Local).AddTicks(3315), "nam", 500m },
                    { new Guid("2867ba66-b7c3-493f-b146-7d96d2a3459f"), "684942884", new DateTime(2020, 12, 7, 13, 32, 33, 974, DateTimeKind.Local).AddTicks(6594), new DateTime(2019, 11, 4, 10, 29, 32, 764, DateTimeKind.Local).AddTicks(2113), "eius", 2600m },
                    { new Guid("6757822f-52e0-4f11-b821-174e77546244"), "391276076", new DateTime(2020, 11, 5, 22, 46, 19, 613, DateTimeKind.Local).AddTicks(9734), new DateTime(2020, 5, 13, 5, 43, 18, 657, DateTimeKind.Local).AddTicks(1986), "laudantium", 9700m },
                    { new Guid("b7567fb0-ec4a-4852-8784-00d08baa44bb"), "621919046", new DateTime(2019, 2, 21, 21, 49, 31, 878, DateTimeKind.Local).AddTicks(834), new DateTime(2019, 8, 28, 5, 52, 54, 570, DateTimeKind.Local).AddTicks(6343), "non", 9400m },
                    { new Guid("00406db7-e859-4fd4-ba99-f75697a312fc"), "748274736", new DateTime(2019, 4, 17, 2, 11, 56, 356, DateTimeKind.Local).AddTicks(8480), new DateTime(2019, 8, 17, 13, 15, 59, 927, DateTimeKind.Local).AddTicks(3556), "deleniti", 4500m },
                    { new Guid("f5289b5d-ad7f-4158-ad48-9ee4c89cdd8f"), "415934795", new DateTime(2020, 2, 3, 17, 16, 11, 534, DateTimeKind.Local).AddTicks(5705), new DateTime(2021, 5, 5, 18, 2, 41, 89, DateTimeKind.Local).AddTicks(9504), "sunt", 6800m },
                    { new Guid("1a8a5572-2fc4-4fa5-a20d-dfd80cb44594"), "313642488", new DateTime(2020, 3, 31, 19, 35, 10, 891, DateTimeKind.Local).AddTicks(5166), new DateTime(2020, 1, 19, 5, 27, 25, 366, DateTimeKind.Local).AddTicks(3445), "cupiditate", 9200m },
                    { new Guid("e8289523-1704-4f85-a208-1cab83e29f8f"), "551076225", new DateTime(2019, 7, 16, 18, 51, 12, 983, DateTimeKind.Local).AddTicks(3561), new DateTime(2021, 2, 12, 11, 50, 0, 241, DateTimeKind.Local).AddTicks(2862), "aut", 8800m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_TransactionProducts_ProductId",
                table: "TransactionProducts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TransactionProducts");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Transactions");
        }
    }
}
