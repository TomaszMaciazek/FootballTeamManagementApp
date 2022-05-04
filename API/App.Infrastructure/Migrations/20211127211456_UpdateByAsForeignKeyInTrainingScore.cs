using Microsoft.EntityFrameworkCore.Migrations;

namespace App.Infrastructure.Migrations
{
    public partial class UpdateByAsForeignKeyInTrainingScore : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_TrainingScores_UpdatedBy",
                table: "TrainingScores",
                column: "UpdatedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_TrainingScores_Users_UpdatedBy",
                table: "TrainingScores",
                column: "UpdatedBy",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrainingScores_Users_UpdatedBy",
                table: "TrainingScores");

            migrationBuilder.DropIndex(
                name: "IX_TrainingScores_UpdatedBy",
                table: "TrainingScores");
        }
    }
}
