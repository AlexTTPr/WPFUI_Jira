﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using WPFUI_Jira.Models.Repository;

#nullable disable

namespace WPFUI_Jira.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20231223025840_Migration25")]
    partial class Migration25
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ProjectUser", b =>
                {
                    b.Property<int>("WorkProjectsId")
                        .HasColumnType("integer");

                    b.Property<int>("WorkersId")
                        .HasColumnType("integer");

                    b.HasKey("WorkProjectsId", "WorkersId");

                    b.HasIndex("WorkersId");

                    b.ToTable("ProjectWorkers", (string)null);
                });

            modelBuilder.Entity("WPFUI_Jira.Models.ActionRecord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ActorId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CrearionTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int?>("TaskCardId")
                        .HasColumnType("integer");

                    b.Property<TimeSpan>("TimeSpent")
                        .HasColumnType("interval");

                    b.HasKey("Id");

                    b.HasIndex("TaskCardId");

                    b.ToTable("ActionRecord");
                });

            modelBuilder.Entity("WPFUI_Jira.Models.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<int>("OwnerId")
                        .HasColumnType("integer");

                    b.Property<int?>("TaskBoardId")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.HasIndex("TaskBoardId");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("WPFUI_Jira.Models.TaskBoard", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("OwnerId")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("TaskBoards");
                });

            modelBuilder.Entity("WPFUI_Jira.Models.TaskCard", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<int?>("ExecutorId")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("ExpirationTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("TaskListId")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("TaskListId");

                    b.ToTable("TaskCards");
                });

            modelBuilder.Entity("WPFUI_Jira.Models.TaskList", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("TaskBoardId")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("TaskBoardId");

                    b.ToTable("TaskLists");
                });

            modelBuilder.Entity("WPFUI_Jira.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ProjectUser", b =>
                {
                    b.HasOne("WPFUI_Jira.Models.Project", null)
                        .WithMany()
                        .HasForeignKey("WorkProjectsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WPFUI_Jira.Models.User", null)
                        .WithMany()
                        .HasForeignKey("WorkersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WPFUI_Jira.Models.ActionRecord", b =>
                {
                    b.HasOne("WPFUI_Jira.Models.TaskCard", null)
                        .WithMany("Actions")
                        .HasForeignKey("TaskCardId");
                });

            modelBuilder.Entity("WPFUI_Jira.Models.Project", b =>
                {
                    b.HasOne("WPFUI_Jira.Models.User", "Owner")
                        .WithMany("OwnedProjects")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WPFUI_Jira.Models.TaskBoard", "TaskBoard")
                        .WithMany()
                        .HasForeignKey("TaskBoardId");

                    b.Navigation("Owner");

                    b.Navigation("TaskBoard");
                });

            modelBuilder.Entity("WPFUI_Jira.Models.TaskBoard", b =>
                {
                    b.HasOne("WPFUI_Jira.Models.User", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("WPFUI_Jira.Models.TaskCard", b =>
                {
                    b.HasOne("WPFUI_Jira.Models.TaskList", null)
                        .WithMany("TaskCards")
                        .HasForeignKey("TaskListId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WPFUI_Jira.Models.TaskList", b =>
                {
                    b.HasOne("WPFUI_Jira.Models.TaskBoard", null)
                        .WithMany("TaskLists")
                        .HasForeignKey("TaskBoardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WPFUI_Jira.Models.TaskBoard", b =>
                {
                    b.Navigation("TaskLists");
                });

            modelBuilder.Entity("WPFUI_Jira.Models.TaskCard", b =>
                {
                    b.Navigation("Actions");
                });

            modelBuilder.Entity("WPFUI_Jira.Models.TaskList", b =>
                {
                    b.Navigation("TaskCards");
                });

            modelBuilder.Entity("WPFUI_Jira.Models.User", b =>
                {
                    b.Navigation("OwnedProjects");
                });
#pragma warning restore 612, 618
        }
    }
}