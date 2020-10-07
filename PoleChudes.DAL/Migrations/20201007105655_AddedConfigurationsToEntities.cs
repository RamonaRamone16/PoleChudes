using Microsoft.EntityFrameworkCore.Migrations;

namespace PoleChudes.DAL.Migrations
{
    public partial class AddedConfigurationsToEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Match_AspNetUsers_UserId1",
                table: "Match");

            migrationBuilder.DropForeignKey(
                name: "FK_Match_Word_WordId",
                table: "Match");

            migrationBuilder.DropForeignKey(
                name: "FK_Word_AspNetUsers_AdminId",
                table: "Word");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Word",
                table: "Word");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Match",
                table: "Match");

            migrationBuilder.DropIndex(
                name: "IX_Match_UserId1",
                table: "Match");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Match");

            migrationBuilder.RenameTable(
                name: "Word",
                newName: "Words");

            migrationBuilder.RenameTable(
                name: "Match",
                newName: "Matches");

            migrationBuilder.RenameIndex(
                name: "IX_Word_AdminId",
                table: "Words",
                newName: "IX_Words_AdminId");

            migrationBuilder.RenameIndex(
                name: "IX_Match_WordId",
                table: "Matches",
                newName: "IX_Matches_WordId");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Matches",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Words",
                table: "Words",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Matches",
                table: "Matches",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_UserId",
                table: "Matches",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_AspNetUsers_UserId",
                table: "Matches",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Words_WordId",
                table: "Matches",
                column: "WordId",
                principalTable: "Words",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Words_AspNetUsers_AdminId",
                table: "Words",
                column: "AdminId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_AspNetUsers_UserId",
                table: "Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Words_WordId",
                table: "Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_Words_AspNetUsers_AdminId",
                table: "Words");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Words",
                table: "Words");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Matches",
                table: "Matches");

            migrationBuilder.DropIndex(
                name: "IX_Matches_UserId",
                table: "Matches");

            migrationBuilder.RenameTable(
                name: "Words",
                newName: "Word");

            migrationBuilder.RenameTable(
                name: "Matches",
                newName: "Match");

            migrationBuilder.RenameIndex(
                name: "IX_Words_AdminId",
                table: "Word",
                newName: "IX_Word_AdminId");

            migrationBuilder.RenameIndex(
                name: "IX_Matches_WordId",
                table: "Match",
                newName: "IX_Match_WordId");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Match",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "Match",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Word",
                table: "Word",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Match",
                table: "Match",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Match_UserId1",
                table: "Match",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Match_AspNetUsers_UserId1",
                table: "Match",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Match_Word_WordId",
                table: "Match",
                column: "WordId",
                principalTable: "Word",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Word_AspNetUsers_AdminId",
                table: "Word",
                column: "AdminId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
