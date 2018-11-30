using System;
using System.Data;
using System.Linq;
using DbContexts.Article;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Share.Models.Article.Entities;
using Xunit;

namespace Repository.Test
{
    public class ArticleDbContextTest
    {
        private IDbConnection Connection { get; set; }

        private IServiceProvider GetAdServiceProvider()
        {
            Connection = new SqliteConnection("DataSource=:memory:");
            Connection.Open();

            var services = new ServiceCollection()
                .AddDbContext<ArticleDbContext>(options => options.UseSqlite((SqliteConnection)Connection))
                .AddScoped<Repository<Article, ArticleDbContext>>();

            return services.BuildServiceProvider();
        }

        [Fact]
        public async void Test_Create_Record_async_test()
        {
            try
            {
                var services = GetAdServiceProvider();
                var repository = services.GetService<Repository<Article, ArticleDbContext>>();
                await services.GetService<ArticleDbContext>().Database.EnsureCreatedAsync();

                var articleObject = new Article { ArticleId = 1, UserIdOrEmail = "Test1", Title = "title1", Content = "content",
                    AttachedAssetsStoredInCloudBaseFolderPath = "https://console.cloud.google.com/storage/browser/spcsad_first?project=oceanic-cacao-203021&folder&organizationId",
                    UserLoggedInSocialProviderName = "facebook"
                 };

                await repository.CreateAsync(articleObject);

                Assert.Equal(1, repository.Entities.Count());
            }
            finally
            {
                Connection.Close();
            }
        }
    }
}
