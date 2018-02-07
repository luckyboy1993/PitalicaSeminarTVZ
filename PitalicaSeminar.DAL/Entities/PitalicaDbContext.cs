using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PitalicaSeminar.DAL.Entities
{
    public partial class PitalicaDbContext : DbContext
    {
        public virtual DbSet<Addresses> Addresses { get; set; }
        public virtual DbSet<ExamResults> ExamResults { get; set; }
        public virtual DbSet<Exams> Exams { get; set; }
        public virtual DbSet<Questions> Questions { get; set; }
        public virtual DbSet<Schools> Schools { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        public PitalicaDbContext(DbContextOptions<PitalicaDbContext> options)
        : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Addresses>(entity =>
            {
                entity.HasKey(e => e.AddressId);

                entity.Property(e => e.CityName).IsRequired();

                entity.Property(e => e.Country).IsRequired();

                entity.Property(e => e.StreetName).IsRequired();
            });

            modelBuilder.Entity<ExamResults>(entity =>
            {
                entity.HasIndex(e => e.ExamId);

                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.Score)
                    .IsRequired()
                    .HasColumnType("nchar(10)");

                entity.Property(e => e.WriteDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Exam)
                    .WithMany(p => p.ExamResults)
                    .HasForeignKey(d => d.ExamId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ExamResult_Exams");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ExamResults)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ExamResult_Users");
            });

            modelBuilder.Entity<Exams>(entity =>
            {
                entity.HasKey(e => e.ExamId);

                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ExamName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Exams)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Exam_Users");
            });

            modelBuilder.Entity<Questions>(entity =>
            {
                entity.HasKey(e => e.QuestionId);

                entity.HasIndex(e => e.ExamId);

                entity.Property(e => e.Answer)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Definition)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Visibility).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Exam)
                    .WithMany(p => p.Questions)
                    .HasForeignKey(d => d.ExamId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Question_Exams");
            });

            modelBuilder.Entity<Schools>(entity =>
            {
                entity.HasKey(e => e.SchoolId);

                entity.HasIndex(e => e.AddressId);

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.Schools)
                    .HasForeignKey(d => d.AddressId);
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.HasIndex(e => e.SchoolId);

                entity.HasIndex(e => e.StringUserId)
                    .HasName("AK_StringUserID")
                    .IsUnique();

                entity.Property(e => e.Password).IsRequired();

                entity.Property(e => e.SchoolId).HasDefaultValueSql("((0))");

                entity.Property(e => e.StringUserId)
                    .IsRequired()
                    .HasColumnName("StringUserID")
                    .HasMaxLength(450)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.School)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.SchoolId);
            });
        }
    }
}
