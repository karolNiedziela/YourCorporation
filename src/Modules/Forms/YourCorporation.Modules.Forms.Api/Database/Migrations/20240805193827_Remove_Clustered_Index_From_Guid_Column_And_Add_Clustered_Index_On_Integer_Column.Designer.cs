﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using YourCorporation.Modules.Forms.Api.Database;

#nullable disable

namespace YourCorporation.Modules.Forms.Api.Database.Migrations
{
    [DbContext(typeof(FormsDbContext))]
    [Migration("20240805193827_Remove_Clustered_Index_From_Guid_Column_And_Add_Clustered_Index_On_Integer_Column")]
    partial class Remove_Clustered_Index_From_Guid_Column_And_Add_Clustered_Index_On_Integer_Column
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("forms")
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("YourCorporation.Modules.Forms.Api.Entities.FormSubmissions.EventSubmissions.EventSubmission", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("ClusterId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ClusterId"));

                    b.Property<string>("Email")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<Guid>("EventFormId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FirstName")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("LastName")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    SqlServerKeyBuilderExtensions.IsClustered(b.HasKey("Id"), false);

                    b.HasIndex("ClusterId");

                    SqlServerIndexBuilderExtensions.IsClustered(b.HasIndex("ClusterId"));

                    b.HasIndex("EventFormId");

