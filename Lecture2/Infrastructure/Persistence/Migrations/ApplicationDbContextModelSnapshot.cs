﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Domain.Courses.Course", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("FinishAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("finish_at")
                        .HasDefaultValueSql("timezone('utc', now())");

                    b.Property<string>("MaxStudents")
                        .IsRequired()
                        .HasColumnType("varchar(255)")
                        .HasColumnName("max_students");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(255)")
                        .HasColumnName("name");

                    b.Property<DateTime>("StartAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("start_at")
                        .HasDefaultValueSql("timezone('utc', now())");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at")
                        .HasDefaultValueSql("timezone('utc', now())");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_courses");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_courses_user_id");

                    b.ToTable("courses", (string)null);
                });

            modelBuilder.Entity("Domain.Faculties.Faculty", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(255)")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_faculties");

                    b.ToTable("faculties", (string)null);
                });

            modelBuilder.Entity("Domain.UserCourse.UserCourse", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid>("CourseId")
                        .HasColumnType("uuid")
                        .HasColumnName("course_id");

                    b.Property<DateTime>("EndAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("end_at");

                    b.Property<bool>("IsJoined")
                        .HasColumnType("boolean")
                        .HasColumnName("is_joined");

                    b.Property<DateTime>("JoinAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("join_at");

                    b.Property<string>("Rating")
                        .IsRequired()
                        .HasColumnType("varchar(255)")
                        .HasColumnName("rating");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_user_course");

                    b.HasIndex("CourseId")
                        .HasDatabaseName("ix_user_course_course_id");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_user_course_user_id");

                    b.ToTable("user_course", (string)null);
                });

            modelBuilder.Entity("Domain.Users.User", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid>("FacultyId")
                        .HasColumnType("uuid")
                        .HasColumnName("faculty_id");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("varchar(255)")
                        .HasColumnName("first_name");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("varchar(255)")
                        .HasColumnName("last_name");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at")
                        .HasDefaultValueSql("timezone('utc', now())");

                    b.HasKey("Id")
                        .HasName("pk_users");

                    b.HasIndex("FacultyId")
                        .HasDatabaseName("ix_users_faculty_id");

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("Domain.Courses.Course", b =>
                {
                    b.HasOne("Domain.Users.User", null)
                        .WithMany("Courses")
                        .HasForeignKey("UserId")
                        .HasConstraintName("fk_courses_users_user_id");
                });

            modelBuilder.Entity("Domain.UserCourse.UserCourse", b =>
                {
                    b.HasOne("Domain.Courses.Course", "Course")
                        .WithMany()
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("fk_user_course_courses_course_id");

                    b.HasOne("Domain.Users.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("fk_user_course_users_user_id");

                    b.Navigation("Course");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Users.User", b =>
                {
                    b.HasOne("Domain.Faculties.Faculty", "Faculty")
                        .WithMany()
                        .HasForeignKey("FacultyId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("fk_users_faculties_id");

                    b.Navigation("Faculty");
                });

            modelBuilder.Entity("Domain.Users.User", b =>
                {
                    b.Navigation("Courses");
                });
#pragma warning restore 612, 618
        }
    }
}