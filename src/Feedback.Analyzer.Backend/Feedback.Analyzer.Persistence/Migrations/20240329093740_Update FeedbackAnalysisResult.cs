using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Feedback.Analyzer.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateFeedbackAnalysisResult : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FeedbackActionablePoints_SpecificPoints",
                table: "FeedbackAnalysisResults",
                newName: "FeedbackOpinion_Questions");

            migrationBuilder.RenameColumn(
                name: "FeedbackActionablePoints_NonActionablePoints",
                table: "FeedbackAnalysisResults",
                newName: "FeedbackOpinion_Ideas");

            migrationBuilder.RenameColumn(
                name: "FeedbackActionablePoints_GenericPoints",
                table: "FeedbackAnalysisResults",
                newName: "FeedbackOpinion_ActionableQuestions");

            migrationBuilder.RenameColumn(
                name: "FeedbackActionablePoints_ActionablePoints",
                table: "FeedbackAnalysisResults",
                newName: "FeedbackOpinion_ActionableIdeas");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FeedbackOpinion_Questions",
                table: "FeedbackAnalysisResults",
                newName: "FeedbackActionablePoints_SpecificPoints");

            migrationBuilder.RenameColumn(
                name: "FeedbackOpinion_Ideas",
                table: "FeedbackAnalysisResults",
                newName: "FeedbackActionablePoints_NonActionablePoints");

            migrationBuilder.RenameColumn(
                name: "FeedbackOpinion_ActionableQuestions",
                table: "FeedbackAnalysisResults",
                newName: "FeedbackActionablePoints_GenericPoints");

            migrationBuilder.RenameColumn(
                name: "FeedbackOpinion_ActionableIdeas",
                table: "FeedbackAnalysisResults",
                newName: "FeedbackActionablePoints_ActionablePoints");
        }
    }
}
