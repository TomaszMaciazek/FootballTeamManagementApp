using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace App.Infrastructure.Migrations
{
    public partial class RemovedPlayersFromTrainings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Players_Trainings_TrainingId",
                table: "Players");

            migrationBuilder.DropIndex(
                name: "IX_Players_TrainingId",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "TrainingId",
                table: "Players");

            migrationBuilder.AlterColumn<int>(
                name: "Score",
                table: "TrainingScores",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Score",
                table: "TrainingScores",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TrainingId",
                table: "Players",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Players_TrainingId",
                table: "Players",
                column: "TrainingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Players_Trainings_TrainingId",
                table: "Players",
                column: "TrainingId",
                principalTable: "Trainings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
