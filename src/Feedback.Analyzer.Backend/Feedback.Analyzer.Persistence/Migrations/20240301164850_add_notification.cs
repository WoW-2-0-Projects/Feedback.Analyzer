using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Feedback.Analyzer.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class add_notification : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NotificationTemplates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    TemplateType = table.Column<int>(type: "integer", nullable: false),
                    Content = table.Column<string>(type: "character varying(129536)", maxLength: 129536, nullable: false),
                    Subject = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    ModifiedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationTemplates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NotificationHistories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TemplateId = table.Column<Guid>(type: "uuid", nullable: false),
                    SenderUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ReceiverUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    Content = table.Column<string>(type: "character varying(129536)", maxLength: 129536, nullable: false),
                    IsSuccessful = table.Column<bool>(type: "boolean", nullable: false),
                    ErrorMessage = table.Column<string>(type: "text", nullable: true),
                    SenderEmailAddress = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ReceiverEmailAddress = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Subject = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailTemplateId = table.Column<Guid>(type: "uuid", nullable: true),
                    SenderPhoneNumber = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ReceiverPhoneNumber = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    SmsTemplateId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    ModifiedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NotificationHistories_NotificationTemplates_EmailTemplateId",
                        column: x => x.EmailTemplateId,
                        principalTable: "NotificationTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NotificationHistories_NotificationTemplates_SmsTemplateId",
                        column: x => x.SmsTemplateId,
                        principalTable: "NotificationTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NotificationHistories_NotificationTemplates_TemplateId",
                        column: x => x.TemplateId,
                        principalTable: "NotificationTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NotificationHistories_EmailTemplateId",
                table: "NotificationHistories",
                column: "EmailTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationHistories_SmsTemplateId",
                table: "NotificationHistories",
                column: "SmsTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationHistories_TemplateId",
                table: "NotificationHistories",
                column: "TemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationTemplates_Type_TemplateType",
                table: "NotificationTemplates",
                columns: new[] { "Type", "TemplateType" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NotificationHistories");

            migrationBuilder.DropTable(
                name: "NotificationTemplates");
        }
    }
}
