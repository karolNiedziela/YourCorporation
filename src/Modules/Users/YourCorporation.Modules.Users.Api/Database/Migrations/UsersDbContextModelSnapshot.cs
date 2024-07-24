﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using YourCorporation.Modules.Users.Api.Database;

#nullable disable

namespace YourCorporation.Modules.Users.Api.Database.Migrations
{
    [DbContext(typeof(UsersDbContext))]
    partial class UsersDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("users")
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("YourCorporation.Modules.Users.Api.Entities.Permission", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("ClusterId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ClusterId"));

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    SqlServerKeyBuilderExtensions.IsClustered(b.HasKey("Id"), false);

                    b.HasIndex("ClusterId");

                    SqlServerIndexBuilderExtensions.IsClustered(b.HasIndex("ClusterId"));

                    b.ToTable("Permissions", "users");
                });

            modelBuilder.Entity("YourCorporation.Modules.Users.Api.Entities.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("ClusterId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ClusterId"));

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    SqlServerKeyBuilderExtensions.IsClustered(b.HasKey("Id"), false);

                    b.HasIndex("ClusterId");

                    SqlServerIndexBuilderExtensions.IsClustered(b.HasIndex("ClusterId"));

                    b.ToTable("Roles", "users");
                });

            modelBuilder.Entity("YourCorporation.Modules.Users.Api.Entities.RolePermission", b =>
                {
                    b.Property<Guid>("PermissionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("ClusterId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ClusterId"));

                    b.HasKey("PermissionId", "RoleId");

                    SqlServerKeyBuilderExtensions.IsClustered(b.HasKey("PermissionId", "RoleId"), false);

                    b.HasIndex("ClusterId");

                    SqlServerIndexBuilderExtensions.IsClustered(b.HasIndex("ClusterId"));

                    b.HasIndex("RoleId");

                    b.ToTable("RolePermission", "users");
                });

            modelBuilder.Entity("YourCorporation.Modules.Users.Api.Entities.SystemUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("ClusterId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ClusterId"));

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    SqlServerKeyBuilderExtensions.IsClustered(b.HasKey("Id"), false);

                    b.HasIndex("ClusterId");

                    SqlServerIndexBuilderExtensions.IsClustered(b.HasIndex("ClusterId"));

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasFilter("[Email] IS NOT NULL");

                    b.ToTable("SystemUsers", "users");
                });

            modelBuilder.Entity("YourCorporation.Modules.Users.Api.Entities.SystemUserRole", b =>
                {
                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("SystemUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("ClusterId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ClusterId"));

                    b.HasKey("RoleId", "SystemUserId");

                    SqlServerKeyBuilderExtensions.IsClustered(b.HasKey("RoleId", "SystemUserId"), false);

                    b.HasIndex("ClusterId");

                    SqlServerIndexBuilderExtensions.IsClustered(b.HasIndex("ClusterId"));

                    b.HasIndex("SystemUserId");

                    b.ToTable("SystemUserRole", "users");
                });

            modelBuilder.Entity("YourCorporation.Modules.Users.Api.Entities.RolePermission", b =>
                {
                    b.HasOne("YourCorporation.Modules.Users.Api.Entities.Permission", null)
                        .WithMany()
                        .HasForeignKey("PermissionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("YourCorporation.Modules.Users.Api.Entities.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("YourCorporation.Modules.Users.Api.Entities.SystemUserRole", b =>
                {
                    b.HasOne("YourCorporation.Modules.Users.Api.Entities.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("YourCorporation.Modules.Users.Api.Entities.SystemUser", null)
                        .WithMany()
                        .HasForeignKey("SystemUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
