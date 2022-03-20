using Microsoft.EntityFrameworkCore;
using PlatformService.Models;

namespace PlatformService.Data
{
    public static class DbPrep
    {
        public static void PreparePopulation(IApplicationBuilder app, bool isProd)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                SeedDatabase(scope.ServiceProvider.GetRequiredService<ApplicationDbContext>(), isProd);
            }
        }

        private static void SeedDatabase(ApplicationDbContext context, bool isPrd)
        {
            if (isPrd)
            {
                try
                {
                    Console.WriteLine("--> Attempting to apply migrations...");
                    context.Database.Migrate();
                }
                catch(Exception ex)
                {
                    Console.WriteLine($"---> Could not run migrations: {ex.Message}");
                }
            }
            if (!context.Platforms.Any())
            {
                Console.WriteLine("---> Seeding data into the database!");

                context.Platforms.AddRange
                (
                    new Platform() { Name = "Kubernetes", Cost = "Free", Publisher = "Microsoft"},
                    new Platform() { Name = "Dot Net", Cost = "Free", Publisher = "Google"}
                );

                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("---> Database already contains some data!");
            }
        }
    }
}