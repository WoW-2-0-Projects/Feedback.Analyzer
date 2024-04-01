using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Feedback.Analyzer.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Add_FeedbackAnalysisWorkflowResultStats_And_Relation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FeedbackAnalysisWorkflowResultStats",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<byte>(type: "smallint", nullable: false),
                    WorkflowResultId = table.Column<Guid>(type: "uuid", nullable: false),
                    Label = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    Percentage = table.Column<short>(type: "smallint", nullable: false),
                    Count = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeedbackAnalysisWorkflowResultStats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FeedbackAnalysisWorkflowResultStats_FeedbackAnalysisWorkflo~",
                        column: x => x.WorkflowResultId,
                        principalTable: "FeedbackAnalysisWorkflowResults",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FeedbackAnalysisWorkflowResultStats_WorkflowResultId",
                table: "FeedbackAnalysisWorkflowResultStats",
                column: "WorkflowResultId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FeedbackAnalysisWorkflowResultStats");
        }
    }
}
