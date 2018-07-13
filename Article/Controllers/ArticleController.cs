using DbContexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Models.Article.Dtos;
using Newtonsoft.Json;
using Services.Article;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

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

        [HttpPost]
        public IActionResult SearchArticles([FromBody] ArticleSortFilterPageOptions options)
        {
            int defaultArticlesHomeDisplay = Convert.ToInt32(_configuration["DefaultArticlesHomeDisplay"]);
            if (defaultArticlesHomeDisplay <= 0) throw new ArgumentOutOfRangeException(nameof(defaultArticlesHomeDisplay));

            options.DefaultPageSize = defaultArticlesHomeDisplay;
            options.PageNumber = 1;

            var anonymous = _articleService.SearchArticles(defaultArticlesHomeDisplay, options);
            return Ok(anonymous);
        }

        [HttpPost]
        public IActionResult CreateArticle([FromBody] ArticleDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            model.ArticleId = model.ArticleLicenseDto.ArticleId = DateTime.UtcNow.Ticks;
            model.AttachedAssetsInCloudStorageId = Guid.NewGuid();

            int inMemoryCachyExpireDays = Convert.ToInt32(_configuration["InMemoryCacheDays"]);
            if (inMemoryCachyExpireDays <= 0) throw new ArgumentOutOfRangeException(nameof(inMemoryCachyExpireDays));
            string htmlFileName = _configuration["ArticleHtmlTemplateFileNameWithExt"];
            if (string.IsNullOrWhiteSpace(htmlFileName)) throw new ArgumentNullException(nameof(htmlFileName));
            string googleStorageBucketName = _configuration["ArticleBucketNameInGoogleCloudStorage"];
            if (string.IsNullOrWhiteSpace(googleStorageBucketName)) throw new ArgumentOutOfRangeException(nameof(googleStorageBucketName));
            GoogleStorageArticleFileDto fileModel = model.GoogleStorageArticleFileDto;
            fileModel.CacheExpiryDateTimeForHtmlTemplate = DateTime.UtcNow.AddDays(Convert.ToDouble(inMemoryCachyExpireDays));
            fileModel.HtmlFileTemplateFullPathWithExt = Path.Combine(Directory.GetCurrentDirectory(), htmlFileName);
            fileModel.GoogleStorageBucketName = googleStorageBucketName;
            fileModel.CACHE_KEY = Constants.Article_HTML_FILE_TEMPLATE;
            fileModel.GoogleStorageObjectNameWithExt = string.Format("{0}{1}", model.AttachedAssetsInCloudStorageId.ToString("N"), Path.GetExtension(htmlFileName));
            fileModel.ContentType = Utility.GetMimeTypes()[Path.GetExtension(htmlFileName)];
            fileModel.AnonymousDataObjectForHtmlTemplate = model.ArticleDtoAsAnonymous;

            model.ArticleLicenseDto.ArticleLicenseId = DateTime.UtcNow.Ticks;
            if (!string.IsNullOrWhiteSpace(model.ArticleLicenseDto.License)) model.ArticleLicenseDto.LicensedDate = DateTime.UtcNow;

            ArticleDto articleDto = _articleService.CreateArticle(model);
            return Ok(articleDto);
        }

        [HttpGet("{articleId}")]
        public IActionResult GetArticleDetail(long articleId)
        {
            if (articleId <= 0) throw new ArgumentOutOfRangeException(nameof(articleId));
            ArticleDto dto = _articleService.GetArticleDetail(articleId);
            return Ok(dto);
        }

        [HttpGet("{articleId}")]
        public IActionResult GetArticleLicenseCommentsCommits(long articleId)
        {
            if (articleId <= 0) throw new ArgumentOutOfRangeException(nameof(articleId));
            dynamic articleJsonAnonymous = _articleService.GetArticleLicenseCommentsCommits(articleId);
            return Ok(articleJsonAnonymous);
        }

        [HttpPost]
        public IActionResult CreateComment([FromBody] ArticleCommentDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            model.ArticleCommentId = DateTime.UtcNow.Ticks;

            ArticleCommentDto dto = _articleService.CreateComment(model);
            return Ok(dto);
        }

        [HttpPost]
        public IActionResult UpdateArticleWithCommitHistory([FromBody] ArticleDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (model.ArticleLicenseDto == null) throw new ArgumentNullException(nameof(model.ArticleLicenseDto));
            if (model.ArticleId != model.ArticleLicenseDto.ArticleId) throw new InvalidDataException(nameof(model.ArticleCommitDtos));
            if (model.ArticleId <= 0 || model.ArticleLicenseDto.ArticleId <= 0) throw new InvalidDataException(nameof(model.ArticleId));
            if (model.ArticleLicenseDto.ArticleLicenseId <= 0) throw new InvalidDataException(nameof(model.ArticleLicenseDto.ArticleLicenseId));

            if (model.ArticleCommitDtos == null || model.ArticleCommitDtos.Count == 0) throw new ArgumentNullException(nameof(model.ArticleCommitDtos));
            ArticleCommitDto articleCommitDto = model.ArticleCommitDtos.First();
            if (model.ArticleId != articleCommitDto.ArticleId) throw new InvalidDataException(nameof(model.ArticleCommitDtos));
            if (model.ArticleId <= 0 || articleCommitDto.ArticleId <= 0) throw new InvalidDataException(nameof(model.ArticleId));

            articleCommitDto.ArticleCommitId = DateTime.UtcNow.Ticks;
            articleCommitDto.CommittedDate = DateTime.UtcNow;
            // re think here : what about template save in storage. what about old template file?

            ArticleDto articleDto = _articleService.UpdateArticleWithCommitHistory(model);
            return Ok(articleDto);
        }

        [HttpGet]
        public IActionResult GetAllUniqueTags()
        {
            HashSet<string> set = _articleService.GetAllUniqueTags();
            return Ok(set);
        }
    }
}