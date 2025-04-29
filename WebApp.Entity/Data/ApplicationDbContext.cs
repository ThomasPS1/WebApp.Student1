using Microsoft.EntityFrameworkCore;
using WebApp.Student1.Models;

namespace WebApp.Student1.Data
{

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Enrollment>()
            .HasOne<Students>(s => s.Students)
            .WithMany(g => g.Enrollment)
            .HasForeignKey(s => s.StudentId);

            modelBuilder.Entity<Enrollment>()
            .HasOne<Courses>(s => s.Course)
            .WithMany(g => g.Enrollment)
            .HasForeignKey(s => s.CourseId);

            modelBuilder.Entity<Courses>()
            .HasOne(c => c.Instructors)
            .WithMany(i => i.Courses)
            .HasForeignKey(c => c.InstructorId);

           




        }
        public DbSet<Students> Students { get; set; }
        public DbSet<Courses> Courses { get; set; }
        public DbSet<Enrollment> Enrollment { get; set; }
        public DbSet<Grades> Grades { get; set; }
        public DbSet<Instructors> Instructors { get; set; }
    }
}

