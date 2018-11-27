using System;
using System.IO;
using Google;
using File;
using AutoMapper;
using Models.Ad.Dtos;
using DbContexts.Ad;
using Repository;
using Microsoft.Extensions.Logging;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Share.Extensions;

namespace Services.Ad
{
    public class AdService : IAdService
    {
        private readonly ILogger _logger;
        private readonly IFileRead _fileReadService;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;
        private readonly IGoogleStorage _googleStorage;
        private readonly IRepository<Models.Ad.Entities.Ad, AdDbContext> _adRepository;

        public AdService(ILogger<AdService> logger, IMapper mapper, ICacheService cacheService, IFileRead fileReadService, IGoogleStorage googleStorage, IRepository<Models.Ad.Entities.Ad, AdDbContext> adRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _fileReadService = fileReadService;
            _cacheService = cacheService;
            _googleStorage = googleStorage;
            _adRepository = adRepository;
        }

        #region CreateAd
        public AdDto CreateAd(AdDto dto)
        {
            // transaction has to implement or not , has to think more required.
            Models.Ad.Entities.Ad ad = this.InsertAd(dto);
            dto.GoogleStorageAdFileDto.AdAnonymousDataObjectForHtmlTemplate = GetAdAsAnonymousObjectForHtmlTemplate(dto);
            this.UploadObjectInGoogleStorage(dto.GoogleStorageAdFileDto);
            dto.GoogleStorageAdFileDto = null;
            return dto;
        }
        private Models.Ad.Entities.Ad InsertAd(AdDto dto)
        {
            Models.Ad.Entities.Ad ad = _mapper.Map<Models.Ad.Entities.Ad>(dto);
            RepositoryResult result = _adRepository.Create(ad);
            if (!result.Succeeded) throw new Exception(string.Join(",", result.Errors));
            return ad;
        }
        private void UploadObjectInGoogleStorage(GoogleStorageAdFileDto model)
        {
            if (model == null) throw new ArgumentNullException(nameof(GoogleStorageAdFileDto));
            if (model.AdAnonymousDataObjectForHtmlTemplate == null) throw new ArgumentNullException(nameof(model.AdAnonymousDataObjectForHtmlTemplate));
            string content = _cacheService.Get<string>(model.CACHE_KEY);
            if (string.IsNullOrWhiteSpace(content))
            {
                content = System.IO.File.ReadAllText(model.HtmlFileTemplateFullPathWithExt);
                if (string.IsNullOrEmpty(content)) throw new Exception(nameof(content));
                content = _cacheService.GetOrAdd<string>(model.CACHE_KEY, () => content, model.CacheExpiryDateTimeForHtmlTemplate);
                if (string.IsNullOrEmpty(content)) throw new Exception(nameof(content));
            }
            content = _fileReadService.FillContent(content, model.AdAnonymousDataObjectForHtmlTemplate);
            if (string.IsNullOrEmpty(content)) throw new Exception(nameof(content));
            Stream stream = new MemoryStream(Encoding.UTF8.GetBytes(content));
            if (stream == null || stream.Length <= 0) throw new Exception(nameof(stream));
            _googleStorage.UploadObject(model.GoogleStorageBucketName, stream, model.GoogleStorageObjectNameWithExt, model.ContentType);
        }

        private dynamic GetAdAsAnonymousObjectForHtmlTemplate(AdDto dto)
        {
            return new
            {
                activedays = dto.AdDisplayDays,
                adaddressatpublicsecuritynearlandmarkname = dto.AddressPartiesMeetingLandmark,
            };
        }

        #endregion

        public dynamic SearchAds(AdSearchDto options)
        {
            var adDtos = _adRepository.Entities.Where(w => w.IsPublished && w.IsActivated).AsNoTracking()
                            .Select(s => new AdDto()
                            {
                                AdId = s.AdId.ToString(),
                                AdTitle = s.AdTitle,
                                UpdatedDateTimeString = s.UpdatedDateTime.TimeAgo(),
                                UserIdOrEmail = s.UserIdOrEmail,
                            })
                            .OrderByDescending(a => a.CreatedDateTime)
                            .OrderByDescending(a => a.UpdatedDateTime)
                            .Take(options.DefaultPageSize).ToList();
            //options.SetupRestOfDto(adDtos.Count);

            //https://stackoverflow.com/questions/51028387/full-text-search-in-ef-core-2-1
            // https://sqlblogcasts.com/blogs/simons/archive/2008/12/18/LINQ-to-SQL---Enabling-Fulltext-searching.aspx
            //https://thinkrethink.net/2017/06/28/entity-framework-and-full-text-search/
            //https://www.mikesdotnetting.com/article/298/implementing-sql-server-full-text-search-in-an-asp-net-mvc-web-application-with-entity-framework
            //https://archive.codeplex.com/?p=effts
            //https://www.youtube.com/watch?v=0XQT6x0Ge08
            //https://github.com/fissoft/Fissoft.EntityFramework.Fts
            //http://jarrettmeyer.com/2017/06/11/sql-server-full-text-search-part-1
            //https://www.red-gate.com/simple-talk/sql/learn-sql-server/understanding-full-text-indexing-in-sql-server/
            //https://entityframeworkcore.com/knowledge-base/51047514/how-to-use-freetext-in-ef-core-2-1
            //https://github.com/ry8806/Blog-EFCore-CustomMigration/blob/master/EFCustomMigrations/Db/CustomMigrations/CustomerLocationMigration.cs
            var aa = _adRepository.Entities.Where(w => EF.Functions.FreeText(w.AdContent, "sffsfs")).ToList();


            return new { records = adDtos, options = options };
        }

        public AdDto GetAdDetail(long adId)
        {
            var ad = _adRepository.Entities.AsNoTracking().Single(i => i.AdId == adId);
            AdDto adDto = _mapper.Map<AdDto>(ad);
            return adDto;
        }

        public AdDto UpdateAd(AdDto adDto)
        {
            Models.Ad.Entities.Ad adExisting = _adRepository.Entities.Single(a => a.AdId == Convert.ToInt64(adDto.AdId));
            adExisting = _mapper.Map<AdDto, Models.Ad.Entities.Ad>(adDto, adExisting);
            int i = _adRepository.SaveChanges();
            AdDto adDtoNew = _mapper.Map<AdDto>(adExisting);
            return adDtoNew;
        }

        public HashSet<string> GetAllUniqueTags()
        {
            List<dynamic> list = _adRepository.Entities.Select(a => new { a.Tag1, a.Tag2, a.Tag3, a.Tag4, a.Tag5, a.Tag6, a.Tag7, a.Tag8, a.Tag9 }).ToList<dynamic>();
            HashSet<string> t = new HashSet<string>();
            foreach (var i in list)
            {
                t.Add(i.Tag1); t.Add(i.Tag2); t.Add(i.Tag3); t.Add(i.Tag4); t.Add(i.Tag5); t.Add(i.Tag6); t.Add(i.Tag7); t.Add(i.Tag8); t.Add(i.Tag9);
            }
            return t;
        }

        public List<AdDto> GetAllAds()
        {
            var ads = _adRepository.Entities.AsNoTracking();
            List<AdDto> adDtos = _mapper.Map<List<AdDto>>(ads);
            return adDtos;
        }
    }

    public interface IAdService
    {
        AdDto CreateAd(AdDto adDto);
        dynamic SearchAds(AdSearchDto options);
        AdDto GetAdDetail(long adId);
        AdDto UpdateAd(AdDto adDto);
        HashSet<string> GetAllUniqueTags();
        List<AdDto> GetAllAds();
    }
}
