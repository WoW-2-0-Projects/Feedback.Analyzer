using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Feedback.Analyzer.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Add_WorkflowExecutionOption_and_AnalysisWorkflowCategory_Relation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AnalysisPromptCategoryId",
                table: "WorkflowExecutionOptions",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowExecutionOptions_AnalysisPromptCategoryId",
                table: "WorkflowExecutionOptions",
                column: "AnalysisPromptCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkflowExecutionOptions_PromptCategories_AnalysisPromptCat~",
                table: "WorkflowExecutionOptions",
                column: "AnalysisPromptCategoryId",
                principalTable: "PromptCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkflowExecutionOptions_PromptCategories_AnalysisPromptCat~",
                table: "WorkflowExecutionOptions");

            migrationBuilder.DropIndex(
                name: "IX_WorkflowExecutionOptions_AnalysisPromptCategoryId",
                table: "WorkflowExecutionOptions");

            migrationBuilder.DropColumn(
                name: "AnalysisPromptCategoryId",
                table: "WorkflowExecutionOptions");
        }
    }
}
