using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OdeToFood.Data;

namespace OdeToFood
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();

            MigrateDatabase(host);
                
            host.Run();
        }

        private static void MigrateDatabase(IWebHost host)
        {
            using(var scope = host.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<OdeToFoodDbContext>();
                if (db.Database.EnsureCreated())
                {
                    db.Database.Migrate();
                }
                
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
