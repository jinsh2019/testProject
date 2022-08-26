using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ContosoUniversity.Models;

namespace ContosoUniversity.Data
{
    public class SchoolContext : DbContext
    {
        public SchoolContext (DbContextOptions<SchoolContext> options)
            : base(options)
        {
        }
        // 实体集通常对应数据库表, 实体对应表中的行
        // public 外部使用
        public DbSet<Student> Students { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Course> Courses { get; set; }

        // 在完成对 SchoolContext 的初始化后，并在模型已锁定并用于初始化上下文之前，
        // 进行调用是必需的，因为在本教程的后续部分中，Student 实体将引用其他实体。
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 内部使用，用以影射到表
            modelBuilder.Entity<Course>().ToTable("Course");
            modelBuilder.Entity<Enrollment>().ToTable("Enrollment");
            modelBuilder.Entity<Student>().ToTable("Student");
        }
    }
}
