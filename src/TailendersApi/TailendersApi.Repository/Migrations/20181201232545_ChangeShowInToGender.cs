using Microsoft.EntityFrameworkCore.Migrations;

namespace TailendersApi.Repository.Migrations
{
    public partial class ChangeShowInToGender : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SearchShowInCategory",
                table: "Profiles",
                newName: "Gender");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Gender",
                table: "Profiles",
                newName: "SearchShowInCategory");
        }
    }
}
