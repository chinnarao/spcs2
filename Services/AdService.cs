using System;
using System.IO;
using System.Threading.Tasks;
using Google;
using File;
using AutoMapper;
using Models.Ad.Dtos;
using Models.Ad.Entities;
using Newtonsoft.Json;
using DbContexts;
using Repository;
using Models.Ad.AdController;
using Microsoft.Extensions.Logging;

namespace Services
{
    public class AdService : IAdService
    {
        private readonly ILogger _logger;
        private readonly IFileRead _fileReadService;
        private readonly IGoogleStorage _googleStorage;
        private readonly IMapper _mapper;
        private readonly Repository<Ad, AdDbContext> _context;

        public AdService(ILogger<AdService> logger, IMapper mapper, IFileRead fileReadService, IGoogleStorage googleStorage, AdDbContext context)
        {
            _logger = logger;
            _mapper = mapper;
            _fileReadService = fileReadService;
            _googleStorage = googleStorage;
            _context = new Repository<Ad, AdDbContext>(context);
        }

        public void StartAdProcess(PostAdModel postAdModel)
        {
            // transaction has to implement or not , has to think more required.
            Ad ad = this.InsertAd(postAdModel.AdDto);
            this.UploadObjectInGoogleStorage(postAdModel);
        }

        private Ad InsertAd(AdDto dto)
        {
            Ad ad = _mapper.Map<Ad>(dto);
            RepositoryResult result = _context.Create(ad);
            if (!result.Succeeded) throw new Exception(string.Join(",", result.Errors));
            return ad;
        }

        private void UploadObjectInGoogleStorage(PostAdModel model)
        {
            string content = _fileReadService.FileAsString(model.FileName, model.InMemoryCachyExpireDays, model.CACHE_KEY);
            if (string.IsNullOrEmpty(content)) throw new Exception(nameof(content));
            content = _fileReadService.FillContent(content, model.AnonymousDataObject);
            if (string.IsNullOrEmpty(content)) throw new Exception(nameof(content));
            Stream stream = _fileReadService.FileAsStream(content);
            if (stream == null || stream.Length <= 0) throw new Exception(nameof(stream));
            _googleStorage.UploadObject(model.GoogleStorageBucketName, stream, model.ObjectNameWithExt, model.ContentType);
        }
    }

    public interface IAdService
    {
        void StartAdProcess(PostAdModel postAdModel);
    }
}
