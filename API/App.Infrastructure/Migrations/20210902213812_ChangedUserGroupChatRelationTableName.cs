using Microsoft.EntityFrameworkCore.Migrations;

namespace App.Infrastructure.Migrations
{
    public partial class ChangedUserGroupChatRelationTableName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupChatUser1_GroupChats_GroupChatsId",
                table: "GroupChatUser1");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupChatUser1_Users_UsersId",
                table: "GroupChatUser1");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GroupChatUser1",
                table: "GroupChatUser1");

            migrationBuilder.RenameTable(
                name: "GroupChatUser1",
                newName: "GroupChatsMembers");

            migrationBuilder.RenameIndex(
                name: "IX_GroupChatUser1_UsersId",
                table: "GroupChatsMembers",
                newName: "IX_GroupChatsMembers_UsersId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GroupChatsMembers",
                table: "GroupChatsMembers",
                columns: new[] { "GroupChatsId", "UsersId" });

            migrationBuilder.AddForeignKey(
                name: "FK_GroupChatsMembers_GroupChats_GroupChatsId",
                table: "GroupChatsMembers",
                column: "GroupChatsId",
                principalTable: "GroupChats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupChatsMembers_Users_UsersId",
                table: "GroupChatsMembers",
                column: "UsersId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupChatsMembers_GroupChats_GroupChatsId",
                table: "GroupChatsMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupChatsMembers_Users_UsersId",
                table: "GroupChatsMembers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GroupChatsMembers",
                table: "GroupChatsMembers");

            migrationBuilder.RenameTable(
                name: "GroupChatsMembers",
                newName: "GroupChatUser1");

            migrationBuilder.RenameIndex(
                name: "IX_GroupChatsMembers_UsersId",
                table: "GroupChatUser1",
                newName: "IX_GroupChatUser1_UsersId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GroupChatUser1",
                table: "GroupChatUser1",
                columns: new[] { "GroupChatsId", "UsersId" });

            migrationBuilder.AddForeignKey(
                name: "FK_GroupChatUser1_GroupChats_GroupChatsId",
                table: "GroupChatUser1",
                column: "GroupChatsId",
                principalTable: "GroupChats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupChatUser1_Users_UsersId",
                table: "GroupChatUser1",
                column: "UsersId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
