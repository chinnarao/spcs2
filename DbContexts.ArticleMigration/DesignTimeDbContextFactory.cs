using DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace DbContexts.ArticleMigration
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ArticleDbContext>
    {
        public ArticleDbContext CreateDbContext(string[] args) //you can ignore args, maybe on later versions of .net core it will be used but right now it isn't
        {
            IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).Build();
            string connection = configuration.GetConnectionString("ArticleConnection");
            var options = new DbContextOptionsBuilder<ArticleDbContext>().UseSqlServer(connection, b => b.MigrationsAssembly("DbContexts.ArticleMigration")).Options;
            return new ArticleDbContext(options);
        }
    }
}
