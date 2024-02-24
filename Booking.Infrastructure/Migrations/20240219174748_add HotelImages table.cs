using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Booking.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addHotelImagestable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Images",
                table: "Tb_Hotel");

            migrationBuilder.CreateTable(
                name: "Tb_HotelImages",
                columns: table => new
                {
                    hotelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tb_HotelImages", x => new { x.hotelId, x.Image });
                    table.ForeignKey(
                        name: "FK_Tb_HotelImages_Tb_Hotel_hotelId",
                        column: x => x.hotelId,
                        principalTable: "Tb_Hotel",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tb_HotelImages");

            migrationBuilder.AddColumn<string>(
                name: "Images",
                table: "Tb_Hotel",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
