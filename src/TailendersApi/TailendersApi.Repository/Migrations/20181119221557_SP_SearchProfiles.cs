using Microsoft.EntityFrameworkCore.Migrations;

namespace TailendersApi.Repository.Migrations
{
    public partial class SP_SearchProfiles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var func = @"CREATE FUNCTION ListToTableInt(@list as VARCHAR(8000), @delim as VARCHAR(10))
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

            var proc = @"CREATE PROCEDURE SearchForProfiles
        @userId VARCHAR(36),
        @take INT,
        @minAge INT,
        @maxAge INT,
        @categories INT

AS

SELECT TOP (@take) *
FROM [dbo].[Profiles] p
WHERE p.ID NOT IN (SELECT pa.PairedProfileId
                   FROM [dbo].[Pairings] pa
                   WHERE pa.ProfileId = @userId)
AND p.Age >= @minAge
AND p.Age <= @maxAge
AND p.SearchShowInCategory IN (SELECT Value FROM ListToTableInt(@categories, ','))";

            migrationBuilder.Sql(proc);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
