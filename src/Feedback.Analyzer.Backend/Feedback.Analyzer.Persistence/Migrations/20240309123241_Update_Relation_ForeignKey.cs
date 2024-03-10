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
                table: "FeedbackAnalysisWorkflows");

            migrationBuilder.DropIndex(
                name: "IX_FeedbackExecutionWorkflows_WorkflowId",
                table: "FeedbackAnalysisWorkflows");

            migrationBuilder.DropColumn(
                name: "WorkflowId",
                table: "FeedbackAnalysisWorkflows");

            migrationBuilder.AddForeignKey(
                name: "FK_FeedbackExecutionWorkflows_AnalysisWorkflows_Id",
                table: "FeedbackAnalysisWorkflows",
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
                table: "FeedbackAnalysisWorkflows");

            migrationBuilder.AddColumn<Guid>(
                name: "WorkflowId",
                table: "FeedbackAnalysisWorkflows",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_FeedbackExecutionWorkflows_WorkflowId",
                table: "FeedbackAnalysisWorkflows",
                column: "WorkflowId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_FeedbackExecutionWorkflows_AnalysisWorkflows_WorkflowId",
                table: "FeedbackAnalysisWorkflows",
                column: "WorkflowId",
                principalTable: "AnalysisWorkflows",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
