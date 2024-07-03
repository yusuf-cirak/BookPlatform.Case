using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookPlatform.Infrastructure.Persistence.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class Book_ShelfLocation_ComplexProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_ShelfLocations_ShelfLocationId",
                table: "Books");

            migrationBuilder.DropTable(
                name: "ShelfLocations");

            migrationBuilder.DropIndex(
                name: "IX_Books_ShelfLocationId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "ShelfLocationId",
                table: "Books");

            migrationBuilder.AddColumn<string>(
                name: "ShelfLocation_Position",
                table: "Books",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ShelfLocation_Room",
                table: "Books",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ShelfLocation_Section",
                table: "Books",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ShelfLocation_Shelf",
                table: "Books",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Books_Id",
                table: "Books",
                column: "Id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Books_Id",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "ShelfLocation_Position",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "ShelfLocation_Room",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "ShelfLocation_Section",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "ShelfLocation_Shelf",
                table: "Books");

            migrationBuilder.AddColumn<string>(
                name: "ShelfLocationId",
                table: "Books",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ShelfLocations",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Position = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Room = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Section = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Shelf = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShelfLocations", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_ShelfLocationId",
                table: "Books",
                column: "ShelfLocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_ShelfLocations_ShelfLocationId",
                table: "Books",
                column: "ShelfLocationId",
                principalTable: "ShelfLocations",
                principalColumn: "Id");
        }
    }
}
