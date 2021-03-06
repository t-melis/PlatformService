using PlatformService.Models;

namespace PlatformService.Data
{
    public static class DbPrep
    {
        public static void PreparePopulation(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                SeedDatabase(scope.ServiceProvider.GetRequiredService<ApplicationDbContext>());
            }
        }

        private static void SeedDatabase(ApplicationDbContext context)
        {
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