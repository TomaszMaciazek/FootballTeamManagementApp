using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace App.Infrastructure.Migrations
{
    public partial class AddedCoachAssignedToTeamEvent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CoachAssignedToTeamEvents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TeamHistoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CoachId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoachAssignedToTeamEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CoachAssignedToTeamEvents_Coaches_CoachId",
                        column: x => x.CoachId,
                        principalTable: "Coaches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CoachAssignedToTeamEvents_TeamHistories_TeamHistoryId",
                        column: x => x.TeamHistoryId,
                        principalTable: "TeamHistories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CoachAssignedToTeamEvents_CoachId",
                table: "CoachAssignedToTeamEvents",
                column: "CoachId");

            migrationBuilder.CreateIndex(
                name: "IX_CoachAssignedToTeamEvents_TeamHistoryId",
                table: "CoachAssignedToTeamEvents",
                column: "TeamHistoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CoachAssignedToTeamEvents");
        }
    }
}
