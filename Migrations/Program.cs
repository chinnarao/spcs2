//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Migrations;
//using Microsoft.EntityFrameworkCore.Infrastructure;
//using System.Linq;
//using System;
//using DbContexts.Ad;
//using DbContexts.Article;

namespace Migrations
{
    class Program
    {
        static void Main(string[] args)
        {
           //AdDatabaseMigrate();
           //ArticleDatabaseMigrate();
        }

        //static void AdDatabaseMigrate()
        //{
        //    string adConnectionString = "Server=localhost;Database=Ad;Trusted_Connection=True;";
        //    var adContext = new AdDbContext(new DbContextOptionsBuilder<AdDbContext>().UseSqlServer(adConnectionString, a => a.MigrationsAssembly("Migrations")).Options);
        //    if (AllMigrationsApplied(adContext))
        //    {
        //        adContext.Database.Migrate();
        //    }
        //}
        //static void ArticleDatabaseMigrate()
        //{
        //    string articleConnectionString = "Server=localhost;Database=Article;Trusted_Connection=True;";
        //    var articleContext = new ArticleDbContext(new DbContextOptionsBuilder<ArticleDbContext>().UseSqlServer(articleConnectionString).Options);
        //    if (AllMigrationsApplied(articleContext))
        //    {
        //        articleContext.Database.Migrate();
        //    }
        //}

        //public static bool AllMigrationsApplied(DbContext context)
        //{
        //    var applied = context.GetService<IHistoryRepository>()
        //        .GetAppliedMigrations()
        //        .Select(m => m.MigrationId);

        //    var total = context.GetService<IMigrationsAssembly>()
        //        .Migrations
        //        .Select(m => m.Key);

        //    return !total.Except(applied).Any();
        //}
    }
}
