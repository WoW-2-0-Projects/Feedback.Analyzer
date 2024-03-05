using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Feedback.Analyzer.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Add_Product_Navigation_To_Workflow : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_FeedbackExecutionWorkflows_ProductId",
                table: "FeedbackExecutionWorkflows",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_FeedbackExecutionWorkflows_Products_ProductId",
                table: "FeedbackExecutionWorkflows",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FeedbackExecutionWorkflows_Products_ProductId",
                table: "FeedbackExecutionWorkflows");

            migrationBuilder.DropIndex(
                name: "IX_FeedbackExecutionWorkflows_ProductId",
                table: "FeedbackExecutionWorkflows");
        }
    }
}
