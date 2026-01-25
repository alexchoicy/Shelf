using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shelf.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addPrimaryPartyInWork : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PrimaryPartyId",
                table: "Work",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Work_PrimaryPartyId",
                table: "Work",
                column: "PrimaryPartyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Work_Parties_PrimaryPartyId",
                table: "Work",
                column: "PrimaryPartyId",
                principalTable: "Parties",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Work_Parties_PrimaryPartyId",
                table: "Work");

            migrationBuilder.DropIndex(
                name: "IX_Work_PrimaryPartyId",
                table: "Work");

            migrationBuilder.DropColumn(
                name: "PrimaryPartyId",
                table: "Work");
        }
    }
}
