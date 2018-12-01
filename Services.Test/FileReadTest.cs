using Microsoft.Extensions.Caching.Memory;
using System;
using Xunit;
using FluentAssertions;
using System.IO;
using Services.Test;
using Services.Commmon;
using NSubstitute;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;

namespace Services.Test
{
    public class FileReadTest
    {
        [Fact]
        public void Test_FileAsString()
        {
            int days = Helper.GetCacheExpireDays();
            string fileName = Helper.GetAdFileName();
            string firstTimeContent = Helper.GetAdTemplateFileContent();
            Action act = () => firstTimeContent.Should().NotBeEmpty();
            act.Should().NotThrow();
            string secondTimeContent = Helper.GetAdTemplateFileContent();
            Action act1 = () => secondTimeContent.Should().BeSameAs(firstTimeContent);
            //act1.Should().NotThrow();
        }

        [Fact]
        public void Test_FillContent()
        {

            var factoryMock = Substitute.For<Func<string>>();
            var optionsMock = Substitute.For<IOptions<MemoryCacheOptions>>();
            optionsMock.Value.Returns(callInfo => new MemoryCacheOptions());
            var memoryCache = new MemoryCache(optionsMock);
            ICacheService cacheService = new CacheService(memoryCache);

            IConfiguration _configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            var anonymousData = new
            {
                Name = "Riya",
                Occupation = "Kavin Brother."
            };

            string content = Helper.GetAdTemplateFileContent();
            IFileRead read = new FileRead(_configuration, cacheService);
            content = read.FillContent(content, anonymousData);
            Action act = () => content.Should().Contain(anonymousData.Name);
            act.Should().NotThrow();
            Action act1 = () => content.Should().Contain(anonymousData.Occupation);
            act1.Should().NotThrow();
        }
    }
}
