using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Feedback.Analyzer.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Update_WorkflowPromptCategoryExecutionOptions_And_FeedbackExecutionWorkflowConfiguration_Relation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkflowPromptCategoryExecutionOptions_FeedbackExecutionWor~",
                table: "WorkflowPromptCategoryExecutionOptions");

            migrationBuilder.DropIndex(
                name: "IX_WorkflowPromptCategoryExecutionOptions_FeedbackExecutionWor~",
                table: "WorkflowPromptCategoryExecutionOptions");

            migrationBuilder.DropColumn(
                name: "FeedbackExecutionWorkflowId",
                table: "WorkflowPromptCategoryExecutionOptions");

            migrationBuilder.AddColumn<Guid>(
                name: "StartingExecutionOptionId",
                table: "FeedbackExecutionWorkflows",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_FeedbackExecutionWorkflows_StartingExecutionOptionId",
                table: "FeedbackExecutionWorkflows",
                column: "StartingExecutionOptionId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_FeedbackExecutionWorkflows_WorkflowPromptCategoryExecutionO~",
                table: "FeedbackExecutionWorkflows",
                column: "StartingExecutionOptionId",
                principalTable: "WorkflowPromptCategoryExecutionOptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FeedbackExecutionWorkflows_WorkflowPromptCategoryExecutionO~",
                table: "FeedbackExecutionWorkflows");

            migrationBuilder.DropIndex(
                name: "IX_FeedbackExecutionWorkflows_StartingExecutionOptionId",
                table: "FeedbackExecutionWorkflows");

            migrationBuilder.DropColumn(
                name: "StartingExecutionOptionId",
                table: "FeedbackExecutionWorkflows");

            migrationBuilder.AddColumn<Guid>(
                name: "FeedbackExecutionWorkflowId",
                table: "WorkflowPromptCategoryExecutionOptions",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowPromptCategoryExecutionOptions_FeedbackExecutionWor~",
                table: "WorkflowPromptCategoryExecutionOptions",
                column: "FeedbackExecutionWorkflowId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkflowPromptCategoryExecutionOptions_FeedbackExecutionWor~",
                table: "WorkflowPromptCategoryExecutionOptions",
                column: "FeedbackExecutionWorkflowId",
                principalTable: "FeedbackExecutionWorkflows",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
