using DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace DbContexts.AdMigration
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AdDbContext>
    {
        public AdDbContext CreateDbContext(string[] args) //you can ignore args, maybe on later versions of .net core it will be used but right now it isn't
        {
            IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).Build();
            string connection = configuration.GetConnectionString("AdConnection");
            var options = new DbContextOptionsBuilder<AdDbContext>().UseSqlServer(connection, b => b.MigrationsAssembly("DbContexts.AdMigration")).Options;
            return new AdDbContext(options);
        }
    }
}
