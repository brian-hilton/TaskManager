using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TaskManager.Models;

namespace TaskManager.Models;

public partial class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<TaskList> TaskLists { get; set; }
    public DbSet<TaskModel> TaskModels { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<TaskList>()
            .HasOne(t1 => t1.User)
            .WithMany(u => u.TaskLists)
            .HasForeignKey(t1 => t1.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<TaskModel>()
            .HasOne(t => t.TaskList)
            .WithMany(static t1 => t1.TaskModels)
            .HasForeignKey(t => t.TaskListId)
            .OnDelete(DeleteBehavior.Cascade);
    }


    //public virtual DbSet<TaskModel> Tasks { get; set; }

    //protected override void OnModelCreating(ModelBuilder modelBuilder)
    //{
    //    modelBuilder.Entity<TaskModel>(entity =>
    //    {
    //        entity.HasKey(e => e.EntityId);

    //        entity.ToTable("tasks");

    //        entity.Property(e => e.EntityId)
    //            .ValueGeneratedNever()
    //            .HasColumnName("entityId");
    //        entity.Property(e => e.Description).HasColumnName("description");
    //        entity.Property(e => e.Status).HasColumnName("status");
    //    });

    //    OnModelCreatingPartial(modelBuilder);
    //}

    //partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}