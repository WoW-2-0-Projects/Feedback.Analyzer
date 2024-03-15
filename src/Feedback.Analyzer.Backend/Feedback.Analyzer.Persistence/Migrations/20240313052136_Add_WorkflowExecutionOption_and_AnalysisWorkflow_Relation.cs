using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Feedback.Analyzer.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Add_WorkflowExecutionOption_and_AnalysisWorkflow_Relation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WorkflowExecutionOptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AnalysisWorkflowId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsDisabled = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkflowExecutionOptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkflowExecutionOptions_AnalysisWorkflows_AnalysisWorkflow~",
                        column: x => x.AnalysisWorkflowId,
                        principalTable: "AnalysisWorkflows",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowExecutionOptions_AnalysisWorkflowId",
                table: "WorkflowExecutionOptions",
                column: "AnalysisWorkflowId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorkflowExecutionOptions");
        }
    }
}
