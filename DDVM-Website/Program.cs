using DDVM_Website.Data;
using DDVM_Website.Models;
using DDVM_Website.Repository.IRepository;
using DDVM_Website.Repository;
using DDVM_Website.Services.Interfaces;
using DDVM_Website.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;

namespace DDVM_Website
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // ✅ 1️⃣ Configure Database Connection
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // ✅ 2️⃣ Configure Identity Services
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // ✅ 3️⃣ Register RoleManager & UserManager
            builder.Services.AddScoped<RoleManager<IdentityRole>>();
            builder.Services.AddScoped<UserManager<ApplicationUser>>();

            // ✅ 4️⃣ Register Controllers
            builder.Services.AddControllers();

            // ✅ 5️⃣ Enable CORS (if needed)
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    policy => policy.AllowAnyOrigin()
                                    .AllowAnyMethod()
                                    .AllowAnyHeader());
            });

            // ✅ 6️⃣ Configure Swagger (API Documentation)
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "College API",
                    Version = "v1",
                    Description = "API for managing college-related data",
                });

                // 🔹 Enable JWT Authentication in Swagger (if using JWT)
                var securityScheme = new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter 'Bearer {token}' to authenticate",
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                };
                c.AddSecurityDefinition("Bearer", securityScheme);
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { securityScheme, new string[] { } }
    });
            });

            var app = builder.Build();

            // ✅ 7️⃣ Apply Migrations & Seed Database
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<ApplicationDbContext>();
                var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

                await DbInitializer.InitializeAsync(context, userManager, roleManager);
            }

            // ✅ 8️⃣ Configure Middleware Pipeline
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseCors("AllowAll");  // 🔹 Enable CORS
            app.UseAuthentication();   // 🔹 Enable Authentication
            app.UseAuthorization();    // 🔹 Enable Authorization

            app.MapControllers();
            app.Run();
        }
    }
}