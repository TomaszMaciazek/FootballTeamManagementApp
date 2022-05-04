using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace App.Infrastructure.Migrations
{
    public partial class AddedTeamPlayersPlayedMatchEvent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "OpponentsScore",
                table: "Matches",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ClubScore",
                table: "Matches",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "TeamPlayersPlayedMatchEvents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TeamHistoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MatchId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamPlayersPlayedMatchEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeamPlayersPlayedMatchEvents_Matches_MatchId",
                        column: x => x.MatchId,
                        principalTable: "Matches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TeamPlayersPlayedMatchEvents_TeamHistories_TeamHistoryId",
                        column: x => x.TeamHistoryId,
                        principalTable: "TeamHistories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TeamPlayersPlayedMatchEvents_MatchId",
                table: "TeamPlayersPlayedMatchEvents",
                column: "MatchId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamPlayersPlayedMatchEvents_TeamHistoryId",
                table: "TeamPlayersPlayedMatchEvents",
                column: "TeamHistoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TeamPlayersPlayedMatchEvents");

            migrationBuilder.DropColumn(
                name: "ClubScore",
                table: "Matches");

            migrationBuilder.AlterColumn<int>(
                name: "OpponentsScore",
                table: "Matches",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
