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
                    Password = table.Column<string>(type: "text", nullable: false)
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
                    { new Guid("448c3aab-95e6-4037-bfcb-5cd23b7710aa"), "116348357", new DateTime(2019, 7, 24, 2, 23, 45, 640, DateTimeKind.Local).AddTicks(6884), new DateTime(2019, 8, 9, 13, 57, 10, 532, DateTimeKind.Local).AddTicks(577), "unde", 6700m },
                    { new Guid("7a3981fd-b74f-4375-b263-6d8fda08ff27"), "532134310", new DateTime(2019, 1, 18, 22, 43, 25, 962, DateTimeKind.Local).AddTicks(3125), new DateTime(2019, 10, 30, 16, 9, 44, 391, DateTimeKind.Local).AddTicks(6147), "id", 9700m },
                    { new Guid("1f527df6-0524-4d33-aaa0-3281f9341555"), "427422610", new DateTime(2020, 1, 31, 12, 58, 29, 403, DateTimeKind.Local).AddTicks(1309), new DateTime(2021, 5, 8, 10, 28, 55, 715, DateTimeKind.Local).AddTicks(4100), "quis", 7200m },
                    { new Guid("0ab77afd-92b2-4eac-a90c-ef97bf3c657e"), "304610872", new DateTime(2020, 1, 19, 8, 29, 49, 38, DateTimeKind.Local).AddTicks(1833), new DateTime(2020, 8, 8, 12, 1, 8, 631, DateTimeKind.Local).AddTicks(3742), "qui", 5100m },
                    { new Guid("dd05afc0-01d8-4546-93f0-466930e05a10"), "437036563", new DateTime(2020, 8, 10, 1, 7, 20, 341, DateTimeKind.Local).AddTicks(4618), new DateTime(2020, 10, 12, 0, 24, 58, 883, DateTimeKind.Local).AddTicks(5502), "architecto", 7900m },
                    { new Guid("3d24f21e-bb45-4b06-b0a4-6f1b0aa1f310"), "307505720", new DateTime(2020, 5, 1, 11, 57, 8, 46, DateTimeKind.Local).AddTicks(9326), new DateTime(2020, 3, 12, 4, 6, 10, 284, DateTimeKind.Local).AddTicks(2390), "commodi", 7500m },
                    { new Guid("30d1e721-b2bb-43a3-bac6-7a8e1e8f816f"), "990282977", new DateTime(2018, 7, 3, 10, 10, 21, 905, DateTimeKind.Local).AddTicks(5407), new DateTime(2019, 8, 11, 12, 28, 57, 237, DateTimeKind.Local).AddTicks(264), "modi", 4400m },
                    { new Guid("7478ce65-87c9-4be0-97cd-e0871987f8f5"), "083096376", new DateTime(2019, 3, 6, 18, 59, 18, 822, DateTimeKind.Local).AddTicks(5351), new DateTime(2020, 10, 6, 6, 54, 27, 368, DateTimeKind.Local).AddTicks(4534), "eos", 9800m },
                    { new Guid("8cd16b00-20d9-4e02-837b-6f19280db3a6"), "971409776", new DateTime(2020, 12, 6, 20, 20, 41, 905, DateTimeKind.Local).AddTicks(6523), new DateTime(2021, 1, 3, 1, 25, 39, 818, DateTimeKind.Local).AddTicks(5226), "expedita", 5800m },
                    { new Guid("108d65eb-4df7-4842-b4a9-889ef403b58c"), "525572514", new DateTime(2021, 3, 21, 6, 0, 22, 133, DateTimeKind.Local).AddTicks(371), new DateTime(2020, 8, 7, 20, 49, 49, 10, DateTimeKind.Local).AddTicks(3649), "voluptas", 8700m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_TransactionProducts_ProductId",
                table: "TransactionProducts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username_Password",
                table: "Users",
                columns: new[] { "Username", "Password" },
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
