using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Feedback.Analyzer.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddFeedbackAnalysisWorkflow_And_AnalysisWorkflow : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FeedbackAnalysisWorkflows",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    Type = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeedbackAnalysisWorkflows", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FeedbackAnalysisWorkflows_AnalysisWorkflows_Id",
                        column: x => x.Id,
                        principalTable: "AnalysisWorkflows",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FeedbackAnalysisWorkflows_Name",
                table: "FeedbackAnalysisWorkflows",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FeedbackAnalysisWorkflows");
        }
    }
}
