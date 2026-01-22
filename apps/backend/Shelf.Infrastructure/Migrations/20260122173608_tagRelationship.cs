using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Shelf.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class tagRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "SourceId",
                table: "Tags",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<string>(
                name: "CreatedByUserId",
                table: "Tags",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsPrimary",
                table: "Tags",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "TagRelationships",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PrimaryTagId = table.Column<int>(type: "integer", nullable: false),
                    RelatedTagId = table.Column<int>(type: "integer", nullable: false),
                    RelationshipType = table.Column<int>(type: "integer", nullable: false),
                    Notes = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagRelationships", x => x.Id);
                    table.CheckConstraint("CK_TagRelationship_DifferentTags", "\"PrimaryTagId\" != \"RelatedTagId\"");
                    table.ForeignKey(
                        name: "FK_TagRelationships_AspNetUsers_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_TagRelationships_Tags_PrimaryTagId",
                        column: x => x.PrimaryTagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TagRelationships_Tags_RelatedTagId",
                        column: x => x.RelatedTagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tags_CreatedByUserId",
                table: "Tags",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TagRelationships_CreatedByUserId",
                table: "TagRelationships",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TagRelationships_PrimaryTagId_RelatedTagId",
                table: "TagRelationships",
                columns: new[] { "PrimaryTagId", "RelatedTagId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TagRelationships_RelatedTagId",
                table: "TagRelationships",
                column: "RelatedTagId");

            migrationBuilder.CreateIndex(
                name: "IX_TagRelationships_RelationshipType",
                table: "TagRelationships",
                column: "RelationshipType");

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_AspNetUsers_CreatedByUserId",
                table: "Tags",
                column: "CreatedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tags_AspNetUsers_CreatedByUserId",
                table: "Tags");

            migrationBuilder.DropTable(
                name: "TagRelationships");

            migrationBuilder.DropIndex(
                name: "IX_Tags_CreatedByUserId",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "IsPrimary",
                table: "Tags");

            migrationBuilder.AlterColumn<int>(
                name: "SourceId",
                table: "Tags",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);
        }
    }
}
