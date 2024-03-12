using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Feedback.Analyzer.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Add_FeedbackAnalysisResult : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FeedbackAnalysisResults",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FeedbackRelevance_IsRelevant = table.Column<bool>(type: "boolean", nullable: false),
                    FeedbackRelevance_ExtractedRelevantContent = table.Column<string>(type: "text", nullable: false),
                    FeedbackRelevance_PiiRedactedContent = table.Column<string>(type: "text", nullable: false),
                    FeedbackRelevance_RecognizedLanguages = table.Column<string[]>(type: "text[]", nullable: false),
                    FeedbackOpinion_OverallOpinion = table.Column<int>(type: "integer", nullable: false),
                    FeedbackOpinion_PositiveOpinionPoints = table.Column<string[]>(type: "text[]", nullable: false),
                    FeedbackOpinion_NegativeOpinionPoints = table.Column<string[]>(type: "text[]", nullable: false),
                    FeedbackActionablePoints_GenericPoints = table.Column<string[]>(type: "text[]", nullable: false),
                    FeedbackActionablePoints_SpecificPoints = table.Column<string[]>(type: "text[]", nullable: false),
                    FeedbackActionablePoints_ActionablePoints = table.Column<string[]>(type: "text[]", nullable: false),
                    FeedbackActionablePoints_NonActionablePoints = table.Column<string[]>(type: "text[]", nullable: false),
                    FeedbackEntities_Facts = table.Column<string[]>(type: "text[]", nullable: false),
                    FeedbackEntities_Questions = table.Column<string[]>(type: "text[]", nullable: false),
                    FeedbackMetrics_Nps = table.Column<float>(type: "real", nullable: false),
                    FeedbackMetrics_Csat = table.Column<float>(type: "real", nullable: false),
                    FeedbackMetrics_Ces = table.Column<float>(type: "real", nullable: false),
                    CustomerFeedbackId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    ModifiedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeedbackAnalysisResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FeedbackAnalysisResults_Feedbacks_CustomerFeedbackId",
                        column: x => x.CustomerFeedbackId,
                        principalTable: "Feedbacks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FeedbackAnalysisResults_CustomerFeedbackId",
                table: "FeedbackAnalysisResults",
                column: "CustomerFeedbackId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FeedbackAnalysisResults");
        }
    }
}
