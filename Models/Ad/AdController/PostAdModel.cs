using Models.Ad.Dtos;
using System;

namespace Models.Ad.AdController
{
    public class PostAdModel
    {
        public string FileName { get; set; }
        public DateTime CacheExpiryDateTime { get; set; }
        public string ObjectNameWithExt { get; set; }
        public string GoogleStorageBucketName { get; set; }
        public object AnonymousDataObject { get; set; }
        public string ContentType { get; set; }
        public string CACHE_KEY { get; set; }
        public string HtmlFileTemplateFullPathWithExt { get; set; }

        public AdDto AdDto { get; set; }
        public object AdDtoAsAnonymous
        {
            get
            {
                return new
                {
                    ActiveDays = AdDto.ActiveDays,
                    AdAddressAtPublicSecurityNearLandmarkName = AdDto.AdAddressAtPublicSecurityNearLandmarkName,
                    AdAddressCity = AdDto.AdAddressCity,
                    AdAddressDistrictOrCounty = AdDto.AdAddressDistrictOrCounty,
                    AdAddressState = AdDto.AdAddressState,
                    AdAddressStreet = AdDto.AdAddressStreet,
                    AdBody = AdDto.AdBody,
                    AdCountryCode = AdDto.AdCountryCode,
                    AdCountryCurrencyISO_4217 = AdDto.AdCountryCurrencyISO_4217,
                    AdCountryName = AdDto.AdCountryName,
                };
            }
        }
    }
}
