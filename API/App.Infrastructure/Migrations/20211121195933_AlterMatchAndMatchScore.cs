using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace App.Infrastructure.Migrations
{
    public partial class AlterMatchAndMatchScore : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CoachesMatches");

            migrationBuilder.AddColumn<int>(
                name: "Agility",
                table: "MatchPlayers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Attitude",
                table: "MatchPlayers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Awareness",
                table: "MatchPlayers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BallControl",
                table: "MatchPlayers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Communication",
                table: "MatchPlayers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Concentration",
                table: "MatchPlayers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Cooperation",
                table: "MatchPlayers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Coordination",
                table: "MatchPlayers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Creativity",
                table: "MatchPlayers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Decisiveness",
                table: "MatchPlayers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Determination",
                table: "MatchPlayers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Discipline",
                table: "MatchPlayers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EmotionsControl",
                table: "MatchPlayers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Endurance",
                table: "MatchPlayers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Engagement",
                table: "MatchPlayers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HeadingAccuracy",
                table: "MatchPlayers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LeftFootBallReceivingAccuracy",
                table: "MatchPlayers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LeftFootDribblingAccuracy",
                table: "MatchPlayers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LeftFootPassAccuracy",
                table: "MatchPlayers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LeftFootShotsAccuracy",
                table: "MatchPlayers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Mobility",
                table: "MatchPlayers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OneVsOneScore",
                table: "MatchPlayers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RightFootBallReceivingAccuracy",
                table: "MatchPlayers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RightFootDribblingAccuracy",
                table: "MatchPlayers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RightFootPassAccuracy",
                table: "MatchPlayers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RightFootShotsAccuracy",
                table: "MatchPlayers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Selfconfidence",
                table: "MatchPlayers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Strength",
                table: "MatchPlayers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StressControl",
                table: "MatchPlayers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TacticalPerformance",
                table: "MatchPlayers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CoachId",
                table: "Matches",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Matches_CoachId",
                table: "Matches",
                column: "CoachId");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Coaches_CoachId",
                table: "Matches",
                column: "CoachId",
                principalTable: "Coaches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Coaches_CoachId",
                table: "Matches");

            migrationBuilder.DropIndex(
                name: "IX_Matches_CoachId",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "Agility",
                table: "MatchPlayers");

            migrationBuilder.DropColumn(
                name: "Attitude",
                table: "MatchPlayers");

            migrationBuilder.DropColumn(
                name: "Awareness",
                table: "MatchPlayers");

            migrationBuilder.DropColumn(
                name: "BallControl",
                table: "MatchPlayers");

            migrationBuilder.DropColumn(
                name: "Communication",
                table: "MatchPlayers");

            migrationBuilder.DropColumn(
                name: "Concentration",
                table: "MatchPlayers");

            migrationBuilder.DropColumn(
                name: "Cooperation",
                table: "MatchPlayers");

            migrationBuilder.DropColumn(
                name: "Coordination",
                table: "MatchPlayers");

            migrationBuilder.DropColumn(
                name: "Creativity",
                table: "MatchPlayers");

            migrationBuilder.DropColumn(
                name: "Decisiveness",
                table: "MatchPlayers");

            migrationBuilder.DropColumn(
                name: "Determination",
                table: "MatchPlayers");

            migrationBuilder.DropColumn(
                name: "Discipline",
                table: "MatchPlayers");

            migrationBuilder.DropColumn(
                name: "EmotionsControl",
                table: "MatchPlayers");

            migrationBuilder.DropColumn(
                name: "Endurance",
                table: "MatchPlayers");

            migrationBuilder.DropColumn(
                name: "Engagement",
                table: "MatchPlayers");

            migrationBuilder.DropColumn(
                name: "HeadingAccuracy",
                table: "MatchPlayers");

            migrationBuilder.DropColumn(
                name: "LeftFootBallReceivingAccuracy",
                table: "MatchPlayers");

            migrationBuilder.DropColumn(
                name: "LeftFootDribblingAccuracy",
                table: "MatchPlayers");

            migrationBuilder.DropColumn(
                name: "LeftFootPassAccuracy",
                table: "MatchPlayers");

            migrationBuilder.DropColumn(
                name: "LeftFootShotsAccuracy",
                table: "MatchPlayers");

            migrationBuilder.DropColumn(
                name: "Mobility",
                table: "MatchPlayers");

            migrationBuilder.DropColumn(
                name: "OneVsOneScore",
                table: "MatchPlayers");

            migrationBuilder.DropColumn(
                name: "RightFootBallReceivingAccuracy",
                table: "MatchPlayers");

            migrationBuilder.DropColumn(
                name: "RightFootDribblingAccuracy",
                table: "MatchPlayers");

            migrationBuilder.DropColumn(
                name: "RightFootPassAccuracy",
                table: "MatchPlayers");

            migrationBuilder.DropColumn(
                name: "RightFootShotsAccuracy",
                table: "MatchPlayers");

            migrationBuilder.DropColumn(
                name: "Selfconfidence",
                table: "MatchPlayers");

            migrationBuilder.DropColumn(
                name: "Strength",
                table: "MatchPlayers");

            migrationBuilder.DropColumn(
                name: "StressControl",
                table: "MatchPlayers");

            migrationBuilder.DropColumn(
                name: "TacticalPerformance",
                table: "MatchPlayers");

            migrationBuilder.DropColumn(
                name: "CoachId",
                table: "Matches");

            migrationBuilder.CreateTable(
                name: "CoachesMatches",
                columns: table => new
                {
                    CoachesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MatchesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoachesMatches", x => new { x.CoachesId, x.MatchesId });
                    table.ForeignKey(
                        name: "FK_CoachesMatches_Coaches_CoachesId",
                        column: x => x.CoachesId,
                        principalTable: "Coaches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CoachesMatches_Matches_MatchesId",
                        column: x => x.MatchesId,
                        principalTable: "Matches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CoachesMatches_MatchesId",
                table: "CoachesMatches",
                column: "MatchesId");
        }
    }
}
