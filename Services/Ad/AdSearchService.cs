using System;
using System.IO;
using AutoMapper;
using Share.Models.Ad.Dtos;
using DbContexts.Ad;
using Repository;
using Microsoft.Extensions.Logging;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Share.Extensions;
using Services.Commmon;
using Services.Google;
using Share.Utilities;
using Microsoft.Extensions.Configuration;
using Services.Common;
using Share.Enums;

namespace Services.Ad
{
    public class AdSearchService : IAdSearchService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;
        private readonly IRepository<Share.Models.Ad.Entities.Ad, AdDbContext> _adRepository;
        private readonly IJsonDataService _jsonDataService;

        public AdSearchService(ILogger<AdService> logger, IMapper mapper, ICacheService cacheService, 
            IRepository<Share.Models.Ad.Entities.Ad, AdDbContext> adRepository,
            IConfiguration configuration, IJsonDataService jsonDataService)
        {
            _configuration = configuration;
            _logger = logger;
            _mapper = mapper;
            _cacheService = cacheService;
            _adRepository = adRepository;
            _jsonDataService = jsonDataService;
        }

        //https://docs.microsoft.com/en-us/aspnet/core/data/ef-rp/sort-filter-page?view=aspnetcore-2.1
        //https://docs.microsoft.com/en-us/aspnet/core/data/ef-rp/sort-filter-page?view=aspnetcore-2.1
        //https://docs.microsoft.com/en-us/sql/relational-databases/search/query-with-full-text-search?view=sql-server-2017
        //https://github.com/uber-asido/backend/blob/e32bf1ddabe500002d835228993707503449e06c/src/Uber.Module.Search.EFCore/Store/SearchItemStore.cs
        //https://github.com/PomeloFoundation/Pomelo.EntityFrameworkCore.MySql/blob/42c335ceac6d93d1c0487ef45fc992810c07fd9d/upstream/EFCore.Upstream.FunctionalTests/Query/DbFunctionsMySqlTest.cs
        public dynamic SearchAds(AdSearchDto options)
        {
            IQueryable<Share.Models.Ad.Entities.Ad> query = _adRepository.Entities.Where(w => w.IsPublished && w.IsActivated && !w.IsDeleted).AsNoTracking();

            if (options.IsValidSortOption)
            {
                switch ((SortOptionsBy)options.SortOptionsId)
                {
                    case SortOptionsBy.ClosestFirst:
                        // handled below in same function , ref : line #73
                        break;
                    case SortOptionsBy.NewestFirst:
                        query.OrderByDescending(o => o.UpdatedDateTime);
                        break;
                    case SortOptionsBy.PriceHighToLow:
                        query.OrderByDescending(o => o.ItemCost);
                        break;
                    case SortOptionsBy.PriceLowToHigh:
                        query.OrderBy(o => o.ItemCost);
                        break;
                    default:
                        break;
                }
            }

            if ((SortOptionsBy)options.SortOptionsId == SortOptionsBy.ClosestFirst)
            {
                if (options.IsValidMileOption)
                {
                    if ((MileOptionsBy)options.SortOptionsId == MileOptionsBy.Maximum)
                        query.OrderBy(o => o.AddressLocation.Distance(options.MapLocation));
                    else
                        query.OrderBy(o => o.AddressLocation.Distance(options.MapLocation) < options.Miles);
                }
                else
                    query.OrderBy(o => o.AddressLocation.Distance(options.MapLocation));
            }
            else if (options.IsValidMileOption)
            {
                if ((MileOptionsBy)options.SortOptionsId == MileOptionsBy.Maximum)
                    query.OrderBy(o => o.AddressLocation.Distance(options.MapLocation));
                else
                    query.OrderBy(o => o.AddressLocation.Distance(options.MapLocation) < options.Miles);
            }

            // implemented and chosen FreeText from 4 options: 1.FreeText 2.Contains 3.ContainsTable 4.FreeTextTable
            // figure out later: SqlServerDbFunctionsExtensions
            if (options.IsValidSearchText)
            {
                query.Where(ft => EF.Functions.FreeText(ft.AdContent, options.SearchText));
                //query.Where(ft => EF.Functions.FreeText(ft.AdTitle, options.SearchText));
            }

            // select columns:
            query.Select(s => new AdDto()
            {
                AdId = s.AdId.ToString(),
                AdTitle = s.AdTitle,
                UpdatedDateTimeString = s.UpdatedDateTime.TimeAgo(),
                UserIdOrEmail = s.UserIdOrEmail,
            });

            //paging
            query.Take(options.DefaultPageSize);

            return new { records = query.ToList(), options = options };
        }
    }

    public interface IAdSearchService
    {
        dynamic SearchAds(AdSearchDto options);
    }
}
