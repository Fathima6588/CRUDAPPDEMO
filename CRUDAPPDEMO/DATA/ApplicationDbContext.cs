using CRUDAPPDEMO.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUDAPPDEMO.DATA
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options): base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure one-to-one: Student has FK TeacherId pointing to Teacher.Id.
            // Mark relationship optional so Student can exist without a Teacher.
            modelBuilder.Entity<Student>()
                .HasOne(s => s.Teacher)
                .WithOne(t => t.Student)
                .HasForeignKey<Student>(s => s.TeacherId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);

            // Unique index on the FK (still okay for nullable column in SQL Server)
            modelBuilder.Entity<Student>()
                .HasIndex(s => s.TeacherId)
                .IsUnique();
        }
    }
}
