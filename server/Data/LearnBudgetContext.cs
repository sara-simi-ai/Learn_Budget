using Microsoft.EntityFrameworkCore;
using server.Models;

namespace server.Data
{
    public class LearnBudgetContext : DbContext
    {
        public LearnBudgetContext(DbContextOptions<LearnBudgetContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseDetail> CourseDetails { get; set; }
        public DbSet<CourseRegistration> CourseRegistrations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.FirstName).IsRequired().HasMaxLength(50);
                entity.Property(e => e.LastName).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Email).IsRequired();
                entity.Property(e => e.Phone).IsRequired().HasMaxLength(10);
                entity.Property(e=> e.Password).IsRequired();
                entity.Property(e => e.IsAdmin).IsRequired();
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Department).IsRequired().HasMaxLength(200);
                entity.Property(e => e.TotalCredits).IsRequired();
                entity.Property(e => e.UsedCredits).IsRequired();
                entity.Ignore(e => e.AvailableCredits);
                entity.HasOne(e => e.User)
                      .WithOne(u => u.Employee)
                      .HasForeignKey<Employee>("UserId")
                      .OnDelete(DeleteBehavior.Cascade)
                      .IsRequired();
            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Name).IsRequired().HasMaxLength(300);
                entity.Property(c => c.Description).HasMaxLength(1000);
                entity.Property(c => c.CreditCost).IsRequired();
                entity.Property(c => c.MaxPlaces).IsRequired();
                entity.Property(c => c.PlacesLeft).IsRequired();
                entity.Property(c => c.RegistrationDeadline).IsRequired();

                entity.HasOne(c => c.CourseDetail)
                      .WithOne(d => d.Course)
                      .HasForeignKey<CourseDetail>(d => d.CourseId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<CourseDetail>(entity =>
            {
                entity.HasKey(d => d.CourseId);
                entity.Property(d => d.Location).IsRequired().HasMaxLength(250);
                entity.Property(d => d.LecturerName).IsRequired().HasMaxLength(200);
            });

            modelBuilder.Entity<CourseRegistration>(entity =>
            {
                entity.HasKey(cr => cr.Id);

                entity.Property(cr => cr.RegistrationDate).IsRequired();

                entity.HasOne(cr => cr.Employee)
                      .WithMany(e => e.Registrations)
                      .HasForeignKey(cr => cr.EmployeeId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(cr => cr.Course)
                      .WithMany(c => c.Registrations)
                      .HasForeignKey(cr => cr.CourseId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasIndex(cr => new { cr.EmployeeId, cr.CourseId }).IsUnique();
            });
        }
    }
}