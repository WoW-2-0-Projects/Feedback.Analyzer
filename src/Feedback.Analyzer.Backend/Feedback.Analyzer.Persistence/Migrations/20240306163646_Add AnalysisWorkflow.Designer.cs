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
    [Migration("20240306163646_Add AnalysisWorkflow")]
    partial class AddAnalysisWorkflow
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
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<Guid?>("SelectedPromptId")
                        .HasColumnType("uuid");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.HasKey("Id");

                    b.HasIndex("Category")
                        .IsUnique();

                    b.HasIndex("SelectedPromptId")
                        .IsUnique();

                    b.ToTable("PromptCategories");
                });

            modelBuilder.Entity("Feedback.Analyzer.Domain.Entities.AnalysisWorkflow", b =>
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

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.HasKey("Id");

                    b.ToTable("AnalysisWorkflows");
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
                        .HasMaxLength(512)
                        .HasColumnType("character varying(512)");

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
                        .HasMaxLength(512)
                        .HasColumnType("character varying(512)");

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
                        .HasMaxLength(32768)
                        .HasColumnType("character varying(32768)");

                    b.Property<TimeSpan>("ExecutionDuration")
                        .HasColumnType("interval");

                    b.Property<DateTimeOffset>("ExecutionTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("PromptId")
                        .HasColumnType("uuid");

                    b.Property<string>("Result")
                        .HasMaxLength(32768)
                        .HasColumnType("character varying(32768)");

                    b.HasKey("Id");

                    b.HasIndex("PromptId");

                    b.ToTable("PromptExecutionHistories");
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

            modelBuilder.Entity("Feedback.Analyzer.Domain.Entities.AnalysisPrompt", b =>
                {
                    b.Navigation("ExecutionHistories");
                });

            modelBuilder.Entity("Feedback.Analyzer.Domain.Entities.AnalysisPromptCategory", b =>
                {
                    b.Navigation("Prompts");
                });

            modelBuilder.Entity("Feedback.Analyzer.Domain.Entities.Client", b =>
                {
                    b.Navigation("Organizations");
                });

            modelBuilder.Entity("Feedback.Analyzer.Domain.Entities.Organization", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("Feedback.Analyzer.Domain.Entities.Product", b =>
                {
                    b.Navigation("CustomerFeedbacks");
                });
#pragma warning restore 612, 618
        }
    }
}
