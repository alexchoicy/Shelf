using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shelf.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ThumbnailOffsetForVideo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TimeOffsetMs",
                table: "MediaVariants",
                type: "integer",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeOffsetMs",
                table: "MediaVariants");
        }
    }
}
