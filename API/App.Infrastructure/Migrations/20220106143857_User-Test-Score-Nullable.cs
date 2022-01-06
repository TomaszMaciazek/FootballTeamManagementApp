using Microsoft.EntityFrameworkCore.Migrations;

namespace App.Infrastructure.Migrations
{
    public partial class UserTestScoreNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserScore",
                table: "UsersTestResults");

            migrationBuilder.AddColumn<double>(
                name: "ScoredPoints",
                table: "UsersTestResults",
                type: "float",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ScoredPoints",
                table: "UsersTestResults");

            migrationBuilder.AddColumn<double>(
                name: "UserScore",
                table: "UsersTestResults",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
