using System;
using System.Collections.Generic;
using Services.Commmon;
using Share.Models.Common;
using Microsoft.Extensions.Configuration;
using Share.Utilities;
using Newtonsoft.Json;
using System.Linq;

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

        public List<Country> GetCountries()
        {
            List<Country> countries = _cacheService.Get<List<Country>>(nameof(GetCountries));
            if (countries == null)
            {
                string json = _fileReadService.ReadJsonFile(_configuration["FolderPathForCountryJson"]);
                Countries jsonCountry = JsonConvert.DeserializeObject<Countries>(json);
                jsonCountry = _cacheService.GetOrAdd<Countries>(nameof(GetCountries), () => jsonCountry, Utility.GetCacheExpireDateTime(_configuration["CacheExpireDays"]));
                if (jsonCountry == null)
                    throw new Exception(nameof(jsonCountry.Country));
                countries = jsonCountry.Country;
            }
            return countries;
        }

        public LookUp GetLookUpBy()
        {
            LookUp lookUp = _cacheService.Get<LookUp>(nameof(GetLookUpBy));
            if (lookUp == null)
            {
                string json = _fileReadService.ReadJsonFile(_configuration["FolderPathForLookUpJson"]);
                lookUp = JsonConvert.DeserializeObject<LookUp>(json);
                IsValidLookUp(lookUp);
                lookUp = _cacheService.GetOrAdd<LookUp>(nameof(GetLookUpBy), () => lookUp, Utility.GetCacheExpireDateTime(_configuration["CacheExpireDays"]));
                IsValidLookUp(lookUp);
            }
            return lookUp;
        }

        private void IsValidLookUp(LookUp lookUp)
        {
            if (lookUp == null ||
                    lookUp.CategoryOptionsBy == null || lookUp.CategoryOptionsBy.Count == 0 ||
                    lookUp.ConditionOptionsBy == null || lookUp.ConditionOptionsBy.Count == 0 ||
                    lookUp.MileOptionsBy == null || lookUp.MileOptionsBy.Count == 0 ||
                lookUp.SortOptionsBy == null || lookUp.SortOptionsBy.Count == 0)
            throw new Exception(nameof(LookUp));
        }

        public List<KeyValueDescription> GetCategoryOptionsBy() => GetLookUpBy().CategoryOptionsBy;
        public List<KeyValueDescription> GetConditionOptionsBy() => GetLookUpBy().ConditionOptionsBy;
        public bool IsValidCategory(int categoryId) => GetCategoryOptionsBy().Any(c => c.Key == categoryId);
        public bool IsValidCondition(int conditionId) => GetConditionOptionsBy().Any(c => c.Key == conditionId);
        public bool IsValidCallingCode(int callingCode) => GetCountries().Any(c => c.CountryCallingCode == callingCode);
        public bool IsValidCountryCode(string countryCode) => GetCountries().Any(c => c.CountryCode == countryCode);
        public bool IsValidCurrencyCode(string currencyCode) => GetCountries().Any(c => c.CurrencyCode == currencyCode);

        public List<KeyValueDescription> GetMileOptionsBy() => GetLookUpBy().MileOptionsBy;
        public List<KeyValueDescription> GetSortOptionsBy() => GetLookUpBy().SortOptionsBy;
        public bool IsValidMileOption(int mileOptionId) => GetMileOptionsBy().Any(c => c.Key == mileOptionId);
        public KeyValueDescription GetMileOptionById(int mileOptionId) => GetMileOptionsBy().FirstOrDefault(c => c.Key == mileOptionId);
        public KeyValueDescription GetSortOptionById(int sortOptionId) => GetSortOptionsBy().FirstOrDefault(c => c.Key == sortOptionId);
        public bool IsValidSortOption(int sortOptionId) => GetSortOptionsBy().Any(c => c.Key == sortOptionId);
        public KeyValueDescription GetMinMileOption() => GetMileOptionsBy().OrderBy(m => m.Key).First();
        public byte GetMaxMileOptionById() => GetMileOptionsBy().Max(c => c.Key);
        public KeyValueDescription GetMinSortOption() => GetSortOptionsBy().OrderBy(m => m.Key).First();
    }
    public interface IJsonDataService
    {
        LookUp GetLookUpBy();
        List<Country> GetCountries();
        List<KeyValueDescription> GetCategoryOptionsBy();
        List<KeyValueDescription> GetConditionOptionsBy();
        List<KeyValueDescription> GetMileOptionsBy();
        List<KeyValueDescription> GetSortOptionsBy();
        bool IsValidCategory(int categoryId);
        bool IsValidCondition(int conditionId);
        bool IsValidMileOption(int mileOptionId);
        bool IsValidSortOption(int sortOptionId);
        bool IsValidCallingCode(int callingCode);
        bool IsValidCountryCode(string countryCode);
        bool IsValidCurrencyCode(string currencyCode);
        KeyValueDescription GetSortOptionById(int sortOptionId);
        KeyValueDescription GetMileOptionById(int mileOptionId);
        KeyValueDescription GetMinMileOption();
        KeyValueDescription GetMinSortOption();
        byte GetMaxMileOptionById();
    }
}
