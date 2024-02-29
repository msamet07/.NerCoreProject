using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Workintech02RestApiDemo.Infrastructure;

public partial class WorkintechBlogDemoContext : DbContext
{
    public WorkintechBlogDemoContext()
    {
    }

    public WorkintechBlogDemoContext(DbContextOptions<WorkintechBlogDemoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Workintech02RestApiDemo.Domain.Entities.Blog> Blogs { get; set; }

    public virtual DbSet<Workintech02RestApiDemo.Domain.Entities.Post> Posts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Workintech02RestApiDemo.Domain.Entities.Blog>(entity =>
        {
            entity.ToTable("Blog");
            entity.Property(x => x.Url).IsRequired();
            entity.Property(e => e.Title)
                .HasMaxLength(250)
                .HasDefaultValue("");
            entity.Property(e => e.Url).HasMaxLength(250);
        });

        modelBuilder.Entity<Workintech02RestApiDemo.Domain.Entities.Post>(entity =>
        {
            entity.ToTable("Post");
            
            entity.Property(e => e.Content).HasMaxLength(500);
            entity.Property(e => e.Title).HasMaxLength(250);

            entity.HasOne(d => d.Blog).WithMany(p => p.Posts)
                .HasForeignKey(d => d.BlogId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Post_Blog");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
