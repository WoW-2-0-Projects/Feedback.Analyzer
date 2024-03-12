using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Feedback.Analyzer.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Add_WorkflowExecutionOption_self_reference_ChildExecutionsOption : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkflowExecutionOptions_WorkflowExecutionOptions_WorkflowE~",
                table: "WorkflowExecutionOptions");

            migrationBuilder.DropIndex(
                name: "IX_WorkflowExecutionOptions_WorkflowExecutionOptionId",
                table: "WorkflowExecutionOptions");

            migrationBuilder.DropColumn(
                name: "WorkflowExecutionOptionId",
                table: "WorkflowExecutionOptions");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowExecutionOptions_ParentId",
                table: "WorkflowExecutionOptions",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkflowExecutionOptions_WorkflowExecutionOptions_ParentId",
                table: "WorkflowExecutionOptions",
                column: "ParentId",
                principalTable: "WorkflowExecutionOptions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkflowExecutionOptions_WorkflowExecutionOptions_ParentId",
                table: "WorkflowExecutionOptions");

            migrationBuilder.DropIndex(
                name: "IX_WorkflowExecutionOptions_ParentId",
                table: "WorkflowExecutionOptions");

            migrationBuilder.AddColumn<Guid>(
                name: "WorkflowExecutionOptionId",
                table: "WorkflowExecutionOptions",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowExecutionOptions_WorkflowExecutionOptionId",
                table: "WorkflowExecutionOptions",
                column: "WorkflowExecutionOptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkflowExecutionOptions_WorkflowExecutionOptions_WorkflowE~",
                table: "WorkflowExecutionOptions",
                column: "WorkflowExecutionOptionId",
                principalTable: "WorkflowExecutionOptions",
                principalColumn: "Id");
        }
    }
}
