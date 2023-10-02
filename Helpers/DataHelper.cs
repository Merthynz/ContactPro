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

            // Create the Demo User
            AppUser demoUser = new()
            {
                Email = "demouser@contactpro.com",
                UserName = "demouser@contactpro.com",
                FirstName = "Demo",
                LastName = "User",
                EmailConfirmed = true
            };

            // Make sure that the email doesn't exist in the database
            try
            {
                var user = await userManager.FindByEmailAsync(demoUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(demoUser, "Abc&123!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("*********  ERROR  **********");
                Console.WriteLine("Error Seeding Demo User.");
                Console.WriteLine(ex.Message);
                Console.WriteLine("*****************************");
                throw;
            }
        }
    }
}
 