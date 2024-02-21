using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AssignmentApp.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Имя = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AssignmentLists",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Название = table.Column<string>(type: "text", nullable: false),
                    Описание = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssignmentLists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssignmentLists_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Assignments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Название = table.Column<string>(type: "text", nullable: false),
                    Описание = table.Column<string>(type: "text", nullable: true),
                    Дата_создания = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Статус = table.Column<string>(type: "text", nullable: false),
                    AssignmentListId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assignments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Assignments_AssignmentLists_AssignmentListId",
                        column: x => x.AssignmentListId,
                        principalTable: "AssignmentLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AssignmentComments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AssignmentId = table.Column<Guid>(type: "uuid", nullable: false),
                    Комментарий = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssignmentComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssignmentComments_Assignments_AssignmentId",
                        column: x => x.AssignmentId,
                        principalTable: "Assignments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssignmentComments_AssignmentId",
                table: "AssignmentComments",
                column: "AssignmentId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignmentLists_UserId",
                table: "AssignmentLists",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Assignments_AssignmentListId",
                table: "Assignments",
                column: "AssignmentListId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssignmentComments");

            migrationBuilder.DropTable(
                name: "Assignments");

            migrationBuilder.DropTable(
                name: "AssignmentLists");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
