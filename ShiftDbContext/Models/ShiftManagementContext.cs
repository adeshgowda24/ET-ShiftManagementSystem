using System;
using System.Collections.Generic;
using System.Data.Entity;
using Microsoft.EntityFrameworkCore;
using testet.Models;


namespace testet.Models;

public partial class ShiftManagementContext : DbContext
{
    public ShiftManagementContext()
    {
    }

    public ShiftManagementContext(DbContextOptions<ShiftManagementContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Shift> Shifts { get; set; }

    public virtual DbSet<ShiftControl> ShiftControls { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    //public virtual DbSet<ProjectDetails> ProjectDetails { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

        => optionsBuilder.UseSqlServer("Server=(LocalDB)\\MSSQLLocalDB;Database=ShiftManagement;Trusted_Connection=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Comment>(entity =>
        {
            entity.ToTable("Comment");

            entity.Property(e => e.CommentId).HasColumnName("CommentID");
            entity.Property(e => e.Comments).HasMaxLength(500);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.ShiftId).HasColumnName("ShiftID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Shift).WithMany(p => p.Comments)
                .HasForeignKey(d => d.ShiftId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Comment_Shift");

            entity.HasOne(d => d.User).WithMany(p => p.Comments)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Comment_User");
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.ToTable("Project");

            entity.Property(e => e.ProjectId).HasColumnName("ProjectID");
            entity.Property(e => e.ClientName).HasMaxLength(50);
            entity.Property(e => e.CreaatedBy).HasMaxLength(50);
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Description)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
            entity.Property(e => e.ModifiedBy).HasMaxLength(50);
            entity.Property(e => e.ProjectName)
                .HasMaxLength(150)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable("Role");

            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.RoleName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Shift>(entity =>
        {
            entity.ToTable("Shift");

            entity.Property(e => e.ShiftId).HasColumnName("ShiftID");
            entity.Property(e => e.ShiftName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Timing).HasColumnType("datetime");
        });

        modelBuilder.Entity<ShiftControl>(entity =>
        {
            entity.ToTable("ShiftControl");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.IsActive).HasColumnName("isActive");
            entity.Property(e => e.ProjectId).HasColumnName("ProjectID");
            entity.Property(e => e.ShiftId).HasColumnName("ShiftID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Project).WithMany(p => p.ShiftControls)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ShiftControl_Project");

            entity.HasOne(d => d.Shift).WithMany(p => p.ShiftControls)
                .HasForeignKey(d => d.ShiftId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ShiftControl_Shift");

            entity.HasOne(d => d.User).WithMany(p => p.ShiftControls)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ShiftControl_User");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.DateOfBirth).HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Gender)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UserName).HasMaxLength(50);
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => e.RoleId);

            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserRoles_User");
        });

        //modelBuilder.Entity<ProjectDetails>(entity =>
        //{
        //    entity.HasKey(e => e.ProjectuserID);

        //    entity.Property(e => e.ProjectuserID).HasColumnName("ProjectuserID");
        //    entity.Property(e => e.Project).HasColumnName("Project");
        //    entity.Property(e => e.User).HasColumnName("User");
        //    entity.Property(e => e.CreatedBy).HasColumnName("CreatedBy");
        //    entity.Property(e => e.CreatedDate).HasColumnName("CreatedDate");
        //    entity.Property(e => e.ModifiedBy).HasColumnName("ModifiedBy");
        //    entity.Property(e => e.ModifiedDate).HasColumnName("ModifiedDate");

        //    //entity.HasOne(d => d.Project).WithMany(p => p.ProjectDetails)
        //    //    .HasForeignKey(d => d.ProjectId)
        //    //    .OnDelete(DeleteBehavior.ClientSetNull)
        //    //    .HasConstraintName("FK_ProjectDetails_Project");

        //    //entity.HasOne(d => d.User).WithMany(p => p.ProjectDetails)
        //    //   .HasForeignKey(d => d.UserId)
        //    //   .OnDelete(DeleteBehavior.ClientSetNull)
        //    //   .HasConstraintName("FK_ProjectDetails_User");


        //});



        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
