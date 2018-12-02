using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Configuration;
using Share.Models.Ad.Dtos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Share.Constants;
using Share.Utilities;
using Services.Common;

namespace Ad.Util
{
    public static class Extensions
    {
        public static IEnumerable<string> Errors(this ModelStateDictionary ModelState)
        {
            return ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
        }

        public static void Defaults(this AdDto model, IConfiguration _configuration)
        {
            byte adDefaultDisplayActiveDays;
            if (byte.TryParse(_configuration["AdDefaultDisplayActiveDays"], out adDefaultDisplayActiveDays))
                model.AdDisplayDays = adDefaultDisplayActiveDays;

            double Longitude;
            if (double.TryParse(_configuration["DefaultLongitude"], out Longitude))
                model.AddressLongitude = "1.0";

            double Lattitude;
            if (double.TryParse(_configuration["DefaultLattitude"], out Lattitude))
                model.AddressLatitude = "1.0";

            model.AdId = DateTime.UtcNow.Ticks.ToString();
            model.AttachedAssetsInCloudStorageId = Guid.NewGuid();
            model.CreatedDateTime = model.UpdatedDateTime = DateTime.UtcNow;
            model.IsDeleted = model.IsActivated = model.IsPublished = false;
        }

        public static void Values(this GoogleStorageAdFileDto fileModel, IConfiguration _configuration, Guid attachedAssetsInCloudStorageGuId)
        {
            string googleStorageBucketName = _configuration["AdBucketNameInGoogleCloudStorage"];
            string htmlFileName = _configuration["AdHtmlTemplateFileNameWithExt"];
            string GoogleStorageFileExtType = Path.GetExtension(htmlFileName);

            fileModel.CacheExpiryDateTimeForHtmlTemplate = Utility.GetCacheExpireDateTime(_configuration["CacheExpireDays"]);
            fileModel.HtmlFileTemplateFullPathWithExt = Path.Combine(Directory.GetCurrentDirectory(), htmlFileName);
            fileModel.GoogleStorageBucketName = googleStorageBucketName;
            fileModel.CACHE_KEY = Constants.AD_HTML_FILE_TEMPLATE;
            fileModel.GoogleStorageObjectNameWithExt = string.Format("{0}{1}", attachedAssetsInCloudStorageGuId, GoogleStorageFileExtType);
            fileModel.ContentType = Utility.GetMimeTypes()[GoogleStorageFileExtType];
        }

        public static KeyValuePair<bool,string> IsValidSearchInputs(this AdSearchDto options, IJsonDataService _jsonDataService)
        {
            List<string> errors = new List<string>();
            if (_jsonDataService.IsValidCategory(options.CategoryId))
                options.IsValidCategoryId = true;
            else
                errors.Add(nameof(options.CategoryId));
            if (_jsonDataService.IsValidCondition(options.ConditionId))
                options.IsValidConditionId = true;
            else
                errors.Add(nameof(options.ConditionId));
            if (_jsonDataService.IsValidMileOption(options.MileOptionsId))
                options.IsValidMileOptionsId = true;
            else
                errors.Add(nameof(options.MileOptionsId));
            if (_jsonDataService.IsValidSortOption(options.SortOptionsId))
                options.IsValidSortOptionsId = true;
            else
                errors.Add(nameof(options.SortOptionsId));
            if (_jsonDataService.IsValidCountryCode(options.CountryCode))
            {
                options.CountryCode = options.CountryCode.Trim();
                options.IsValidCountryCode = true;
            }
            else
                errors.Add(nameof(options.CountryCode));
            if (_jsonDataService.IsValidCountryCode(options.CurrencyCode))
            {
                options.CurrencyCode = options.CurrencyCode.Trim();
                options.IsValidCurrencyCode = true;
            }
            else
                errors.Add(nameof(options.CurrencyCode));

            if (!string.IsNullOrEmpty(options.SearchText))
            {
                options.SearchText = options.SearchText.Trim().ToLower();
                options.IsValidSearchText = true;
            }
            if (!string.IsNullOrEmpty(options.CityName))
            {
                options.CityName = options.CityName.Trim().ToLower();
                options.IsValidCityName = true;
            }
            if (!string.IsNullOrEmpty(options.ZipCode))
            {
                options.ZipCode = options.ZipCode.Trim().ToLower();
                options.IsValidZipCode = true;
            }
            if (options.MinPrice > 0.0)
                options.IsValidMinPrice = true;
            if (options.MaxPrice > 0.0)
                options.IsValidMaxPrice = true;
            if (options.MinPrice > options.MaxPrice)
                options.IsValidMinPrice = options.IsValidMaxPrice = false;
            return new KeyValuePair<bool, string>(errors.Count > 0, string.Join(Path.PathSeparator, errors));
        }
    }
}
