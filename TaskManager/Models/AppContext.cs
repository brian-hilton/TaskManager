using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TaskManager.Models;

namespace TaskManager.Models;

public partial class testContext : DbContext
{
    public testContext(DbContextOptions<testContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TaskModel> Tasks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TaskModel>(entity =>
        {
            entity.HasKey(e => e.EntityId);

            entity.ToTable("tasks");

            entity.Property(e => e.EntityId)
                .ValueGeneratedNever()
                .HasColumnName("entityId");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Status).HasColumnName("status");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}