using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Model;

namespace EFCoreDemo
{
    public class EFCoreDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source = .;Initial Catalog = EFCore;User Id = sa;Password = abcABC123;");
            //base.OnConfiguring(optionsBuilder);
        }

        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Blog>().ToTable("blog");
            modelBuilder.Entity<Blog>(t =>
            {
                t.Property(p => p.CreatedDateTime).HasColumnType("DATETIME").HasDefaultValueSql("GETDATE()");
                //t.Property(p => p.UpdatedDateTime).HasColumnType("DATETIME").ValueGeneratedOnAddOrUpdate().HasDefaultValueSql("GETDATE()"); // 更新的时候不会更新，错误

                // 使用计算列
                t.Property(p => p.UpdatedDateTime).HasColumnType("DATETIME").HasComputedColumnSql("GETDATE()");
            });
            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            var entities = ChangeTracker.Entries().ToList();
            var updatedEntities = entities.Where(e => e.Entity is IUpdatedable && e.State == EntityState.Modified)
                .ToList();
            updatedEntities.ForEach(e =>
                ((IUpdatedable)e.Entity).UpdatedDateTime = DateTime.Now
                );
            return base.SaveChanges();
        }
    }
}