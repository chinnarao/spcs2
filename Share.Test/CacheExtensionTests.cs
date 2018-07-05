using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching;
using Microsoft.Extensions.Caching.Memory;
using Xunit;
using Microsoft.Extensions.Caching.Distributed;
using System.Collections.Generic;

//https://github.com/ttrider/CacheAdapterAsync
namespace Share.Test
{
    public class CacheExtensionTests
    {
        [Fact]
        public async Task SimpleGetItemTest()
        {
            var mc = new MemoryCache(new MemoryCacheOptions());

            var targetKey = "test";
            var targetValue = 1;

            var index = targetValue;
            mc.Set(targetKey, targetValue);

            var result = await mc.GetOrCreateExclusiveAsync<int>(targetKey, (k) => Task.FromResult(index++));

            Assert.Equal(targetValue, result);
        }

        [Fact]
        public async Task SimpleGetItemstringTest()
        {
            var mc = new MemoryCache(new MemoryCacheOptions());

            var targetKey = "test";
            var targetValue = "Provides the thread-safe extension to the IMemoryCache";

            //var index = targetValue;
            mc.Set(targetKey, targetValue);

            var result = await mc.GetOrCreateExclusiveAsync<string>(targetKey, (k) => Task.FromResult(targetValue));

            Assert.Equal(targetValue, result);
        }

        [Fact]
        public async Task SimpleAddItemTest()
        {
            var mc = new MemoryCache(new MemoryCacheOptions());

            var targetKey = "test";
            var targetValue = 111;
            var index = targetValue;

            var result = await mc.GetOrCreateExclusiveAsync<int>(targetKey, (k) => Task.FromResult(index++));

            Assert.Equal(targetValue, result);
            Assert.NotEqual(index, result);
        }

        [Fact]
        public async Task SimpleAddObjectItemTest()
        {
            var mc = new MemoryCache(new MemoryCacheOptions());
            var targetKey = "111";
            Dictionary<string, string> CountryNamesDictionary = new Dictionary<string, string>
            {
                {"AD", "Andorra"},{"AR", "Argentina"},{"AS", "AmericanSamoa"},{"AT", "Austria"},{"AU", "Australia"},{"AX", "�landIslands"},{"BD", "Bangladesh"},{"BE", "Belgium"},{"BG", "Bulgaria"},{"BR", "Brazil"},{"BY", "Belarus"},{"CA", "Canada"},{"CH", "Switzerland"},{"CO", "Colombia"},{"CR", "CostaRica"},{"CZ", "Czechia"},{"DE", "Germany"},{"DK", "Denmark"},{"DO", "DominicanRepublic"},{"DZ", "Algeria"},{"ES", "Spain"},{"FI", "Finland"},{"FO", "FaroeIslands"},{"FR", "France"},{"GB", "UnitedKingdom"},{"GF", "FrenchGuiana"},{"GG", "Guernsey"},{"GL", "Greenland"},{"GP", "Guadeloupe"},{"GT", "Guatemala"},{"GU", "Guam"},{"HR", "Croatia"},{"HU", "Hungary"},{"IE", "Ireland"},{"IM", "IsleofMan"},{"IN", "India"},{"IS", "Iceland"},{"IT", "Italy"},{"JE", "Jersey"},{"JP", "Japan"},{"LI", "Liechtenstein"},{"LK", "SriLanka"},{"LT", "Lithuania"},{"LU", "Luxembourg"},{"MC", "Monaco"},{"MD", "Moldova"},{"MH", "MarshallIslands"},{"MK", "Macedonia"},{"MP", "NorthernMarianaIslands"},{"MQ", "Martinique"},{"MT", "Malta"},{"MX", "Mexico"},{"MY", "Malaysia"},{"NC", "NewCaledonia"},{"NL", "Netherlands"},{"NO", "Norway"},{"NZ", "NewZealand"},{"PH", "Philippines"},{"PK", "Pakistan"},{"PL", "Poland"},{"PM", "St.Pierre&Miquelon"},{"PR", "PuertoRico"},{"PT", "Portugal"},{"RE", "R�union"},{"RO", "Romania"},{"RU", "Russia"},{"SE", "Sweden"},{"SI", "Slovenia"},{"SJ", "Svalbard&JanMayen"},{"SK", "Slovakia"},{"SM", "SanMarino"},{"TH", "Thailand"},{"TR", "Turkey"},{"US", "UnitedStates"},{"VA", "VaticanCity"},{"VI", "U.S.VirginIslands"},{"WF", "Wallis&Futuna"},{"YT", "Mayotte"},{"ZA", "SouthAfrica"}
            };

            var result = await mc.GetOrCreateExclusiveAsync<Dictionary<string, string>>(targetKey, (k) => Task.FromResult(CountryNamesDictionary));

            Assert.Equal(CountryNamesDictionary, result);
        }

        [Fact]
        public async Task SimpleMultiRequestsTest()
        {
            var mc = new MemoryCache(new MemoryCacheOptions());

            var key = "test";
            var targetValue = 1;
            var index = targetValue;
            var tasks = Enumerable.Repeat(Task.Run(async () =>
            {
                return await mc.GetOrCreateExclusiveAsync(key, (k) => Task.FromResult(index++));
            }), 1000).ToList();


            await Task.WhenAll(tasks);
            foreach (var task in tasks)
            {
                Assert.Equal(targetValue, task.Result);
            }
            Assert.Equal(targetValue, mc.Get<int>(key));
        }
    }
}
