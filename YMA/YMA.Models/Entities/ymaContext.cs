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

    public virtual DbSet<company> companies { get; set; }

    public virtual DbSet<company_invite> company_invites { get; set; }

    public virtual DbSet<exchange> exchanges { get; set; }

    public virtual DbSet<featured_brand> featured_brands { get; set; }

    public virtual DbSet<featured_category> featured_categories { get; set; }

    public virtual DbSet<featured_company> featured_companies { get; set; }

    public virtual DbSet<featured_product> featured_products { get; set; }

    public virtual DbSet<log> logs { get; set; }

    public virtual DbSet<product> products { get; set; }

    public virtual DbSet<version> versions { get; set; }

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
            entity.HasKey(e => e.id).HasName("ads_pkey");
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

        modelBuilder.Entity<company>(entity =>
        {
            entity.HasKey(e => e.id).HasName("companies_pkey");

            entity.Property(e => e.create_date).HasColumnType("timestamp without time zone");
        });

        modelBuilder.Entity<company_invite>(entity =>
        {
            entity.HasKey(e => e.id).HasName("company_invites_pkey");

            entity.Property(e => e.create_date).HasColumnType("timestamp without time zone");
            entity.Property(e => e.reply_date).HasColumnType("timestamp without time zone");

            entity.HasOne(d => d.receiver).WithMany(p => p.company_invitereceivers)
                .HasForeignKey(d => d.receiver_id)
                .HasConstraintName("company_invites_fkey_companies_receiver");

            entity.HasOne(d => d.sender).WithMany(p => p.company_invitesenders)
                .HasForeignKey(d => d.sender_id)
                .HasConstraintName("company_invites_fkey_companies_sender");
        });

        modelBuilder.Entity<exchange>(entity =>
        {
            entity.HasKey(e => e.id).HasName("exchanges_pkey");
        });

        modelBuilder.Entity<featured_brand>(entity =>
        {
            entity.HasKey(e => e.id).HasName("featured_brands_pkey");

            entity.HasOne(d => d.brand).WithMany(p => p.featured_brands)
                .HasForeignKey(d => d.brand_id)
                .HasConstraintName("featured_brands_fkey_brands");
        });

        modelBuilder.Entity<featured_category>(entity =>
        {
            entity.HasKey(e => e.id).HasName("featured_categories_pkey");

            entity.HasOne(d => d.category).WithMany(p => p.featured_categories)
                .HasForeignKey(d => d.category_id)
                .HasConstraintName("featured_categories_fkey_categories");
        });

        modelBuilder.Entity<featured_company>(entity =>
        {
            entity.HasKey(e => e.id).HasName("featured_companies_pkey");

            entity.HasOne(d => d.company).WithMany(p => p.featured_companies)
                .HasForeignKey(d => d.company_id)
                .HasConstraintName("featured_companies_fkey_companies");
        });

        modelBuilder.Entity<featured_product>(entity =>
        {
            entity.HasKey(e => e.id).HasName("featured_products_pkey");

            entity.HasOne(d => d.product).WithMany(p => p.featured_products)
                .HasForeignKey(d => d.product_id)
                .HasConstraintName("featured_products_fkey_products");
        });

        modelBuilder.Entity<log>(entity =>
        {
            entity.HasKey(e => e.id).HasName("logs_pkey");

            entity.Property(e => e.create_date).HasColumnType("timestamp without time zone");
        });

        modelBuilder.Entity<product>(entity =>
        {
            entity.HasKey(e => e.id).HasName("products_pkey");

            entity.Property(e => e.create_date).HasColumnType("timestamp without time zone");

            entity.HasOne(d => d.brand).WithMany(p => p.products)
                .HasForeignKey(d => d.brand_id)
                .HasConstraintName("products_fkey_brands");

            entity.HasOne(d => d.category).WithMany(p => p.products)
                .HasForeignKey(d => d.category_id)
                .HasConstraintName("products_fkey_categories");

            entity.HasOne(d => d.company).WithMany(p => p.products)
                .HasForeignKey(d => d.company_id)
                .HasConstraintName("products_fkey_companies");
        });

        modelBuilder.Entity<version>(entity =>
        {
            entity.HasKey(e => e.id).HasName("versions_pkey");

            entity.Property(e => e.version1).HasColumnName("version");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
