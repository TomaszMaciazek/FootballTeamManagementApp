using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace App.Infrastructure.Migrations
{
    public partial class TeamAddedRelationWithPlayerMatchPerformance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TeamId",
                table: "MatchPlayersPerformances",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MatchPlayersPerformances_TeamId",
                table: "MatchPlayersPerformances",
                column: "TeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_MatchPlayersPerformances_Teams_TeamId",
                table: "MatchPlayersPerformances",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MatchPlayersPerformances_Teams_TeamId",
                table: "MatchPlayersPerformances");

            migrationBuilder.DropIndex(
                name: "IX_MatchPlayersPerformances_TeamId",
                table: "MatchPlayersPerformances");

            migrationBuilder.DropColumn(
                name: "TeamId",
                table: "MatchPlayersPerformances");
        }
    }
}
