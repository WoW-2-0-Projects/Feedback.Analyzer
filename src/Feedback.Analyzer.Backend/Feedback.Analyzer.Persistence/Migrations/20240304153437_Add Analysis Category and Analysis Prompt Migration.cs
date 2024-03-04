using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Feedback.Analyzer.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddAnalysisCategoryandAnalysisPromptMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CategoryId",
                table: "Prompts",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "AnalysisPromptCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
                    SelectedPromptId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnalysisPromptCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnalysisPromptCategories_Prompts_SelectedPromptId",
                        column: x => x.SelectedPromptId,
                        principalTable: "Prompts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Prompts_CategoryId",
                table: "Prompts",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_AnalysisPromptCategories_SelectedPromptId",
                table: "AnalysisPromptCategories",
                column: "SelectedPromptId");

            migrationBuilder.AddForeignKey(
                name: "FK_Prompts_AnalysisPromptCategories_CategoryId",
                table: "Prompts",
                column: "CategoryId",
                principalTable: "AnalysisPromptCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prompts_AnalysisPromptCategories_CategoryId",
                table: "Prompts");

            migrationBuilder.DropTable(
                name: "AnalysisPromptCategories");

            migrationBuilder.DropIndex(
                name: "IX_Prompts_CategoryId",
                table: "Prompts");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Prompts");
        }
    }
}
