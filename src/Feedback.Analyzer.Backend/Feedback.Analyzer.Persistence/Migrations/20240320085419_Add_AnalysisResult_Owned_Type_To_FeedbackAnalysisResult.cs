using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Feedback.Analyzer.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Add_AnalysisResult_Owned_Type_To_FeedbackAnalysisResult : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AnalysisResult_ErrorMessage",
                table: "FeedbackAnalysisResults",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AnalysisResult_HistoryId",
                table: "FeedbackAnalysisResults",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AnalysisResult_Status",
                table: "FeedbackAnalysisResults",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_FeedbackAnalysisResults_AnalysisResult_HistoryId",
                table: "FeedbackAnalysisResults",
                column: "AnalysisResult_HistoryId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_FeedbackAnalysisResults_PromptExecutionHistories_AnalysisRe~",
                table: "FeedbackAnalysisResults",
                column: "AnalysisResult_HistoryId",
                principalTable: "PromptExecutionHistories",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FeedbackAnalysisResults_PromptExecutionHistories_AnalysisRe~",
                table: "FeedbackAnalysisResults");

            migrationBuilder.DropIndex(
                name: "IX_FeedbackAnalysisResults_AnalysisResult_HistoryId",
                table: "FeedbackAnalysisResults");

            migrationBuilder.DropColumn(
                name: "AnalysisResult_ErrorMessage",
                table: "FeedbackAnalysisResults");

            migrationBuilder.DropColumn(
                name: "AnalysisResult_HistoryId",
                table: "FeedbackAnalysisResults");

            migrationBuilder.DropColumn(
                name: "AnalysisResult_Status",
                table: "FeedbackAnalysisResults");
        }
    }
}
