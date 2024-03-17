using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Feedback.Analyzer.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Add_FeedbackAnalysisWorkflowResult_And_FeedbackAnalysisResult : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "FeedbackAnalysisWorkflowResultId",
                table: "FeedbackAnalysisResults",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_FeedbackAnalysisResults_FeedbackAnalysisWorkflowResultId",
                table: "FeedbackAnalysisResults",
                column: "FeedbackAnalysisWorkflowResultId");

            migrationBuilder.AddForeignKey(
                name: "FK_FeedbackAnalysisResults_FeedbackAnalysisWorkflowResults_Fee~",
                table: "FeedbackAnalysisResults",
                column: "FeedbackAnalysisWorkflowResultId",
                principalTable: "FeedbackAnalysisWorkflowResults",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FeedbackAnalysisResults_FeedbackAnalysisWorkflowResults_Fee~",
                table: "FeedbackAnalysisResults");

            migrationBuilder.DropIndex(
                name: "IX_FeedbackAnalysisResults_FeedbackAnalysisWorkflowResultId",
                table: "FeedbackAnalysisResults");

            migrationBuilder.DropColumn(
                name: "FeedbackAnalysisWorkflowResultId",
                table: "FeedbackAnalysisResults");
        }
    }
}
