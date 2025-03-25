using DDVM_Website.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DDVM_Website.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // 📌 Define DbSets for all models
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<News> News { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 📌 Identity Role Configuration
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = "1", Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole { Id = "2", Name = "Teacher", NormalizedName = "TEACHER" },
                new IdentityRole { Id = "3", Name = "Student", NormalizedName = "STUDENT" }
            );

            // 📌 Student-Department Relationship (One-to-Many)
            modelBuilder.Entity<Student>()
                .HasOne(s => s.Department)
                .WithMany(d => d.Students)
                .HasForeignKey(s => s.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            // 📌 Teacher-Department Relationship (One-to-Many)
            modelBuilder.Entity<Teacher>()
                .HasOne(t => t.Department)
                .WithMany(d => d.Teachers)
                .HasForeignKey(t => t.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            // 📌 Event Configuration
            modelBuilder.Entity<Event>()
                .Property(e => e.EventDate)
                .HasColumnType("datetime2");

            // 📌 News Configuration
            modelBuilder.Entity<News>()
                .Property(n => n.PublishedDate)
                .HasColumnType("datetime2");

            // 📌 Course Configuration
            modelBuilder.Entity<Course>()
                .HasIndex(c => c.CourseName)
                .IsUnique();

            // 📌 Seeding Admin User
            var adminUser = new ApplicationUser
            {
                Id = "admin-user-id",
                UserName = "admin@college.com",
                NormalizedUserName = "ADMIN@COLLEGE.COM",
                Email = "admin@college.com",
                NormalizedEmail = "ADMIN@COLLEGE.COM",
                EmailConfirmed = true,
                SecurityStamp = string.Empty
            };

            var passwordHasher = new PasswordHasher<ApplicationUser>();
            adminUser.PasswordHash = passwordHasher.HashPassword(adminUser, "Admin@123");

            modelBuilder.Entity<ApplicationUser>().HasData(adminUser);

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string> { RoleId = "1", UserId = "admin-user-id" }
            );
        }
    }
}