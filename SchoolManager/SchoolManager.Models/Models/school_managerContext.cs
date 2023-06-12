using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SchoolManager.Models.Models;

public partial class school_managerContext : DbContext
{
    public school_managerContext()
    {
    }

    public school_managerContext(DbContextOptions<school_managerContext> options)
        : base(options)
    {
    }

    public virtual DbSet<classroom> classrooms { get; set; }

    public virtual DbSet<student> students { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Server=localhost;Port=5432;Database=school_manager;Userid=postgres;Password=admin;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<classroom>(entity =>
        {
            entity.HasKey(e => e.id).HasName("classrooms_pkey");

            entity.Property(e => e.branch).HasMaxLength(1);
        });

        modelBuilder.Entity<student>(entity =>
        {
            entity.HasKey(e => e.id).HasName("students_pkey");

            entity.HasIndex(e => e.email, "students_email_key").IsUnique();

            entity.HasIndex(e => e.phone, "students_phone_key").IsUnique();

            entity.Property(e => e.email).HasMaxLength(100);
            entity.Property(e => e.first_name).HasMaxLength(50);
            entity.Property(e => e.last_name).HasMaxLength(50);
            entity.Property(e => e.phone).HasMaxLength(15);

            entity.HasOne(d => d.classroom).WithMany(p => p.students)
                .HasForeignKey(d => d.classroom_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("students_fkey_classrooms");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
