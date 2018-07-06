using System;
using System.IO;
using Google;
using File;
using AutoMapper;
using Models.Ad.Dtos;
using Models.Ad.Entities;
using DbContexts;
using Repository;
using Microsoft.Extensions.Logging;
using System.Text;

namespace Services
{
    public class AdService : IAdService
    {
        private readonly ILogger _logger;
        private readonly IFileRead _fileReadService;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;
        private readonly IGoogleStorage _googleStorage;
        private readonly Repository<Ad, AdDbContext> _context;

        public AdService(ILogger<AdService> logger, IMapper mapper, ICacheService cacheService, IFileRead fileReadService, IGoogleStorage googleStorage, AdDbContext context)
        {
            _logger = logger;
            _mapper = mapper;
            _fileReadService = fileReadService;
            _cacheService = cacheService;
            _googleStorage = googleStorage;
            _context = new Repository<Ad, AdDbContext>(context);
        }

        public void StartAdProcess(AdDto dto)
        {
            // transaction has to implement or not , has to think more required.
            Ad ad = this.InsertAd(dto);
            this.UploadObjectInGoogleStorage(dto.GoogleStorageAdFileDto);
        }

        private Ad InsertAd(AdDto dto)
        {
            Ad ad = _mapper.Map<Ad>(dto);
            RepositoryResult result = _context.Create(ad);
            if (!result.Succeeded) throw new Exception(string.Join(",", result.Errors));
            return ad;
        }

        private void UploadObjectInGoogleStorage(GoogleStorageAdFileDto model)
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

    public interface IAdService
    {
        void StartAdProcess(AdDto dto);
    }
}
