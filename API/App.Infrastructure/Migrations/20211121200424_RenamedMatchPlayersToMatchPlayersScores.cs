using Microsoft.EntityFrameworkCore.Migrations;

namespace App.Infrastructure.Migrations
{
    public partial class RenamedMatchPlayersToMatchPlayersScores : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MatchPlayers_Matches_MatchId",
                table: "MatchPlayers");

            migrationBuilder.DropForeignKey(
                name: "FK_MatchPlayers_Players_PlayerId",
                table: "MatchPlayers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MatchPlayers",
                table: "MatchPlayers");

            migrationBuilder.RenameTable(
                name: "MatchPlayers",
                newName: "MatchPlayersScores");

            migrationBuilder.RenameIndex(
                name: "IX_MatchPlayers_PlayerId",
                table: "MatchPlayersScores",
                newName: "IX_MatchPlayersScores_PlayerId");

            migrationBuilder.RenameIndex(
                name: "IX_MatchPlayers_MatchId",
                table: "MatchPlayersScores",
                newName: "IX_MatchPlayersScores_MatchId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MatchPlayersScores",
                table: "MatchPlayersScores",
                column: "Id");

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

            migrationBuilder.DropPrimaryKey(
                name: "PK_MatchPlayersScores",
                table: "MatchPlayersScores");

            migrationBuilder.RenameTable(
                name: "MatchPlayersScores",
                newName: "MatchPlayers");

            migrationBuilder.RenameIndex(
                name: "IX_MatchPlayersScores_PlayerId",
                table: "MatchPlayers",
                newName: "IX_MatchPlayers_PlayerId");

            migrationBuilder.RenameIndex(
                name: "IX_MatchPlayersScores_MatchId",
                table: "MatchPlayers",
                newName: "IX_MatchPlayers_MatchId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MatchPlayers",
                table: "MatchPlayers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MatchPlayers_Matches_MatchId",
                table: "MatchPlayers",
                column: "MatchId",
                principalTable: "Matches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MatchPlayers_Players_PlayerId",
                table: "MatchPlayers",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
