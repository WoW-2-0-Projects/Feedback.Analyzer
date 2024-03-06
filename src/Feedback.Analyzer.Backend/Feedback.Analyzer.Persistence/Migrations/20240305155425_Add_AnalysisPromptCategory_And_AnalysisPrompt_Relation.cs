using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Feedback.Analyzer.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Add_AnalysisPromptCategory_And_AnalysisPrompt_Relation : Migration
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
                name: "PromptCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Category = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    SelectedPromptId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromptCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PromptCategories_Prompts_SelectedPromptId",
                        column: x => x.SelectedPromptId,
                        principalTable: "Prompts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Prompts_CategoryId_Version_Revision",
                table: "Prompts",
                columns: new[] { "CategoryId", "Version", "Revision" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PromptCategories_Category",
                table: "PromptCategories",
                column: "Category");

            migrationBuilder.CreateIndex(
                name: "IX_PromptCategories_SelectedPromptId",
                table: "PromptCategories",
                column: "SelectedPromptId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Prompts_PromptCategories_CategoryId",
                table: "Prompts",
                column: "CategoryId",
                principalTable: "PromptCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prompts_PromptCategories_CategoryId",
                table: "Prompts");

            migrationBuilder.DropTable(
                name: "PromptCategories");

            migrationBuilder.DropIndex(
                name: "IX_Prompts_CategoryId_Version_Revision",
                table: "Prompts");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Prompts");
        }
    }
}
