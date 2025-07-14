using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PizzaOrder.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitPizzaSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AddressLine1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddressLine2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MobileNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OrderStatus = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PizzaDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Topping = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderDetailId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PizzaDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PizzaDetails_OrderDetails_OrderDetailId",
                        column: x => x.OrderDetailId,
                        principalTable: "OrderDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "OrderDetails",
                columns: new[] { "Id", "AddressLine1", "AddressLine2", "Amount", "Date", "MobileNo", "OrderStatus" },
                values: new object[] { 1, "123 Pizza Street", "Apt 2B", 1200, new DateTime(2025, 7, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "1234567890", "InProgress" });

            migrationBuilder.InsertData(
                table: "PizzaDetails",
                columns: new[] { "Id", "Description", "OrderDetailId", "Topping" },
                values: new object[,]
                {
                    { 1, "Pepperoni Pizza", 1, "Sausage" },
                    { 2, "Cheesy Delight", 1, "ExtraCheese" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_PizzaDetails_OrderDetailId",
                table: "PizzaDetails",
                column: "OrderDetailId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PizzaDetails");

            migrationBuilder.DropTable(
                name: "OrderDetails");
        }
    }
}
