﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using vaccine_chain_bk.Models;

#nullable disable

namespace vaccine_chain_bk.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241124165315_addAdminAccount")]
    partial class addAdminAccount
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.20")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("vaccine_chain_bk.Models.Device", b =>
                {
                    b.Property<string>("DeviceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SensorType")
                        .HasColumnType("int");

                    b.HasKey("DeviceId");

                    b.ToTable("Devices");
                });

            modelBuilder.Entity("vaccine_chain_bk.Models.Dose", b =>
                {
                    b.Property<int>("DoseNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DoseNumber"));

                    b.Property<string>("Administrator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateAdministered")
                        .HasColumnType("datetime2");

                    b.Property<string>("LocationAdministered")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VaccineId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("DoseNumber");

                    b.HasIndex("VaccineId");

                    b.ToTable("Doses");
                });

            modelBuilder.Entity("vaccine_chain_bk.Models.Log", b =>
                {
                    b.Property<int>("LogId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LogId"));

                    b.Property<string>("DeviceId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("Status")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Timestamp")
                        .HasColumnType("datetime2");

                    b.Property<string>("Unit")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VaccineId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<double?>("Value")
                        .HasColumnType("float");

                    b.HasKey("LogId");

                    b.HasIndex("DeviceId");

                    b.HasIndex("VaccineId");

                    b.ToTable("Logs");
                });

            modelBuilder.Entity("vaccine_chain_bk.Models.Role", b =>
                {
                    b.Property<Guid>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RoleId");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            RoleId = new Guid("7140fc1c-7e7b-4b5b-aeec-70ac91de934f"),
                            Name = "Admin"
                        },
                        new
                        {
                            RoleId = new Guid("be12fb13-04d6-4acf-8ad4-07927308bb6c"),
                            Name = "User"
                        });
                });

            modelBuilder.Entity("vaccine_chain_bk.Models.User", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProfilePicture")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("UserId");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            UserId = new Guid("bc2e7c38-0214-4489-9dd2-eafbce0a71b0"),
                            Address = "Admin Address",
                            DateOfBirth = new DateTime(2002, 8, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "admin@gmail.com",
                            FirstName = "Admin",
                            LastName = "Admin",
                            Password = "AQAAAAIAAYagAAAAECLbiaF8ayWY4vet6Vsoa/u5cJ9RV3l2xJVQ6wDMAF9eQYImsIB2SD0DLwgMRlxe2g==",
                            RoleId = new Guid("7140fc1c-7e7b-4b5b-aeec-70ac91de934f"),
                            Status = 0
                        });
                });

            modelBuilder.Entity("vaccine_chain_bk.Models.Vaccine", b =>
                {
                    b.Property<string>("VaccineId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("BatchNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ExpirationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Manufacturer")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VaccineName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("VaccineId");

                    b.ToTable("Vaccines");
                });

            modelBuilder.Entity("vaccine_chain_bk.Models.Dose", b =>
                {
                    b.HasOne("vaccine_chain_bk.Models.Vaccine", "Vaccine")
                        .WithMany("Doses")
                        .HasForeignKey("VaccineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Vaccine");
                });

            modelBuilder.Entity("vaccine_chain_bk.Models.Log", b =>
                {
                    b.HasOne("vaccine_chain_bk.Models.Device", "Device")
                        .WithMany("Logs")
                        .HasForeignKey("DeviceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("vaccine_chain_bk.Models.Vaccine", "Vaccine")
                        .WithMany("Logs")
                        .HasForeignKey("VaccineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Device");

                    b.Navigation("Vaccine");
                });

            modelBuilder.Entity("vaccine_chain_bk.Models.User", b =>
                {
                    b.HasOne("vaccine_chain_bk.Models.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("vaccine_chain_bk.Models.Device", b =>
                {
                    b.Navigation("Logs");
                });

            modelBuilder.Entity("vaccine_chain_bk.Models.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("vaccine_chain_bk.Models.Vaccine", b =>
                {
                    b.Navigation("Doses");

                    b.Navigation("Logs");
                });
#pragma warning restore 612, 618
        }
    }
}
