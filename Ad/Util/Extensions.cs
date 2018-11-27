using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Configuration;
using Models.Ad.Dtos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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
                model.AddressLongitude = Longitude;

            double Lattitude;
            if (double.TryParse(_configuration["DefaultLattitude"], out Lattitude))
                model.AddressLatitude = Lattitude;

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

            int inMemoryCachyExpireDays;
            if (int.TryParse(_configuration["InMemoryCacheDays"], out inMemoryCachyExpireDays))
                fileModel.CacheExpiryDateTimeForHtmlTemplate = DateTime.UtcNow.AddDays(Convert.ToDouble(inMemoryCachyExpireDays));

            fileModel.HtmlFileTemplateFullPathWithExt = Path.Combine(Directory.GetCurrentDirectory(), htmlFileName);
            fileModel.GoogleStorageBucketName = googleStorageBucketName;
            fileModel.CACHE_KEY = Constants.AD_HTML_FILE_TEMPLATE;
            fileModel.GoogleStorageObjectNameWithExt = string.Format("{0}{1}", attachedAssetsInCloudStorageGuId, GoogleStorageFileExtType);
            fileModel.ContentType = Utility.GetMimeTypes()[GoogleStorageFileExtType];
        }
    }
}
