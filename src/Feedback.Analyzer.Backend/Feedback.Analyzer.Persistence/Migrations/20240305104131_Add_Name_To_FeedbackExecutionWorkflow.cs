using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Feedback.Analyzer.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Add_Name_To_FeedbackExecutionWorkflow : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "FeedbackExecutionWorkflows",
                type: "character varying(128)",
                maxLength: 128,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_FeedbackExecutionWorkflows_Name_ProductId",
                table: "FeedbackExecutionWorkflows",
                columns: new[] { "Name", "ProductId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_FeedbackExecutionWorkflows_Name_ProductId",
                table: "FeedbackExecutionWorkflows");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "FeedbackExecutionWorkflows");
        }
    }
}
