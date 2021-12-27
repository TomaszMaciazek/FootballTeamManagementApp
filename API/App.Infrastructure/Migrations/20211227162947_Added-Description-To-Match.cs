using Microsoft.EntityFrameworkCore.Migrations;

namespace App.Infrastructure.Migrations
{
    public partial class AddedDescriptionToMatch : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Matches",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Matches");
        }
    }
}
