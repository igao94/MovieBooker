using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ShowtimeEntityAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Movies",
                newName: "CreationDate");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Actors",
                newName: "CreationDate");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Movies",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Showtimes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MovieId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AvailableSeats = table.Column<int>(type: "int", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Showtimes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Showtimes_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Showtimes_MovieId",
                table: "Showtimes",
                column: "MovieId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Showtimes");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Movies");

            migrationBuilder.RenameColumn(
                name: "CreationDate",
                table: "Movies",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "CreationDate",
                table: "Actors",
                newName: "CreatedAt");
        }
    }
}
