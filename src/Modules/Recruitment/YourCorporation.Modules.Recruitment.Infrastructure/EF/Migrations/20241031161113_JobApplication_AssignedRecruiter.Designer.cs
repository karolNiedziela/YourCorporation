﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using YourCorporation.Modules.Recruitment.Infrastructure.EF;

#nullable disable

namespace YourCorporation.Modules.Recruitment.Infrastructure.EF.Migrations
{
    [DbContext(typeof(RecruitmentDbContext))]
    [Migration("20241031161113_JobApplication_AssignedRecruiter")]
    partial class JobApplication_AssignedRecruiter
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("recruitment")
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("YourCorporation.Modules.Recruitment.Core.ContactStatuses.ContactStatus", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("ClusterId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ClusterId"));

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Substatus")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    SqlServerKeyBuilderExtensions.IsClustered(b.HasKey("Id"), false);

                    b.HasIndex("ClusterId");

                    SqlServerIndexBuilderExtensions.IsClustered(b.HasIndex("ClusterId"));

                    b.ToTable("ContactStatus", "recruitment");

                    b.HasData(
                        new
                        {
                            Id = new Guid("0380bc27-18de-4683-bd10-37c267f4f979"),
                            Status = "Applicant",
                            Substatus = "Not Verified"
                        });
                });

            modelBuilder.Entity("YourCorporation.Modules.Recruitment.Core.Contacts.Contact", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("BirthDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("BirthDate");

                    b.Property<int>("ClusterId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ClusterId"));

                    b.Property<Guid?>("ContactStatusId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FirstName")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("FirstName");

                    b.Property<string>("LastName")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("LastName");

                    b.Property<string>("LinkedinUrl")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)")
                        .HasColumnName("LinkedinUrl");

                    b.Property<string>("PrivateEmail")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("PrivateEmail");

                    b.Property<string>("PrivatePhone")
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)")
                        .HasColumnName("PrivatePhone");

                    b.HasKey("Id");

                    SqlServerKeyBuilderExtensions.IsClustered(b.HasKey("Id"), false);

                    b.HasIndex("ClusterId");

                    SqlServerIndexBuilderExtensions.IsClustered(b.HasIndex("ClusterId"));

                    b.HasIndex("ContactStatusId");

                    b.ToTable("Contact", "recruitment");
                });

            modelBuilder.Entity("YourCorporation.Modules.Recruitment.Core.JobApplications.JobApplication", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CVUrl")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<int>("ClusterId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ClusterId"));

                    b.Property<Guid?>("ContactId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("JobOfferId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("JobOfferSubmissionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    SqlServerKeyBuilderExtensions.IsClustered(b.HasKey("Id"), false);

                    b.HasIndex("ClusterId");

                    SqlServerIndexBuilderExtensions.IsClustered(b.HasIndex("ClusterId"));

                    b.HasIndex("ContactId");

                    b.ToTable("JobApplication", "recruitment");
                });

            modelBuilder.Entity("YourCorporation.Modules.Recruitment.Core.Queues.RecruitmentQueue", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("ClusterId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ClusterId"));

                    b.ComplexProperty<Dictionary<string, object>>("Name", "YourCorporation.Modules.Recruitment.Core.Queues.RecruitmentQueue.Name#RecruitmentQueueName", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("nvarchar(100)")
                                .HasColumnName("Name");
                        });

                    b.HasKey("Id");

                    SqlServerKeyBuilderExtensions.IsClustered(b.HasKey("Id"), false);

                    b.HasIndex("ClusterId");

                    SqlServerIndexBuilderExtensions.IsClustered(b.HasIndex("ClusterId"));

                    b.ToTable("RecruitmentQueue", "recruitment");
                });

            modelBuilder.Entity("YourCorporation.Modules.Recruitment.Core.WorkLocations.WorkLocation", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("ClusterId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ClusterId"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(6)
                        .HasColumnType("nvarchar(6)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    SqlServerKeyBuilderExtensions.IsClustered(b.HasKey("Id"), false);

                    b.HasIndex("ClusterId");

                    SqlServerIndexBuilderExtensions.IsClustered(b.HasIndex("ClusterId"));

                    b.ToTable("WorkLocation", "recruitment");
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

                    b.ToTable("Inbox", "recruitment");
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

                    b.ToTable("Outbox", "recruitment");
                });

            modelBuilder.Entity("YourCorporation.Modules.Recruitment.Core.Contacts.Contact", b =>
                {
                    b.HasOne("YourCorporation.Modules.Recruitment.Core.ContactStatuses.ContactStatus", "ContactStatus")
                        .WithMany()
                        .HasForeignKey("ContactStatusId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("ContactStatus");
                });

            modelBuilder.Entity("YourCorporation.Modules.Recruitment.Core.JobApplications.JobApplication", b =>
                {
                    b.HasOne("YourCorporation.Modules.Recruitment.Core.Contacts.Contact", null)
                        .WithMany()
                        .HasForeignKey("ContactId");

                    b.OwnsMany("YourCorporation.Modules.Recruitment.Core.WorkLocations.WorkLocationId", "ChosenWorkLocations", b1 =>
                        {
                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            SqlServerPropertyBuilderExtensions.UseIdentityColumn(b1.Property<int>("Id"));

                            b1.Property<Guid>("JobApplicationId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<Guid>("Value")
                                .HasColumnType("uniqueidentifier")
                                .HasColumnName("WorkLocationId");

                            b1.HasKey("Id");

                            b1.HasIndex("JobApplicationId");

                            b1.ToTable("JobApplicationChosenWorkLocation", "recruitment");

                            b1.WithOwner()
                                .HasForeignKey("JobApplicationId");
                        });

                    b.OwnsOne("YourCorporation.Modules.Recruitment.Core.JobApplications.ValueObjects.AssignedRecruiter", "AssignedRecruiter", b1 =>
                        {
                            b1.Property<Guid>("JobApplicationId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("FullName")
                                .HasMaxLength(200)
                                .HasColumnType("nvarchar(200)")
                                .HasColumnName("AssignedRecruiterFullName");

                            b1.Property<Guid?>("Id")
                                .HasColumnType("uniqueidentifier")
                                .HasColumnName("AssignedRecruiterId");

                            b1.HasKey("JobApplicationId");

                            b1.ToTable("JobApplication", "recruitment");

                            b1.WithOwner()
                                .HasForeignKey("JobApplicationId");
                        });

                    b.OwnsMany("YourCorporation.Modules.Recruitment.Core.RecruitmentQueues.ValueObjects.RecruitmentQueueId", "RecruitmentQueues", b1 =>
                        {
                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            SqlServerPropertyBuilderExtensions.UseIdentityColumn(b1.Property<int>("Id"));

                            b1.Property<Guid>("JobApplicationId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<Guid>("Value")
                                .HasColumnType("uniqueidentifier")
                                .HasColumnName("RecruitmentQueueId");

                            b1.HasKey("Id");

                            b1.HasIndex("JobApplicationId");

                            b1.ToTable("JobApplicationRecruitmentQueue", "recruitment");

                            b1.WithOwner()
                                .HasForeignKey("JobApplicationId");
                        });

                    b.Navigation("AssignedRecruiter");

                    b.Navigation("ChosenWorkLocations");

                    b.Navigation("RecruitmentQueues");
                });

            modelBuilder.Entity("YourCorporation.Modules.Recruitment.Core.Queues.RecruitmentQueue", b =>
                {
                    b.OwnsMany("YourCorporation.Modules.Recruitment.Core.WorkLocations.WorkLocationId", "WorkLocations", b1 =>
                        {
                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            SqlServerPropertyBuilderExtensions.UseIdentityColumn(b1.Property<int>("Id"));

                            b1.Property<Guid>("RecruitmentQueueId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<Guid>("Value")
                                .HasColumnType("uniqueidentifier")
                                .HasColumnName("WorkLocationId");

                            b1.HasKey("Id");

                            b1.HasIndex("RecruitmentQueueId");

                            b1.ToTable("RecruitmentQueueWorkLocation", "recruitment");

                            b1.WithOwner()
                                .HasForeignKey("RecruitmentQueueId");
                        });

                    b.Navigation("WorkLocations");
                });
#pragma warning restore 612, 618
        }
    }
}
