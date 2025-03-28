using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ShowtimeSeatReservationEntityAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShowtimeSeats_AspNetUsers_UserId",
                table: "ShowtimeSeats");

            migrationBuilder.DropIndex(
                name: "IX_ShowtimeSeats_ShowtimeId_SeatNumber",
                table: "ShowtimeSeats");

            migrationBuilder.DropIndex(
                name: "IX_ShowtimeSeats_UserId",
                table: "ShowtimeSeats");

            migrationBuilder.DropColumn(
                name: "IsReserved",
                table: "ShowtimeSeats");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ShowtimeSeats");

            migrationBuilder.CreateTable(
                name: "ShowtimeSeatReservations",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ShowtimeSeatId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ReservedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IsReserved = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShowtimeSeatReservations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShowtimeSeatReservations_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_ShowtimeSeatReservations_ShowtimeSeats_ShowtimeSeatId",
                        column: x => x.ShowtimeSeatId,
                        principalTable: "ShowtimeSeats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShowtimeSeats_ShowtimeId",
                table: "ShowtimeSeats",
                column: "ShowtimeId");

            migrationBuilder.CreateIndex(
                name: "IX_ShowtimeSeatReservations_ShowtimeSeatId_ReservedDate",
                table: "ShowtimeSeatReservations",
                columns: new[] { "ShowtimeSeatId", "ReservedDate" });

            migrationBuilder.CreateIndex(
                name: "IX_ShowtimeSeatReservations_UserId",
                table: "ShowtimeSeatReservations",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShowtimeSeatReservations");

            migrationBuilder.DropIndex(
                name: "IX_ShowtimeSeats_ShowtimeId",
                table: "ShowtimeSeats");

            migrationBuilder.AddColumn<bool>(
                name: "IsReserved",
                table: "ShowtimeSeats",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "ShowtimeSeats",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ShowtimeSeats_ShowtimeId_SeatNumber",
                table: "ShowtimeSeats",
                columns: new[] { "ShowtimeId", "SeatNumber" },
                unique: true);

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
    }
}
