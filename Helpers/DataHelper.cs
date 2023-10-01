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

            // Make sure the user doesn't exist already
            if (await userManager.FindByEmailAsync("demouser1@contactpro.com") == null)
            {
                // Create the user
                AppUser demoUser = new()
                {
                    Email = "demouser1@contactpro.com",
                    UserName = "demouser1@contactpro.com",
                    FirstName = "Demo",
                    LastName = "User",
                    EmailConfirmed = true
                };

                // Get the password from secrets.json or env variable
                await userManager.CreateAsync(demoUser, config.GetSection("DemoSettings")["DemoData"] ?? Environment.GetEnvironmentVariable("DemoPassword"));
            }

            
        }
    }
}
 