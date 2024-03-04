using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Feedback.Analyzer.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FeedbackExecutionWorkflowsMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FeedbackExecutionWorkflows",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    ModifiedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeedbackExecutionWorkflows", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkflowPromptCategoryExecutionOptions",
                columns: table => new
                {
                    AnalysisPromptCategoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    FeedbackExecutionWorkflowId = table.Column<Guid>(type: "uuid", nullable: false),
                    Id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkflowPromptCategoryExecutionOptions", x => new { x.FeedbackExecutionWorkflowId, x.AnalysisPromptCategoryId });
                    table.ForeignKey(
                        name: "FK_WorkflowPromptCategoryExecutionOptions_FeedbackExecutionWor~",
                        column: x => x.FeedbackExecutionWorkflowId,
                        principalTable: "FeedbackExecutionWorkflows",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkflowPromptCategoryExecutionOptions_PromptCategories_Ana~",
                        column: x => x.AnalysisPromptCategoryId,
                        principalTable: "PromptCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowPromptCategoryExecutionOptions_AnalysisPromptCatego~",
                table: "WorkflowPromptCategoryExecutionOptions",
                column: "AnalysisPromptCategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorkflowPromptCategoryExecutionOptions");

            migrationBuilder.DropTable(
                name: "FeedbackExecutionWorkflows");
        }
    }
}
