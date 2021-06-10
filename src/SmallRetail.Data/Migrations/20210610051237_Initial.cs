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
                    { new Guid("466e46c6-d313-4554-ac70-acfc0270ab98"), "23765498", new DateTime(2021, 6, 10, 5, 12, 37, 279, DateTimeKind.Utc).AddTicks(1199), new DateTime(2021, 6, 10, 5, 12, 37, 279, DateTimeKind.Utc).AddTicks(1505), "10155509", 52700m },
                    { new Guid("58cf2b4b-ac62-4376-af17-bd619c85e56a"), "71546358", new DateTime(2021, 6, 10, 5, 12, 37, 279, DateTimeKind.Utc).AddTicks(1998), new DateTime(2021, 6, 10, 5, 12, 37, 279, DateTimeKind.Utc).AddTicks(1999), "47562431", 66900m },
                    { new Guid("a775adfe-9fff-4ffb-9886-bb879f27db4b"), "75766147", new DateTime(2021, 6, 10, 5, 12, 37, 279, DateTimeKind.Utc).AddTicks(2053), new DateTime(2021, 6, 10, 5, 12, 37, 279, DateTimeKind.Utc).AddTicks(2054), "43255813", 53600m },
                    { new Guid("a6fee65b-4b59-4996-bd61-a7891e5204ba"), "24963242", new DateTime(2021, 6, 10, 5, 12, 37, 279, DateTimeKind.Utc).AddTicks(2077), new DateTime(2021, 6, 10, 5, 12, 37, 279, DateTimeKind.Utc).AddTicks(2078), "45537160", 1400m },
                    { new Guid("baa252f1-d914-4008-98fd-443a5d3ba8bb"), "74385114", new DateTime(2021, 6, 10, 5, 12, 37, 279, DateTimeKind.Utc).AddTicks(2101), new DateTime(2021, 6, 10, 5, 12, 37, 279, DateTimeKind.Utc).AddTicks(2101), "83797712", 6800m },
                    { new Guid("0a7f8dd3-4093-4f9f-bd00-e5b0baf627d0"), "37803287", new DateTime(2021, 6, 10, 5, 12, 37, 279, DateTimeKind.Utc).AddTicks(2132), new DateTime(2021, 6, 10, 5, 12, 37, 279, DateTimeKind.Utc).AddTicks(2132), "42849798", 96000m },
                    { new Guid("a206fd15-ce59-4963-97b1-c07224ac9c2b"), "44316971", new DateTime(2021, 6, 10, 5, 12, 37, 279, DateTimeKind.Utc).AddTicks(2156), new DateTime(2021, 6, 10, 5, 12, 37, 279, DateTimeKind.Utc).AddTicks(2156), "52574475", 50000m },
                    { new Guid("b2c151db-6d5a-4d3c-8bbc-58e710bad8db"), "20567249", new DateTime(2021, 6, 10, 5, 12, 37, 279, DateTimeKind.Utc).AddTicks(2178), new DateTime(2021, 6, 10, 5, 12, 37, 279, DateTimeKind.Utc).AddTicks(2179), "63825881", 22800m },
                    { new Guid("0cc2c8db-8e96-42cd-9761-a1439ebdfb55"), "30309555", new DateTime(2021, 6, 10, 5, 12, 37, 279, DateTimeKind.Utc).AddTicks(2201), new DateTime(2021, 6, 10, 5, 12, 37, 279, DateTimeKind.Utc).AddTicks(2201), "62448762", 67100m },
                    { new Guid("cbb881cc-cc3f-40ce-afb8-9c0c790f9814"), "89934134", new DateTime(2021, 6, 10, 5, 12, 37, 279, DateTimeKind.Utc).AddTicks(2226), new DateTime(2021, 6, 10, 5, 12, 37, 279, DateTimeKind.Utc).AddTicks(2226), "85360956", 71300m }
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
