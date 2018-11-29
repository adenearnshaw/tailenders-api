using Microsoft.EntityFrameworkCore.Migrations;

namespace TailendersApi.Repository.Migrations
{
    public partial class Updating_FK_Relationships : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pairings_Conversations_ConversationID",
                table: "Pairings");

            migrationBuilder.DropForeignKey(
                name: "FK_Pairings_Profiles_ProfileEntityID",
                table: "Pairings");

            migrationBuilder.DropForeignKey(
                name: "FK_ProfileImages_Profiles_ProfileEntityID",
                table: "ProfileImages");

            migrationBuilder.DropIndex(
                name: "IX_ProfileImages_ProfileEntityID",
                table: "ProfileImages");

            migrationBuilder.DropIndex(
                name: "IX_Pairings_ConversationID",
                table: "Pairings");

            migrationBuilder.DropIndex(
                name: "IX_Pairings_ProfileEntityID",
                table: "Pairings");

            migrationBuilder.DropColumn(
                name: "ProfileEntityID",
                table: "ProfileImages");

            migrationBuilder.DropColumn(
                name: "ConversationID",
                table: "Pairings");

            migrationBuilder.DropColumn(
                name: "ProfileEntityID",
                table: "Pairings");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Profiles",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "ProfileImages",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Pairings",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Conversations",
                newName: "ConversationId");

            migrationBuilder.AddColumn<string>(
                name: "ContactDetails",
                table: "Profiles",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ProfileId",
                table: "ProfileImages",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ProfileId",
                table: "Pairings",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ShowContactDetails",
                table: "Pairings",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_ProfileImages_ProfileId",
                table: "ProfileImages",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_Pairings_ProfileId",
                table: "Pairings",
                column: "ProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pairings_Profiles_ProfileId",
                table: "Pairings",
                column: "ProfileId",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProfileImages_Profiles_ProfileId",
                table: "ProfileImages",
                column: "ProfileId",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pairings_Profiles_ProfileId",
                table: "Pairings");

            migrationBuilder.DropForeignKey(
                name: "FK_ProfileImages_Profiles_ProfileId",
                table: "ProfileImages");

            migrationBuilder.DropIndex(
                name: "IX_ProfileImages_ProfileId",
                table: "ProfileImages");

            migrationBuilder.DropIndex(
                name: "IX_Pairings_ProfileId",
                table: "Pairings");

            migrationBuilder.DropColumn(
                name: "ContactDetails",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "ShowContactDetails",
                table: "Pairings");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Profiles",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ProfileImages",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Pairings",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "ConversationId",
                table: "Conversations",
                newName: "ID");

            migrationBuilder.AlterColumn<string>(
                name: "ProfileId",
                table: "ProfileImages",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProfileEntityID",
                table: "ProfileImages",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ProfileId",
                table: "Pairings",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ConversationID",
                table: "Pairings",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProfileEntityID",
                table: "Pairings",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProfileImages_ProfileEntityID",
                table: "ProfileImages",
                column: "ProfileEntityID");

            migrationBuilder.CreateIndex(
                name: "IX_Pairings_ConversationID",
                table: "Pairings",
                column: "ConversationID");

            migrationBuilder.CreateIndex(
                name: "IX_Pairings_ProfileEntityID",
                table: "Pairings",
                column: "ProfileEntityID");

            migrationBuilder.AddForeignKey(
                name: "FK_Pairings_Conversations_ConversationID",
                table: "Pairings",
                column: "ConversationID",
                principalTable: "Conversations",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Pairings_Profiles_ProfileEntityID",
                table: "Pairings",
                column: "ProfileEntityID",
                principalTable: "Profiles",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProfileImages_Profiles_ProfileEntityID",
                table: "ProfileImages",
                column: "ProfileEntityID",
                principalTable: "Profiles",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
