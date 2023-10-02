using ContactPro.Data;
using ContactPro.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ContactPro.Helpers
{
    public static class DataHelper
    {
        public static async Task ManageDataAsync
            (IServiceProvider svcProvider)
        {
            // Get an instance of the db application context
            var dbContextSvc = svcProvider.GetRequiredService<ApplicationDbContext>();

            // Migration: This is equivalent to update-database
            await dbContextSvc.Database.MigrateAsync();
        }

        public static async Task SeedDemoUserAsync(IServiceProvider svcProvider)
        {
            // Get our dependencies from the service provider
            UserManager<AppUser>? userManager = svcProvider.GetRequiredService<UserManager<AppUser>>();
            IConfiguration config = svcProvider.GetRequiredService<IConfiguration>();

            // Make sure the user doesn't exist already
            if (await userManager.FindByEmailAsync("demouser4@contactpro.com") == null)
            {
                // Create the user
                AppUser demoUser = new()
                {
                    Email = "demouser4@contactpro.com",
                    UserName = "demouser4@contactpro.com",
                    FirstName = "Demo",
                    LastName = "User",
                    EmailConfirmed = true
                };

                // Get the password from secrets.json or env variable
                await userManager.CreateAsync(demoUser, config.GetSection("DemoSettings")["DemoPassword"] ?? Environment.GetEnvironmentVariable("DemoPassword"));
            }

            
        }
    }
}
 