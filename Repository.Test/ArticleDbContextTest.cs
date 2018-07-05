using System;
using System.Data;
using System.Linq;
using DbContexts;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Models.Ad.Entities;
using Models.Article.Entities;
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

                var articleObject = new Article { ArticleId = 1, UserEmail = "Test1", Title = "title1",
                    AttachedAssetsStoredInCloudBaseFolderPath = "https://console.cloud.google.com/storage/browser/spcsad_first?project=oceanic-cacao-203021&folder&organizationId",
                    UserId = "chinnarao", UserLoggedInSocialProviderName = "facebook"
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
