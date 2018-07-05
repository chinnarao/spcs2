using System;
using System.Linq;
using System.Threading;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using NSubstitute;
using Xunit;
using Services;
using System.Collections.Generic;

namespace Services.Test
{
    public class CacheServiceTests
    {
        // this one will fail as implementation purely based on _memoryCache.GetOrCreate allows for calling
        // factory method multiple times
        [Fact]
        public void GetOrAdd_CallsFactoryMethodOnce()
        {
            var factoryMock = Substitute.For<Func<string>>();
            var optionsMock = Substitute.For<IOptions<MemoryCacheOptions>>();
            optionsMock.Value.Returns(callInfo => new MemoryCacheOptions());
            var memoryCache = new MemoryCache(optionsMock);

            var subject = new CacheService(memoryCache);

            var threads = Enumerable.Range(0, 10)
                .Select(_ => new Thread(() => subject.GetOrAdd("key", factoryMock, DateTime.MaxValue))).ToList();

            threads.ForEach(thread => thread.Start());
            threads.ForEach(thread => thread.Join());

            factoryMock.Received(10)();
        }

        [Fact]
        public void GetOrAdd_CallsLockedFactoryMethodOnce()
        {
            var factoryMock = Substitute.For<Func<string>>();
            var optionsMock = Substitute.For<IOptions<MemoryCacheOptions>>();
            optionsMock.Value.Returns(callInfo => new MemoryCacheOptions());
            var memoryCache = new MemoryCache(optionsMock);

            var subject = new LockedFactoryCacheService(memoryCache);

            var threads = Enumerable.Range(0, 10).Select(_ => new Thread(() => subject.GetOrAdd("key", factoryMock, DateTime.MaxValue.AddDays(-1)))).ToList();
            threads.ForEach(thread => thread.Start());
            threads.ForEach(thread => thread.Join());
            factoryMock.Received(1)();
        }

        [Fact]
        public void GetOrAdd_CallsLocked1FactoryMethodOnce()
        {
            var memoryCache = new MemoryCache(new MemoryCacheOptions());
            var subject = new LockedFactoryCacheService(memoryCache);
            Dictionary<string, string> c = new Dictionary<string, string>
            {
                {"AD", "Andorra"},{"AR", "Argentina"},{"AS", "AmericanSamoa"},{"AT", "Austria"},{"AU", "Australia"},{"AX", "�landIslands"},{"BD", "Bangladesh"},{"BE", "Belgium"},{"BG", "Bulgaria"},{"BR", "Brazil"},{"BY", "Belarus"},{"CA", "Canada"},{"CH", "Switzerland"},{"CO", "Colombia"},{"CR", "CostaRica"},{"CZ", "Czechia"},{"DE", "Germany"},{"DK", "Denmark"},{"DO", "DominicanRepublic"},{"DZ", "Algeria"},{"ES", "Spain"},{"FI", "Finland"},{"FO", "FaroeIslands"},{"FR", "France"},{"GB", "UnitedKingdom"},{"GF", "FrenchGuiana"},{"GG", "Guernsey"},{"GL", "Greenland"},{"GP", "Guadeloupe"},{"GT", "Guatemala"},{"GU", "Guam"},{"HR", "Croatia"},{"HU", "Hungary"},{"IE", "Ireland"},{"IM", "IsleofMan"},{"IN", "India"},{"IS", "Iceland"},{"IT", "Italy"},{"JE", "Jersey"},{"JP", "Japan"},{"LI", "Liechtenstein"},{"LK", "SriLanka"},{"LT", "Lithuania"},{"LU", "Luxembourg"},{"MC", "Monaco"},{"MD", "Moldova"},{"MH", "MarshallIslands"},{"MK", "Macedonia"},{"MP", "NorthernMarianaIslands"},{"MQ", "Martinique"},{"MT", "Malta"},{"MX", "Mexico"},{"MY", "Malaysia"},{"NC", "NewCaledonia"},{"NL", "Netherlands"},{"NO", "Norway"},{"NZ", "NewZealand"},{"PH", "Philippines"},{"PK", "Pakistan"},{"PL", "Poland"},{"PM", "St.Pierre&Miquelon"},{"PR", "PuertoRico"},{"PT", "Portugal"},{"RE", "R�union"},{"RO", "Romania"},{"RU", "Russia"},{"SE", "Sweden"},{"SI", "Slovenia"},{"SJ", "Svalbard&JanMayen"},{"SK", "Slovakia"},{"SM", "SanMarino"},{"TH", "Thailand"},{"TR", "Turkey"},{"US", "UnitedStates"},{"VA", "VaticanCity"},{"VI", "U.S.VirginIslands"},{"WF", "Wallis&Futuna"},{"YT", "Mayotte"},{"ZA", "SouthAfrica"}
            };
            Func<Dictionary<string, string>> func1 = () => c;//delegate () { return c; };

            var result = subject.GetOrAdd("key", () => c, DateTimeOffset.MaxValue);
            Assert.Equal(c, result);
        }
    }
}
