using DbContexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Models.Article.ArticleController;
using Newtonsoft.Json;
using Services;

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
            var article = _articleService.Get(articleId);
            return Ok(new { Name = "Chinna", Email = "chinnarao@live.com" });
        }

        [HttpPost]
        public IActionResult PostArticle([FromBody] PostArticleModel data)
        {
            Models.Article.Pocos.Commit c = new Models.Article.Pocos.Commit();
            //var a = Json(c);
            return Ok(new { Name = "Chinna", Email = "chinnarao@live.com" });
        }
    }
}