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

    public virtual DbSet<Classroom> classrooms { get; set; }

    public virtual DbSet<Student> students { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Server=localhost;Port=5432;Database=school_manager;Userid=postgres;Password=admin;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Classroom>(entity =>
        {
            entity.HasKey(e => e.id).HasName("classrooms_pkey");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.id).HasName("students_pkey");

            entity.HasOne(d => d.classroom).WithMany(p => p.students)
                .HasForeignKey(d => d.classroom_id)
                .HasConstraintName("students_fkey_classrooms");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
