using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TailendersApi.Repository.Migrations
{
    public partial class AddingBlocking : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastUpdated",
                table: "Pairings",
                newName: "UpdatedAt");

            migrationBuilder.AddColumn<DateTime>(
                name: "BlockedAt",
                table: "Profiles",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsBlocked",
                table: "Profiles",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "BlockedProfiles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    ProfileId = table.Column<string>(nullable: true),
                    BlockedAt = table.Column<DateTime>(nullable: false),
                    ReasonCode = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlockedProfiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BlockedProfiles_Profiles_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BlockedProfiles_ProfileId",
                table: "BlockedProfiles",
                column: "ProfileId",
                unique: true,
                filter: "[ProfileId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlockedProfiles");

            migrationBuilder.DropColumn(
                name: "BlockedAt",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "IsBlocked",
                table: "Profiles");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "Pairings",
                newName: "LastUpdated");
        }
    }
}
