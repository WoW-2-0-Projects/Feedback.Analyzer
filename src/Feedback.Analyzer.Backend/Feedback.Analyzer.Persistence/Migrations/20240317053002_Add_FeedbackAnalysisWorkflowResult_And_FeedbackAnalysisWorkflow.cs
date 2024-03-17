using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Feedback.Analyzer.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Add_FeedbackAnalysisWorkflowResult_And_FeedbackAnalysisWorkflow : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "FeedbackAnalysisWorkflowResultId",
                table: "FeedbackAnalysisResults",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "FeedbackAnalysisWorkflowResults",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    WorkflowId = table.Column<Guid>(type: "uuid", nullable: false),
                    FeedbacksCount = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    StartedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    FinishedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeedbackAnalysisWorkflowResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FeedbackAnalysisWorkflowResults_FeedbackAnalysisWorkflows_W~",
                        column: x => x.WorkflowId,
                        principalTable: "FeedbackAnalysisWorkflows",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FeedbackAnalysisResults_FeedbackAnalysisWorkflowResultId",
                table: "FeedbackAnalysisResults",
                column: "FeedbackAnalysisWorkflowResultId");

            migrationBuilder.CreateIndex(
                name: "IX_FeedbackAnalysisWorkflowResults_WorkflowId",
                table: "FeedbackAnalysisWorkflowResults",
                column: "WorkflowId");

            migrationBuilder.AddForeignKey(
                name: "FK_FeedbackAnalysisResults_FeedbackAnalysisWorkflowResults_Fee~",
                table: "FeedbackAnalysisResults",
                column: "FeedbackAnalysisWorkflowResultId",
                principalTable: "FeedbackAnalysisWorkflowResults",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FeedbackAnalysisResults_FeedbackAnalysisWorkflowResults_Fee~",
                table: "FeedbackAnalysisResults");

            migrationBuilder.DropTable(
                name: "FeedbackAnalysisWorkflowResults");

            migrationBuilder.DropIndex(
                name: "IX_FeedbackAnalysisResults_FeedbackAnalysisWorkflowResultId",
                table: "FeedbackAnalysisResults");

            migrationBuilder.DropColumn(
                name: "FeedbackAnalysisWorkflowResultId",
                table: "FeedbackAnalysisResults");
        }
    }
}
