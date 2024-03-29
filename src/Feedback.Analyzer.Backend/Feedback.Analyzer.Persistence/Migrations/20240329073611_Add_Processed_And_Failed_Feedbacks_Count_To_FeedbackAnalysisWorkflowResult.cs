using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Feedback.Analyzer.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Add_Processed_And_Failed_Feedbacks_Count_To_FeedbackAnalysisWorkflowResult : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "FeedbacksCount",
                table: "FeedbackAnalysisWorkflowResults",
                type: "integer",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(20,0)");

            migrationBuilder.AddColumn<int>(
                name: "FailedFeedbacksCount",
                table: "FeedbackAnalysisWorkflowResults",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProcessedFeedbacksCount",
                table: "FeedbackAnalysisWorkflowResults",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FailedFeedbacksCount",
                table: "FeedbackAnalysisWorkflowResults");

            migrationBuilder.DropColumn(
                name: "ProcessedFeedbacksCount",
                table: "FeedbackAnalysisWorkflowResults");

            migrationBuilder.AlterColumn<decimal>(
                name: "FeedbacksCount",
                table: "FeedbackAnalysisWorkflowResults",
                type: "numeric(20,0)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");
        }
    }
}
