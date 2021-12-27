using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace App.Infrastructure.Migrations
{
    public partial class AddedPlayerLeftTeamEvent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PlayerLeftTeamEvents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlayerHistoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TeamHistoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerLeftTeamEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlayerLeftTeamEvents_PlayerHistories_PlayerHistoryId",
                        column: x => x.PlayerHistoryId,
                        principalTable: "PlayerHistories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PlayerLeftTeamEvents_TeamHistories_TeamHistoryId",
                        column: x => x.TeamHistoryId,
                        principalTable: "TeamHistories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlayerLeftTeamEvents_PlayerHistoryId",
                table: "PlayerLeftTeamEvents",
                column: "PlayerHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerLeftTeamEvents_TeamHistoryId",
                table: "PlayerLeftTeamEvents",
                column: "TeamHistoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlayerLeftTeamEvents");
        }
    }
}
