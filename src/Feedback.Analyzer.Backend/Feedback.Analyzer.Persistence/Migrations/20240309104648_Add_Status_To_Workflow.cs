using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Feedback.Analyzer.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Add_Status_To_Workflow : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "FeedbackExecutionWorkflows",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "FeedbackExecutionWorkflows");
        }
    }
}
