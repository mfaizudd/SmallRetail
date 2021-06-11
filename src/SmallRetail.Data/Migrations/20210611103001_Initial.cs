using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

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
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
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
                    TransactionId = table.Column<int>(type: "integer", nullable: false),
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
                    { new Guid("243e5424-d4b5-4591-8cac-eeec11001a6b"), "658265231", new DateTime(2018, 7, 25, 18, 30, 7, 596, DateTimeKind.Local).AddTicks(7894), new DateTime(2020, 5, 19, 17, 13, 44, 105, DateTimeKind.Local).AddTicks(383), "et", 1500m },
                    { new Guid("b86c8736-1e77-4aae-bcf2-e94a4bb73b33"), "894631317", new DateTime(2020, 7, 24, 11, 50, 29, 632, DateTimeKind.Local).AddTicks(2057), new DateTime(2019, 8, 6, 12, 18, 56, 133, DateTimeKind.Local).AddTicks(7548), "exercitationem", 3800m },
                    { new Guid("d18744ed-3d94-464a-b23c-adc290f449f4"), "334928920", new DateTime(2019, 6, 6, 20, 34, 44, 432, DateTimeKind.Local).AddTicks(535), new DateTime(2020, 3, 5, 21, 48, 50, 338, DateTimeKind.Local).AddTicks(561), "harum", 8700m },
                    { new Guid("fce70afe-ca9e-4fbf-8a81-c3076cc4506a"), "255223381", new DateTime(2019, 5, 12, 1, 41, 42, 561, DateTimeKind.Local).AddTicks(6589), new DateTime(2019, 8, 22, 19, 39, 5, 15, DateTimeKind.Local).AddTicks(1693), "rem", 9300m },
                    { new Guid("618fb69e-a2bd-46b4-a3fb-3f7a61d7038f"), "048824971", new DateTime(2020, 11, 2, 2, 7, 58, 959, DateTimeKind.Local).AddTicks(4718), new DateTime(2020, 3, 16, 17, 2, 23, 316, DateTimeKind.Local).AddTicks(9403), "rerum", 8900m },
                    { new Guid("91bc0ed7-6ce0-4363-a77e-f1e96df11f69"), "392835968", new DateTime(2019, 11, 11, 12, 19, 9, 742, DateTimeKind.Local).AddTicks(6465), new DateTime(2021, 5, 28, 12, 40, 32, 983, DateTimeKind.Local).AddTicks(5648), "autem", 5700m },
                    { new Guid("4033c891-3cb8-41bb-9850-4def82393423"), "090184237", new DateTime(2020, 11, 13, 12, 0, 34, 859, DateTimeKind.Local).AddTicks(3005), new DateTime(2020, 9, 11, 9, 11, 54, 641, DateTimeKind.Local).AddTicks(9296), "corrupti", 4600m },
                    { new Guid("632b77ba-c9c5-46ee-9a04-347c9c09f7d8"), "368056060", new DateTime(2020, 5, 31, 18, 53, 54, 780, DateTimeKind.Local).AddTicks(5652), new DateTime(2021, 2, 12, 4, 12, 20, 33, DateTimeKind.Local).AddTicks(4476), "ea", 6100m },
                    { new Guid("6be3b8b6-1e53-4fff-b521-e14db68809d8"), "472162324", new DateTime(2018, 6, 18, 12, 27, 42, 952, DateTimeKind.Local).AddTicks(709), new DateTime(2021, 4, 7, 11, 12, 9, 269, DateTimeKind.Local).AddTicks(5187), "aperiam", 6600m },
                    { new Guid("fdc6cf6b-f524-48fb-a5fd-5a7f099d11bc"), "492557691", new DateTime(2019, 1, 7, 10, 12, 6, 730, DateTimeKind.Local).AddTicks(1406), new DateTime(2021, 1, 14, 18, 36, 52, 67, DateTimeKind.Local).AddTicks(6763), "quis", 7800m }
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
