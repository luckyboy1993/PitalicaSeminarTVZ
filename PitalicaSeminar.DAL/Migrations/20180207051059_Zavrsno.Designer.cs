﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using PitalicaSeminar.DAL.Entities;
using System;

namespace PitalicaSeminar.DAL.Migrations
{
    [DbContext(typeof(PitalicaDbContext))]
    [Migration("20180207051059_Zavrsno")]
    partial class Zavrsno
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PitalicaSeminar.DAL.Entities.Addresses", b =>
                {
                    b.Property<int>("AddressId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CityName")
                        .IsRequired();

                    b.Property<string>("Country")
                        .IsRequired();

                    b.Property<string>("StreetName")
                        .IsRequired();

                    b.HasKey("AddressId");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("PitalicaSeminar.DAL.Entities.ExamResults", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ExamId");

                    b.Property<string>("Score")
                        .IsRequired()
                        .HasColumnType("nchar(10)");

                    b.Property<int>("UserId");

                    b.Property<DateTime>("WriteDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.HasKey("Id");

                    b.HasIndex("ExamId");

                    b.HasIndex("UserId");

                    b.ToTable("ExamResults");
                });

            modelBuilder.Entity("PitalicaSeminar.DAL.Entities.Exams", b =>
                {
                    b.Property<int>("ExamId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("ExamName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<int?>("MaxScore");

                    b.Property<string>("Password")
                        .IsRequired();

                    b.Property<int>("UserId");

                    b.HasKey("ExamId");

                    b.HasIndex("UserId");

                    b.ToTable("Exams");
                });

            modelBuilder.Entity("PitalicaSeminar.DAL.Entities.Questions", b =>
                {
                    b.Property<int>("QuestionId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Answer")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<string>("Definition")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<int>("ExamId");

                    b.Property<int>("Score");

                    b.Property<bool?>("Visibility")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("((1))");

                    b.HasKey("QuestionId");

                    b.HasIndex("ExamId");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("PitalicaSeminar.DAL.Entities.Schools", b =>
                {
                    b.Property<int>("SchoolId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AddressId");

                    b.Property<string>("Name");

                    b.HasKey("SchoolId");

                    b.HasIndex("AddressId");

                    b.ToTable("Schools");
                });

            modelBuilder.Entity("PitalicaSeminar.DAL.Entities.Users", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Password")
                        .IsRequired();

                    b.Property<int>("SchoolId")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("((0))");

                    b.Property<string>("StringUserId")
                        .IsRequired()
                        .HasColumnName("StringUserID")
                        .HasMaxLength(450)
                        .IsUnicode(false);

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.HasKey("UserId");

                    b.HasIndex("SchoolId");

                    b.HasIndex("StringUserId")
                        .IsUnique()
                        .HasName("AK_StringUserID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("PitalicaSeminar.DAL.Entities.ExamResults", b =>
                {
                    b.HasOne("PitalicaSeminar.DAL.Entities.Exams", "Exam")
                        .WithMany("ExamResults")
                        .HasForeignKey("ExamId")
                        .HasConstraintName("FK_ExamResult_Exams");

                    b.HasOne("PitalicaSeminar.DAL.Entities.Users", "User")
                        .WithMany("ExamResults")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_ExamResult_Users");
                });

            modelBuilder.Entity("PitalicaSeminar.DAL.Entities.Exams", b =>
                {
                    b.HasOne("PitalicaSeminar.DAL.Entities.Users", "User")
                        .WithMany("Exams")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_Exam_Users");
                });

            modelBuilder.Entity("PitalicaSeminar.DAL.Entities.Questions", b =>
                {
                    b.HasOne("PitalicaSeminar.DAL.Entities.Exams", "Exam")
                        .WithMany("Questions")
                        .HasForeignKey("ExamId")
                        .HasConstraintName("FK_Question_Exams");
                });

            modelBuilder.Entity("PitalicaSeminar.DAL.Entities.Schools", b =>
                {
                    b.HasOne("PitalicaSeminar.DAL.Entities.Addresses", "Address")
                        .WithMany("Schools")
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PitalicaSeminar.DAL.Entities.Users", b =>
                {
                    b.HasOne("PitalicaSeminar.DAL.Entities.Schools", "School")
                        .WithMany("Users")
                        .HasForeignKey("SchoolId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
