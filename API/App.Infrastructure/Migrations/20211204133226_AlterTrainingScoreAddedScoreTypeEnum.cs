using Microsoft.EntityFrameworkCore.Migrations;

namespace App.Infrastructure.Migrations
{
    public partial class AlterTrainingScoreAddedScoreTypeEnum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "BallControl",
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

            migrationBuilder.AddColumn<int>(
                name: "ScoreType",
                table: "TrainingScores",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "Value",
                table: "TrainingScores",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ScoreType",
                table: "TrainingScores");

            migrationBuilder.DropColumn(
                name: "Value",
                table: "TrainingScores");

            migrationBuilder.AddColumn<int>(
                name: "Agility",
                table: "TrainingScores",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Attitude",
                table: "TrainingScores",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Awareness",
                table: "TrainingScores",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BallControl",
                table: "TrainingScores",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Communication",
                table: "TrainingScores",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Concentration",
                table: "TrainingScores",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Cooperation",
                table: "TrainingScores",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Coordination",
                table: "TrainingScores",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Creativity",
                table: "TrainingScores",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Decisiveness",
                table: "TrainingScores",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Determination",
                table: "TrainingScores",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Discipline",
                table: "TrainingScores",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EmotionsControl",
                table: "TrainingScores",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Endurance",
                table: "TrainingScores",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Engagement",
                table: "TrainingScores",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HeadingAccuracy",
                table: "TrainingScores",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LeftFootBallReceivingAccuracy",
                table: "TrainingScores",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LeftFootDribblingAccuracy",
                table: "TrainingScores",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LeftFootPassAccuracy",
                table: "TrainingScores",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LeftFootShotsAccuracy",
                table: "TrainingScores",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Mobility",
                table: "TrainingScores",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OneVsOneScore",
                table: "TrainingScores",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RightFootBallReceivingAccuracy",
                table: "TrainingScores",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RightFootDribblingAccuracy",
                table: "TrainingScores",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RightFootPassAccuracy",
                table: "TrainingScores",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RightFootShotsAccuracy",
                table: "TrainingScores",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Selfconfidence",
                table: "TrainingScores",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Strength",
                table: "TrainingScores",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StressControl",
                table: "TrainingScores",
                type: "int",
                nullable: true);
        }
    }
}
