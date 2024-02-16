using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Booking.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tb_Company",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalProfits = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tb_Company", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Tb_Customer",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tb_Customer", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Tb_Hotel",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Rate = table.Column<double>(type: "float", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Images = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tb_Hotel", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Tb_Hotel_Tb_Company_CompId",
                        column: x => x.CompId,
                        principalTable: "Tb_Company",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Tb_Order",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TotalCost = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    PaymentType = table.Column<int>(type: "int", nullable: false),
                    CustomerID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tb_Order", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Tb_Order_Tb_Customer_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "Tb_Customer",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Tb_Room",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Number = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    Taken = table.Column<bool>(type: "bit", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Images = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HotelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tb_Room", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Tb_Room_Tb_Hotel_HotelId",
                        column: x => x.HotelId,
                        principalTable: "Tb_Hotel",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "RoomOrders",
                columns: table => new
                {
                    RoomID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Start_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    End_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomOrders", x => new { x.OrderID, x.RoomID });
                    table.ForeignKey(
                        name: "FK_RoomOrders_Tb_Order_OrderID",
                        column: x => x.OrderID,
                        principalTable: "Tb_Order",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoomOrders_Tb_Room_RoomID",
                        column: x => x.RoomID,
                        principalTable: "Tb_Room",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoomOrders_RoomID",
                table: "RoomOrders",
                column: "RoomID");

            migrationBuilder.CreateIndex(
                name: "IX_Tb_Hotel_CompId",
                table: "Tb_Hotel",
                column: "CompId");

            migrationBuilder.CreateIndex(
                name: "IX_Tb_Order_CustomerID",
                table: "Tb_Order",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_Tb_Room_HotelId",
                table: "Tb_Room",
                column: "HotelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoomOrders");

            migrationBuilder.DropTable(
                name: "Tb_Order");

            migrationBuilder.DropTable(
                name: "Tb_Room");

            migrationBuilder.DropTable(
                name: "Tb_Customer");

            migrationBuilder.DropTable(
                name: "Tb_Hotel");

            migrationBuilder.DropTable(
                name: "Tb_Company");
        }
    }
}
