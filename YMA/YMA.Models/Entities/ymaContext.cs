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

    public virtual DbSet<address> addresses { get; set; }

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

        modelBuilder.Entity<address>(entity =>
        {
            entity.HasKey(e => e.id).HasName("addresses_pkey");

            entity.Property(e => e.create_date).HasColumnType("timestamp without time zone");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
