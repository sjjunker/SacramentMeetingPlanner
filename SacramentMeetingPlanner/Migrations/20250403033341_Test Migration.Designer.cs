﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SacramentMeetingPlanner.Data;

#nullable disable

namespace SacramentMeetingPlanner.Migrations
{
    [DbContext(typeof(SacramentMeetingPlannerContext))]
    [Migration("20250403033341_Test Migration")]
    partial class TestMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SacramentMeetingPlanner.Models.Planner", b =>
                {
                    b.Property<int>("PlannerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PlannerId"));

                    b.Property<string>("Benediction")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClosingHymn")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Conducting")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("IntermediateHymn")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Invocation")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OpeningHymn")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SacramentHymn")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PlannerId");

                    b.ToTable("Planner");
                });

            modelBuilder.Entity("SacramentMeetingPlanner.Models.SpeakerTopic", b =>
                {
                    b.Property<int>("SpeakerTopicId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SpeakerTopicId"));

                    b.Property<int>("PlannerId")
                        .HasColumnType("int");

                    b.Property<string>("Speaker")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Topic")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SpeakerTopicId");

                    b.HasIndex("PlannerId");

                    b.ToTable("SpeakerTopic");
                });

            modelBuilder.Entity("SacramentMeetingPlanner.Models.SpeakerTopic", b =>
                {
                    b.HasOne("SacramentMeetingPlanner.Models.Planner", null)
                        .WithMany("SpeakersTopics")
                        .HasForeignKey("PlannerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SacramentMeetingPlanner.Models.Planner", b =>
                {
                    b.Navigation("SpeakersTopics");
                });
#pragma warning restore 612, 618
        }
    }
}
