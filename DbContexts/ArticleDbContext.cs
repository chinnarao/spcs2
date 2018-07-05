using DbContexts.ArticleConfigurations;
using Microsoft.EntityFrameworkCore;
using Models.Article.Entities;
using System;

namespace DbContexts
{
    //https://github.com/damienbod/AspNetCoreMultipleProject/blob/master/src/DataAccessMsSqlServerProvider/DomainModelMsSqlServerContext.cs
    public class ArticleDbContext : DbContext
    {
        public ArticleDbContext(DbContextOptions<ArticleDbContext> options) : base(options)
        {
        }

        /// <summary>
        /// this method will be called by DbContexts.ArticleMigration Project, for first time db creation purpose
        /// </summary>
        /// <param name="connection"> connection string</param>
        /// <returns>ArticleDbContext</returns>
        public static ArticleDbContext Create(string connection)
        {
            if (string.IsNullOrEmpty(connection)) throw new ArgumentNullException(nameof(connection));
            return new ArticleDbContext(new DbContextOptionsBuilder<ArticleDbContext>().UseSqlServer(connection).Options);
        }

        public DbSet<Article> Articles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ArticleConfig());
        }
    }
}
