using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shelf.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class textContent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TextContent",
                table: "MediaAssets",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TextContent",
                table: "MediaAssets");
        }
    }
}
