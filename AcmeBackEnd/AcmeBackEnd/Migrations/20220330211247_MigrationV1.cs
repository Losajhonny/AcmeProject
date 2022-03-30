using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AcmeBackEnd.Migrations
{
    public partial class MigrationV1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserModel",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserModel", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "QuizModel",
                columns: table => new
                {
                    QuizId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UniqueId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserRefId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuizModel", x => x.QuizId);
                    table.ForeignKey(
                        name: "FK_QuizModel_UserModel_UserRefId",
                        column: x => x.UserRefId,
                        principalTable: "UserModel",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FieldModel",
                columns: table => new
                {
                    FieldId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Required = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TypeField = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QuizRefId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FieldModel", x => x.FieldId);
                    table.ForeignKey(
                        name: "FK_FieldModel_QuizModel_QuizRefId",
                        column: x => x.QuizRefId,
                        principalTable: "QuizModel",
                        principalColumn: "QuizId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AnswerModel",
                columns: table => new
                {
                    AnswerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuizRefId = table.Column<int>(type: "int", nullable: true),
                    FieldRefId = table.Column<int>(type: "int", nullable: true),
                    Result = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnswerModel", x => x.AnswerId);
                    table.ForeignKey(
                        name: "FK_AnswerModel_FieldModel_FieldRefId",
                        column: x => x.FieldRefId,
                        principalTable: "FieldModel",
                        principalColumn: "FieldId");
                    table.ForeignKey(
                        name: "FK_AnswerModel_QuizModel_QuizRefId",
                        column: x => x.QuizRefId,
                        principalTable: "QuizModel",
                        principalColumn: "QuizId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnswerModel_FieldRefId",
                table: "AnswerModel",
                column: "FieldRefId");

            migrationBuilder.CreateIndex(
                name: "IX_AnswerModel_QuizRefId",
                table: "AnswerModel",
                column: "QuizRefId");

            migrationBuilder.CreateIndex(
                name: "IX_FieldModel_QuizRefId",
                table: "FieldModel",
                column: "QuizRefId");

            migrationBuilder.CreateIndex(
                name: "IX_QuizModel_UserRefId",
                table: "QuizModel",
                column: "UserRefId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnswerModel");

            migrationBuilder.DropTable(
                name: "FieldModel");

            migrationBuilder.DropTable(
                name: "QuizModel");

            migrationBuilder.DropTable(
                name: "UserModel");
        }
    }
}
