﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Feedback.Analyzer.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Add_PromptExecutionHistory_And_AnalysisPrompt_Relation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PromptExecutionHistories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PromptId = table.Column<Guid>(type: "uuid", nullable: false),
                    Result = table.Column<string>(type: "character varying(32768)", maxLength: 32768, nullable: true),
                    Exception = table.Column<string>(type: "character varying(32768)", maxLength: 32768, nullable: true),
                    ExecutionTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
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

            migrationBuilder.CreateIndex(
                name: "IX_PromptExecutionHistories_PromptId",
                table: "PromptExecutionHistories",
                column: "PromptId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PromptExecutionHistories");
        }
    }
}
