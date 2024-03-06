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
            migrationBuilder.DropIndex(
                name: "IX_PromptCategories_SelectedPromptId",
                table: "PromptCategories");

            migrationBuilder.AlterColumn<string>(
                name: "Category",
                table: "PromptCategories",
                type: "character varying(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.CreateIndex(
                name: "IX_PromptCategories_Category",
                table: "PromptCategories",
                column: "Category",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PromptCategories_SelectedPromptId",
                table: "PromptCategories",
                column: "SelectedPromptId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PromptCategories_Category",
                table: "PromptCategories");

            migrationBuilder.DropIndex(
                name: "IX_PromptCategories_SelectedPromptId",
                table: "PromptCategories");

            migrationBuilder.AlterColumn<int>(
                name: "Category",
                table: "PromptCategories",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(128)",
                oldMaxLength: 128);

            migrationBuilder.CreateIndex(
                name: "IX_PromptCategories_SelectedPromptId",
                table: "PromptCategories",
                column: "SelectedPromptId");
        }
    }
}
