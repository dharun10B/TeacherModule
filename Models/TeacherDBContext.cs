using Microsoft.EntityFrameworkCore;

namespace TeacherModule.Models
{
    public class TeacherDBContext : DbContext
    {
        public TeacherDBContext(DbContextOptions<TeacherDBContext> options) : base(options) { }

        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Batch> Batches { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Teacher>()
            .HasMany(t => t.Courses)
            .WithOne(c => c.Teacher); // Assuming Course has a navigation property to Teacher

            // Seed data for Teachers
            modelBuilder.Entity<Teacher>().HasData(
                new Teacher { Id = 1, Name = "Alice Smith", Email = "alice@example.com", PhoneNumber = "123-456-7890", Address = "123 Elm St" },
                new Teacher { Id = 2, Name = "Bob Johnson", Email = "bob@example.com", PhoneNumber = "234-567-8901", Address = "456 Oak St" }
            );

            // Seed data for Courses
            modelBuilder.Entity<Course>().HasData(
                new Course { Id = 1, CourseName = "Math 101", Description = "Introduction to Mathematics", TeacherId = 1 },
                new Course { Id = 2, CourseName = "Science 101", Description = "Introduction to Science", TeacherId = 2 }
            );

            // Seed data for Students
            modelBuilder.Entity<Student>().HasData(
                new Student { Id = 1, Name = "John Doe", Email = "john.doe@example.com", PhoneNumber = "345-678-9012", Address = "789 Pine St" },
                new Student { Id = 2, Name = "Jane Roe", Email = "jane.roe@example.com", PhoneNumber = "456-789-0123", Address = "321 Maple St" }
            );

            // Seed data for Batches
            modelBuilder.Entity<Batch>().HasData(
                new Batch { Id = 1, BatchName = "Batch A", BatchTiming = "09:00 AM - 12:00 PM", BatchType = "Regular", CourseId = 1 },
                new Batch { Id = 2, BatchName = "Batch B", BatchTiming = "01:00 PM - 04:00 PM", BatchType = "Evening", CourseId = 2 }
            );
        }
    }
}