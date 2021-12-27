using Microsoft.EntityFrameworkCore.Migrations;

namespace App.Infrastructure.Migrations
{
    public partial class AddedRestrictForTeamAndTeamHistoryRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeamHistories_Teams_TeamId",
                table: "TeamHistories");

            migrationBuilder.AddForeignKey(
                name: "FK_TeamHistories_Teams_TeamId",
                table: "TeamHistories",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeamHistories_Teams_TeamId",
                table: "TeamHistories");

            migrationBuilder.AddForeignKey(
                name: "FK_TeamHistories_Teams_TeamId",
                table: "TeamHistories",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
