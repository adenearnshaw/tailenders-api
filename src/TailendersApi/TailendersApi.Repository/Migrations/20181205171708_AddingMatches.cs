using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TailendersApi.Repository.Migrations
{
    public partial class AddingMatches : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Conversations");

            migrationBuilder.DropColumn(
                name: "ShowContactDetails",
                table: "Pairings");

            migrationBuilder.CreateTable(
                name: "Matches",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    MatchedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matches", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MatchContactPreferences",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    MatchId = table.Column<string>(nullable: true),
                    ProfileId = table.Column<string>(nullable: true),
                    ContactDetailsVisible = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchContactPreferences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MatchContactPreferences_Matches_MatchId",
                        column: x => x.MatchId,
                        principalTable: "Matches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MatchContactPreferences_Profiles_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProfileMatches",
                columns: table => new
                {
                    ProfileId = table.Column<string>(nullable: false),
                    MatchId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfileMatches", x => new { x.ProfileId, x.MatchId });
                    table.ForeignKey(
                        name: "FK_ProfileMatches_Matches_MatchId",
                        column: x => x.MatchId,
                        principalTable: "Matches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProfileMatches_Profiles_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MatchContactPreferences_MatchId",
                table: "MatchContactPreferences",
                column: "MatchId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchContactPreferences_ProfileId",
                table: "MatchContactPreferences",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfileMatches_MatchId",
                table: "ProfileMatches",
                column: "MatchId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MatchContactPreferences");

            migrationBuilder.DropTable(
                name: "ProfileMatches");

            migrationBuilder.DropTable(
                name: "Matches");

            migrationBuilder.AddColumn<bool>(
                name: "ShowContactDetails",
                table: "Pairings",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Conversations",
                columns: table => new
                {
                    ConversationId = table.Column<string>(nullable: false),
                    Data = table.Column<string>(nullable: true),
                    LastUpdated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conversations", x => x.ConversationId);
                });
        }
    }
}
