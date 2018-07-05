using DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace DbContexts.AdMigration
{
    class Program
    {
        static void Main(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).Build();
            string connection = configuration.GetConnectionString("AdConnection");

            var articleContext = AdDbContext.Create(connection);
            if (articleContext.AllMigrationsApplied())
            {
                articleContext.Database.Migrate();
            }
        }
    }
}
