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
using Microsoft.EntityFrameworkCore;
using Share.Extensions;

namespace Services.Article
{
    public class ArticleService : IArticleService
    {
        private readonly ILogger _logger;
        private readonly IFileRead _fileReadService;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;
        private readonly IGoogleStorage _googleStorage;
        private readonly IRepository<Models.Article.Entities.Article, ArticleDbContext> _articleRepository;
        private readonly IRepository<ArticleCommit, ArticleDbContext> _articleCommitRepository;
        private readonly IRepository<ArticleComment, ArticleDbContext> _articleCommentRepository;
        private readonly IRepository<ArticleLicense, ArticleDbContext> _articleLicenseRepository;

        public ArticleService(ILogger<ArticleService> logger, IMapper mapper, ICacheService cacheService, IFileRead fileReadService, IGoogleStorage googleStorage, 
                                IRepository<Models.Article.Entities.Article, ArticleDbContext> articleRepository,
                                IRepository<ArticleCommit, ArticleDbContext> articleCommitRepository,
                                IRepository<ArticleComment, ArticleDbContext> articleCommentRepository,
                                IRepository<ArticleLicense, ArticleDbContext> articleLicenseRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _fileReadService = fileReadService;
            _cacheService = cacheService;
            _googleStorage = googleStorage;
            _articleRepository = articleRepository;
            _articleCommitRepository = articleCommitRepository;
            _articleCommentRepository = articleCommentRepository;
            _articleLicenseRepository = articleLicenseRepository;
        }
        
        #region GetAll Method
        public dynamic GetAll(int defaultArticlesHomeDisplay, ArticleSortFilterPageOptions options)
        {
            var articleDtos = _articleRepository.Entities.AsNoTracking()
                            .Select( s => new ArticleDto() { ArticleId = s.ArticleId,
                                Title = s.Title,
                                UpdatedDateTimeString = s.UpdatedDateTime.TimeAgo(),
                                UserIdOrEmail = s.UserIdOrEmail,
                                AllRelatedSubjectsIncludesVersionsWithComma = s.AllRelatedSubjectsIncludesVersionsWithComma })
                            .OrderByDescending( a => a.CreatedDateTime)
                            .OrderByDescending(a => a.UpdatedDateTime)
                            .Take(defaultArticlesHomeDisplay).ToList();
            options.SetupRestOfDto(articleDtos.Count);
            return new { articles = articleDtos, option = options };
        }
        private void ConvertToArticleCommitDtos(HashSet<ArticleDto> articleDtos)
        {
            if (articleDtos == null)    return;
            foreach (var articleDto in articleDtos)
            {
                //if (string.IsNullOrWhiteSpace(articleDto.Commits)) continue;
                //IEnumerable<ArticleCommitDto> commits = JsonConvert.DeserializeObject<IEnumerable<ArticleCommitDto>>(articleDto.Commits);
                //if (commits != null)
                //    articleDto.ArticleCommitDtos = commits.OrderByDescending(c => c.Date).ToList();
            }
        }
        #endregion

        #region CreateArticle
        public ArticleDto CreateArticle(ArticleDto dto)
        {
            // transaction has to implement or not , has to think more required.
            ArticleDto articleDto = this.InsertArticle(dto);
            this.UploadObjectInGoogleStorage(dto.GoogleStorageArticleFileDto);
            return articleDto;
        }
        private ArticleDto InsertArticle(ArticleDto dto)
        {
            InitArticleLicense(dto);
            
            Models.Article.Entities.Article article = _mapper.Map<Models.Article.Entities.Article>(dto);
            RepositoryResult result = _articleRepository.Create(article);
            if (!result.Succeeded) throw new Exception(string.Join(",", result.Errors));
            return dto;
        }
        private void InitArticleLicense(ArticleDto dto)
        {
            ArticleLicenseDto articleLicenseDto = new ArticleLicenseDto() { ArticleId = dto.ArticleId, ArticleLicenseId = DateTime.UtcNow.Ticks, LicensedDate = DateTime.UtcNow };
            if (dto.ArticleLicenseDto != null && !string.IsNullOrWhiteSpace(dto.ArticleLicenseDto.License))
                articleLicenseDto.License = dto.ArticleLicenseDto.License;
            dto.ArticleLicenseDto = articleLicenseDto;
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

        public dynamic GetArticleLicenseCommentsCommits(long articleId)
        {

            var anonymous = _articleRepository.Entities.Where( a => a.ArticleId == articleId)
                            .Select( s => new { License = s.ArticleLicense.License,
                                    ArticleCommits = s.ArticleCommits.OrderByDescending(x => x.CommittedDate).ToList(),
                                    ArticleComments = s.ArticleComments.OrderByDescending(x => x.CommentedDate)
                                  });
            return anonymous;
        }

        public ArticleDto Detail(long articleId)
        {
            var article = _articleRepository.Entities.Include(i => i.ArticleLicense).Include(i =>i.ArticleCommits).Include(i => i.ArticleComments).AsNoTracking().First(i => i.ArticleId == articleId);
            ArticleDto articleDto = _mapper.Map<ArticleDto>(article);
            return articleDto;
        }

        public ArticleCommentDto CreateComment(ArticleCommentDto articleCommentDto)
        {
            ArticleComment articleComment = _mapper.Map<ArticleComment>(articleCommentDto);
            _articleCommentRepository.Create(articleComment);
            return articleCommentDto;
        }

        public ArticleDto UpdateArticleWithCommitHistory(ArticleDto articleDto)
        {
            Models.Article.Entities.Article articleExisting = _articleRepository.Entities.Include(a => a.ArticleLicense).Single(a => a.ArticleId == articleDto.ArticleId);
            if (!string.Equals(articleExisting?.ArticleLicense?.License, articleDto?.ArticleLicenseDto?.License))
                articleDto.ArticleLicenseDto.LicensedDate = DateTime.UtcNow;
            articleExisting = _mapper.Map<ArticleDto, Models.Article.Entities.Article>(articleDto, articleExisting);
            int i = _articleRepository.SaveChanges();
            ArticleDto articleDtoNew = _mapper.Map<ArticleDto>(articleExisting);
            return articleDtoNew;
        }
    }

    public interface IArticleService
    {
        ArticleDto CreateArticle(ArticleDto dto);
        dynamic GetAll(int defaultArticlesHomeDisplay, ArticleSortFilterPageOptions options);
        dynamic GetArticleLicenseCommentsCommits(long articleId);
        ArticleDto Detail(long articleId);
        ArticleCommentDto CreateComment(ArticleCommentDto articleCommentDto);
        ArticleDto UpdateArticleWithCommitHistory(ArticleDto articleDto);
    }
}
