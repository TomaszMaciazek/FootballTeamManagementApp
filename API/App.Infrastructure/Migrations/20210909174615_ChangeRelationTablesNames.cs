using Microsoft.EntityFrameworkCore.Migrations;

namespace App.Infrastructure.Migrations
{
    public partial class ChangeRelationTablesNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CoachMatch_Coaches_CoachesId",
                table: "CoachMatch");

            migrationBuilder.DropForeignKey(
                name: "FK_CoachMatch_Matches_MatchesId",
                table: "CoachMatch");

            migrationBuilder.DropForeignKey(
                name: "FK_CoachTraining_Coaches_CoachesId",
                table: "CoachTraining");

            migrationBuilder.DropForeignKey(
                name: "FK_CoachTraining_Trainings_TrainingsId",
                table: "CoachTraining");

            migrationBuilder.DropForeignKey(
                name: "FK_IndividualChatUser_IndividualChats_IndividualChatsId",
                table: "IndividualChatUser");

            migrationBuilder.DropForeignKey(
                name: "FK_IndividualChatUser_Users_UsersId",
                table: "IndividualChatUser");

            migrationBuilder.DropForeignKey(
                name: "FK_RoleUser_Roles_RolesId",
                table: "RoleUser");

            migrationBuilder.DropForeignKey(
                name: "FK_RoleUser_Users_UsersId",
                table: "RoleUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoleUser",
                table: "RoleUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IndividualChatUser",
                table: "IndividualChatUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CoachTraining",
                table: "CoachTraining");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CoachMatch",
                table: "CoachMatch");

            migrationBuilder.RenameTable(
                name: "RoleUser",
                newName: "UserRoles");

            migrationBuilder.RenameTable(
                name: "IndividualChatUser",
                newName: "IndividualChatsMembers");

            migrationBuilder.RenameTable(
                name: "CoachTraining",
                newName: "CoachesTrainings");

            migrationBuilder.RenameTable(
                name: "CoachMatch",
                newName: "CoachesMatches");

            migrationBuilder.RenameIndex(
                name: "IX_RoleUser_UsersId",
                table: "UserRoles",
                newName: "IX_UserRoles_UsersId");

            migrationBuilder.RenameIndex(
                name: "IX_IndividualChatUser_UsersId",
                table: "IndividualChatsMembers",
                newName: "IX_IndividualChatsMembers_UsersId");

            migrationBuilder.RenameIndex(
                name: "IX_CoachTraining_TrainingsId",
                table: "CoachesTrainings",
                newName: "IX_CoachesTrainings_TrainingsId");

            migrationBuilder.RenameIndex(
                name: "IX_CoachMatch_MatchesId",
                table: "CoachesMatches",
                newName: "IX_CoachesMatches_MatchesId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserRoles",
                table: "UserRoles",
                columns: new[] { "RolesId", "UsersId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_IndividualChatsMembers",
                table: "IndividualChatsMembers",
                columns: new[] { "IndividualChatsId", "UsersId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_CoachesTrainings",
                table: "CoachesTrainings",
                columns: new[] { "CoachesId", "TrainingsId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_CoachesMatches",
                table: "CoachesMatches",
                columns: new[] { "CoachesId", "MatchesId" });

            migrationBuilder.AddForeignKey(
                name: "FK_CoachesMatches_Coaches_CoachesId",
                table: "CoachesMatches",
                column: "CoachesId",
                principalTable: "Coaches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CoachesMatches_Matches_MatchesId",
                table: "CoachesMatches",
                column: "MatchesId",
                principalTable: "Matches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CoachesTrainings_Coaches_CoachesId",
                table: "CoachesTrainings",
                column: "CoachesId",
                principalTable: "Coaches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CoachesTrainings_Trainings_TrainingsId",
                table: "CoachesTrainings",
                column: "TrainingsId",
                principalTable: "Trainings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IndividualChatsMembers_IndividualChats_IndividualChatsId",
                table: "IndividualChatsMembers",
                column: "IndividualChatsId",
                principalTable: "IndividualChats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IndividualChatsMembers_Users_UsersId",
                table: "IndividualChatsMembers",
                column: "UsersId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoles_Roles_RolesId",
                table: "UserRoles",
                column: "RolesId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoles_Users_UsersId",
                table: "UserRoles",
                column: "UsersId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CoachesMatches_Coaches_CoachesId",
                table: "CoachesMatches");

            migrationBuilder.DropForeignKey(
                name: "FK_CoachesMatches_Matches_MatchesId",
                table: "CoachesMatches");

            migrationBuilder.DropForeignKey(
                name: "FK_CoachesTrainings_Coaches_CoachesId",
                table: "CoachesTrainings");

            migrationBuilder.DropForeignKey(
                name: "FK_CoachesTrainings_Trainings_TrainingsId",
                table: "CoachesTrainings");

            migrationBuilder.DropForeignKey(
                name: "FK_IndividualChatsMembers_IndividualChats_IndividualChatsId",
                table: "IndividualChatsMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_IndividualChatsMembers_Users_UsersId",
                table: "IndividualChatsMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRoles_Roles_RolesId",
                table: "UserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRoles_Users_UsersId",
                table: "UserRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserRoles",
                table: "UserRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IndividualChatsMembers",
                table: "IndividualChatsMembers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CoachesTrainings",
                table: "CoachesTrainings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CoachesMatches",
                table: "CoachesMatches");

            migrationBuilder.RenameTable(
                name: "UserRoles",
                newName: "RoleUser");

            migrationBuilder.RenameTable(
                name: "IndividualChatsMembers",
                newName: "IndividualChatUser");

            migrationBuilder.RenameTable(
                name: "CoachesTrainings",
                newName: "CoachTraining");

            migrationBuilder.RenameTable(
                name: "CoachesMatches",
                newName: "CoachMatch");

            migrationBuilder.RenameIndex(
                name: "IX_UserRoles_UsersId",
                table: "RoleUser",
                newName: "IX_RoleUser_UsersId");

            migrationBuilder.RenameIndex(
                name: "IX_IndividualChatsMembers_UsersId",
                table: "IndividualChatUser",
                newName: "IX_IndividualChatUser_UsersId");

            migrationBuilder.RenameIndex(
                name: "IX_CoachesTrainings_TrainingsId",
                table: "CoachTraining",
                newName: "IX_CoachTraining_TrainingsId");

            migrationBuilder.RenameIndex(
                name: "IX_CoachesMatches_MatchesId",
                table: "CoachMatch",
                newName: "IX_CoachMatch_MatchesId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoleUser",
                table: "RoleUser",
                columns: new[] { "RolesId", "UsersId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_IndividualChatUser",
                table: "IndividualChatUser",
                columns: new[] { "IndividualChatsId", "UsersId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_CoachTraining",
                table: "CoachTraining",
                columns: new[] { "CoachesId", "TrainingsId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_CoachMatch",
                table: "CoachMatch",
                columns: new[] { "CoachesId", "MatchesId" });

            migrationBuilder.AddForeignKey(
                name: "FK_CoachMatch_Coaches_CoachesId",
                table: "CoachMatch",
                column: "CoachesId",
                principalTable: "Coaches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CoachMatch_Matches_MatchesId",
                table: "CoachMatch",
                column: "MatchesId",
                principalTable: "Matches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CoachTraining_Coaches_CoachesId",
                table: "CoachTraining",
                column: "CoachesId",
                principalTable: "Coaches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CoachTraining_Trainings_TrainingsId",
                table: "CoachTraining",
                column: "TrainingsId",
                principalTable: "Trainings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IndividualChatUser_IndividualChats_IndividualChatsId",
                table: "IndividualChatUser",
                column: "IndividualChatsId",
                principalTable: "IndividualChats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IndividualChatUser_Users_UsersId",
                table: "IndividualChatUser",
                column: "UsersId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoleUser_Roles_RolesId",
                table: "RoleUser",
                column: "RolesId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoleUser_Users_UsersId",
                table: "RoleUser",
                column: "UsersId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
