using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ConfiguredUserShowtimeSeatRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "ShowtimeSeats",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ShowtimeSeats_UserId",
                table: "ShowtimeSeats",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShowtimeSeats_AspNetUsers_UserId",
                table: "ShowtimeSeats",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShowtimeSeats_AspNetUsers_UserId",
                table: "ShowtimeSeats");

            migrationBuilder.DropIndex(
                name: "IX_ShowtimeSeats_UserId",
                table: "ShowtimeSeats");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ShowtimeSeats");
        }
    }
}
