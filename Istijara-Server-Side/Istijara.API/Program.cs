using Istijara.API.Extensions;
using Istijara.Repository.Data;
using Istijara.Repository.Data.Identity;
using Istijara.Repository.Repositories;
using Istijara.Service.Configurations;
using Istijara.Service.Services;
using Istijara.Service.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Istijara.Core.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace Istijara.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);

            #region Config Services
            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddIdentityServices(builder.Configuration);
            builder.Services.AddApplicationServices();
            builder.Services.Configure<JWT>(builder.Configuration.GetSection("JWT"));
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowFlutter",
                    policy => policy.AllowAnyHeader()
                                    .AllowAnyMethod()
                                    .AllowAnyOrigin());
            });
            builder.Services.AddDbContext<AppIdentityDbContext>(options =>
            {
                var identityConnection = builder.Configuration.GetConnectionString("IdentityConnection")
                ?? throw new InvalidOperationException("Connection string not found.");
                options.UseSqlServer(identityConnection);
            });
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string not found.");
                options.UseSqlServer(connectionString);
            });

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IItemServices , ItemServices> ();
            builder.Services.AddScoped<IAdminServices, AdminService>();


            #endregion

            var app = builder.Build();
            var scop = app.Services.CreateScope();
            var services = scop.ServiceProvider;
            var loggerFactory = services.GetRequiredService<ILoggerFactory>();
            try
            {
                var context = services.GetRequiredService<ApplicationDbContext>();
                await context.Database.MigrateAsync();

                var identityContext = services.GetRequiredService<AppIdentityDbContext>();
                await identityContext.Database.MigrateAsync();

                //var userManager = services.GetRequiredService<UserManager<AppUser>>();
                //var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                //await AppIdentityDbContextSeed.SeedUser(userManager);
                //await AppIdentityDbContextSeed.SeedRoles(roleManager, userManager);
            }
            catch (Exception ex)
            {

                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "An error occurred during migration");

            }
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors("AllowFlutter");


            app.MapControllers();

            app.Run();
        }
    }
}
