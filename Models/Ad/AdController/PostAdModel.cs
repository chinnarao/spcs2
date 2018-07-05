using Models.Ad.Dtos;

namespace Models.Ad.AdController
{
    public class PostAdModel
    {
        public string FileName { get; set; }
        public int InMemoryCachyExpireDays { get; set; }
        public string ObjectNameWithExt { get; set; }
        public string GoogleStorageBucketName { get; set; }
        public object AnonymousDataObject { get; set; }
        public string ContentType { get; set; }
        public string CACHE_KEY { get; set; }

        public AdDto AdDto { get; set; }
        public object ConvertToAnonymousType(PostAdModel input)
        {
            AdDto dto = input.AdDto;
            return new {
                ActiveDays = dto.ActiveDays,
                AdAddressAtPublicSecurityNearLandmarkName = dto.AdAddressAtPublicSecurityNearLandmarkName,
                AdAddressCity = dto.AdAddressCity,
                AdAddressDistrictOrCounty = dto.AdAddressDistrictOrCounty,
                AdAddressState = dto.AdAddressState,
                AdAddressStreet = dto.AdAddressStreet,
                AdBody = dto.AdBody,
                AdCountryCode = dto.AdCountryCode,
                AdCountryCurrency = dto.AdCountryCurrency,
                AdCountryCurrencyISO_4217 = dto.AdCountryCurrencyISO_4217,
                AdCountryName = dto.AdCountryName,
            };
        }
    }
}
