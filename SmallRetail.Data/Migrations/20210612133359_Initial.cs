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
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Username = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

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
                    { new Guid("df0b77b8-50cd-4f1f-9a9f-0f08fc16dc1f"), "615960672", new DateTime(2018, 10, 7, 16, 1, 14, 0, DateTimeKind.Local).AddTicks(5348), new DateTime(2021, 2, 24, 21, 55, 34, 50, DateTimeKind.Local).AddTicks(603), "voluptas", 7600m },
                    { new Guid("6acbd74f-2935-47ec-9286-0e1488674804"), "921334996", new DateTime(2019, 6, 17, 14, 35, 1, 642, DateTimeKind.Local).AddTicks(7667), new DateTime(2020, 6, 20, 7, 16, 39, 448, DateTimeKind.Local).AddTicks(5469), "et", 1800m },
                    { new Guid("28ed9a3e-135a-4650-a614-5c45959e1334"), "535804743", new DateTime(2019, 8, 14, 10, 47, 44, 769, DateTimeKind.Local).AddTicks(4147), new DateTime(2021, 5, 28, 18, 25, 49, 512, DateTimeKind.Local).AddTicks(1219), "autem", 2800m },
                    { new Guid("455cb7eb-c61f-4b55-9e49-0ab61caabdfc"), "836519107", new DateTime(2018, 12, 27, 17, 16, 16, 420, DateTimeKind.Local).AddTicks(156), new DateTime(2019, 11, 13, 0, 5, 59, 833, DateTimeKind.Local).AddTicks(9399), "commodi", 9900m },
                    { new Guid("dbf08abb-cf54-41c1-b712-424a30e355ec"), "324923215", new DateTime(2020, 3, 4, 22, 58, 41, 394, DateTimeKind.Local).AddTicks(2914), new DateTime(2020, 12, 12, 18, 59, 51, 73, DateTimeKind.Local).AddTicks(2207), "voluptatem", 2600m },
                    { new Guid("6d28c1b9-91c6-4d77-89e0-2e2b0b29593b"), "317688636", new DateTime(2021, 1, 26, 19, 27, 28, 352, DateTimeKind.Local).AddTicks(2694), new DateTime(2019, 10, 24, 21, 10, 36, 444, DateTimeKind.Local).AddTicks(3571), "provident", 3200m },
                    { new Guid("0863e056-3d15-4177-97b8-9f9c4656163a"), "254795921", new DateTime(2020, 6, 4, 23, 47, 40, 984, DateTimeKind.Local).AddTicks(7181), new DateTime(2021, 2, 21, 12, 11, 33, 102, DateTimeKind.Local).AddTicks(5800), "natus", 9000m },
                    { new Guid("e2de859d-f000-4ff3-8c85-8dcf8b8c4314"), "938203186", new DateTime(2021, 1, 7, 11, 54, 39, 821, DateTimeKind.Local).AddTicks(6597), new DateTime(2020, 7, 1, 21, 3, 13, 937, DateTimeKind.Local).AddTicks(1327), "sit", 2500m },
                    { new Guid("fe3529f7-2bd8-4ede-8799-1ac217e5bab0"), "114661841", new DateTime(2019, 2, 5, 8, 46, 49, 955, DateTimeKind.Local).AddTicks(6914), new DateTime(2020, 10, 21, 16, 31, 38, 438, DateTimeKind.Local).AddTicks(5541), "omnis", 6800m },
                    { new Guid("9069920b-fd4d-4e20-998e-dcaa3db96839"), "749158279", new DateTime(2020, 3, 30, 7, 53, 37, 363, DateTimeKind.Local).AddTicks(5173), new DateTime(2020, 7, 2, 9, 44, 15, 940, DateTimeKind.Local).AddTicks(5083), "autem", 4800m }
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
