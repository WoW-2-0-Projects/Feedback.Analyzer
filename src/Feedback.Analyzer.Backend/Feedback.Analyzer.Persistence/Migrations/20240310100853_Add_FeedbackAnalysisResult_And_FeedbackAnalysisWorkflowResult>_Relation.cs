using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Feedback.Analyzer.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Add_FeedbackAnalysisResult_And_FeedbackAnalysisWorkflowResult_Relation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FeedbackAnalysisWorkflowResults_FeedbackExecutionWorkflows_~",
                table: "FeedbackAnalysisWorkflowResults");

            migrationBuilder.DropForeignKey(
                name: "FK_FeedbackExecutionWorkflows_AnalysisWorkflows_Id",
                table: "FeedbackExecutionWorkflows");

            migrationBuilder.DropForeignKey(
                name: "FK_FeedbackExecutionWorkflows_Products_ProductId",
                table: "FeedbackExecutionWorkflows");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FeedbackExecutionWorkflows",
                table: "FeedbackExecutionWorkflows");

            migrationBuilder.RenameTable(
                name: "FeedbackExecutionWorkflows",
                newName: "FeedbackAnalysisWorkflows");

            migrationBuilder.RenameIndex(
                name: "IX_FeedbackExecutionWorkflows_ProductId",
                table: "FeedbackAnalysisWorkflows",
                newName: "IX_FeedbackAnalysisWorkflows_ProductId");

            migrationBuilder.AddColumn<Guid>(
                name: "FeedbackAnalysisWorkflowResultId",
                table: "FeedbackAnalysisResults",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_FeedbackAnalysisWorkflows",
                table: "FeedbackAnalysisWorkflows",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_FeedbackAnalysisResults_FeedbackAnalysisWorkflowResultId",
                table: "FeedbackAnalysisResults",
                column: "FeedbackAnalysisWorkflowResultId");

            migrationBuilder.AddForeignKey(
                name: "FK_FeedbackAnalysisResults_FeedbackAnalysisWorkflowResults_Fee~",
                table: "FeedbackAnalysisResults",
                column: "FeedbackAnalysisWorkflowResultId",
                principalTable: "FeedbackAnalysisWorkflowResults",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FeedbackAnalysisWorkflowResults_FeedbackAnalysisWorkflows_W~",
                table: "FeedbackAnalysisWorkflowResults",
                column: "WorkflowId",
                principalTable: "FeedbackAnalysisWorkflows",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FeedbackAnalysisWorkflows_AnalysisWorkflows_Id",
                table: "FeedbackAnalysisWorkflows",
                column: "Id",
                principalTable: "AnalysisWorkflows",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FeedbackAnalysisWorkflows_Products_ProductId",
                table: "FeedbackAnalysisWorkflows",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FeedbackAnalysisResults_FeedbackAnalysisWorkflowResults_Fee~",
                table: "FeedbackAnalysisResults");

            migrationBuilder.DropForeignKey(
                name: "FK_FeedbackAnalysisWorkflowResults_FeedbackAnalysisWorkflows_W~",
                table: "FeedbackAnalysisWorkflowResults");

            migrationBuilder.DropForeignKey(
                name: "FK_FeedbackAnalysisWorkflows_AnalysisWorkflows_Id",
                table: "FeedbackAnalysisWorkflows");

            migrationBuilder.DropForeignKey(
                name: "FK_FeedbackAnalysisWorkflows_Products_ProductId",
                table: "FeedbackAnalysisWorkflows");

            migrationBuilder.DropIndex(
                name: "IX_FeedbackAnalysisResults_FeedbackAnalysisWorkflowResultId",
                table: "FeedbackAnalysisResults");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FeedbackAnalysisWorkflows",
                table: "FeedbackAnalysisWorkflows");

            migrationBuilder.DropColumn(
                name: "FeedbackAnalysisWorkflowResultId",
                table: "FeedbackAnalysisResults");

            migrationBuilder.RenameTable(
                name: "FeedbackAnalysisWorkflows",
                newName: "FeedbackExecutionWorkflows");

            migrationBuilder.RenameIndex(
                name: "IX_FeedbackAnalysisWorkflows_ProductId",
                table: "FeedbackExecutionWorkflows",
                newName: "IX_FeedbackExecutionWorkflows_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FeedbackExecutionWorkflows",
                table: "FeedbackExecutionWorkflows",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FeedbackAnalysisWorkflowResults_FeedbackExecutionWorkflows_~",
                table: "FeedbackAnalysisWorkflowResults",
                column: "WorkflowId",
                principalTable: "FeedbackExecutionWorkflows",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FeedbackExecutionWorkflows_AnalysisWorkflows_Id",
                table: "FeedbackExecutionWorkflows",
                column: "Id",
                principalTable: "AnalysisWorkflows",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FeedbackExecutionWorkflows_Products_ProductId",
                table: "FeedbackExecutionWorkflows",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
