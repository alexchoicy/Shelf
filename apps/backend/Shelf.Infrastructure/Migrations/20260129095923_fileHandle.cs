using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shelf.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fileHandle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parties_Files_CoverFileId",
                table: "Parties");

            migrationBuilder.DropForeignKey(
                name: "FK_Work_Files_CoverFileId",
                table: "Work");

            migrationBuilder.DropTable(
                name: "MediaItems");

            migrationBuilder.DropIndex(
                name: "IX_Work_CoverFileId",
                table: "Work");

            migrationBuilder.DropIndex(
                name: "IX_Parties_CoverFileId",
                table: "Parties");

            migrationBuilder.DropColumn(
                name: "CoverFileId",
                table: "Work");

            migrationBuilder.DropColumn(
                name: "CoverFileId",
                table: "Parties");

            migrationBuilder.AddColumn<int>(
                name: "AudioChannels",
                table: "Files",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AudioSampleRate",
                table: "Files",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Bitrate",
                table: "Files",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "FrameRate",
                table: "Files",
                type: "numeric",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MediaAssets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    WorkId = table.Column<Guid>(type: "uuid", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    MediaType = table.Column<int>(type: "integer", nullable: false),
                    Order = table.Column<int>(type: "integer", nullable: false),
                    State = table.Column<int>(type: "integer", nullable: false),
                    Kind = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MediaAssets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MediaAssets_Work_WorkId",
                        column: x => x.WorkId,
                        principalTable: "Work",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PartyCovers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PartyId = table.Column<Guid>(type: "uuid", nullable: false),
                    FileId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsCurrent = table.Column<bool>(type: "boolean", nullable: false),
                    Note = table.Column<string>(type: "text", nullable: true),
                    SetByUserId = table.Column<string>(type: "text", nullable: true),
                    SetBySourceId = table.Column<int>(type: "integer", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    DeletedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartyCovers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PartyCovers_AspNetUsers_SetByUserId",
                        column: x => x.SetByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_PartyCovers_Files_FileId",
                        column: x => x.FileId,
                        principalTable: "Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PartyCovers_Parties_PartyId",
                        column: x => x.PartyId,
                        principalTable: "Parties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PartyCovers_Sources_SetBySourceId",
                        column: x => x.SetBySourceId,
                        principalTable: "Sources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "WorkCovers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    WorkId = table.Column<Guid>(type: "uuid", nullable: false),
                    FileId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsCurrent = table.Column<bool>(type: "boolean", nullable: false),
                    Note = table.Column<string>(type: "text", nullable: true),
                    SetByUserId = table.Column<string>(type: "text", nullable: true),
                    SetBySourceId = table.Column<int>(type: "integer", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    DeletedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkCovers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkCovers_AspNetUsers_SetByUserId",
                        column: x => x.SetByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_WorkCovers_Files_FileId",
                        column: x => x.FileId,
                        principalTable: "Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkCovers_Sources_SetBySourceId",
                        column: x => x.SetBySourceId,
                        principalTable: "Sources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_WorkCovers_Work_WorkId",
                        column: x => x.WorkId,
                        principalTable: "Work",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MediaVariants",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MediaAssetId = table.Column<Guid>(type: "uuid", nullable: false),
                    FileId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsDefault = table.Column<bool>(type: "boolean", nullable: false),
                    Purpose = table.Column<int>(type: "integer", nullable: false),
                    VariantKey = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MediaVariants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MediaVariants_Files_FileId",
                        column: x => x.FileId,
                        principalTable: "Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MediaVariants_MediaAssets_MediaAssetId",
                        column: x => x.MediaAssetId,
                        principalTable: "MediaAssets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MediaAssets_WorkId",
                table: "MediaAssets",
                column: "WorkId");

            migrationBuilder.CreateIndex(
                name: "IX_MediaVariants_FileId",
                table: "MediaVariants",
                column: "FileId");

            migrationBuilder.CreateIndex(
                name: "IX_MediaVariants_MediaAssetId",
                table: "MediaVariants",
                column: "MediaAssetId");

            migrationBuilder.CreateIndex(
                name: "IX_PartyCovers_FileId",
                table: "PartyCovers",
                column: "FileId");

            migrationBuilder.CreateIndex(
                name: "IX_PartyCovers_PartyId",
                table: "PartyCovers",
                column: "PartyId");

            migrationBuilder.CreateIndex(
                name: "IX_PartyCovers_SetBySourceId",
                table: "PartyCovers",
                column: "SetBySourceId");

            migrationBuilder.CreateIndex(
                name: "IX_PartyCovers_SetByUserId",
                table: "PartyCovers",
                column: "SetByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkCovers_FileId",
                table: "WorkCovers",
                column: "FileId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkCovers_SetBySourceId",
                table: "WorkCovers",
                column: "SetBySourceId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkCovers_SetByUserId",
                table: "WorkCovers",
                column: "SetByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkCovers_WorkId",
                table: "WorkCovers",
                column: "WorkId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MediaVariants");

            migrationBuilder.DropTable(
                name: "PartyCovers");

            migrationBuilder.DropTable(
                name: "WorkCovers");

            migrationBuilder.DropTable(
                name: "MediaAssets");

            migrationBuilder.DropColumn(
                name: "AudioChannels",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "AudioSampleRate",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "Bitrate",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "FrameRate",
                table: "Files");

            migrationBuilder.AddColumn<Guid>(
                name: "CoverFileId",
                table: "Work",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CoverFileId",
                table: "Parties",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MediaItems",
                columns: table => new
                {
                    FileId = table.Column<Guid>(type: "uuid", nullable: false),
                    WorkId = table.Column<Guid>(type: "uuid", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Kind = table.Column<int>(type: "integer", nullable: false),
                    MediaType = table.Column<int>(type: "integer", nullable: false),
                    Order = table.Column<int>(type: "integer", nullable: false),
                    State = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MediaItems", x => new { x.FileId, x.WorkId });
                    table.ForeignKey(
                        name: "FK_MediaItems_Files_FileId",
                        column: x => x.FileId,
                        principalTable: "Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MediaItems_Work_WorkId",
                        column: x => x.WorkId,
                        principalTable: "Work",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Work_CoverFileId",
                table: "Work",
                column: "CoverFileId");

            migrationBuilder.CreateIndex(
                name: "IX_Parties_CoverFileId",
                table: "Parties",
                column: "CoverFileId");

            migrationBuilder.CreateIndex(
                name: "IX_MediaItems_WorkId",
                table: "MediaItems",
                column: "WorkId");

            migrationBuilder.AddForeignKey(
                name: "FK_Parties_Files_CoverFileId",
                table: "Parties",
                column: "CoverFileId",
                principalTable: "Files",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Work_Files_CoverFileId",
                table: "Work",
                column: "CoverFileId",
                principalTable: "Files",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
