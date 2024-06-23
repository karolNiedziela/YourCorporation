﻿// <auto-generated />
using System;
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
    [Migration("20240610094720_Recruitment_Add_JobApplication_Candidate_WorkLocation")]
    partial class Recruitment_Add_JobApplication_Candidate_WorkLocation
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("recruitment")
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("YourCorporation.Modules.Recruitment.Core.Candidates.Candidate", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("BirthDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("BirthDate");

                    b.Property<string>("FirstName")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("FirstName");

                    b.Property<string>("LastName")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("LastName");

                    b.Property<string>("LinkedinUrl")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("LinkedinUrl");

                    b.Property<string>("PrivateEmail")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("PrivateEmail");

                    b.Property<string>("PrivatePhone")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("PrivatePhone");

                    b.HasKey("Id");

                    b.ToTable("Candidates", "recruitment");
                });

            modelBuilder.Entity("YourCorporation.Modules.Recruitment.Core.JobApplications.JobApplication", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CVUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("CandidateId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("JobApplicationStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("JobOfferSubmissionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("JobApplications", "recruitment");
                });

            modelBuilder.Entity("YourCorporation.Modules.Recruitment.Core.JobApplications.JobApplicationChosenWorkLocation", b =>
                {
                    b.Property<Guid>("JobApplicationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("WorkLocationId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("JobApplicationId", "WorkLocationId");

                    b.HasIndex("WorkLocationId");

                    b.HasIndex("JobApplicationId", "WorkLocationId");

                    b.ToTable("JobApplicationChosenWorkLocations", "recruitment");
                });

            modelBuilder.Entity("YourCorporation.Modules.Recruitment.Core.WorkLocations.WorkLocation", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("WorkLocations", "recruitment");
                });

            modelBuilder.Entity("YourCorporation.Shared.Abstractions.Messaging.Inbox.InboxMessage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

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

                    b.ToTable("Inbox", "recruitment");
                });

            modelBuilder.Entity("YourCorporation.Shared.Abstractions.Messaging.Outbox.OutboxMessage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

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

                    b.ToTable("Outbox", "recruitment");
                });

            modelBuilder.Entity("YourCorporation.Modules.Recruitment.Core.JobApplications.JobApplication", b =>
                {
                    b.OwnsOne("YourCorporation.Modules.Recruitment.Core.JobApplications.ValueObjects.JobOffer", "JobOffer", b1 =>
                        {
                            b1.Property<Guid>("JobApplicationId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<Guid>("Id")
                                .HasColumnType("uniqueidentifier")
                                .HasColumnName("JobOfferId");

                            b1.Property<string>("Name")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("JobOfferName");

                            b1.HasKey("JobApplicationId");

                            b1.ToTable("JobApplications", "recruitment");

                            b1.WithOwner()
                                .HasForeignKey("JobApplicationId");
                        });

                    b.Navigation("JobOffer");
                });

            modelBuilder.Entity("YourCorporation.Modules.Recruitment.Core.JobApplications.JobApplicationChosenWorkLocation", b =>
                {
                    b.HasOne("YourCorporation.Modules.Recruitment.Core.JobApplications.JobApplication", null)
                        .WithMany("ChosenWorkLocations")
                        .HasForeignKey("JobApplicationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("YourCorporation.Modules.Recruitment.Core.WorkLocations.WorkLocation", null)
                        .WithMany()
                        .HasForeignKey("WorkLocationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("YourCorporation.Modules.Recruitment.Core.JobApplications.JobApplication", b =>
                {
                    b.Navigation("ChosenWorkLocations");
                });
#pragma warning restore 612, 618
        }
    }
}
