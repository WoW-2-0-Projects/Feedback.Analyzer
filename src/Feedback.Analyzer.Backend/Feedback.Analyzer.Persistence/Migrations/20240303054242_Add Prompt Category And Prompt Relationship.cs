﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Feedback.Analyzer.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddPromptCategoryAndPromptRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Prompts");

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
                    ExecutionOrder = table.Column<int>(type: "integer", nullable: false),
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
                name: "IX_Prompts_CategoryId",
                table: "Prompts",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_PromptCategories_ExecutionOrder",
                table: "PromptCategories",
                column: "ExecutionOrder");

            migrationBuilder.CreateIndex(
                name: "IX_PromptCategories_SelectedPromptId",
                table: "PromptCategories",
                column: "SelectedPromptId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PromptCategories_Type",
                table: "PromptCategories",
                column: "Type");

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
                name: "IX_Prompts_CategoryId",
                table: "Prompts");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Prompts");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Prompts",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
