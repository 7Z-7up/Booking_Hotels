using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Booking.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class YourMigrationName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Images",
                table: "Tb_Room");

            migrationBuilder.CreateTable(
                name: "RoomImages",
                columns: table => new
                {
                    RoomId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomImages", x => new { x.RoomId, x.Image });
                    table.ForeignKey(
                        name: "FK_RoomImages_Tb_Room_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Tb_Room",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoomImages");

            migrationBuilder.AddColumn<string>(
                name: "Images",
                table: "Tb_Room",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
