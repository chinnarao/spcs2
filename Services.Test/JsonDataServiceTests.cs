using System;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using NSubstitute;
using Xunit;
using Services.Commmon;
using Microsoft.Extensions.Configuration;
using Services.Common;
using FluentAssertions;

namespace Services.Test
{
    public class JsonDataServiceTests
    {
        [Fact]
        public void GetCountries_Test()
        {
            var factoryMock = Substitute.For<Func<string>>();
            var optionsMock = Substitute.For<IOptions<MemoryCacheOptions>>();
            optionsMock.Value.Returns(callInfo => new MemoryCacheOptions());
            var memoryCache = new MemoryCache(optionsMock);
            ICacheService cacheService = new CacheService(memoryCache);

            IConfiguration _configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            IFileRead read = new FileRead(_configuration, cacheService);

            IJsonDataService jsonService = new JsonDataService(_configuration, cacheService, read);
            var list = jsonService.GetCountries();
            Action act = () => list.Count.Should().BeGreaterThan(0);
            act.Should().NotThrow();
        }

        [Fact]
        public void GetLookUp_Test()
        {
            var factoryMock = Substitute.For<Func<string>>();
            var optionsMock = Substitute.For<IOptions<MemoryCacheOptions>>();
            optionsMock.Value.Returns(callInfo => new MemoryCacheOptions());
            var memoryCache = new MemoryCache(optionsMock);
            ICacheService cacheService = new CacheService(memoryCache);

            IConfiguration _configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            IFileRead read = new FileRead(_configuration, cacheService);

            IJsonDataService jsonService = new JsonDataService(_configuration, cacheService, read);
            var list = jsonService.GetCategoryOptionsBy();
            Action act = () => list.Count.Should().BeGreaterThan(0);
            act.Should().NotThrow();

            list = jsonService.GetCategoryOptionsBy();
            act = () => list.Count.Should().BeGreaterThan(0);
            act.Should().NotThrow();

            list = jsonService.GetCategoryOptionsBy();
            act = () => list.Count.Should().BeGreaterThan(0);
            act.Should().NotThrow();

            list = jsonService.GetCategoryOptionsBy();
            act = () => list.Count.Should().BeGreaterThan(0);
            act.Should().NotThrow();

            list = jsonService.GetConditionOptionsBy();
            act = () => list.Count.Should().BeGreaterThan(0);
            act.Should().NotThrow();

            list = jsonService.GetMileOptionsBy();
            act = () => list.Count.Should().BeGreaterThan(0);
            act.Should().NotThrow();

            list = jsonService.GetSortOptionsBy();
            act = () => list.Count.Should().BeGreaterThan(0);
            act.Should().NotThrow();

            list = jsonService.GetConditionOptionsBy();
            act = () => list.Count.Should().BeGreaterThan(0);
            act.Should().NotThrow();

            list = jsonService.GetMileOptionsBy();
            act = () => list.Count.Should().BeGreaterThan(0);
            act.Should().NotThrow();

            list = jsonService.GetSortOptionsBy();
            act = () => list.Count.Should().BeGreaterThan(0);
            act.Should().NotThrow();
        }
    }
}
