using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Services.Commmon;
using Share.Models.Json;
using Microsoft.Extensions.Configuration;
using Share.Utilities;
using System.IO;
using System.Reflection;

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

        public Dictionary<int, string> GetCategoryOptionsBy()
        {
            json_lookup lookUp = GetLookUp();
            Dictionary<int, string>  dict = lookUp.categoryOptionsBy;
            return dict;
        }

        public Dictionary<int, string> GetConditionOptionsBy()
        {
            json_lookup lookUp = GetLookUp();
            Dictionary<int, string> dict = lookUp.conditionOptionsBy;
            return dict;
        }

        public List<country> GetCountries()
        {
            string pathss = Utility.GetAssemblyPath();
            string sfs  = Directory.GetCurrentDirectory() + " [] " + Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

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

        public Dictionary<int, string> GetMileOptionsBy()
        {
            json_lookup lookUp = GetLookUp();
            Dictionary<int, string> dict = lookUp.mileOptionsBy;
            return dict;
        }

        public Dictionary<int, string> GetSortOptionsBy()
        {
            json_lookup lookUp = GetLookUp();
            Dictionary<int, string> dict = lookUp.sortOptionsBy;
            return dict;
        }

        private json_lookup GetLookUp()
        {
            json_lookup jsonLookUp = _cacheService.Get<json_lookup>(nameof(GetLookUp));
            if (jsonLookUp == null || 
                jsonLookUp.categoryOptionsBy == null || 
                jsonLookUp.conditionOptionsBy == null || 
                jsonLookUp.mileOptionsBy == null || 
                jsonLookUp.sortOptionsBy == null)
            {
                string json = _fileReadService.ReadJsonFile(_configuration["JsonFileNameWithExtForLookUp"]);
                jsonLookUp = JsonConvert.DeserializeObject<json_lookup>(json);
                jsonLookUp = _cacheService.GetOrAdd<json_lookup>(nameof(GetLookUp), () => jsonLookUp, Utility.GetCacheExpireDateTime(_configuration["CacheExpireDays"]));
                if (jsonLookUp == null)
                    throw new Exception(nameof(jsonLookUp));
                if (jsonLookUp.categoryOptionsBy == null)
                    throw new Exception(nameof(jsonLookUp.categoryOptionsBy));
                if (jsonLookUp.conditionOptionsBy == null)
                    throw new Exception(nameof(jsonLookUp.conditionOptionsBy));
                if (jsonLookUp.mileOptionsBy == null)
                    throw new Exception(nameof(jsonLookUp.mileOptionsBy));
                if (jsonLookUp.sortOptionsBy == null)
                    throw new Exception(nameof(jsonLookUp.sortOptionsBy));
            }
            return jsonLookUp;
        }
    }
    public interface IJsonDataService
    {
        List<country> GetCountries();
        Dictionary<int, string> GetCategoryOptionsBy();
        Dictionary<int, string> GetConditionOptionsBy();
        Dictionary<int, string> GetMileOptionsBy();
        Dictionary<int, string> GetSortOptionsBy();
    }
}
