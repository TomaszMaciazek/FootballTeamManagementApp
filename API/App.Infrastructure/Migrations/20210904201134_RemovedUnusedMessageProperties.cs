using Microsoft.EntityFrameworkCore.Migrations;

namespace App.Infrastructure.Migrations
{
    public partial class RemovedUnusedMessageProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "IndividualMessages");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "IndividualMessages");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "GroupMessages");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "GroupMessages");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "IndividualMessages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "IndividualMessages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "GroupMessages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "GroupMessages",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
