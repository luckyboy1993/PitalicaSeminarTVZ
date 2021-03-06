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
    [Migration("20180205153753_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PitalicaSeminar.DAL.Entities.Exam", b =>
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

            modelBuilder.Entity("PitalicaSeminar.DAL.Entities.ExamResult", b =>
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

            modelBuilder.Entity("PitalicaSeminar.DAL.Entities.Question", b =>
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

            modelBuilder.Entity("PitalicaSeminar.DAL.Entities.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Password")
                        .IsRequired();

                    b.Property<string>("StringUserID")
                        .IsRequired()
                        .HasMaxLength(450)
                        .IsUnicode(false);

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("PitalicaSeminar.DAL.Entities.Exam", b =>
                {
                    b.HasOne("PitalicaSeminar.DAL.Entities.User", "User")
                        .WithMany("Exams")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_Exam_Users");
                });

            modelBuilder.Entity("PitalicaSeminar.DAL.Entities.ExamResult", b =>
                {
                    b.HasOne("PitalicaSeminar.DAL.Entities.Exam", "Exam")
                        .WithMany("ExamResults")
                        .HasForeignKey("ExamId")
                        .HasConstraintName("FK_ExamResult_Exams");

                    b.HasOne("PitalicaSeminar.DAL.Entities.User", "User")
                        .WithMany("ExamResults")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_ExamResult_Users");
                });

            modelBuilder.Entity("PitalicaSeminar.DAL.Entities.Question", b =>
                {
                    b.HasOne("PitalicaSeminar.DAL.Entities.Exam", "Exam")
                        .WithMany("Questions")
                        .HasForeignKey("ExamId")
                        .HasConstraintName("FK_Question_Exams");
                });
#pragma warning restore 612, 618
        }
    }
}
