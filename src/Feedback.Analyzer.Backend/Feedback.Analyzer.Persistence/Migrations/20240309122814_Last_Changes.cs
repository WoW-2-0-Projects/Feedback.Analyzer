using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Feedback.Analyzer.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Last_Changes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    ModifiedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeedbackAnalysisResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FeedbackAnalysisResults_Feedbacks_CustomerFeedbackId",
                        column: x => x.CustomerFeedbackId,
                        principalTable: "Feedbacks",
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
                    WorkflowId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    ModifiedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeedbackExecutionWorkflows", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FeedbackExecutionWorkflows_AnalysisWorkflows_WorkflowId",
                        column: x => x.WorkflowId,
                        principalTable: "AnalysisWorkflows",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FeedbackExecutionWorkflows_Products_ProductId",
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
                    FeedbacksCount = table.Column<decimal>(type: "numeric(20,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeedbackAnalysisWorkflowResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FeedbackAnalysisWorkflowResults_FeedbackExecutionWorkflows_~",
                        column: x => x.WorkflowId,
                        principalTable: "FeedbackAnalysisWorkflows",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnalysisWorkflows_EntryExecutionOptionId",
                table: "AnalysisWorkflows",
                column: "EntryExecutionOptionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FeedbackAnalysisResults_CustomerFeedbackId",
                table: "FeedbackAnalysisResults",
                column: "CustomerFeedbackId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FeedbackAnalysisWorkflowResults_WorkflowId",
                table: "FeedbackAnalysisWorkflowResults",
                column: "WorkflowId");

            migrationBuilder.CreateIndex(
                name: "IX_FeedbackExecutionWorkflows_ProductId",
                table: "FeedbackAnalysisWorkflows",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_FeedbackExecutionWorkflows_WorkflowId",
                table: "FeedbackAnalysisWorkflows",
                column: "WorkflowId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowExecutionOptions_AnalysisPromptCategoryId",
                table: "WorkflowExecutionOptions",
                column: "AnalysisPromptCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowExecutionOptions_ParentId",
                table: "WorkflowExecutionOptions",
                column: "ParentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FeedbackAnalysisResults");

            migrationBuilder.DropTable(
                name: "FeedbackAnalysisWorkflowResults");

            migrationBuilder.DropTable(
                name: "FeedbackAnalysisWorkflows");

            migrationBuilder.DropTable(
                name: "AnalysisWorkflows");

            migrationBuilder.DropTable(
                name: "WorkflowExecutionOptions");
        }
    }
}
