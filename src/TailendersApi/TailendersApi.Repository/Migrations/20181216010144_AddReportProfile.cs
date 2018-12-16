using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TailendersApi.Repository.Migrations
{
    public partial class AddReportProfile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ReportedProfiles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    ProfileId = table.Column<string>(nullable: true),
                    ReportedAt = table.Column<DateTime>(nullable: false),
                    ReasonCode = table.Column<int>(nullable: false),
                    HasBeenReviewed = table.Column<bool>(nullable: false),
                    ReviewNotes = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportedProfiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReportedProfiles_Profiles_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReportedProfiles_ProfileId",
                table: "ReportedProfiles",
                column: "ProfileId",
                unique: true,
                filter: "[ProfileId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReportedProfiles");
        }
    }
}
