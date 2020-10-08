using Microsoft.EntityFrameworkCore.Migrations;

namespace PoleChudes.DAL.Migrations
{
    public partial class ChangedFieldNameFromWordEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Qestion",
                table: "Words");

            migrationBuilder.AddColumn<string>(
                name: "Question",
                table: "Words",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Question",
                table: "Words");

            migrationBuilder.AddColumn<string>(
                name: "Qestion",
                table: "Words",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
