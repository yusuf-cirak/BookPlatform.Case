using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookPlatform.Infrastructure.Persistence.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class PictureUrl_and_FullTextSearch_Books : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PictureUrl",
                table: "Books",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PictureUrl",
                table: "Books");
        }
    }
}
