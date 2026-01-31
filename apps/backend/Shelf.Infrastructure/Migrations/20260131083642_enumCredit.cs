using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Shelf.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class enumCredit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Work_Parties_PrimaryPartyId",
                table: "Work");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkCredits_CreditRoles_CreditRoleId",
                table: "WorkCredits");

            migrationBuilder.DropTable(
                name: "CreditRoles");

            migrationBuilder.DropIndex(
                name: "IX_WorkCredits_CreditRoleId",
                table: "WorkCredits");

            migrationBuilder.DropColumn(
                name: "NovelContent",
                table: "Work");

            migrationBuilder.RenameColumn(
                name: "CreditRoleId",
                table: "WorkCredits",
                newName: "Role");

            migrationBuilder.RenameColumn(
                name: "PrimaryPartyId",
                table: "Work",
                newName: "PartyId");

            migrationBuilder.RenameIndex(
                name: "IX_Work_PrimaryPartyId",
                table: "Work",
                newName: "IX_Work_PartyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Work_Parties_PartyId",
                table: "Work",
                column: "PartyId",
                principalTable: "Parties",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Work_Parties_PartyId",
                table: "Work");

            migrationBuilder.RenameColumn(
                name: "Role",
                table: "WorkCredits",
                newName: "CreditRoleId");

            migrationBuilder.RenameColumn(
                name: "PartyId",
                table: "Work",
                newName: "PrimaryPartyId");

            migrationBuilder.RenameIndex(
                name: "IX_Work_PartyId",
                table: "Work",
                newName: "IX_Work_PrimaryPartyId");

            migrationBuilder.AddColumn<string>(
                name: "NovelContent",
                table: "Work",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "CreditRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    NormalizedName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreditRoles", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkCredits_CreditRoleId",
                table: "WorkCredits",
                column: "CreditRoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Work_Parties_PrimaryPartyId",
                table: "Work",
                column: "PrimaryPartyId",
                principalTable: "Parties",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkCredits_CreditRoles_CreditRoleId",
                table: "WorkCredits",
                column: "CreditRoleId",
                principalTable: "CreditRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
