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
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Services
{
    public class ArticleService : IArticleService
    {
        private readonly ILogger _logger;
        private readonly IFileRead _fileReadService;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;
        private readonly IGoogleStorage _googleStorage;
        private readonly IRepository<Article, ArticleDbContext> _context;

        public ArticleService(ILogger<ArticleService> logger, IMapper mapper, ICacheService cacheService, IFileRead fileReadService, IGoogleStorage googleStorage, IRepository<Article, ArticleDbContext> context)
        {
            _logger = logger;
            _mapper = mapper;
            _fileReadService = fileReadService;
            _cacheService = cacheService;
            _googleStorage = googleStorage;
            _context = context;
        }
        
        #region GetAll Method
        public HashSet<ArticleDto> GetAll(int defaultArticlesHomeDisplay)
        {
            var articles = _context.Entities
                        .OrderByDescending( a => a.CreatedDateTime)
                        .OrderByDescending(a => a.UpdatedDateTime)
                        .Take(defaultArticlesHomeDisplay).ToHashSet<Article>();
            var articleDtos = _mapper.Map<HashSet<ArticleDto>>(articles);
            ConvertToArticleCommitDtos(articleDtos);
            return articleDtos;
        }
        private void ConvertToArticleCommitDtos(HashSet<ArticleDto> articleDtos)
        {
            if (articleDtos == null)    return;
            foreach (var articleDto in articleDtos)
            {
                if (string.IsNullOrWhiteSpace(articleDto.Commits)) continue;
                IEnumerable<ArticleCommitDto> commits = JsonConvert.DeserializeObject<IEnumerable<ArticleCommitDto>>(articleDto.Commits);
                if (commits != null)
                    articleDto.ArticleCommitDtos = commits.OrderByDescending(c => c.DT).ToList();
            }
        }
        #endregion

        #region StartArticleProcess
        public void StartArticleProcess(ArticleDto dto)
        {
            // transaction has to implement or not , has to think more required.
            this.InsertArticle(dto);
            this.UploadObjectInGoogleStorage(dto.GoogleStorageArticleFileDto);
        }
        private void InsertArticle(ArticleDto dto)
        {
            dto.Commits = JsonConvert.SerializeObject(dto.ArticleCommitDtos);
            Article article = _mapper.Map<Article>(dto);
            RepositoryResult result = _context.Create(article);
            if (!result.Succeeded) throw new Exception(string.Join(",", result.Errors));
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
        #endregion
    }

    public interface IArticleService
    {
        void StartArticleProcess(ArticleDto dto);
        HashSet<ArticleDto> GetAll(int defaultArticlesHomeDisplay);
    }
}
