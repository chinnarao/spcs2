using System;
using System.IO;
using Google;
using File;
using AutoMapper;
using Models.Article.Entities;
using Repository;
using DbContexts;
using Microsoft.Extensions.Logging;
using Models.Article.Dtos;
using System.Text;

namespace Services
{
    public class ArticleService : IArticleService
    {
        private readonly ILogger _logger;
        private readonly IFileRead _fileReadService;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;
        private readonly IGoogleStorage _googleStorage;
        private readonly Repository<Article, ArticleDbContext> _context;

        public ArticleService(ILogger<ArticleService> logger, IMapper mapper, ICacheService cacheService, IFileRead fileReadService, IGoogleStorage googleStorage, ArticleDbContext context)
        {
            _logger = logger;
            _mapper = mapper;
            _fileReadService = fileReadService;
            _cacheService = cacheService;
            _googleStorage = googleStorage;
            _context = new Repository<Article, ArticleDbContext>(context);
        }

        public void StartArticleProcess(ArticleDto dto)
        {
            // transaction has to implement or not , has to think more required.
            Article article = this.InsertArticle(dto);
            this.UploadObjectInGoogleStorage(dto.GoogleStorageArticleFileDto);
        }

        private Article InsertArticle(ArticleDto dto)
        {
            Article article = _mapper.Map<Article>(dto);
            RepositoryResult result = _context.Create(article);
            if (!result.Succeeded) throw new Exception(string.Join(",", result.Errors));
            return article;
        }

        private void UploadObjectInGoogleStorage(GoogleStorageArticleFileDto model)
        {
            string content = _cacheService.Get<string>(model.CACHE_KEY);
            if (string.IsNullOrWhiteSpace(content))
            {
                content = System.IO.File.ReadAllText(model.HtmlFileTemplateFullPathWithExt);
                if (string.IsNullOrEmpty(content)) throw new Exception(nameof(content));
                content = _cacheService.GetOrAdd<string>(model.CACHE_KEY, () => content, model.CacheExpiryDateTimeForHtmlTemplate);
                if (string.IsNullOrEmpty(content)) throw new Exception(nameof(content));
            }
            content = _fileReadService.FillContent(content, model.AnonymousDataObjectForHtmlTemplate);
            if (string.IsNullOrEmpty(content)) throw new Exception(nameof(content));
            Stream stream = new MemoryStream(Encoding.UTF8.GetBytes(content));
            if (stream == null || stream.Length <= 0) throw new Exception(nameof(stream));
            _googleStorage.UploadObject(model.GoogleStorageBucketName, stream, model.GoogleStorageObjectNameWithExt, model.ContentType);
        }
    }

    public interface IArticleService
    {
        void StartArticleProcess(ArticleDto dto);
    }
}
