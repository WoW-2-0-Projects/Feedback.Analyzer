﻿// <auto-generated />
using System;
using Feedback.Analyzer.Persistence.DataContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Feedback.Analyzer.Persistence.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240305104131_Add_Name_To_FeedbackExecutionWorkflow")]
    partial class Add_Name_To_FeedbackExecutionWorkflow
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Feedback.Analyzer.Domain.Entities.AnalysisPrompt", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset>("CreatedTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTimeOffset?>("DeletedTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("ModifiedTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Prompt")
                        .IsRequired()
                        .HasMaxLength(32768)
                        .HasColumnType("character varying(32768)");

                    b.Property<int>("Revision")
                        .HasColumnType("integer");

                    b.Property<int>("Version")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId", "Version", "Revision")
                        .IsUnique();

                    b.ToTable("Prompts");
                });

            modelBuilder.Entity("Feedback.Analyzer.Domain.Entities.AnalysisPromptCategory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("SelectedPromptId")
                        .HasColumnType("uuid");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("Category");

                    b.HasIndex("SelectedPromptId")
                        .IsUnique();

                    b.ToTable("PromptCategories");
                });

            modelBuilder.Entity("Feedback.Analyzer.Domain.Entities.Client", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset>("CreatedTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTimeOffset?>("DeletedTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)");

                    b.Property<DateTimeOffset?>("ModifiedTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.HasKey("Id");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("Feedback.Analyzer.Domain.Entities.CustomerFeedback", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasMaxLength(2048)
                        .HasColumnType("character varying(2048)");

                    b.Property<DateTimeOffset>("CreatedTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTimeOffset?>("DeletedTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("ModifiedTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uuid");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("Feedbacks");
                });

            modelBuilder.Entity("Feedback.Analyzer.Domain.Entities.FeedbackAnalysisResult", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("CategorizedOpinions")
                        .IsRequired()
                        .HasColumnType("json");

                    b.Property<DateTimeOffset>("CreatedTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("CustomerFeedbackId")
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset?>("DeletedTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("EntityIdentifications")
                        .IsRequired()
                        .HasColumnType("json");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("ModifiedTime")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("CustomerFeedbackId")
                        .IsUnique();

                    b.ToTable("FeedbackAnalysisResults");
                });

            modelBuilder.Entity("Feedback.Analyzer.Domain.Entities.FeedbackExecutionWorkflow", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset>("CreatedTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTimeOffset?>("DeletedTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("ModifiedTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("EntryExecutionOptionId")
                        .HasColumnType("uuid");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("EntryExecutionOptionId")
                        .IsUnique();

                    b.HasIndex("Name", "ProductId")
                        .IsUnique();

                    b.ToTable("FeedbackExecutionWorkflows");
                });

            modelBuilder.Entity("Feedback.Analyzer.Domain.Entities.Organization", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("ClientId")
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset?>("DeletedTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(8192)
                        .HasColumnType("character varying(8192)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.ToTable("Organizations");
                });

            modelBuilder.Entity("Feedback.Analyzer.Domain.Entities.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset>("CreatedTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTimeOffset?>("DeletedTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(8192)
                        .HasColumnType("character varying(8192)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("ModifiedTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<Guid>("OrganizationId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("OrganizationId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Feedback.Analyzer.Domain.Entities.PromptExecutionHistory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Exception")
                        .HasColumnType("text");

                    b.Property<TimeSpan>("ExecutionDuration")
                        .HasColumnType("interval");

                    b.Property<DateTime>("ExecutionTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("PromptId")
                        .HasColumnType("uuid");

                    b.Property<string>("Result")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("PromptId");

                    b.ToTable("PromptExecutionHistories");
                });

            modelBuilder.Entity("Feedback.Analyzer.Domain.Entities.WorkflowExecutionOptions", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AnalysisPromptCategoryId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("ParentId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("AnalysisPromptCategoryId");

                    b.HasIndex("ParentId");

                    b.ToTable("WorkflowExecutionOptions");
                });

            modelBuilder.Entity("Feedback.Analyzer.Domain.Entities.AnalysisPrompt", b =>
                {
                    b.HasOne("Feedback.Analyzer.Domain.Entities.AnalysisPromptCategory", "Category")
                        .WithMany("Prompts")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Feedback.Analyzer.Domain.Entities.AnalysisPromptCategory", b =>
                {
                    b.HasOne("Feedback.Analyzer.Domain.Entities.AnalysisPrompt", "SelectedPrompt")
                        .WithOne()
                        .HasForeignKey("Feedback.Analyzer.Domain.Entities.AnalysisPromptCategory", "SelectedPromptId");

                    b.Navigation("SelectedPrompt");
                });

            modelBuilder.Entity("Feedback.Analyzer.Domain.Entities.CustomerFeedback", b =>
                {
                    b.HasOne("Feedback.Analyzer.Domain.Entities.Product", "Product")
                        .WithMany("CustomerFeedbacks")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Feedback.Analyzer.Domain.Entities.FeedbackAnalysisResult", b =>
                {
                    b.HasOne("Feedback.Analyzer.Domain.Entities.CustomerFeedback", "CustomerFeedback")
                        .WithOne("FeedbackAnalysisResult")
                        .HasForeignKey("Feedback.Analyzer.Domain.Entities.FeedbackAnalysisResult", "CustomerFeedbackId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("Feedback.Analyzer.Domain.Entities.FeedbackActionablePoints", "FeedbackActionablePoints", b1 =>
                        {
                            b1.Property<Guid>("FeedbackAnalysisResultId")
                                .HasColumnType("uuid");

                            b1.Property<string[]>("ActionablePoints")
                                .IsRequired()
                                .HasColumnType("text[]");

                            b1.Property<string[]>("GenericPoints")
                                .IsRequired()
                                .HasColumnType("text[]");

                            b1.Property<string[]>("NonActionablePoints")
                                .IsRequired()
                                .HasColumnType("text[]");

                            b1.Property<string[]>("SpecificPoints")
                                .IsRequired()
                                .HasColumnType("text[]");

                            b1.HasKey("FeedbackAnalysisResultId");

                            b1.ToTable("FeedbackAnalysisResults");

                            b1.WithOwner()
                                .HasForeignKey("FeedbackAnalysisResultId");
                        });

                    b.OwnsOne("Feedback.Analyzer.Domain.Entities.FeedbackEntities", "FeedbackEntities", b1 =>
                        {
                            b1.Property<Guid>("FeedbackAnalysisResultId")
                                .HasColumnType("uuid");

                            b1.Property<string[]>("Facts")
                                .IsRequired()
                                .HasColumnType("text[]");

                            b1.Property<string[]>("Questions")
                                .IsRequired()
                                .HasColumnType("text[]");

                            b1.HasKey("FeedbackAnalysisResultId");

                            b1.ToTable("FeedbackAnalysisResults");

                            b1.WithOwner()
                                .HasForeignKey("FeedbackAnalysisResultId");
                        });

                    b.OwnsOne("Feedback.Analyzer.Domain.Entities.FeedbackMetrics", "FeedbackMetrics", b1 =>
                        {
                            b1.Property<Guid>("FeedbackAnalysisResultId")
                                .HasColumnType("uuid");

                            b1.Property<float>("Ces")
                                .HasColumnType("real");

                            b1.Property<float>("Csat")
                                .HasColumnType("real");

                            b1.Property<float>("Nps")
                                .HasColumnType("real");

                            b1.HasKey("FeedbackAnalysisResultId");

                            b1.ToTable("FeedbackAnalysisResults");

                            b1.WithOwner()
                                .HasForeignKey("FeedbackAnalysisResultId");
                        });

                    b.OwnsOne("Feedback.Analyzer.Domain.Entities.FeedbackOpinion", "FeedbackOpinion", b1 =>
                        {
                            b1.Property<Guid>("FeedbackAnalysisResultId")
                                .HasColumnType("uuid");

                            b1.Property<string[]>("NegativeOpinionPoints")
                                .IsRequired()
                                .HasColumnType("text[]");

                            b1.Property<int>("OverallOpinion")
                                .HasColumnType("integer");

                            b1.Property<string[]>("PositiveOpinionPoints")
                                .IsRequired()
                                .HasColumnType("text[]");

                            b1.HasKey("FeedbackAnalysisResultId");

                            b1.ToTable("FeedbackAnalysisResults");

                            b1.WithOwner()
                                .HasForeignKey("FeedbackAnalysisResultId");
                        });

                    b.OwnsOne("Feedback.Analyzer.Domain.Entities.FeedbackRelevance", "FeedbackRelevance", b1 =>
                        {
                            b1.Property<Guid>("FeedbackAnalysisResultId")
                                .HasColumnType("uuid");

                            b1.Property<string>("ExtractedRelevantContent")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.Property<bool>("IsRelevant")
                                .HasColumnType("boolean");

                            b1.Property<string>("PiiRedactedContent")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.Property<string[]>("RecognizedLanguages")
                                .IsRequired()
                                .HasColumnType("text[]");

                            b1.HasKey("FeedbackAnalysisResultId");

                            b1.ToTable("FeedbackAnalysisResults");

                            b1.WithOwner()
                                .HasForeignKey("FeedbackAnalysisResultId");
                        });

                    b.Navigation("CustomerFeedback");

                    b.Navigation("FeedbackActionablePoints")
                        .IsRequired();

                    b.Navigation("FeedbackEntities")
                        .IsRequired();

                    b.Navigation("FeedbackMetrics")
                        .IsRequired();

                    b.Navigation("FeedbackOpinion")
                        .IsRequired();

                    b.Navigation("FeedbackRelevance")
                        .IsRequired();
                });

            modelBuilder.Entity("Feedback.Analyzer.Domain.Entities.FeedbackExecutionWorkflow", b =>
                {
                    b.HasOne("Feedback.Analyzer.Domain.Entities.WorkflowExecutionOptions", "EntryExecutionOption")
                        .WithOne("FeedbackExecutionWorkflow")
                        .HasForeignKey("Feedback.Analyzer.Domain.Entities.FeedbackExecutionWorkflow", "EntryExecutionOptionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EntryExecutionOption");
                });

            modelBuilder.Entity("Feedback.Analyzer.Domain.Entities.Organization", b =>
                {
                    b.HasOne("Feedback.Analyzer.Domain.Entities.Client", "Client")
                        .WithMany("Organizations")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");
                });

            modelBuilder.Entity("Feedback.Analyzer.Domain.Entities.Product", b =>
                {
                    b.HasOne("Feedback.Analyzer.Domain.Entities.Organization", "Organization")
                        .WithMany("Products")
                        .HasForeignKey("OrganizationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Organization");
                });

            modelBuilder.Entity("Feedback.Analyzer.Domain.Entities.PromptExecutionHistory", b =>
                {
                    b.HasOne("Feedback.Analyzer.Domain.Entities.AnalysisPrompt", "Prompt")
                        .WithMany("ExecutionHistories")
                        .HasForeignKey("PromptId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Prompt");
                });

            modelBuilder.Entity("Feedback.Analyzer.Domain.Entities.WorkflowExecutionOptions", b =>
                {
                    b.HasOne("Feedback.Analyzer.Domain.Entities.AnalysisPromptCategory", "AnalysisPromptCategory")
                        .WithMany("FeedbackWorkflowExecutionOptions")
                        .HasForeignKey("AnalysisPromptCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Feedback.Analyzer.Domain.Entities.WorkflowExecutionOptions", null)
                        .WithMany("ChildExecutionOptions")
                        .HasForeignKey("ParentId");

                    b.Navigation("AnalysisPromptCategory");
                });

            modelBuilder.Entity("Feedback.Analyzer.Domain.Entities.AnalysisPrompt", b =>
                {
                    b.Navigation("ExecutionHistories");
                });

            modelBuilder.Entity("Feedback.Analyzer.Domain.Entities.AnalysisPromptCategory", b =>
                {
                    b.Navigation("FeedbackWorkflowExecutionOptions");

                    b.Navigation("Prompts");
                });

            modelBuilder.Entity("Feedback.Analyzer.Domain.Entities.Client", b =>
                {
                    b.Navigation("Organizations");
                });

            modelBuilder.Entity("Feedback.Analyzer.Domain.Entities.CustomerFeedback", b =>
                {
                    b.Navigation("FeedbackAnalysisResult")
                        .IsRequired();
                });

            modelBuilder.Entity("Feedback.Analyzer.Domain.Entities.Organization", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("Feedback.Analyzer.Domain.Entities.Product", b =>
                {
                    b.Navigation("CustomerFeedbacks");
                });

            modelBuilder.Entity("Feedback.Analyzer.Domain.Entities.WorkflowExecutionOptions", b =>
                {
                    b.Navigation("ChildExecutionOptions");

                    b.Navigation("FeedbackExecutionWorkflow")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
