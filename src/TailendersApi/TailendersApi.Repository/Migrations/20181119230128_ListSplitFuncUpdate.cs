using Microsoft.EntityFrameworkCore.Migrations;

namespace TailendersApi.Repository.Migrations
{
    public partial class ListSplitFuncUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProfileImageEntity_Profiles_ProfileEntityID",
                table: "ProfileImageEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProfileImageEntity",
                table: "ProfileImageEntity");

            migrationBuilder.RenameTable(
                name: "ProfileImageEntity",
                newName: "ProfileImages");

            migrationBuilder.RenameIndex(
                name: "IX_ProfileImageEntity_ProfileEntityID",
                table: "ProfileImages",
                newName: "IX_ProfileImages_ProfileEntityID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProfileImages",
                table: "ProfileImages",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_ProfileImages_Profiles_ProfileEntityID",
                table: "ProfileImages",
                column: "ProfileEntityID",
                principalTable: "Profiles",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            var func = @"ALTER FUNCTION dbo.ListToTableInt(@list as VARCHAR(8000), @delim as VARCHAR(10))
RETURNS @listTable TABLE(VALUE INT)
AS
BEGIN
    DECLARE @delimPosition INT
    SET @delimPosition = CHARINDEX(@delim, @list)

    WHILE @delimPosition > 0
    BEGIN   
        INSERT INTO @listTable(Value)
            VALUES(CAST(RTRIM(LEFT(@list, @delimPosition - 1)) AS INT))

        SET @delimPosition = CHARINDEX(@delim, @list)
    END

    IF LEN(@list) > 0
        INSERT INTO @listTable(VALUE)
            VALUES(CAST(RTRIM(@list) AS INT))
    
    RETURN

END
GO";

            migrationBuilder.Sql(func);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProfileImages_Profiles_ProfileEntityID",
                table: "ProfileImages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProfileImages",
                table: "ProfileImages");

            migrationBuilder.RenameTable(
                name: "ProfileImages",
                newName: "ProfileImageEntity");

            migrationBuilder.RenameIndex(
                name: "IX_ProfileImages_ProfileEntityID",
                table: "ProfileImageEntity",
                newName: "IX_ProfileImageEntity_ProfileEntityID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProfileImageEntity",
                table: "ProfileImageEntity",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_ProfileImageEntity_Profiles_ProfileEntityID",
                table: "ProfileImageEntity",
                column: "ProfileEntityID",
                principalTable: "Profiles",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
