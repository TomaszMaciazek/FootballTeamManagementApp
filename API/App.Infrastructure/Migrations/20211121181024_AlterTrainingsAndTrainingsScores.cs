using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace App.Infrastructure.Migrations
{
    public partial class AlterTrainingsAndTrainingsScores : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CoachesTrainings");

            migrationBuilder.DropColumn(
                name: "Localization",
                table: "Trainings");

            migrationBuilder.RenameColumn(
                name: "Score",
                table: "TrainingScores",
                newName: "BallControl");

            migrationBuilder.AddColumn<int>(
                name: "Agility",
                table: "TrainingScores",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Attitude",
                table: "TrainingScores",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Awareness",
                table: "TrainingScores",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Communication",
                table: "TrainingScores",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Concentration",
                table: "TrainingScores",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Cooperation",
                table: "TrainingScores",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Coordination",
                table: "TrainingScores",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Creativity",
                table: "TrainingScores",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Decisiveness",
                table: "TrainingScores",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Determination",
                table: "TrainingScores",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Discipline",
                table: "TrainingScores",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EmotionsControl",
                table: "TrainingScores",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Endurance",
                table: "TrainingScores",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Engagement",
                table: "TrainingScores",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HeadingAccuracy",
                table: "TrainingScores",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LeftFootBallReceivingAccuracy",
                table: "TrainingScores",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LeftFootDribblingAccuracy",
                table: "TrainingScores",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LeftFootPassAccuracy",
                table: "TrainingScores",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LeftFootShotsAccuracy",
                table: "TrainingScores",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Mobility",
                table: "TrainingScores",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OneVsOneScore",
                table: "TrainingScores",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RightFootBallReceivingAccuracy",
                table: "TrainingScores",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RightFootDribblingAccuracy",
                table: "TrainingScores",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RightFootPassAccuracy",
                table: "TrainingScores",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RightFootShotsAccuracy",
                table: "TrainingScores",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Selfconfidence",
                table: "TrainingScores",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Strength",
                table: "TrainingScores",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StressControl",
                table: "TrainingScores",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "CoachId",
                table: "Trainings",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Trainings_CoachId",
                table: "Trainings",
                column: "CoachId");

            migrationBuilder.AddForeignKey(
                name: "FK_Trainings_Coaches_CoachId",
                table: "Trainings",
                column: "CoachId",
                principalTable: "Coaches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trainings_Coaches_CoachId",
                table: "Trainings");

            migrationBuilder.DropIndex(
                name: "IX_Trainings_CoachId",
                table: "Trainings");

            migrationBuilder.DropColumn(
                name: "Agility",
                table: "TrainingScores");

            migrationBuilder.DropColumn(
                name: "Attitude",
                table: "TrainingScores");

            migrationBuilder.DropColumn(
                name: "Awareness",
                table: "TrainingScores");

            migrationBuilder.DropColumn(
                name: "Communication",
                table: "TrainingScores");

            migrationBuilder.DropColumn(
                name: "Concentration",
                table: "TrainingScores");

            migrationBuilder.DropColumn(
                name: "Cooperation",
                table: "TrainingScores");

            migrationBuilder.DropColumn(
                name: "Coordination",
                table: "TrainingScores");

            migrationBuilder.DropColumn(
                name: "Creativity",
                table: "TrainingScores");

            migrationBuilder.DropColumn(
                name: "Decisiveness",
                table: "TrainingScores");

            migrationBuilder.DropColumn(
                name: "Determination",
                table: "TrainingScores");

            migrationBuilder.DropColumn(
                name: "Discipline",
                table: "TrainingScores");

            migrationBuilder.DropColumn(
                name: "EmotionsControl",
                table: "TrainingScores");

            migrationBuilder.DropColumn(
                name: "Endurance",
                table: "TrainingScores");

            migrationBuilder.DropColumn(
                name: "Engagement",
                table: "TrainingScores");

            migrationBuilder.DropColumn(
                name: "HeadingAccuracy",
                table: "TrainingScores");

            migrationBuilder.DropColumn(
                name: "LeftFootBallReceivingAccuracy",
                table: "TrainingScores");

            migrationBuilder.DropColumn(
                name: "LeftFootDribblingAccuracy",
                table: "TrainingScores");

            migrationBuilder.DropColumn(
                name: "LeftFootPassAccuracy",
                table: "TrainingScores");

            migrationBuilder.DropColumn(
                name: "LeftFootShotsAccuracy",
                table: "TrainingScores");

            migrationBuilder.DropColumn(
                name: "Mobility",
                table: "TrainingScores");

            migrationBuilder.DropColumn(
                name: "OneVsOneScore",
                table: "TrainingScores");

            migrationBuilder.DropColumn(
                name: "RightFootBallReceivingAccuracy",
                table: "TrainingScores");

            migrationBuilder.DropColumn(
                name: "RightFootDribblingAccuracy",
                table: "TrainingScores");

            migrationBuilder.DropColumn(
                name: "RightFootPassAccuracy",
                table: "TrainingScores");

            migrationBuilder.DropColumn(
                name: "RightFootShotsAccuracy",
                table: "TrainingScores");

            migrationBuilder.DropColumn(
                name: "Selfconfidence",
                table: "TrainingScores");

            migrationBuilder.DropColumn(
                name: "Strength",
                table: "TrainingScores");

            migrationBuilder.DropColumn(
                name: "StressControl",
                table: "TrainingScores");

            migrationBuilder.DropColumn(
                name: "CoachId",
                table: "Trainings");

            migrationBuilder.RenameColumn(
                name: "BallControl",
                table: "TrainingScores",
                newName: "Score");

            migrationBuilder.AddColumn<string>(
                name: "Localization",
                table: "Trainings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "CoachesTrainings",
                columns: table => new
                {
                    CoachesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TrainingsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoachesTrainings", x => new { x.CoachesId, x.TrainingsId });
                    table.ForeignKey(
                        name: "FK_CoachesTrainings_Coaches_CoachesId",
                        column: x => x.CoachesId,
                        principalTable: "Coaches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CoachesTrainings_Trainings_TrainingsId",
                        column: x => x.TrainingsId,
                        principalTable: "Trainings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CoachesTrainings_TrainingsId",
                table: "CoachesTrainings",
                column: "TrainingsId");
        }
    }
}
