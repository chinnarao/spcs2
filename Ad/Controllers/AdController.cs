using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Models.Ad.AdController;
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
        public IActionResult PostAd([FromBody]PostAdModel model)
        {
            int inMemoryCachyExpireDays = Convert.ToInt32(_configuration["InMemoryCacheDays"]);
            if (inMemoryCachyExpireDays <= 0) throw new ArgumentOutOfRangeException(nameof(inMemoryCachyExpireDays));

            string fileName = _configuration["AdHtmlTemplateFileName"];
            if (string.IsNullOrWhiteSpace(fileName)) throw new ArgumentNullException(nameof(fileName));

            string googleStorageBucketName = _configuration["AdBucketNameInGoogleCloudStorage"];
            if (string.IsNullOrWhiteSpace(googleStorageBucketName)) throw new ArgumentOutOfRangeException(nameof(googleStorageBucketName));

            Guid assetKey = Guid.NewGuid();

            model.InMemoryCachyExpireDays = inMemoryCachyExpireDays;
            model.FileName = fileName;
            model.GoogleStorageBucketName = googleStorageBucketName;
            model.CACHE_KEY = Constants.AD_HTML_FILE_TEMPLATE;
            model.AnonymousDataObject = model.ConvertToAnonymousType(model);
            model.ObjectNameWithExt = string.Format("{0}{1}", assetKey.ToString("N"), Path.GetExtension(fileName));
            model.ContentType = Utility.GetMimeTypes()[Path.GetExtension(fileName)];

            model.AdDto.AttachedAssetsInCloudStorageId = assetKey;

            _adService.StartAdProcess(model);

            return Ok(new { AttachedAssetsInCloudStorageId = assetKey });
        }

        [HttpPost]
        public IActionResult PostAd1([FromBody]PostAdModel data)
        {
            return Ok();
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok( new { Name = "Chinna", Email = "chinnarao@live.com" });
        }
    }
}