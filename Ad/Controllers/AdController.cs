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
        public IActionResult PostAd([FromBody]PostAdModel data)
        {
            if (string.IsNullOrWhiteSpace(data.Name)) return BadRequest("Invalid input :" + nameof(data.Name));
            if (string.IsNullOrWhiteSpace(data.Occupation)) return BadRequest("Invalid input :" + nameof(data.Occupation));
            int inMemoryCachyExpireDays = Convert.ToInt32(_configuration["InMemoryCacheDays"]);
            if (inMemoryCachyExpireDays <= 0) throw new ArgumentOutOfRangeException(nameof(inMemoryCachyExpireDays));
            string fileName = _configuration["AdHtmlTemplateFileName"];
            if (string.IsNullOrWhiteSpace(fileName)) throw new ArgumentOutOfRangeException(nameof(fileName));
            string bucketName = _configuration["AdBucketNameInGoogleCloudStorage"];
            if (string.IsNullOrWhiteSpace(bucketName)) throw new ArgumentOutOfRangeException(nameof(bucketName));
            Guid assetKey = Guid.NewGuid();
            string objectNameWithExt = string.Format("{0}{1}", assetKey.ToString("N"), Path.GetExtension(fileName));
            string contentType = Utility.GetMimeTypes()[Path.GetExtension(fileName)];
            var anonymousDataObject = data.ConvertToAnonymousType(data);

            _adService.UploadObjectInGoogleStorage(fileName, inMemoryCachyExpireDays, objectNameWithExt, bucketName, anonymousDataObject, contentType, Constants.AD_HTML_FILE_TEMPLATE);
            return Ok(new { Name = "Chinna", Email = "chinnarao@live.com", ObjectName = objectNameWithExt });
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok( new { Name = "Chinna", Email = "chinnarao@live.com" });
        }
    }
}