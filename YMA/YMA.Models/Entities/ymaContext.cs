using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace YMA.Entities.Entities;

public partial class ymaContext : DbContext
{
    public ymaContext()
    {
    }

    public ymaContext(DbContextOptions<ymaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<account> accounts { get; set; }

    public virtual DbSet<ad> ads { get; set; }

    public virtual DbSet<address> addresses { get; set; }

    public virtual DbSet<brand> brands { get; set; }

    public virtual DbSet<category> categories { get; set; }

    public virtual DbSet<exchange> exchanges { get; set; }

    public virtual DbSet<featured_brand> featured_brands { get; set; }

    public virtual DbSet<featured_category> featured_categories { get; set; }

    public virtual DbSet<log> logs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Server=localhost;Port=5432;Database=yma;Userid=postgres;Password=admin;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<account>(entity =>
        {
            entity.HasKey(e => e.id).HasName("accounts_pkey");

            entity.Property(e => e.create_date).HasColumnType("timestamp without time zone");

            entity.HasOne(d => d.default_address).WithMany(p => p.accounts)
                .HasForeignKey(d => d.default_address_id)
                .HasConstraintName("accounts_fkey_addresses");
        });

        modelBuilder.Entity<ad>(entity =>
        {
            entity.HasNoKey();
        });

        modelBuilder.Entity<address>(entity =>
        {
            entity.HasKey(e => e.id).HasName("addresses_pkey");

            entity.Property(e => e.create_date).HasColumnType("timestamp without time zone");
        });

        modelBuilder.Entity<brand>(entity =>
        {
            entity.HasKey(e => e.id).HasName("brands_pkey");
        });

        modelBuilder.Entity<category>(entity =>
        {
            entity.HasKey(e => e.id).HasName("categories_pkey");
        });

        modelBuilder.Entity<exchange>(entity =>
        {
            entity.HasNoKey();
        });

        modelBuilder.Entity<featured_brand>(entity =>
        {
            entity.HasNoKey();

            entity.HasOne(d => d.brand).WithMany()
                .HasForeignKey(d => d.brand_id)
                .HasConstraintName("featured_brands_fkey_brands");
        });

        modelBuilder.Entity<featured_category>(entity =>
        {
            entity.HasNoKey();

            entity.HasOne(d => d.category).WithMany()
                .HasForeignKey(d => d.category_id)
                .HasConstraintName("featured_categories_fkey_categories");
        });

        modelBuilder.Entity<log>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.create_date).HasColumnType("timestamp without time zone");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
