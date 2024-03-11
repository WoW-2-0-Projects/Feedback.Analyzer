using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Feedback.Analyzer.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Lastchanges : Migration
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

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Products",
                type: "character varying(8192)",
                maxLength: 8192,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(512)",
                oldMaxLength: 512);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Organizations",
                type: "character varying(8192)",
                maxLength: 8192,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(512)",
                oldMaxLength: 512);

            migrationBuilder.CreateTable(
                name: "PromptCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Category = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
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

            migrationBuilder.CreateTable(
                name: "PromptExecutionHistories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PromptId = table.Column<Guid>(type: "uuid", nullable: false),
                    Result = table.Column<string>(type: "text", nullable: true),
                    Exception = table.Column<string>(type: "text", nullable: true),
                    ExecutionTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ExecutionDuration = table.Column<TimeSpan>(type: "interval", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromptExecutionHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PromptExecutionHistories_Prompts_PromptId",
                        column: x => x.PromptId,
                        principalTable: "Prompts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkflowExecutionOptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AnalysisPromptCategoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    WorkflowId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsDisabled = table.Column<bool>(type: "boolean", nullable: false),
                    ParentId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkflowExecutionOptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkflowExecutionOptions_PromptCategories_AnalysisPromptCat~",
                        column: x => x.AnalysisPromptCategoryId,
                        principalTable: "PromptCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkflowExecutionOptions_WorkflowExecutionOptions_ParentId",
                        column: x => x.ParentId,
                        principalTable: "WorkflowExecutionOptions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AnalysisWorkflows",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    EntryExecutionOptionId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnalysisWorkflows", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnalysisWorkflows_WorkflowExecutionOptions_EntryExecutionOp~",
                        column: x => x.EntryExecutionOptionId,
                        principalTable: "WorkflowExecutionOptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FeedbackAnalysisWorkflows",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    ModifiedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeedbackAnalysisWorkflows", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FeedbackAnalysisWorkflows_AnalysisWorkflows_Id",
                        column: x => x.Id,
                        principalTable: "AnalysisWorkflows",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FeedbackAnalysisWorkflows_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FeedbackAnalysisWorkflowResults",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    WorkflowId = table.Column<Guid>(type: "uuid", nullable: false),
                    FeedbacksCount = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    StartedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    FinishedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeedbackAnalysisWorkflowResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FeedbackAnalysisWorkflowResults_FeedbackAnalysisWorkflows_W~",
                        column: x => x.WorkflowId,
                        principalTable: "FeedbackAnalysisWorkflows",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FeedbackAnalysisResults",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FeedbackRelevance_IsRelevant = table.Column<bool>(type: "boolean", nullable: false),
                    FeedbackRelevance_ExtractedRelevantContent = table.Column<string>(type: "text", nullable: false),
                    FeedbackRelevance_PiiRedactedContent = table.Column<string>(type: "text", nullable: false),
                    FeedbackRelevance_RecognizedLanguages = table.Column<string[]>(type: "text[]", nullable: false),
                    FeedbackOpinion_OverallOpinion = table.Column<int>(type: "integer", nullable: false),
                    FeedbackOpinion_PositiveOpinionPoints = table.Column<string[]>(type: "text[]", nullable: false),
                    FeedbackOpinion_NegativeOpinionPoints = table.Column<string[]>(type: "text[]", nullable: false),
                    FeedbackActionablePoints_GenericPoints = table.Column<string[]>(type: "text[]", nullable: false),
                    FeedbackActionablePoints_SpecificPoints = table.Column<string[]>(type: "text[]", nullable: false),
                    FeedbackActionablePoints_ActionablePoints = table.Column<string[]>(type: "text[]", nullable: false),
                    FeedbackActionablePoints_NonActionablePoints = table.Column<string[]>(type: "text[]", nullable: false),
                    FeedbackEntities_Facts = table.Column<string[]>(type: "text[]", nullable: false),
                    FeedbackEntities_Questions = table.Column<string[]>(type: "text[]", nullable: false),
                    FeedbackMetrics_Nps = table.Column<float>(type: "real", nullable: false),
                    FeedbackMetrics_Csat = table.Column<float>(type: "real", nullable: false),
                    FeedbackMetrics_Ces = table.Column<float>(type: "real", nullable: false),
                    CategorizedOpinions = table.Column<string>(type: "json", nullable: false),
                    EntityIdentifications = table.Column<string>(type: "json", nullable: false),
                    CustomerFeedbackId = table.Column<Guid>(type: "uuid", nullable: false),
                    FeedbackAnalysisWorkflowResultId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    ModifiedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeedbackAnalysisResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FeedbackAnalysisResults_FeedbackAnalysisWorkflowResults_Fee~",
                        column: x => x.FeedbackAnalysisWorkflowResultId,
                        principalTable: "FeedbackAnalysisWorkflowResults",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FeedbackAnalysisResults_Feedbacks_CustomerFeedbackId",
                        column: x => x.CustomerFeedbackId,
                        principalTable: "Feedbacks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Prompts_CategoryId_Version_Revision",
                table: "Prompts",
                columns: new[] { "CategoryId", "Version", "Revision" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AnalysisWorkflows_EntryExecutionOptionId",
                table: "AnalysisWorkflows",
                column: "EntryExecutionOptionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FeedbackAnalysisResults_CustomerFeedbackId",
                table: "FeedbackAnalysisResults",
                column: "CustomerFeedbackId");

            migrationBuilder.CreateIndex(
                name: "IX_FeedbackAnalysisResults_FeedbackAnalysisWorkflowResultId",
                table: "FeedbackAnalysisResults",
                column: "FeedbackAnalysisWorkflowResultId");

            migrationBuilder.CreateIndex(
                name: "IX_FeedbackAnalysisWorkflowResults_WorkflowId",
                table: "FeedbackAnalysisWorkflowResults",
                column: "WorkflowId");

            migrationBuilder.CreateIndex(
                name: "IX_FeedbackAnalysisWorkflows_ProductId",
                table: "FeedbackAnalysisWorkflows",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_PromptCategories_Category",
                table: "PromptCategories",
                column: "Category");

            migrationBuilder.CreateIndex(
                name: "IX_PromptCategories_SelectedPromptId",
                table: "PromptCategories",
                column: "SelectedPromptId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PromptExecutionHistories_PromptId",
                table: "PromptExecutionHistories",
                column: "PromptId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowExecutionOptions_AnalysisPromptCategoryId",
                table: "WorkflowExecutionOptions",
                column: "AnalysisPromptCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowExecutionOptions_ParentId",
                table: "WorkflowExecutionOptions",
                column: "ParentId");

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
                name: "FeedbackAnalysisResults");

            migrationBuilder.DropTable(
                name: "PromptExecutionHistories");

            migrationBuilder.DropTable(
                name: "FeedbackAnalysisWorkflowResults");

            migrationBuilder.DropTable(
                name: "FeedbackAnalysisWorkflows");

            migrationBuilder.DropTable(
                name: "AnalysisWorkflows");

            migrationBuilder.DropTable(
                name: "WorkflowExecutionOptions");

            migrationBuilder.DropTable(
                name: "PromptCategories");

            migrationBuilder.DropIndex(
                name: "IX_Prompts_CategoryId_Version_Revision",
                table: "Prompts");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Prompts");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Products",
                type: "character varying(512)",
                maxLength: 512,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(8192)",
                oldMaxLength: 8192);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Organizations",
                type: "character varying(512)",
                maxLength: 512,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(8192)",
                oldMaxLength: 8192);
        }
    }
}
