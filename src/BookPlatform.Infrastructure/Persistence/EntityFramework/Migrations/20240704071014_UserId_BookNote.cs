using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookPlatform.Infrastructure.Persistence.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class UserId_BookNote : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "BookNotes",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_BookNotes_UserId",
                table: "BookNotes",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookNotes_AspNetUsers_UserId",
                table: "BookNotes",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookNotes_AspNetUsers_UserId",
                table: "BookNotes");

            migrationBuilder.DropIndex(
                name: "IX_BookNotes_UserId",
                table: "BookNotes");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "BookNotes");
        }
    }
}
