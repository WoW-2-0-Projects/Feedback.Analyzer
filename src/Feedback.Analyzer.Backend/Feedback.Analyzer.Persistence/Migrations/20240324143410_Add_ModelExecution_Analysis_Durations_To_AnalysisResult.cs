using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Feedback.Analyzer.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Add_ModelExecution_Analysis_Durations_To_AnalysisResult : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AnalysisResult_AnalysisDuration",
                table: "FeedbackAnalysisResults",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AnalysisResult_ModelExecutionDuration",
                table: "FeedbackAnalysisResults",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AnalysisResult_AnalysisDuration",
                table: "FeedbackAnalysisResults");

            migrationBuilder.DropColumn(
                name: "AnalysisResult_ModelExecutionDuration",
                table: "FeedbackAnalysisResults");
        }
    }
}
