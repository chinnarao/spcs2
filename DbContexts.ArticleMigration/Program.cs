using DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace DbContexts.ArticleMigration
{
    class Program
    {
        static void Main(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).Build();
            string connection = configuration.GetConnectionString("ArticleConnection");
            var articleContext = ArticleDbContext.Create(connection);
            if (articleContext.AllMigrationsApplied())
            {
                articleContext.Database.Migrate();
            }
        }
    }
}
