using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Feedback.Analyzer.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddFeedbackAnalysisWorkflow_And_Product_Relation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_FeedbackAnalysisWorkflows_Name",
                table: "FeedbackAnalysisWorkflows");

            migrationBuilder.AddColumn<Guid>(
                name: "ProductId",
                table: "FeedbackAnalysisWorkflows",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_FeedbackAnalysisWorkflows_Name_ProductId",
                table: "FeedbackAnalysisWorkflows",
                columns: new[] { "Name", "ProductId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FeedbackAnalysisWorkflows_ProductId",
                table: "FeedbackAnalysisWorkflows",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_FeedbackAnalysisWorkflows_Products_ProductId",
                table: "FeedbackAnalysisWorkflows",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FeedbackAnalysisWorkflows_Products_ProductId",
                table: "FeedbackAnalysisWorkflows");

            migrationBuilder.DropIndex(
                name: "IX_FeedbackAnalysisWorkflows_Name_ProductId",
                table: "FeedbackAnalysisWorkflows");

            migrationBuilder.DropIndex(
                name: "IX_FeedbackAnalysisWorkflows_ProductId",
                table: "FeedbackAnalysisWorkflows");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "FeedbackAnalysisWorkflows");

            migrationBuilder.CreateIndex(
                name: "IX_FeedbackAnalysisWorkflows_Name",
                table: "FeedbackAnalysisWorkflows",
                column: "Name",
                unique: true);
        }
    }
}
