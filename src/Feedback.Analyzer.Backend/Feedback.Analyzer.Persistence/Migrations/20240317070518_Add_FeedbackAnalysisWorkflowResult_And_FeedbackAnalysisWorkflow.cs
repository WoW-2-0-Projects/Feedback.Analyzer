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
                name: "IX_FeedbackAnalysisWorkflowResults_WorkflowId",
                table: "FeedbackAnalysisWorkflowResults",
                column: "WorkflowId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FeedbackAnalysisWorkflowResults");
        }
    }
}
