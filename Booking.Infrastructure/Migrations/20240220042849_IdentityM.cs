using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Booking.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class IdentityM : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tb_Company_AspNetUsers_ID",
                table: "Tb_Company");

            migrationBuilder.DropForeignKey(
                name: "FK_Tb_Customer_AspNetUsers_ID",
                table: "Tb_Customer");

            migrationBuilder.AddForeignKey(
                name: "FK_Tb_Company_AspNetUsers_ID",
                table: "Tb_Company",
                column: "ID",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tb_Customer_AspNetUsers_ID",
                table: "Tb_Customer",
                column: "ID",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tb_Company_AspNetUsers_ID",
                table: "Tb_Company");

            migrationBuilder.DropForeignKey(
                name: "FK_Tb_Customer_AspNetUsers_ID",
                table: "Tb_Customer");

            migrationBuilder.AddForeignKey(
                name: "FK_Tb_Company_AspNetUsers_ID",
                table: "Tb_Company",
                column: "ID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tb_Customer_AspNetUsers_ID",
                table: "Tb_Customer",
                column: "ID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
