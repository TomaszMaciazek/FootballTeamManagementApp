using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace App.Infrastructure.Migrations
{
    public partial class TeamAddedRelations_WithPlayers_PointsAndCards : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TeamId",
                table: "PlayersCards",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TeamId",
                table: "MatchPoints",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PlayersCards_TeamId",
                table: "PlayersCards",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchPoints_TeamId",
                table: "MatchPoints",
                column: "TeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_MatchPoints_Teams_TeamId",
                table: "MatchPoints",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PlayersCards_Teams_TeamId",
                table: "PlayersCards",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MatchPoints_Teams_TeamId",
                table: "MatchPoints");

            migrationBuilder.DropForeignKey(
                name: "FK_PlayersCards_Teams_TeamId",
                table: "PlayersCards");

            migrationBuilder.DropIndex(
                name: "IX_PlayersCards_TeamId",
                table: "PlayersCards");

            migrationBuilder.DropIndex(
                name: "IX_MatchPoints_TeamId",
                table: "MatchPoints");

            migrationBuilder.DropColumn(
                name: "TeamId",
                table: "PlayersCards");

            migrationBuilder.DropColumn(
                name: "TeamId",
                table: "MatchPoints");
        }
    }
}
