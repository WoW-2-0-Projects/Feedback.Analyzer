using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Feedback.Analyzer.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Update_Relation_ForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FeedbackExecutionWorkflows_AnalysisWorkflows_WorkflowId",
                table: "FeedbackExecutionWorkflows");

            migrationBuilder.DropIndex(
                name: "IX_FeedbackExecutionWorkflows_WorkflowId",
                table: "FeedbackExecutionWorkflows");

            migrationBuilder.DropColumn(
                name: "WorkflowId",
                table: "FeedbackExecutionWorkflows");

            migrationBuilder.AddForeignKey(
                name: "FK_FeedbackExecutionWorkflows_AnalysisWorkflows_Id",
                table: "FeedbackExecutionWorkflows",
                column: "Id",
                principalTable: "AnalysisWorkflows",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FeedbackExecutionWorkflows_AnalysisWorkflows_Id",
                table: "FeedbackExecutionWorkflows");

            migrationBuilder.AddColumn<Guid>(
                name: "WorkflowId",
                table: "FeedbackExecutionWorkflows",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_FeedbackExecutionWorkflows_WorkflowId",
                table: "FeedbackExecutionWorkflows",
                column: "WorkflowId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_FeedbackExecutionWorkflows_AnalysisWorkflows_WorkflowId",
                table: "FeedbackExecutionWorkflows",
                column: "WorkflowId",
                principalTable: "AnalysisWorkflows",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
