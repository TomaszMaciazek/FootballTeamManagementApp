using Microsoft.EntityFrameworkCore.Migrations;

namespace App.Infrastructure.Migrations
{
    public partial class TestsAddedPassedAttribute : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Passed",
                table: "UsersTestResults",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "PassedMinimalValue",
                table: "TestTemplates",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Passed",
                table: "UsersTestResults");

            migrationBuilder.DropColumn(
                name: "PassedMinimalValue",
                table: "TestTemplates");
        }
    }
}
