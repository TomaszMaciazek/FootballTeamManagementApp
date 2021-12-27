using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace App.Infrastructure.Migrations
{
    public partial class AddedPlayerJoinedTeamEvent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PlayerHistoryId",
                table: "MatchPlayersPerformances",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PlayerJoinedTeamEvents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlayerHistoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TeamHistoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerJoinedTeamEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlayerJoinedTeamEvents_PlayerHistories_PlayerHistoryId",
                        column: x => x.PlayerHistoryId,
                        principalTable: "PlayerHistories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PlayerJoinedTeamEvents_TeamHistories_TeamHistoryId",
                        column: x => x.TeamHistoryId,
                        principalTable: "TeamHistories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MatchPlayersPerformances_PlayerHistoryId",
                table: "MatchPlayersPerformances",
                column: "PlayerHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerJoinedTeamEvents_PlayerHistoryId",
                table: "PlayerJoinedTeamEvents",
                column: "PlayerHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerJoinedTeamEvents_TeamHistoryId",
                table: "PlayerJoinedTeamEvents",
                column: "TeamHistoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_MatchPlayersPerformances_PlayerHistories_PlayerHistoryId",
                table: "MatchPlayersPerformances",
                column: "PlayerHistoryId",
                principalTable: "PlayerHistories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MatchPlayersPerformances_PlayerHistories_PlayerHistoryId",
                table: "MatchPlayersPerformances");

            migrationBuilder.DropTable(
                name: "PlayerJoinedTeamEvents");

            migrationBuilder.DropIndex(
                name: "IX_MatchPlayersPerformances_PlayerHistoryId",
                table: "MatchPlayersPerformances");

            migrationBuilder.DropColumn(
                name: "PlayerHistoryId",
                table: "MatchPlayersPerformances");
        }
    }
}
