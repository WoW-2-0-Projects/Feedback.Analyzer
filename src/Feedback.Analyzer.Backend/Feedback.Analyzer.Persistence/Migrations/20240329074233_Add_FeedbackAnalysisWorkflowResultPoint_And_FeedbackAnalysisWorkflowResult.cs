using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Feedback.Analyzer.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Add_FeedbackAnalysisWorkflowResultPoint_And_FeedbackAnalysisWorkflowResult : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FeedbackAnalysisWorkflowResultPoints",
                columns: table => new
                {
                    FeedbackResultId = table.Column<Guid>(type: "uuid", nullable: false),
                    WorkflowResultId = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<byte>(type: "smallint", nullable: false),
                    Point = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeedbackAnalysisWorkflowResultPoints", x => new { x.WorkflowResultId, x.FeedbackResultId });
                    table.ForeignKey(
                        name: "FK_FeedbackAnalysisWorkflowResultPoints_FeedbackAnalysisResult~",
                        column: x => x.FeedbackResultId,
                        principalTable: "FeedbackAnalysisResults",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FeedbackAnalysisWorkflowResultPoints_FeedbackAnalysisWorkfl~",
                        column: x => x.WorkflowResultId,
                        principalTable: "FeedbackAnalysisWorkflowResults",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FeedbackAnalysisWorkflowResultPoints_FeedbackResultId",
                table: "FeedbackAnalysisWorkflowResultPoints",
                column: "FeedbackResultId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FeedbackAnalysisWorkflowResultPoints");
        }
    }
}
