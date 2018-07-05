using System;
using System.IO;
using System.Threading.Tasks;
using Google;
using File;
using AutoMapper;
using Models.Ad.Dtos;
using Models.Ad.Entities;
using Newtonsoft.Json;
using Models.Article.Entities;
using Models.Article.Pocos;
using Repository;
using DbContexts;

namespace Services
{
    public class ArticleService : IArticleService
    {
        private readonly IFileRead _fileReadService;
        private readonly IGoogleStorage _googleStorage;
        private readonly IMapper _mapper;
        private readonly Repository<Article, ArticleDbContext> _context;

        public ArticleService(IMapper mapper, IFileRead fileReadService, IGoogleStorage googleStorage, ArticleDbContext context)
        {
            _mapper = mapper;
            _fileReadService = fileReadService;
            _googleStorage = googleStorage;
            _context = new Repository<Article, ArticleDbContext>(context);
        }

        public Article Get(long articleId)
        {

            Commit c = new Commit();
            string jsonData = JsonConvert.SerializeObject(c);
            c = JsonConvert.DeserializeObject<Commit>(jsonData);
            throw new NotImplementedException();
        }
    }

    public interface IArticleService
    {
        Article Get(long articleId);
    }
}
