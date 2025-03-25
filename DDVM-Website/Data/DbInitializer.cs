using DDVM_Website.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DDVM_Website.Data
{
    public static class DbInitializer
    {
        public static async Task InitializeAsync(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            // Ensure database is created and migrations are applied
            await context.Database.MigrateAsync();

            // Seed roles
            string[] roles = { "Admin", "Teacher", "Student" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            // Seed admin user
            if (!context.Users.Any(u => u.Email == "admin@college.com"))
            {
                var adminUser = new ApplicationUser
                {
                    UserName = "admin@college.com",
                    Email = "admin@college.com",
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(adminUser, "Admin@123"); // Use a strong password
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }

            // Seed Departments
            if (!context.Departments.Any())
            {
                context.Departments.AddRange(
                    new Department { Name = "Computer Science" },
                    new Department { Name = "Mathematics" },
                    new Department { Name = "Physics" },
                    new Department { Name = "English" },
                    new Department { Name = "Commerce" }
                );
                await context.SaveChangesAsync();
            }

            // Seed Courses
            if (!context.Courses.Any())
            {
                context.Courses.AddRange(
                    new Course { CourseName = "B.A.", DurationInYears = 3 },
                    new Course { CourseName = "B.Com", DurationInYears = 3 },
                    new Course { CourseName = "D.Pharma", DurationInYears = 2 },
                    new Course { CourseName = "M.A.", DurationInYears = 2 },
                    new Course { CourseName = "M.Com", DurationInYears = 2 }
                );
                await context.SaveChangesAsync();
            }

            // Seed News
            if (!context.News.Any())
            {
                context.News.AddRange(
                    new News { Title = "Admissions Open for 2025", Content = "Admissions are now open for the 2025 academic session. Apply now!", PublishedDate = DateTime.UtcNow },
                    new News { Title = "College Annual Fest", Content = "Join us for the grand annual fest on 15th April!", PublishedDate = DateTime.UtcNow }
                );
                await context.SaveChangesAsync();
            }

            // Seed Events
            if (!context.Events.Any())
            {
                context.Events.AddRange(
                    new Event { Title = "Tech Seminar", Description = "A seminar on AI and Machine Learning.", EventDate = DateTime.UtcNow.AddDays(10) },
                    new Event { Title = "Sports Day", Description = "Annual college sports day event.", EventDate = DateTime.UtcNow.AddDays(20) }
                );
                await context.SaveChangesAsync();
            }
        }
    }
}