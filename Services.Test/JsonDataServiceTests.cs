using System;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using NSubstitute;
using Xunit;
using Services.Commmon;
using Microsoft.Extensions.Configuration;
using Services.Common;
using FluentAssertions;
using System.Collections.Generic;
using Newtonsoft.Json;
using Share.Models.Common;
using System.Linq;

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

        [Fact]
        public void createDynamicJObjectTest()
        {
            var factoryMock = Substitute.For<Func<string>>();
            var optionsMock = Substitute.For<IOptions<MemoryCacheOptions>>();
            optionsMock.Value.Returns(callInfo => new MemoryCacheOptions());
            var memoryCache = new MemoryCache(optionsMock);
            ICacheService cacheService = new CacheService(memoryCache);
            IConfiguration _configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            IFileRead read = new FileRead(_configuration, cacheService);
            IJsonDataService jsonService = new JsonDataService(_configuration, cacheService, read);

            var countries = jsonService.GetCountries();

            string codesJson   = read.ReadJsonFile("Json,codes.json");
            List<Code> codes = JsonConvert.DeserializeObject<List<Code>>(codesJson);
            List<string> missingCN1 = new List<string>();
            List<string> missingCN2 = new List<string>();
            List<string> missingCN3 = new List<string>();
            List<string> missingCN4 = new List<string>();
            foreach (var c in countries)
            {
                Code code = codes.FirstOrDefault(a => a.CountryName == c.CountryName);
                if (code == null)
                {
                    missingCN1.Add(c.CountryName);
                }

                Code code1 = codes.FirstOrDefault(a => a.CountryName.Contains(c.CountryName));
                if (code1 == null)
                {
                    missingCN2.Add(c.CountryName);
                }

                Code code2 = codes.FirstOrDefault(a => a.CountryName.StartsWith(c.CountryName));
                if (code2 == null)
                {
                    missingCN3.Add(c.CountryName);
                }
                Code code3 = codes.FirstOrDefault(a => c.CountryName.ToLower().Contains(a.CountryName.ToLower()));
                if (code3 == null)
                {
                    missingCN4.Add(c.CountryName);
                }
            }

            List<string> missingCN5 = new List<string>();
            List<string> missingCN6 = new List<string>();
            foreach (var i in codes)
            {
                Country sfsf = countries.FirstOrDefault(a => a.CountryName.StartsWith(i.CountryName));
                if (sfsf == null)
                {
                    missingCN5.Add(i.CountryName);
                }

                Country sfsf1 = countries.FirstOrDefault(a => a.CountryName.Contains(i.CountryName));
                if (sfsf1 == null)
                {
                    missingCN6.Add(i.CountryName);
                }

            }
            Action act = () => missingCN6.Count.Should().BeGreaterThan(0);
            act.Should().NotThrow();
        }

        [Fact]
        public void createDynamicJObjectTest1()
        {
            var factoryMock = Substitute.For<Func<string>>();
            var optionsMock = Substitute.For<IOptions<MemoryCacheOptions>>();
            optionsMock.Value.Returns(callInfo => new MemoryCacheOptions());
            var memoryCache = new MemoryCache(optionsMock);
            ICacheService cacheService = new CacheService(memoryCache);
            IConfiguration _configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            IFileRead read = new FileRead(_configuration, cacheService);
            IJsonDataService jsonService = new JsonDataService(_configuration, cacheService, read);

            var countries = jsonService.GetCountries();

            string codesJson = read.ReadJsonFile("Json,codes2.json");
            List<Code> codes = JsonConvert.DeserializeObject<List<Code>>(codesJson);
            List<string> missingCN2 = new List<string>();
            List<string> missingCN3 = new List<string>();
            foreach (var c in codes)
            {
                var found = countries.FirstOrDefault(a => a.CountryName == c.CountryName);
                if (found == null)
                {
                    missingCN2.Add(c.CountryName);
                }
                else
                {
                    missingCN3.Add(c.CountryName);
                }
            }
            Action act = () => missingCN2.Count.Should().BeGreaterThan(0);
            act.Should().NotThrow();
        }

        [Fact]
        public void createDynamicJObjectTest2()
        {
            var factoryMock = Substitute.For<Func<string>>();
            var optionsMock = Substitute.For<IOptions<MemoryCacheOptions>>();
            optionsMock.Value.Returns(callInfo => new MemoryCacheOptions());
            var memoryCache = new MemoryCache(optionsMock);
            ICacheService cacheService = new CacheService(memoryCache);
            IConfiguration _configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            IFileRead read = new FileRead(_configuration, cacheService);
            IJsonDataService jsonService = new JsonDataService(_configuration, cacheService, read);

            var countries = jsonService.GetCountries();

            string codesJson = read.ReadJsonFile("Json,codes2.json");
            List<Code> codes = JsonConvert.DeserializeObject<List<Code>>(codesJson);
            List<string> missingCN2 = new List<string>();
            List<string> missingCN3 = new List<string>();
            foreach (var c in codes)
            {
                var found = countries.FirstOrDefault(a => a.CountryName == c.CountryName);
                if (found == null)
                {
                    missingCN2.Add(c.CountryName);
                }
                else
                {
                    missingCN3.Add(c.CountryName);
                    found.CountryCallingCode = c.CountryCallingCode;
                }
            }

            foreach (var c in missingCN2)
            {
                var found = countries.FirstOrDefault(a => a.CountryName == c);
                if (found != null)
                {
                    found.CountryCallingCode = 0;
                }
            }

            countries = countries.OrderBy(a => a.CountryCode).ThenBy(b => b.CountryName).ToList();
            var json = JsonConvert.SerializeObject(countries);
            Action act = () => missingCN2.Count.Should().BeGreaterThan(0);
            act.Should().NotThrow();
        }
    }
}
