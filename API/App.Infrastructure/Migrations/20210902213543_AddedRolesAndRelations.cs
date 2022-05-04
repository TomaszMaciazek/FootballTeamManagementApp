using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace App.Infrastructure.Migrations
{
    public partial class AddedRolesAndRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupChatUser_GroupChats_GroupChatsId",
                table: "GroupChatUser");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupChatUser_Users_UsersId",
                table: "GroupChatUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GroupChatUser",
                table: "GroupChatUser");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "GroupChatUser",
                newName: "GroupChatOwners");

            migrationBuilder.RenameColumn(
                name: "UsersId",
                table: "GroupChatOwners",
                newName: "OwnedGroupChatsId");

            migrationBuilder.RenameColumn(
                name: "GroupChatsId",
                table: "GroupChatOwners",
                newName: "ChatOwnersId");

            migrationBuilder.RenameIndex(
                name: "IX_GroupChatUser_UsersId",
                table: "GroupChatOwners",
                newName: "IX_GroupChatOwners_OwnedGroupChatsId");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleteForbidden",
                table: "Matches",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_GroupChatOwners",
                table: "GroupChatOwners",
                columns: new[] { "ChatOwnersId", "OwnedGroupChatsId" });

            migrationBuilder.CreateTable(
                name: "GroupChatUser1",
                columns: table => new
                {
                    GroupChatsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsersId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupChatUser1", x => new { x.GroupChatsId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_GroupChatUser1_GroupChats_GroupChatsId",
                        column: x => x.GroupChatsId,
                        principalTable: "GroupChats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupChatUser1_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoleClaims",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleClaims_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RoleUser",
                columns: table => new
                {
                    RolesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsersId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleUser", x => new { x.RolesId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_RoleUser_Roles_RolesId",
                        column: x => x.RolesId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoleUser_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GroupChatUser1_UsersId",
                table: "GroupChatUser1",
                column: "UsersId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaims_RoleId",
                table: "RoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleUser_UsersId",
                table: "RoleUser",
                column: "UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupChatOwners_GroupChats_OwnedGroupChatsId",
                table: "GroupChatOwners",
                column: "OwnedGroupChatsId",
                principalTable: "GroupChats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupChatOwners_Users_ChatOwnersId",
                table: "GroupChatOwners",
                column: "ChatOwnersId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupChatOwners_GroupChats_OwnedGroupChatsId",
                table: "GroupChatOwners");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupChatOwners_Users_ChatOwnersId",
                table: "GroupChatOwners");

            migrationBuilder.DropTable(
                name: "GroupChatUser1");

            migrationBuilder.DropTable(
                name: "RoleClaims");

            migrationBuilder.DropTable(
                name: "RoleUser");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GroupChatOwners",
                table: "GroupChatOwners");

            migrationBuilder.DropColumn(
                name: "IsDeleteForbidden",
                table: "Matches");

            migrationBuilder.RenameTable(
                name: "GroupChatOwners",
                newName: "GroupChatUser");

            migrationBuilder.RenameColumn(
                name: "OwnedGroupChatsId",
                table: "GroupChatUser",
                newName: "UsersId");

            migrationBuilder.RenameColumn(
                name: "ChatOwnersId",
                table: "GroupChatUser",
                newName: "GroupChatsId");

            migrationBuilder.RenameIndex(
                name: "IX_GroupChatOwners_OwnedGroupChatsId",
                table: "GroupChatUser",
                newName: "IX_GroupChatUser_UsersId");

            migrationBuilder.AddColumn<int>(
                name: "Role",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_GroupChatUser",
                table: "GroupChatUser",
                columns: new[] { "GroupChatsId", "UsersId" });

            migrationBuilder.AddForeignKey(
                name: "FK_GroupChatUser_GroupChats_GroupChatsId",
                table: "GroupChatUser",
                column: "GroupChatsId",
                principalTable: "GroupChats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupChatUser_Users_UsersId",
                table: "GroupChatUser",
                column: "UsersId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
