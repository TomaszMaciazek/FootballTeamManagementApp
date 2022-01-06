using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace App.Infrastructure.Migrations
{
    public partial class TestsRefactor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsersTestResults_TestTemplates_TestId",
                table: "UsersTestResults");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersTestResults_Users_UserId",
                table: "UsersTestResults");

            migrationBuilder.DropTable(
                name: "TestOptionQuestionsAnswerTemplates");

            migrationBuilder.DropTable(
                name: "UserBoolTestQuestionsAnswers");

            migrationBuilder.DropTable(
                name: "UserOptionsTestQuestionsAnswers");

            migrationBuilder.DropTable(
                name: "BoolTestQuestionsTemplates");

            migrationBuilder.DropTable(
                name: "OptionsTestQuestionsTemplates");

            migrationBuilder.CreateTable(
                name: "TestQuestion",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Number = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TestId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestQuestion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestQuestion_TestTemplates_TestId",
                        column: x => x.TestId,
                        principalTable: "TestTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TestQuestionAnswer",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuestionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserResultId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AnswerValue = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestQuestionAnswer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestQuestionAnswer_TestQuestion_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "TestQuestion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TestQuestionAnswer_UsersTestResults_UserResultId",
                        column: x => x.UserResultId,
                        principalTable: "UsersTestResults",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TestQuestionOption",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Label = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<int>(type: "int", nullable: false),
                    Points = table.Column<double>(type: "float", nullable: false),
                    QuestionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestQuestionOption", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestQuestionOption_TestQuestion_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "TestQuestion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TestQuestion_TestId",
                table: "TestQuestion",
                column: "TestId");

            migrationBuilder.CreateIndex(
                name: "IX_TestQuestionAnswer_QuestionId",
                table: "TestQuestionAnswer",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_TestQuestionAnswer_UserResultId",
                table: "TestQuestionAnswer",
                column: "UserResultId");

            migrationBuilder.CreateIndex(
                name: "IX_TestQuestionOption_QuestionId",
                table: "TestQuestionOption",
                column: "QuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_UsersTestResults_TestTemplates_TestId",
                table: "UsersTestResults",
                column: "TestId",
                principalTable: "TestTemplates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersTestResults_Users_UserId",
                table: "UsersTestResults",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsersTestResults_TestTemplates_TestId",
                table: "UsersTestResults");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersTestResults_Users_UserId",
                table: "UsersTestResults");

            migrationBuilder.DropTable(
                name: "TestQuestionAnswer");

            migrationBuilder.DropTable(
                name: "TestQuestionOption");

            migrationBuilder.DropTable(
                name: "TestQuestion");

            migrationBuilder.CreateTable(
                name: "BoolTestQuestionsTemplates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CorrectAnswer = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    PageNumber = table.Column<int>(type: "int", nullable: false),
                    PointsToEarn = table.Column<double>(type: "float", nullable: false),
                    QuestionNumber = table.Column<int>(type: "int", nullable: false),
                    TestId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoolTestQuestionsTemplates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BoolTestQuestionsTemplates_TestTemplates_TestId",
                        column: x => x.TestId,
                        principalTable: "TestTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OptionsTestQuestionsTemplates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    PageNumber = table.Column<int>(type: "int", nullable: false),
                    PointsToEarn = table.Column<double>(type: "float", nullable: false),
                    QuestionNumber = table.Column<int>(type: "int", nullable: false),
                    TestId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OptionsTestQuestionsTemplates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OptionsTestQuestionsTemplates_TestTemplates_TestId",
                        column: x => x.TestId,
                        principalTable: "TestTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserBoolTestQuestionsAnswers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    QuestionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Value = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserBoolTestQuestionsAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserBoolTestQuestionsAnswers_BoolTestQuestionsTemplates_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "BoolTestQuestionsTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserBoolTestQuestionsAnswers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TestOptionQuestionsAnswerTemplates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsCorrect = table.Column<bool>(type: "bit", nullable: false),
                    PointsForAnswer = table.Column<double>(type: "float", nullable: false),
                    QuestionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    QuestionNumber = table.Column<int>(type: "int", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Value = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestOptionQuestionsAnswerTemplates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestOptionQuestionsAnswerTemplates_OptionsTestQuestionsTemplates_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "OptionsTestQuestionsTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserOptionsTestQuestionsAnswers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    QuestionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Value = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserOptionsTestQuestionsAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserOptionsTestQuestionsAnswers_OptionsTestQuestionsTemplates_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "OptionsTestQuestionsTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserOptionsTestQuestionsAnswers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BoolTestQuestionsTemplates_TestId",
                table: "BoolTestQuestionsTemplates",
                column: "TestId");

            migrationBuilder.CreateIndex(
                name: "IX_OptionsTestQuestionsTemplates_TestId",
                table: "OptionsTestQuestionsTemplates",
                column: "TestId");

            migrationBuilder.CreateIndex(
                name: "IX_TestOptionQuestionsAnswerTemplates_QuestionId",
                table: "TestOptionQuestionsAnswerTemplates",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_UserBoolTestQuestionsAnswers_QuestionId",
                table: "UserBoolTestQuestionsAnswers",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_UserBoolTestQuestionsAnswers_UserId",
                table: "UserBoolTestQuestionsAnswers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserOptionsTestQuestionsAnswers_QuestionId",
                table: "UserOptionsTestQuestionsAnswers",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_UserOptionsTestQuestionsAnswers_UserId",
                table: "UserOptionsTestQuestionsAnswers",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UsersTestResults_TestTemplates_TestId",
                table: "UsersTestResults",
                column: "TestId",
                principalTable: "TestTemplates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersTestResults_Users_UserId",
                table: "UsersTestResults",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
