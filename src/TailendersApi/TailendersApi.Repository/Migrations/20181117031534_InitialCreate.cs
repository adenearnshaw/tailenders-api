using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TailendersApi.Repository.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Conversations",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    LastUpdated = table.Column<DateTime>(nullable: false),
                    Data = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conversations", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Profiles",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Age = table.Column<int>(nullable: false),
                    ShowAge = table.Column<bool>(nullable: false),
                    Location = table.Column<string>(nullable: true),
                    Latitude = table.Column<double>(nullable: false),
                    Longitude = table.Column<double>(nullable: false),
                    Bio = table.Column<string>(nullable: true),
                    FavouritePosition = table.Column<int>(nullable: false),
                    PhotoUrlData = table.Column<string>(nullable: true),
                    SearchShowInCategory = table.Column<int>(nullable: false),
                    SearchForCategory = table.Column<int>(nullable: false),
                    SearchRadius = table.Column<int>(nullable: false),
                    SearchMinAge = table.Column<int>(nullable: false),
                    SearchMaxAge = table.Column<int>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profiles", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Pairings",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    ProfileId = table.Column<string>(nullable: true),
                    PairedProfileId = table.Column<string>(nullable: true),
                    IsLiked = table.Column<bool>(nullable: false),
                    IsBlocked = table.Column<bool>(nullable: false),
                    LastUpdated = table.Column<DateTime>(nullable: false),
                    ConversationID = table.Column<string>(nullable: true),
                    ProfileEntityID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pairings", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Pairings_Conversations_ConversationID",
                        column: x => x.ConversationID,
                        principalTable: "Conversations",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Pairings_Profiles_ProfileEntityID",
                        column: x => x.ProfileEntityID,
                        principalTable: "Profiles",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pairings_ConversationID",
                table: "Pairings",
                column: "ConversationID");

            migrationBuilder.CreateIndex(
                name: "IX_Pairings_ProfileEntityID",
                table: "Pairings",
                column: "ProfileEntityID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pairings");

            migrationBuilder.DropTable(
                name: "Conversations");

            migrationBuilder.DropTable(
                name: "Profiles");
        }
    }
}
