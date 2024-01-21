using AynarMVC_Exam.Contexts;
using AynarMVC_Exam.Exceptions;
using AynarMVC_Exam.Helpers.Enums;
using AynarMVC_Exam.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace AynarMVC_Exam.Helpers
{
    public static class Seed
    {
        public static IApplicationBuilder UseSeedData(this WebApplication app)
        {
            app.Use(async (context, next) =>
            {
                using var scope = context.RequestServices.CreateScope();
                var userManager = context.RequestServices.GetRequiredService<UserManager<AppUser>>();
                var roleManager = context.RequestServices.GetRequiredService<RoleManager<IdentityRole>>();
                var dbContext = context.RequestServices.GetRequiredService<AynarDbContext>();

                if (!await roleManager.Roles.AnyAsync())
                    await CreateRolesAsync(roleManager);
                if (await userManager.FindByNameAsync(app.Configuration["Admin:Username"]) == null)
                    await CreateAdminUserAsync(userManager, app);
                if (!await dbContext.Settings.AnyAsync())
                   await SetInitialSettingsAsync(app, dbContext);

                await next();
            });
            return app;
        }

        public static async Task CreateRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            foreach (var role in Enum.GetNames(typeof(Roles)))
            {
                var result = await roleManager.CreateAsync(new IdentityRole(role));
                if (!result.Succeeded)
                {
                    StringBuilder sb = new StringBuilder();
                    foreach(var error in result.Errors)
                    {
                        sb.Append(error.Description + " ");
                    }
                    throw new RolesCreationFailedException(sb.ToString().TrimEnd());
                }
            }
        }

        public static async Task CreateAdminUserAsync(UserManager<AppUser> userManager, WebApplication app)
        {
            var admin = new AppUser
            {
                UserName = app.Configuration["Admin:Username"]
            };
            var adminResult = await userManager.CreateAsync(admin, app.Configuration["Admin:Password"]);
            if (!adminResult.Succeeded)
            {
                StringBuilder sb = new StringBuilder();
                foreach (var error in adminResult.Errors)
                {
                    sb.Append(error.Description + " ");
                }
                throw new AdminUserCreationFailedException(sb.ToString().TrimEnd());
            }
            var roleResult = await userManager.AddToRoleAsync(admin, nameof(Roles.Admin));
            if (!roleResult.Succeeded)
            {
                StringBuilder sb = new StringBuilder();
                foreach (var error in adminResult.Errors)
                {
                    sb.Append(error.Description + " ");
                }
                throw new RoleAssignFailedException(sb.ToString().TrimEnd());
            }
        }

        public static async Task SetInitialSettingsAsync(WebApplication app, AynarDbContext dbContext)
        {
            var setting = new Setting
            {
                PhoneNumber = app.Configuration["Settings:PhoneNumber"],
                State = app.Configuration["Settings:State"],
                Street = app.Configuration["Settings:Street"],
                City = app.Configuration["Settings:City"],
                ContactEmail = app.Configuration["Settings:Email"],
                About = app.Configuration["Settings:About"]
            };
            await dbContext.Settings.AddAsync(setting);
            await dbContext.SaveChangesAsync();
        }
    }
}
