using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Feedback.Analyzer.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Update_WorkflowPromptCategoryExecutionOptions_Self_Reference : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkflowPromptCategoryExecutionOptions",
                table: "WorkflowPromptCategoryExecutionOptions");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "WorkflowPromptCategoryExecutionOptions",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ParentId",
                table: "WorkflowPromptCategoryExecutionOptions",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkflowPromptCategoryExecutionOptions",
                table: "WorkflowPromptCategoryExecutionOptions",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowPromptCategoryExecutionOptions_AnalysisPromptCatego~",
                table: "WorkflowPromptCategoryExecutionOptions",
                column: "AnalysisPromptCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowPromptCategoryExecutionOptions_ParentId",
                table: "WorkflowPromptCategoryExecutionOptions",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkflowPromptCategoryExecutionOptions_WorkflowPromptCatego~",
                table: "WorkflowPromptCategoryExecutionOptions",
                column: "ParentId",
                principalTable: "WorkflowPromptCategoryExecutionOptions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkflowPromptCategoryExecutionOptions_WorkflowPromptCatego~",
                table: "WorkflowPromptCategoryExecutionOptions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkflowPromptCategoryExecutionOptions",
                table: "WorkflowPromptCategoryExecutionOptions");

            migrationBuilder.DropIndex(
                name: "IX_WorkflowPromptCategoryExecutionOptions_AnalysisPromptCatego~",
                table: "WorkflowPromptCategoryExecutionOptions");

            migrationBuilder.DropIndex(
                name: "IX_WorkflowPromptCategoryExecutionOptions_ParentId",
                table: "WorkflowPromptCategoryExecutionOptions");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "WorkflowPromptCategoryExecutionOptions");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "WorkflowPromptCategoryExecutionOptions");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkflowPromptCategoryExecutionOptions",
                table: "WorkflowPromptCategoryExecutionOptions",
                columns: new[] { "AnalysisPromptCategoryId", "FeedbackExecutionWorkflowId" });
        }
    }
}
