using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shelf.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class workMedium : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Medium",
                table: "Work",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Medium",
                table: "Work");
        }
    }
}
