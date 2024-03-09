using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Feedback.Analyzer.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Add_WorkflowPromptCategoryExecutionOptions_And_AnalysisPromptCategory_Relation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WorkflowExecutionOptions",
                columns: table => new
                {
                    AnalysisPromptCategoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    FeedbackExecutionWorkflowId = table.Column<Guid>(type: "uuid", nullable: false),
                    Id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkflowPromptCategoryExecutionOptions", x => new { x.AnalysisPromptCategoryId, x.FeedbackExecutionWorkflowId });
                    table.ForeignKey(
                        name: "FK_WorkflowPromptCategoryExecutionOptions_FeedbackExecutionWor~",
                        column: x => x.FeedbackExecutionWorkflowId,
                        principalTable: "FeedbackExecutionWorkflows",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowPromptCategoryExecutionOptions_FeedbackExecutionWor~",
                table: "WorkflowExecutionOptions",
                column: "FeedbackExecutionWorkflowId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorkflowExecutionOptions");
        }
    }
}
