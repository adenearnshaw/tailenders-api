using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TailendersApi.Repository.Migrations
{
    public partial class UpdatingImageEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhotoUrlData",
                table: "Profiles");

            migrationBuilder.CreateTable(
                name: "ProfileImageEntity",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    ProfileId = table.Column<string>(nullable: true),
                    ImageUrl = table.Column<string>(nullable: true),
                    LastUpdated = table.Column<DateTime>(nullable: false),
                    ProfileEntityID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfileImageEntity", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ProfileImageEntity_Profiles_ProfileEntityID",
                        column: x => x.ProfileEntityID,
                        principalTable: "Profiles",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProfileImageEntity_ProfileEntityID",
                table: "ProfileImageEntity",
                column: "ProfileEntityID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProfileImageEntity");

            migrationBuilder.AddColumn<string>(
                name: "PhotoUrlData",
                table: "Profiles",
                nullable: true);
        }
    }
}
