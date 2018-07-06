using DbContexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Models.Article.Dtos;
using Newtonsoft.Json;
using Services;
using System;
using System.IO;

//https://webapphuddle.com/design-tags-feature-in-web-apps/
//https://www.maketecheasier.com/5-online-tools-to-create-tag-clouds/
//https://www.ebates.com/rf.do?referrerid=vdPURIW4fINgWguIRm6jRQ%3D%3D&eeid=28585
//https://market.mashape.com/
namespace Article.Controllers
{
    [Route("[controller]/[action]")]
    public class ArticleController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;
        private readonly IArticleService _articleService;

        public ArticleController(IConfiguration configuration, ILogger<ArticleController> logger, IArticleService articleService)
        {
            _configuration = configuration;
            _logger = logger;
            _articleService = articleService;
        }

        [HttpGet]
        public IActionResult Get(long articleId)
        {
            return Ok(new { Name = "Chinna", Email = "chinnarao@live.com" });
        }

        [HttpPost]
        public IActionResult PostArticle([FromBody] ArticleDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            model.ArticleId = DateTime.Now.Ticks;
            model.AttachedAssetsInCloudStorageId = Guid.NewGuid();

            int inMemoryCachyExpireDays = Convert.ToInt32(_configuration["InMemoryCacheDays"]);
            if (inMemoryCachyExpireDays <= 0) throw new ArgumentOutOfRangeException(nameof(inMemoryCachyExpireDays));
            string htmlFileName = _configuration["ArticleHtmlTemplateFileNameWithExt"];
            if (string.IsNullOrWhiteSpace(htmlFileName)) throw new ArgumentNullException(nameof(htmlFileName));
            string googleStorageBucketName = _configuration["ArticleBucketNameInGoogleCloudStorage"];
            if (string.IsNullOrWhiteSpace(googleStorageBucketName)) throw new ArgumentOutOfRangeException(nameof(googleStorageBucketName));
            GoogleStorageArticleFileDto fileModel = model.GoogleStorageArticleFileDto;
            fileModel.CacheExpiryDateTimeForHtmlTemplate = DateTime.Now.AddDays(Convert.ToDouble(inMemoryCachyExpireDays));
            fileModel.HtmlFileTemplateFullPathWithExt = Path.Combine(Directory.GetCurrentDirectory(), htmlFileName);
            fileModel.GoogleStorageBucketName = googleStorageBucketName;
            fileModel.CACHE_KEY = Constants.Article_HTML_FILE_TEMPLATE;
            fileModel.GoogleStorageObjectNameWithExt = string.Format("{0}{1}", model.AttachedAssetsInCloudStorageId.ToString("N"), Path.GetExtension(htmlFileName));
            fileModel.ContentType = Utility.GetMimeTypes()[Path.GetExtension(htmlFileName)];
            fileModel.AnonymousDataObjectForHtmlTemplate = model.ArticleDtoAsAnonymous;

            _articleService.StartArticleProcess(model);

            var returnAnonymousObject = new { Id = model.ArticleId, AttachedAssetsInCloudStorageId = model.AttachedAssetsInCloudStorageId.ToString("N") };
            return Ok(returnAnonymousObject);
            //return Ok(new { Name = "Chinna", Email = "chinnarao@live.com" });
        }
    }
}