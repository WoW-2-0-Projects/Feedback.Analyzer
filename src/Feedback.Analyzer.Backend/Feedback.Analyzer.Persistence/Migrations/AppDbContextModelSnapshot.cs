﻿// <auto-generated />
using System;
using Feedback.Analyzer.Persistence.DataContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Feedback.Analyzer.Persistence.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

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

            modelBuilder.Entity("Feedback.Analyzer.Domain.Entities.NotificationHistory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(129536)
                        .HasColumnType("character varying(129536)");

                    b.Property<DateTimeOffset>("CreatedTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTimeOffset?>("DeletedTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("ErrorMessage")
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsSuccessful")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("ModifiedTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("ReceiverUserId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("SenderUserId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("TemplateId")
                        .HasColumnType("uuid");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("TemplateId");

                    b.ToTable("NotificationHistories", (string)null);

                    b.HasDiscriminator<int>("Type");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Feedback.Analyzer.Domain.Entities.NotificationTemplate", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(129536)
                        .HasColumnType("character varying(129536)");

                    b.Property<DateTimeOffset>("CreatedTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTimeOffset?>("DeletedTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("ModifiedTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("TemplateType")
                        .HasColumnType("integer");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("Type", "TemplateType")
                        .IsUnique();

                    b.ToTable("NotificationTemplates", (string)null);

                    b.HasDiscriminator<int>("Type");

                    b.UseTphMappingStrategy();
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

            modelBuilder.Entity("Feedback.Analyzer.Domain.Entities.EmailHistory", b =>
                {
                    b.HasBaseType("Feedback.Analyzer.Domain.Entities.NotificationHistory");

                    b.Property<Guid>("EmailTemplateId")
                        .HasColumnType("uuid");

                    b.Property<string>("ReceiverEmailAddress")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("SenderEmailAddress")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("Subject")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasIndex("EmailTemplateId");

                    b.HasDiscriminator().HasValue(0);
                });

            modelBuilder.Entity("Feedback.Analyzer.Domain.Entities.SmsHistory", b =>
                {
                    b.HasBaseType("Feedback.Analyzer.Domain.Entities.NotificationHistory");

                    b.Property<string>("ReceiverPhoneNumber")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("SenderPhoneNumber")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<Guid>("SmsTemplateId")
                        .HasColumnType("uuid");

                    b.HasIndex("SmsTemplateId");

                    b.HasDiscriminator().HasValue(1);
                });

            modelBuilder.Entity("Feedback.Analyzer.Domain.Entities.EmailTemplate", b =>
                {
                    b.HasBaseType("Feedback.Analyzer.Domain.Entities.NotificationTemplate");

                    b.Property<string>("Subject")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasDiscriminator().HasValue(0);
                });

            modelBuilder.Entity("Feedback.Analyzer.Domain.Entities.SmsTemplate", b =>
                {
                    b.HasBaseType("Feedback.Analyzer.Domain.Entities.NotificationTemplate");

                    b.HasDiscriminator().HasValue(1);
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

            modelBuilder.Entity("Feedback.Analyzer.Domain.Entities.NotificationHistory", b =>
                {
                    b.HasOne("Feedback.Analyzer.Domain.Entities.NotificationTemplate", "Template")
                        .WithMany("Histories")
                        .HasForeignKey("TemplateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Template");
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

            modelBuilder.Entity("Feedback.Analyzer.Domain.Entities.EmailHistory", b =>
                {
                    b.HasOne("Feedback.Analyzer.Domain.Entities.EmailTemplate", "EmailTemplate")
                        .WithMany()
                        .HasForeignKey("EmailTemplateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EmailTemplate");
                });

            modelBuilder.Entity("Feedback.Analyzer.Domain.Entities.SmsHistory", b =>
                {
                    b.HasOne("Feedback.Analyzer.Domain.Entities.SmsTemplate", "SmsTemplate")
                        .WithMany()
                        .HasForeignKey("SmsTemplateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SmsTemplate");
                });

            modelBuilder.Entity("Feedback.Analyzer.Domain.Entities.Client", b =>
                {
                    b.Navigation("Organizations");
                });

            modelBuilder.Entity("Feedback.Analyzer.Domain.Entities.NotificationTemplate", b =>
                {
                    b.Navigation("Histories");
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
