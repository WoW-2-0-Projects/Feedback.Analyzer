using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Feedback.Analyzer.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddrelationPromptExecutionHistoryandAnalysisPrompt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CategoryId",
                table: "Prompts",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "PromptCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Category = table.Column<int>(type: "integer", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    SelectedPromptId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromptCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PromptCategories_Prompts_SelectedPromptId",
                        column: x => x.SelectedPromptId,
                        principalTable: "Prompts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PromptExecutionHistories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PromptId = table.Column<Guid>(type: "uuid", nullable: false),
                    Result = table.Column<string>(type: "character varying(32768)", maxLength: 32768, nullable: true),
                    Exception = table.Column<string>(type: "character varying(32768)", maxLength: 32768, nullable: true),
                    ExecutionTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    ExecutionDurationInMilliSeconds = table.Column<TimeSpan>(type: "interval", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromptExecutionHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PromptExecutionHistories_Prompts_PromptId",
                        column: x => x.PromptId,
                        principalTable: "Prompts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Prompts_CategoryId_Version_Revision",
                table: "Prompts",
                columns: new[] { "CategoryId", "Version", "Revision" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PromptCategories_SelectedPromptId",
                table: "PromptCategories",
                column: "SelectedPromptId");

            migrationBuilder.CreateIndex(
                name: "IX_PromptExecutionHistories_PromptId",
                table: "PromptExecutionHistories",
                column: "PromptId");

            migrationBuilder.AddForeignKey(
                name: "FK_Prompts_PromptCategories_CategoryId",
                table: "Prompts",
                column: "CategoryId",
                principalTable: "PromptCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prompts_PromptCategories_CategoryId",
                table: "Prompts");

            migrationBuilder.DropTable(
                name: "PromptCategories");

            migrationBuilder.DropTable(
                name: "PromptExecutionHistories");

            migrationBuilder.DropIndex(
                name: "IX_Prompts_CategoryId_Version_Revision",
                table: "Prompts");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Prompts");
        }
    }
}
