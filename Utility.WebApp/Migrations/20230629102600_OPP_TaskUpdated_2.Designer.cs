﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Utility.WebApp;

#nullable disable

namespace Utility.WebApp.Migrations
{
    [DbContext(typeof(UtilityDbContext))]
    [Migration("20230629102600_OPP_TaskUpdated_2")]
    partial class OPP_TaskUpdated_2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Utility.WebApp.Domains.OnePercentProgress.OPP_Project", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("OPP_Projects");
                });

            modelBuilder.Entity("Utility.WebApp.Domains.OnePercentProgress.OPP_Task", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<Guid>("OPP_ParentTaskId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("OPP_ProjectId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("OPP_ProjectId");

                    b.ToTable("OPP_Tasks");
                });

            modelBuilder.Entity("Utility.WebApp.Domains.OnePercentProgress.OPP_Task", b =>
                {
                    b.HasOne("Utility.WebApp.Domains.OnePercentProgress.OPP_Project", "OPP_Project")
                        .WithMany("OPP_Tasks")
                        .HasForeignKey("OPP_ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("OPP_Project");
                });

            modelBuilder.Entity("Utility.WebApp.Domains.OnePercentProgress.OPP_Project", b =>
                {
                    b.Navigation("OPP_Tasks");
                });
#pragma warning restore 612, 618
        }
    }
}
