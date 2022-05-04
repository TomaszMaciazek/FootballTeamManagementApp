using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace App.Infrastructure.Migrations
{
    public partial class AddedPlayerMatchScoreRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MatchPlayersScores_MatchPlayersPerformances_PlayerPerformanceId",
                table: "MatchPlayersScores");

            migrationBuilder.RenameColumn(
                name: "PlayerPerformanceId",
                table: "MatchPlayersScores",
                newName: "PlayerId");

            migrationBuilder.RenameIndex(
                name: "IX_MatchPlayersScores_PlayerPerformanceId",
                table: "MatchPlayersScores",
                newName: "IX_MatchPlayersScores_PlayerId");

            migrationBuilder.AddColumn<Guid>(
                name: "MatchId",
                table: "MatchPlayersScores",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MatchPlayersScores_MatchId",
                table: "MatchPlayersScores",
                column: "MatchId");

            migrationBuilder.AddForeignKey(
                name: "FK_MatchPlayersScores_Matches_MatchId",
                table: "MatchPlayersScores",
                column: "MatchId",
                principalTable: "Matches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MatchPlayersScores_Players_PlayerId",
                table: "MatchPlayersScores",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MatchPlayersScores_Matches_MatchId",
                table: "MatchPlayersScores");

            migrationBuilder.DropForeignKey(
                name: "FK_MatchPlayersScores_Players_PlayerId",
                table: "MatchPlayersScores");

            migrationBuilder.DropIndex(
                name: "IX_MatchPlayersScores_MatchId",
                table: "MatchPlayersScores");

            migrationBuilder.DropColumn(
                name: "MatchId",
                table: "MatchPlayersScores");

            migrationBuilder.RenameColumn(
                name: "PlayerId",
                table: "MatchPlayersScores",
                newName: "PlayerPerformanceId");

            migrationBuilder.RenameIndex(
                name: "IX_MatchPlayersScores_PlayerId",
                table: "MatchPlayersScores",
                newName: "IX_MatchPlayersScores_PlayerPerformanceId");

            migrationBuilder.AddForeignKey(
                name: "FK_MatchPlayersScores_MatchPlayersPerformances_PlayerPerformanceId",
                table: "MatchPlayersScores",
                column: "PlayerPerformanceId",
                principalTable: "MatchPlayersPerformances",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
