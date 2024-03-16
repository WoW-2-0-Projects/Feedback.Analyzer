using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Feedback.Analyzer.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Update_AnalysisWorkflow_And_WorkflowExecutionOption_Relation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkflowExecutionOptions_AnalysisWorkflows_AnalysisWorkflow~",
                table: "WorkflowExecutionOptions");

            migrationBuilder.DropIndex(
                name: "IX_WorkflowExecutionOptions_AnalysisWorkflowId",
                table: "WorkflowExecutionOptions");

            migrationBuilder.DropColumn(
                name: "AnalysisWorkflowId",
                table: "WorkflowExecutionOptions");

            migrationBuilder.AddColumn<Guid>(
                name: "EntryExecutionOptionId",
                table: "AnalysisWorkflows",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_AnalysisWorkflows_EntryExecutionOptionId",
                table: "AnalysisWorkflows",
                column: "EntryExecutionOptionId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AnalysisWorkflows_WorkflowExecutionOptions_EntryExecutionOp~",
                table: "AnalysisWorkflows",
                column: "EntryExecutionOptionId",
                principalTable: "WorkflowExecutionOptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnalysisWorkflows_WorkflowExecutionOptions_EntryExecutionOp~",
                table: "AnalysisWorkflows");

            migrationBuilder.DropIndex(
                name: "IX_AnalysisWorkflows_EntryExecutionOptionId",
                table: "AnalysisWorkflows");

            migrationBuilder.DropColumn(
                name: "EntryExecutionOptionId",
                table: "AnalysisWorkflows");

            migrationBuilder.AddColumn<Guid>(
                name: "AnalysisWorkflowId",
                table: "WorkflowExecutionOptions",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowExecutionOptions_AnalysisWorkflowId",
                table: "WorkflowExecutionOptions",
                column: "AnalysisWorkflowId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkflowExecutionOptions_AnalysisWorkflows_AnalysisWorkflow~",
                table: "WorkflowExecutionOptions",
                column: "AnalysisWorkflowId",
                principalTable: "AnalysisWorkflows",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
