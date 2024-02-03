using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using N5Now.Api.Entities;

namespace N5Now.Api.Data;

public partial class N5nowContext : DbContext
{
    public N5nowContext()
    {
    }

    public N5nowContext(DbContextOptions<N5nowContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Permission> Permissions { get; set; }

    public virtual DbSet<TypePermission> TypePermissions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Name=N5Now");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.IdEmployee).HasName("PK__Employee__51C8DD7A3C4869A1");

            entity.ToTable("Employee");

            entity.Property(e => e.Company)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.CreationUser)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Department)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.LastModificationDate).HasColumnType("datetime");
            entity.Property(e => e.LastModificationUser)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Permission>(entity =>
        {
            entity.HasKey(e => e.IdPermission).HasName("PK__Permissi__17C26EA23857B545");

            entity.ToTable("Permission");

            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.CreationUser)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.DateFrom).HasColumnType("datetime");
            entity.Property(e => e.DateUntil).HasColumnType("datetime");
            entity.Property(e => e.LastModificationDate).HasColumnType("datetime");
            entity.Property(e => e.LastModificationUser)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Observation)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.IdEmployeeNavigation).WithMany(p => p.Permissions)
                .HasForeignKey(d => d.IdEmployee)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Permission_Employee");

            entity.HasOne(d => d.IdTypePermissionNavigation).WithMany(p => p.Permissions)
                .HasForeignKey(d => d.IdTypePermission)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Permission_TypePermission");
        });

        modelBuilder.Entity<TypePermission>(entity =>
        {
            entity.HasKey(e => e.IdTypePermission).HasName("PK__TypePerm__CB7948CC769B0E69");

            entity.ToTable("TypePermission");

            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.CreationUser)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.LastModificationDate).HasColumnType("datetime");
            entity.Property(e => e.LastModificationUser)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
