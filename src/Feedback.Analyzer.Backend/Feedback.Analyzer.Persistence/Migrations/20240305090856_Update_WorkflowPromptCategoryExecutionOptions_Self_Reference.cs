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
                table: "WorkflowExecutionOptions");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "WorkflowExecutionOptions",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ParentId",
                table: "WorkflowExecutionOptions",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkflowPromptCategoryExecutionOptions",
                table: "WorkflowExecutionOptions",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowPromptCategoryExecutionOptions_AnalysisPromptCatego~",
                table: "WorkflowExecutionOptions",
                column: "AnalysisPromptCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowPromptCategoryExecutionOptions_ParentId",
                table: "WorkflowExecutionOptions",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkflowPromptCategoryExecutionOptions_WorkflowPromptCatego~",
                table: "WorkflowExecutionOptions",
                column: "ParentId",
                principalTable: "WorkflowExecutionOptions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkflowPromptCategoryExecutionOptions_WorkflowPromptCatego~",
                table: "WorkflowExecutionOptions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkflowPromptCategoryExecutionOptions",
                table: "WorkflowExecutionOptions");

            migrationBuilder.DropIndex(
                name: "IX_WorkflowPromptCategoryExecutionOptions_AnalysisPromptCatego~",
                table: "WorkflowExecutionOptions");

            migrationBuilder.DropIndex(
                name: "IX_WorkflowPromptCategoryExecutionOptions_ParentId",
                table: "WorkflowExecutionOptions");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "WorkflowExecutionOptions");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "WorkflowExecutionOptions");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkflowPromptCategoryExecutionOptions",
                table: "WorkflowExecutionOptions",
                columns: new[] { "AnalysisPromptCategoryId", "FeedbackExecutionWorkflowId" });
        }
    }
}
