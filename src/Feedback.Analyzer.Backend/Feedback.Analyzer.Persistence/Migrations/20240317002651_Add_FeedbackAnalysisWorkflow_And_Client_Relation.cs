using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Feedback.Analyzer.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Add_FeedbackAnalysisWorkflow_And_Client_Relation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ClientId",
                table: "FeedbackAnalysisWorkflows",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_FeedbackAnalysisWorkflows_ClientId",
                table: "FeedbackAnalysisWorkflows",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_FeedbackAnalysisWorkflows_Clients_ClientId",
                table: "FeedbackAnalysisWorkflows",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FeedbackAnalysisWorkflows_Clients_ClientId",
                table: "FeedbackAnalysisWorkflows");

            migrationBuilder.DropIndex(
                name: "IX_FeedbackAnalysisWorkflows_ClientId",
                table: "FeedbackAnalysisWorkflows");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "FeedbackAnalysisWorkflows");
        }
    }
}
