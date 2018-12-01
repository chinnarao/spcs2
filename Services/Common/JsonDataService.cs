using System;
using System.Collections.Generic;
using Services.Commmon;
using Share.Models.Json;
using Microsoft.Extensions.Configuration;
using Share.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Services.Common
{
    public class JsonDataService : IJsonDataService
    {
        private readonly IConfiguration _configuration;
        private readonly IFileRead _fileReadService;
        private readonly ICacheService _cacheService;

        public JsonDataService(IConfiguration configuration, ICacheService cacheService, IFileRead fileReadService)
        {
            _configuration = configuration;
            _fileReadService = fileReadService;
            _cacheService = cacheService;
        }

        public List<country> GetCountries()
        {
            List<country> countries = _cacheService.Get<List<country>>(nameof(GetCountries));
            if (countries == null || countries.Count == 0)
            {
                string json = _fileReadService.ReadJsonFile(_configuration["JsonFileNameWithExtForCountry"]);
                json_country jsonCountry = JsonConvert.DeserializeObject<json_country>(json);
                jsonCountry = _cacheService.GetOrAdd<json_country>(nameof(GetCountries), () => jsonCountry, Utility.GetCacheExpireDateTime(_configuration["CacheExpireDays"]));
                if (jsonCountry == null || jsonCountry.country.Count == 0)
                    throw new Exception(nameof(jsonCountry.country));
                countries = jsonCountry.country;
            }
            return countries;
        }

        public List<KeyValueDescription> GetCategoryOptionsBy()
        {
            return GetLookUpBy(nameof(GetCategoryOptionsBy), "categoryOptionsBy");
        }

        public List<KeyValueDescription> GetConditionOptionsBy()
        {
            return GetLookUpBy(nameof(GetConditionOptionsBy), "conditionOptionsBy");
        }

        public List<KeyValueDescription> GetMileOptionsBy()
        {
            return GetLookUpBy(nameof(GetMileOptionsBy), "mileOptionsBy");
        }

        public List<KeyValueDescription> GetSortOptionsBy()
        {
            return GetLookUpBy(nameof(GetSortOptionsBy), "sortOptionsBy");
        }

        private List<KeyValueDescription> GetLookUpBy(string CACHEKEY, string JSON_PROPERTY_NAME)
        {
            List<KeyValueDescription> options = _cacheService.Get<List<KeyValueDescription>>(CACHEKEY);
            if (options == null || options.Count == 0)
            {
                string json = _fileReadService.ReadJsonFile(_configuration["JsonFileNameWithExtForLookUp"]);
                JObject jObject = JObject.Parse(json);
                options = JsonConvert.DeserializeObject<List<KeyValueDescription>>(jObject.SelectToken(JSON_PROPERTY_NAME).ToString());
                if (options == null)
                    throw new Exception(nameof(options));
                options = _cacheService.GetOrAdd<List<KeyValueDescription>>(CACHEKEY, () => options, Utility.GetCacheExpireDateTime(_configuration["CacheExpireDays"]));
                if (options == null || options.Count == 0)
                    throw new Exception(nameof(options));
            }
            return options;
        }
    }
    public interface IJsonDataService
    {
        List<country> GetCountries();
        List<KeyValueDescription> GetCategoryOptionsBy();
        List<KeyValueDescription> GetConditionOptionsBy();
        List<KeyValueDescription> GetMileOptionsBy();
        List<KeyValueDescription> GetSortOptionsBy();
    }
}
