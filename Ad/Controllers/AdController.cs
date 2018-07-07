using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Models.Ad.Dtos;
using Services;
using System;
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
        public IActionResult PostAd([FromBody]AdDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            int adDefaultDisplayActiveDays = Convert.ToInt32(_configuration["AdDefaultDisplayActiveDays"]);
            if (adDefaultDisplayActiveDays <= 0) throw new ArgumentOutOfRangeException(nameof(adDefaultDisplayActiveDays));
            model.AdId = DateTime.Now.Ticks;
            model.AttachedAssetsInCloudStorageId = Guid.NewGuid();

            int inMemoryCachyExpireDays = Convert.ToInt32(_configuration["InMemoryCacheDays"]);
            if (inMemoryCachyExpireDays <= 0) throw new ArgumentOutOfRangeException(nameof(inMemoryCachyExpireDays));
            string htmlFileName = _configuration["AdHtmlTemplateFileNameWithExt"];
            if (string.IsNullOrWhiteSpace(htmlFileName)) throw new ArgumentNullException(nameof(htmlFileName));
            string googleStorageBucketName = _configuration["AdBucketNameInGoogleCloudStorage"];
            if (string.IsNullOrWhiteSpace(googleStorageBucketName)) throw new ArgumentOutOfRangeException(nameof(googleStorageBucketName));

            GoogleStorageAdFileDto fileModel = model.GoogleStorageAdFileDto;
            fileModel.CacheExpiryDateTimeForHtmlTemplate = DateTime.Now.AddDays(Convert.ToDouble(inMemoryCachyExpireDays));
            fileModel.HtmlFileTemplateFullPathWithExt = Path.Combine(Directory.GetCurrentDirectory(), htmlFileName);
            fileModel.GoogleStorageBucketName = googleStorageBucketName;
            fileModel.CACHE_KEY = Constants.AD_HTML_FILE_TEMPLATE;
            fileModel.GoogleStorageObjectNameWithExt = string.Format("{0}{1}", model.AttachedAssetsInCloudStorageId.ToString("N"), Path.GetExtension(htmlFileName));
            fileModel.ContentType = Utility.GetMimeTypes()[Path.GetExtension(htmlFileName)];
            fileModel.AnonymousDataObjectForHtmlTemplate = model.AdDtoAsAnonymous;

            _adService.StartAdProcess(model);
            return Ok(model);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok( new { Name = "Chinna", Email = "chinnarao@live.com" });
        }
    }
}