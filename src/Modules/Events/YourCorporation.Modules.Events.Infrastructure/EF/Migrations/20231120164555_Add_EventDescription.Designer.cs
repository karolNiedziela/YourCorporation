﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using YourCorporation.Modules.Events.Infrastructure.EF;

#nullable disable

namespace YourCorporation.Modules.Events.Infrastructure.EF.Migrations
{
    [DbContext(typeof(EventsDbContext))]
    [Migration("20231120164555_Add_EventDescription")]
    partial class Add_EventDescription
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("events")
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("YourCorporation.Modules.Events.Core.Attendees.Attendee", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Email");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("FirstName");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("LastName");

                    b.HasKey("Id");

                    b.ToTable("Attendee", "events");
                });

            modelBuilder.Entity("YourCorporation.Modules.Events.Core.Events.Entities.ConfirmedEventAttendee", b =>
                {
                    b.Property<Guid>("AttendeeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("EventId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("ConfirmationDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset>("SignUpDate")
                        .HasColumnType("datetimeoffset");

                    b.HasKey("AttendeeId", "EventId");

                    b.HasIndex("EventId");

                    b.ToTable("ConfirmedEventAttendees", "events");
                });

            modelBuilder.Entity("YourCorporation.Modules.Events.Core.Events.Entities.DeclaredEventAttendee", b =>
                {
                    b.Property<Guid>("AttendeeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("EventId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("SignUpDate")
                        .HasColumnType("datetimeoffset");

                    b.HasKey("AttendeeId", "EventId");

                    b.HasIndex("EventId");

                    b.ToTable("DeclaredEventAttendees", "events");
                });

            modelBuilder.Entity("YourCorporation.Modules.Events.Core.Events.Entities.EventSpeaker", b =>
                {
                    b.Property<Guid>("SpeakerId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("SpeakerId");

                    b.Property<Guid>("EventId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("EventId");

                    b.HasKey("SpeakerId", "EventId");

                    b.HasIndex("EventId");

                    b.ToTable("EventSpeaker", "events");
                });

            modelBuilder.Entity("YourCorporation.Modules.Events.Core.Events.Entities.WaitlistedEventAttendee", b =>
                {
                    b.Property<Guid>("EventId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AttendeeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsMovedToDeclaredAttendee")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("MovedToDeclaredAttendee")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset>("SignUpDate")
                        .HasColumnType("datetimeoffset");

                    b.HasKey("EventId", "AttendeeId");

                    b.HasIndex("AttendeeId");

                    b.ToTable("WaitlistedEventAttendees", "events");
                });

            modelBuilder.Entity("YourCorporation.Modules.Events.Core.Events.Event", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Description");

                    b.Property<string>("Mode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Name");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Events", "events");
                });

            modelBuilder.Entity("YourCorporation.Modules.Events.Core.Sessions.Entities.SessionSpeaker", b =>
                {
                    b.Property<Guid>("SpeakerId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("SpeakerId");

                    b.Property<Guid>("SessionId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("SessionId");

                    b.HasKey("SpeakerId", "SessionId");

                    b.HasIndex("SessionId");

                    b.ToTable("SessionSpeaker", "events");
                });

            modelBuilder.Entity("YourCorporation.Modules.Events.Core.Sessions.Session", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("EventId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("EventId");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Name");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.ToTable("Sessions", "events");
                });

            modelBuilder.Entity("YourCorporation.Modules.Events.Core.Speakers.Speaker", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("FirstName");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("LastName");

                    b.HasKey("Id");

                    b.ToTable("Speaker", "events");
                });

            modelBuilder.Entity("YourCorporation.Modules.Events.Core.Events.Entities.ConfirmedEventAttendee", b =>
                {
                    b.HasOne("YourCorporation.Modules.Events.Core.Attendees.Attendee", null)
                        .WithMany()
                        .HasForeignKey("AttendeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("YourCorporation.Modules.Events.Core.Events.Event", null)
                        .WithMany("ConfirmedAttendees")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("YourCorporation.Modules.Events.Core.Events.Entities.DeclaredEventAttendee", b =>
                {
                    b.HasOne("YourCorporation.Modules.Events.Core.Attendees.Attendee", null)
                        .WithMany()
                        .HasForeignKey("AttendeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("YourCorporation.Modules.Events.Core.Events.Event", null)
                        .WithMany("DeclaredAttendees")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("YourCorporation.Modules.Events.Core.Events.Entities.EventSpeaker", b =>
                {
                    b.HasOne("YourCorporation.Modules.Events.Core.Events.Event", null)
                        .WithMany("Speakers")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("YourCorporation.Modules.Events.Core.Speakers.Speaker", null)
                        .WithMany()
                        .HasForeignKey("SpeakerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("YourCorporation.Modules.Events.Core.Events.Entities.WaitlistedEventAttendee", b =>
                {
                    b.HasOne("YourCorporation.Modules.Events.Core.Attendees.Attendee", null)
                        .WithMany()
                        .HasForeignKey("AttendeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("YourCorporation.Modules.Events.Core.Events.Event", null)
                        .WithMany("WaitlistedAttendees")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("YourCorporation.Modules.Events.Core.Events.Event", b =>
                {
                    b.OwnsOne("YourCorporation.Modules.Events.Core.Events.ValueObjects.BegginingAndEndOfEvent", "BegginingAndEndOfEvent", b1 =>
                        {
                            b1.Property<Guid>("EventId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<DateTimeOffset>("EndTime")
                                .HasColumnType("datetimeoffset")
                                .HasColumnName("EndTime");

                            b1.Property<DateTimeOffset>("StartTime")
                                .HasColumnType("datetimeoffset")
                                .HasColumnName("StartTime");

                            b1.HasKey("EventId");

                            b1.ToTable("Events", "events");

                            b1.WithOwner()
                                .HasForeignKey("EventId");
                        });

                    b.OwnsOne("YourCorporation.Modules.Events.Core.Events.ValueObjects.EventLimits", "EventLimits", b1 =>
                        {
                            b1.Property<Guid>("EventId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<int?>("AttendeesLimit")
                                .HasColumnType("int")
                                .HasColumnName("AttendeesLimit");

                            b1.HasKey("EventId");

                            b1.ToTable("Events", "events");

                            b1.WithOwner()
                                .HasForeignKey("EventId");
                        });

                    b.Navigation("BegginingAndEndOfEvent");

                    b.Navigation("EventLimits");
                });

            modelBuilder.Entity("YourCorporation.Modules.Events.Core.Sessions.Entities.SessionSpeaker", b =>
                {
                    b.HasOne("YourCorporation.Modules.Events.Core.Sessions.Session", null)
                        .WithMany("Speakers")
                        .HasForeignKey("SessionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("YourCorporation.Modules.Events.Core.Speakers.Speaker", null)
                        .WithMany()
                        .HasForeignKey("SpeakerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("YourCorporation.Modules.Events.Core.Sessions.Session", b =>
                {
                    b.HasOne("YourCorporation.Modules.Events.Core.Events.Event", null)
                        .WithMany()
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("YourCorporation.Modules.Events.Core.Sessions.ValueObjects.BegginingAndEndOfSession", "BegginingAndEndOfSession", b1 =>
                        {
                            b1.Property<Guid>("SessionId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<DateTimeOffset>("EndTime")
                                .HasColumnType("datetimeoffset")
                                .HasColumnName("EndTime");

                            b1.Property<DateTimeOffset>("StartTime")
                                .HasColumnType("datetimeoffset")
                                .HasColumnName("StartTime");

                            b1.HasKey("SessionId");

                            b1.ToTable("Sessions", "events");

                            b1.WithOwner()
                                .HasForeignKey("SessionId");
                        });

                    b.Navigation("BegginingAndEndOfSession");
                });

            modelBuilder.Entity("YourCorporation.Modules.Events.Core.Events.Event", b =>
                {
                    b.Navigation("ConfirmedAttendees");

                    b.Navigation("DeclaredAttendees");

                    b.Navigation("Speakers");

                    b.Navigation("WaitlistedAttendees");
                });

            modelBuilder.Entity("YourCorporation.Modules.Events.Core.Sessions.Session", b =>
                {
                    b.Navigation("Speakers");
                });
#pragma warning restore 612, 618
        }
    }
}
