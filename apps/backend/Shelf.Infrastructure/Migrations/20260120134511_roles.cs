using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Shelf.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class roles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "39773055-af77-4687-afc5-ceb1d99b5a8e", "508a0eaf-dbca-47d9-baeb-597b81a4957e", "Admin", "ADMIN" },
                    { "e1368ff1-fb86-4763-8bd7-eb5a4269084e", "70b645e2-64b9-4d69-8a37-46413af238b0", "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "39773055-af77-4687-afc5-ceb1d99b5a8e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e1368ff1-fb86-4763-8bd7-eb5a4269084e");
        }
    }
}
