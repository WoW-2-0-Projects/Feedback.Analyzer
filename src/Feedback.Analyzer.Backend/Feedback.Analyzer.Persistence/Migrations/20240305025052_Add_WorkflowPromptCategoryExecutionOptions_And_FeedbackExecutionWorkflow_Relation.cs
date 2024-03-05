using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Feedback.Analyzer.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Add_WorkflowPromptCategoryExecutionOptions_And_FeedbackExecutionWorkflow_Relation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddForeignKey(
                name: "FK_WorkflowPromptCategoryExecutionOptions_PromptCategories_Ana~",
                table: "WorkflowPromptCategoryExecutionOptions",
                column: "AnalysisPromptCategoryId",
                principalTable: "PromptCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkflowPromptCategoryExecutionOptions_PromptCategories_Ana~",
                table: "WorkflowPromptCategoryExecutionOptions");
        }
    }
}
