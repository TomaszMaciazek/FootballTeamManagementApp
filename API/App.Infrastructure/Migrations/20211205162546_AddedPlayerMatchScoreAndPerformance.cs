using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace App.Infrastructure.Migrations
{
    public partial class AddedPlayerMatchScoreAndPerformance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "Agility",
                table: "MatchPlayersScores");

            migrationBuilder.DropColumn(
                name: "Attitude",
                table: "MatchPlayersScores");

            migrationBuilder.DropColumn(
                name: "Awareness",
                table: "MatchPlayersScores");

            migrationBuilder.DropColumn(
                name: "BallControl",
                table: "MatchPlayersScores");

            migrationBuilder.DropColumn(
                name: "Communication",
                table: "MatchPlayersScores");

            migrationBuilder.DropColumn(
                name: "Concentration",
                table: "MatchPlayersScores");

            migrationBuilder.DropColumn(
                name: "Cooperation",
                table: "MatchPlayersScores");

            migrationBuilder.DropColumn(
                name: "Coordination",
                table: "MatchPlayersScores");

            migrationBuilder.DropColumn(
                name: "Creativity",
                table: "MatchPlayersScores");

            migrationBuilder.DropColumn(
                name: "Decisiveness",
                table: "MatchPlayersScores");

            migrationBuilder.DropColumn(
                name: "Determination",
                table: "MatchPlayersScores");

            migrationBuilder.DropColumn(
                name: "Discipline",
                table: "MatchPlayersScores");

            migrationBuilder.DropColumn(
                name: "EmotionsControl",
                table: "MatchPlayersScores");

            migrationBuilder.DropColumn(
                name: "Endurance",
                table: "MatchPlayersScores");

            migrationBuilder.DropColumn(
                name: "Engagement",
                table: "MatchPlayersScores");

            migrationBuilder.DropColumn(
                name: "HeadingAccuracy",
                table: "MatchPlayersScores");

            migrationBuilder.DropColumn(
                name: "LeftFootBallReceivingAccuracy",
                table: "MatchPlayersScores");

            migrationBuilder.DropColumn(
                name: "LeftFootDribblingAccuracy",
                table: "MatchPlayersScores");

            migrationBuilder.DropColumn(
                name: "LeftFootPassAccuracy",
                table: "MatchPlayersScores");

            migrationBuilder.DropColumn(
                name: "LeftFootShotsAccuracy",
                table: "MatchPlayersScores");

            migrationBuilder.DropColumn(
                name: "MatchId",
                table: "MatchPlayersScores");

            migrationBuilder.DropColumn(
                name: "Mobility",
                table: "MatchPlayersScores");

            migrationBuilder.DropColumn(
                name: "OneVsOneScore",
                table: "MatchPlayersScores");

            migrationBuilder.DropColumn(
                name: "RightFootBallReceivingAccuracy",
                table: "MatchPlayersScores");

            migrationBuilder.DropColumn(
                name: "RightFootDribblingAccuracy",
                table: "MatchPlayersScores");

            migrationBuilder.DropColumn(
                name: "RightFootPassAccuracy",
                table: "MatchPlayersScores");

            migrationBuilder.DropColumn(
                name: "RightFootShotsAccuracy",
                table: "MatchPlayersScores");

            migrationBuilder.DropColumn(
                name: "Selfconfidence",
                table: "MatchPlayersScores");

            migrationBuilder.DropColumn(
                name: "Strength",
                table: "MatchPlayersScores");

            migrationBuilder.DropColumn(
                name: "StressControl",
                table: "MatchPlayersScores");

            migrationBuilder.DropColumn(
                name: "TacticalPerformance",
                table: "MatchPlayersScores");

            migrationBuilder.RenameColumn(
                name: "PlayerPosition",
                table: "MatchPlayersScores",
                newName: "ScoreType");

            migrationBuilder.RenameColumn(
                name: "PlayerId",
                table: "MatchPlayersScores",
                newName: "PlayerPerformanceId");

            migrationBuilder.RenameIndex(
                name: "IX_MatchPlayersScores_PlayerId",
                table: "MatchPlayersScores",
                newName: "IX_MatchPlayersScores_PlayerPerformanceId");

            migrationBuilder.AddColumn<double>(
                name: "Value",
                table: "MatchPlayersScores",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateTable(
                name: "MatchPlayersPerformances",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MatchId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PlayerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PlayerPosition = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchPlayersPerformances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MatchPlayersPerformances_Matches_MatchId",
                        column: x => x.MatchId,
                        principalTable: "Matches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MatchPlayersPerformances_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MatchPlayersPerformances_MatchId",
                table: "MatchPlayersPerformances",
                column: "MatchId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchPlayersPerformances_PlayerId",
                table: "MatchPlayersPerformances",
                column: "PlayerId");

            migrationBuilder.AddForeignKey(
                name: "FK_MatchPlayersScores_MatchPlayersPerformances_PlayerPerformanceId",
                table: "MatchPlayersScores",
                column: "PlayerPerformanceId",
                principalTable: "MatchPlayersPerformances",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MatchPlayersScores_MatchPlayersPerformances_PlayerPerformanceId",
                table: "MatchPlayersScores");

            migrationBuilder.DropTable(
                name: "MatchPlayersPerformances");

            migrationBuilder.DropColumn(
                name: "Value",
                table: "MatchPlayersScores");

            migrationBuilder.RenameColumn(
                name: "ScoreType",
                table: "MatchPlayersScores",
                newName: "PlayerPosition");

            migrationBuilder.RenameColumn(
                name: "PlayerPerformanceId",
                table: "MatchPlayersScores",
                newName: "PlayerId");

            migrationBuilder.RenameIndex(
                name: "IX_MatchPlayersScores_PlayerPerformanceId",
                table: "MatchPlayersScores",
                newName: "IX_MatchPlayersScores_PlayerId");

            migrationBuilder.AddColumn<int>(
                name: "Agility",
                table: "MatchPlayersScores",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Attitude",
                table: "MatchPlayersScores",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Awareness",
                table: "MatchPlayersScores",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BallControl",
                table: "MatchPlayersScores",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Communication",
                table: "MatchPlayersScores",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Concentration",
                table: "MatchPlayersScores",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Cooperation",
                table: "MatchPlayersScores",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Coordination",
                table: "MatchPlayersScores",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Creativity",
                table: "MatchPlayersScores",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Decisiveness",
                table: "MatchPlayersScores",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Determination",
                table: "MatchPlayersScores",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Discipline",
                table: "MatchPlayersScores",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EmotionsControl",
                table: "MatchPlayersScores",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Endurance",
                table: "MatchPlayersScores",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Engagement",
                table: "MatchPlayersScores",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HeadingAccuracy",
                table: "MatchPlayersScores",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LeftFootBallReceivingAccuracy",
                table: "MatchPlayersScores",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LeftFootDribblingAccuracy",
                table: "MatchPlayersScores",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LeftFootPassAccuracy",
                table: "MatchPlayersScores",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LeftFootShotsAccuracy",
                table: "MatchPlayersScores",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "MatchId",
                table: "MatchPlayersScores",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Mobility",
                table: "MatchPlayersScores",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OneVsOneScore",
                table: "MatchPlayersScores",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RightFootBallReceivingAccuracy",
                table: "MatchPlayersScores",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RightFootDribblingAccuracy",
                table: "MatchPlayersScores",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RightFootPassAccuracy",
                table: "MatchPlayersScores",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RightFootShotsAccuracy",
                table: "MatchPlayersScores",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Selfconfidence",
                table: "MatchPlayersScores",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Strength",
                table: "MatchPlayersScores",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StressControl",
                table: "MatchPlayersScores",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TacticalPerformance",
                table: "MatchPlayersScores",
                type: "int",
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
    }
}
