using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Models.Ad.Dtos;
using Services.Ad;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Ad.Util;

namespace Ad.Controllers
{
    //https://github.com/aspnet/Docs/blob/master/aspnetcore/fundamentals/logging/index/sample2/Controllers/TodoController.cs
    [Route("api/[controller]/[action]")]
    public class AdController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;
        private readonly IAdService _adService;

        public AdController(IConfiguration configuration, ILogger<AdController> logger, IAdService adService)
        {
            _configuration = configuration;
            _logger = logger;
            _adService = adService;
        }

        [HttpPost]
        public IActionResult CreateAd([FromBody]AdDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Errors());
            model.Defaults(_configuration);
            model.GoogleStorageAdFileDto = new GoogleStorageAdFileDto();
            model.GoogleStorageAdFileDto.Values(_configuration, model.AttachedAssetsInCloudStorageId.Value);
            AdDto dto = _adService.CreateAd(model);
            return Ok(dto);
        }
        
        [HttpPost]
        public IActionResult SearchAds([FromBody] AdSortFilterPageOptions options)
        {
            int defaultPageSize = Convert.ToInt32(_configuration["DefaultAdsHomeDisplay"]);
            if (defaultPageSize <= 0) throw new ArgumentOutOfRangeException(nameof(defaultPageSize));

            options.DefaultPageSize = defaultPageSize;
            options.PageNumber = 1;

            var anonymous = _adService.SearchAds(options);
            return Ok(anonymous);
        }

        [HttpGet("{adId}")]
        public IActionResult GetAdDetail(long adId)
        {
            if (adId <= 0) throw new ArgumentOutOfRangeException(nameof(adId));
            AdDto dto = _adService.GetAdDetail(adId);
            return Ok(dto);
        }

        [HttpPost]
        public IActionResult UpdateAd([FromBody] AdDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            AdDto adDto = _adService.UpdateAd(model);
            return Ok(adDto);
        }

        [HttpGet]
        public IActionResult GetAllUniqueTags()
        {
            HashSet<string> set = _adService.GetAllUniqueTags();
            return Ok(set);
        }

        [HttpGet]
        public IActionResult GetAllAds()
        {
            List<AdDto> dtos = _adService.GetAllAds();
            return Ok(dtos);
        }
    }
}