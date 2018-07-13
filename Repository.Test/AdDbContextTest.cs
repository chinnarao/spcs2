using System;
using System.Data;
using System.Linq;
using DbContexts;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Models.Ad.Entities;
using Xunit;

namespace Repository.Test
{
    public class AdDbContextTest
    {
        private IDbConnection Connection { get; set; }

        private IServiceProvider GetAdServiceProvider()
        {
            Connection = new SqliteConnection("DataSource=:memory:");
            Connection.Open();

            var services = new ServiceCollection()
                .AddDbContext<AdDbContext>(options => options.UseSqlite((SqliteConnection)Connection))
                .AddScoped<Repository<Ad, AdDbContext>>();

            return services.BuildServiceProvider();
        }

        [Fact]
        public async void Test_Create_Record_async_test()
        {
            try
            {
                var services = GetAdServiceProvider();
                var repository = services.GetService<Repository<Ad, AdDbContext>>();
                await services.GetService<AdDbContext>().Database.EnsureCreatedAsync();

                var adObject = new Ad { AdId = 1, UserIdOrEmail = "Test1", AdTitle = "title1", AddressZipCode = "535558",
                    AttachedAssetsStoredInCloudBaseFolderPath = "https://console.cloud.google.com/storage/browser/spcsad_first?project=oceanic-cacao-203021&folder&organizationId",
                    UserLoggedInSocialProviderName = "facebook"
                 };

                await repository.CreateAsync(adObject);

                Assert.Equal(1, repository.Entities.Count());
            }
            finally
            {
                Connection.Close();
            }
        }
    }
}
