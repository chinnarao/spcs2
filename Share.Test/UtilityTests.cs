using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching;
using Microsoft.Extensions.Caching.Memory;
using Xunit;
using Microsoft.Extensions.Caching.Distributed;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Share.Test
{
    public class MyClass
    {
        public HashSet<string> HS { get; set; }
    }

    public class UtilityTests
    {
        [Fact]
        public void HashSetTest()
        {
            var query = from i in Enumerable.Range(0, 10)
                        select new { i, j = i + 1 };
            var resultSet = query.ToHashSet();

            HashSet<int> a = new HashSet<int>();
            var query1 = from i in Enumerable.Range(0, 10)
                        select new { i };
            var resultSet1 = query1.ToHashSet();


            HashSet<string> set = new HashSet<string>();
            set.Add("a");
            set.Add("b");
            set.Add("B");
            set.Add("1");
            set.Add("a");
            set.Add("a");

            

            Assert.True(set.Count >= 0);
            
        }

        [Fact]
        public void createDynamicJObjectTest()
        {
            JEnumerable<JObject> list = new JEnumerable<JObject>();
            dynamic product = new JObject();
            product.ProductName = "Elbow Grease";
            product.Enabled = true;
            product.Price = 4.90m;
            product.StockCount = 9000;
            product.StockValue = 44100;
            product.Tags = new JArray("Real", "OnSale");
            

            string a = product.ToString();
        }
    }
}