                    b.ToTable("EventSubmissions", "forms");
                });

            modelBuilder.Entity("YourCorporation.Modules.Forms.Api.Entities.FormSubmissions.JobOfferSubmissions.JobOfferSubmission", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CVUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ClusterId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ClusterId"));

                    b.Property<string>("Email")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("FirstName")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<Guid>("JobOfferFormId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LastName")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    SqlServerKeyBuilderExtensions.IsClustered(b.HasKey("Id"), false);

                    b.HasIndex("ClusterId");

                    SqlServerIndexBuilderExtensions.IsClustered(b.HasIndex("ClusterId"));

                    b.HasIndex("JobOfferFormId");

                    b.ToTable("JobOfferSubmissions", "forms");
                });

            modelBuilder.Entity("YourCorporation.Modules.Forms.Api.Entities.FormSubmissions.JobOfferSubmissions.JobOfferSubmissionChosenWorkLocation", b =>
                {
                    b.Property<Guid>("JobOfferSubmissionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("WorkLocationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("ClusterId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ClusterId"));

                    b.HasKey("JobOfferSubmissionId", "WorkLocationId");

                    SqlServerKeyBuilderExtensions.IsClustered(b.HasKey("JobOfferSubmissionId", "WorkLocationId"), false);

                    b.HasIndex("ClusterId");

                    SqlServerIndexBuilderExtensions.IsClustered(b.HasIndex("ClusterId"));

                    b.HasIndex("WorkLocationId");

                    b.ToTable("JobOfferSubmissionChosenWorkLocation", "forms");
                });

            modelBuilder.Entity("YourCorporation.Modules.Forms.Api.Entities.Forms.EventForms.EventForm", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("ClusterId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ClusterId"));

                    b.Property<DateTimeOffset>("EndTime")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("EventDescription")
                        .HasMaxLength(2000)
                        .HasColumnType("nvarchar(2000)");

                    b.Property<Guid>("EventId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("EventName")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool>("IsUniqueSubmission")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("Name")
                        .HasMaxLength(108)
                        .HasColumnType("nvarchar(108)");

                    b.Property<DateTimeOffset>("StartTime")
                        .HasColumnType("datetimeoffset");

                    b.HasKey("Id");

                    SqlServerKeyBuilderExtensions.IsClustered(b.HasKey("Id"), false);

                    b.HasIndex("ClusterId");

                    SqlServerIndexBuilderExtensions.IsClustered(b.HasIndex("ClusterId"));

                    b.HasIndex("EventId")
                        .IsUnique();

                    b.ToTable("EventForms", "forms");
                });

            modelBuilder.Entity("YourCorporation.Modules.Forms.Api.Entities.Forms.JobOfferForms.JobOfferForm", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("ClusterId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ClusterId"));

                    b.Property<bool>("IsUniqueSubmission")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<Guid>("JobOfferId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasMaxLength(108)
                        .HasColumnType("nvarchar(108)");

                    b.HasKey("Id");

                    SqlServerKeyBuilderExtensions.IsClustered(b.HasKey("Id"), false);

                    b.HasIndex("ClusterId");

                    SqlServerIndexBuilderExtensions.IsClustered(b.HasIndex("ClusterId"));

                    b.HasIndex("JobOfferId")
                        .IsUnique();

                    b.ToTable("JobOfferForms", "forms");
                });

            modelBuilder.Entity("YourCorporation.Modules.Forms.Api.Entities.Forms.JobOfferForms.JobOfferFormWorkLocation", b =>
                {
                    b.Property<Guid>("JobOfferFormId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("WorkLocationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("ClusterId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ClusterId"));

                    b.HasKey("JobOfferFormId", "WorkLocationId");

                    SqlServerKeyBuilderExtensions.IsClustered(b.HasKey("JobOfferFormId", "WorkLocationId"), false);

                    b.HasIndex("ClusterId");

                    SqlServerIndexBuilderExtensions.IsClustered(b.HasIndex("ClusterId"));

                    b.HasIndex("WorkLocationId");

                    b.ToTable("JobOfferFormWorkLocation", "forms");
                });

            modelBuilder.Entity("YourCorporation.Modules.Forms.Api.Entities.WorkLocations.WorkLocation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("ClusterId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ClusterId"));

                    b.Property<string>("Code")
                        .HasMaxLength(6)
                        .HasColumnType("nvarchar(6)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    SqlServerKeyBuilderExtensions.IsClustered(b.HasKey("Id"), false);

                    b.HasIndex("ClusterId");

                    SqlServerIndexBuilderExtensions.IsClustered(b.HasIndex("ClusterId"));

                    b.ToTable("WorkLocations", "forms");
                });

            modelBuilder.Entity("YourCorporation.Shared.Abstractions.Messaging.Inbox.InboxMessage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("ClusterId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ClusterId"));

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("CorrelationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ProcessedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ReceivedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    SqlServerKeyBuilderExtensions.IsClustered(b.HasKey("Id"), false);

                    b.HasIndex("ClusterId");

                    SqlServerIndexBuilderExtensions.IsClustered(b.HasIndex("ClusterId"));

                    b.ToTable("Inbox", "forms");
                });

            modelBuilder.Entity("YourCorporation.Shared.Abstractions.Messaging.Outbox.OutboxMessage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("ClusterId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ClusterId"));

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("CorrelationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("SentAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("TraceId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    SqlServerKeyBuilderExtensions.IsClustered(b.HasKey("Id"), false);

                    b.HasIndex("ClusterId");

                    SqlServerIndexBuilderExtensions.IsClustered(b.HasIndex("ClusterId"));

                    b.ToTable("Outbox", "forms");
                });

            modelBuilder.Entity("YourCorporation.Modules.Forms.Api.Entities.FormSubmissions.EventSubmissions.EventSubmission", b =>
                {
                    b.HasOne("YourCorporation.Modules.Forms.Api.Entities.Forms.EventForms.EventForm", "EventForm")
                        .WithMany("Submissions")
                        .HasForeignKey("EventFormId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("EventForm");
                });

            modelBuilder.Entity("YourCorporation.Modules.Forms.Api.Entities.FormSubmissions.JobOfferSubmissions.JobOfferSubmission", b =>
                {
                    b.HasOne("YourCorporation.Modules.Forms.Api.Entities.Forms.JobOfferForms.JobOfferForm", "JobOfferForm")
                        .WithMany("Submissions")
                        .HasForeignKey("JobOfferFormId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("JobOfferForm");
                });

            modelBuilder.Entity("YourCorporation.Modules.Forms.Api.Entities.FormSubmissions.JobOfferSubmissions.JobOfferSubmissionChosenWorkLocation", b =>
                {
                    b.HasOne("YourCorporation.Modules.Forms.Api.Entities.FormSubmissions.JobOfferSubmissions.JobOfferSubmission", null)
                        .WithMany("JobOfferSubmissionChosenWorkLocations")
                        .HasForeignKey("JobOfferSubmissionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("YourCorporation.Modules.Forms.Api.Entities.WorkLocations.WorkLocation", null)
                        .WithMany()
                        .HasForeignKey("WorkLocationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("YourCorporation.Modules.Forms.Api.Entities.Forms.JobOfferForms.JobOfferFormWorkLocation", b =>
                {
                    b.HasOne("YourCorporation.Modules.Forms.Api.Entities.Forms.JobOfferForms.JobOfferForm", null)
                        .WithMany("JobOfferFormWorkLocations")
                        .HasForeignKey("JobOfferFormId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("YourCorporation.Modules.Forms.Api.Entities.WorkLocations.WorkLocation", null)
                        .WithMany()
                        .HasForeignKey("WorkLocationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("YourCorporation.Modules.Forms.Api.Entities.FormSubmissions.JobOfferSubmissions.JobOfferSubmission", b =>
                {
                    b.Navigation("JobOfferSubmissionChosenWorkLocations");
                });

            modelBuilder.Entity("YourCorporation.Modules.Forms.Api.Entities.Forms.EventForms.EventForm", b =>
                {
                    b.Navigation("Submissions");
                });

            modelBuilder.Entity("YourCorporation.Modules.Forms.Api.Entities.Forms.JobOfferForms.JobOfferForm", b =>
                {
                    b.Navigation("JobOfferFormWorkLocations");

                    b.Navigation("Submissions");
                });
#pragma warning restore 612, 618
        }
    }
}
