using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Models.Ad.Dtos;
using Services.Ad;
using System;
using System.Collections.Generic;
using System.IO;

namespace Ad.Controllers
{
    //https://github.com/aspnet/Docs/blob/master/aspnetcore/fundamentals/logging/index/sample2/Controllers/TodoController.cs
    [Route("[controller]/[action]")]
    public class AdController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;
        private readonly IAdService _adService;

        public AdController(IConfiguration configuration, ILogger<AdController> logger, IAdService adService)
        {
            _configuration = configuration;
            _logger = logger;
            _adService = adService;
        }

        [HttpPost]
        public IActionResult CreateAd([FromBody]AdDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            int adDefaultDisplayActiveDays = Convert.ToInt32(_configuration["AdDefaultDisplayActiveDays"]);
            if (adDefaultDisplayActiveDays <= 0) throw new ArgumentOutOfRangeException(nameof(adDefaultDisplayActiveDays));
            int inMemoryCachyExpireDays = Convert.ToInt32(_configuration["InMemoryCacheDays"]);
            if (inMemoryCachyExpireDays <= 0) throw new ArgumentOutOfRangeException(nameof(inMemoryCachyExpireDays));
            string htmlFileName = _configuration["AdHtmlTemplateFileNameWithExt"];
            if (string.IsNullOrWhiteSpace(htmlFileName)) throw new ArgumentNullException(nameof(htmlFileName));
            string googleStorageBucketName = _configuration["AdBucketNameInGoogleCloudStorage"];
            if (string.IsNullOrWhiteSpace(googleStorageBucketName)) throw new ArgumentOutOfRangeException(nameof(googleStorageBucketName));

            GoogleStorageAdFileDto fileModel = new GoogleStorageAdFileDto();
            fileModel.CacheExpiryDateTimeForHtmlTemplate = DateTime.UtcNow.AddDays(Convert.ToDouble(inMemoryCachyExpireDays));
            fileModel.HtmlFileTemplateFullPathWithExt = Path.Combine(Directory.GetCurrentDirectory(), htmlFileName);
            fileModel.GoogleStorageBucketName = googleStorageBucketName;
            fileModel.CACHE_KEY = Constants.AD_HTML_FILE_TEMPLATE;
            fileModel.GoogleStorageObjectNameWithExt = string.Format("{0}{1}", model.AttachedAssetsInCloudStorageId?.ToString("N"), Path.GetExtension(htmlFileName));
            fileModel.ContentType = Utility.GetMimeTypes()[Path.GetExtension(htmlFileName)];

            model.GoogleStorageAdFileDto = fileModel;
            model.AdId = DateTime.UtcNow.Ticks;
            model.AttachedAssetsInCloudStorageId = Guid.NewGuid();
            model.CreatedDateTime = model.UpdatedDateTime = DateTime.UtcNow;

            AdDto dto = _adService.CreateAd(model);
            dto.GoogleStorageAdFileDto = null;
            dto.UpdatedDateTimeString = null;
            return Ok(dto);
        }

        [HttpPost]
        public IActionResult SearchAds([FromBody] AdSortFilterPageOptions options)
        {
            int defaultPageSize = Convert.ToInt32(_configuration["DefaultAdsHomeDisplay"]);
            if (defaultPageSize <= 0) throw new ArgumentOutOfRangeException(nameof(defaultPageSize));

            options.DefaultPageSize = defaultPageSize;
            options.PageNumber = 1;

            var anonymous = _adService.SearchAds(options);
            return Ok(anonymous);
        }

        [HttpGet("{adId}")]
        public IActionResult GetAdDetail(long adId)
        {
            if (adId <= 0) throw new ArgumentOutOfRangeException(nameof(adId));
            AdDto dto = _adService.GetAdDetail(adId);
            return Ok(dto);
        }

        [HttpPost]
        public IActionResult UpdateAd([FromBody] AdDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            AdDto adDto = _adService.UpdateAd(model);
            return Ok(adDto);
        }

        [HttpGet]
        public IActionResult GetAllUniqueTags()
        {
            HashSet<string> set = _adService.GetAllUniqueTags();
            return Ok(set);
        }
    }
}