using System;
using System.ComponentModel.DataAnnotations;

namespace Models.Ad.Dtos
{
    public class AdDto
    {
        public Int64 AdId { get; set; }
        public int ActiveDays { get; set; } 
        public string AdAddressAtPublicSecurityNearLandmarkName { get; set; }
        public string AdAddressCity { get; set; }
        public string AdAddressDistrictOrCounty { get; set; }
        public string AdAddressState { get; set; }
        public string AdAddressStreet { get; set; }
        public string AdBody { get; set; }
        [MaxLength(2)]
        public string AdCountryCode { get; set; }
        [MaxLength(3)]
        public string AdCountryCurrencyISO_4217 { get; set; }
        public string AdCountryName { get; set; }
        public string AdDescription { get; set; }
        public double? AdItemsCost { get; set; }   //enter with currency code ex: dollar or rupees
        public string AdItemsCostInCurrencyName { get; set; }   // enter with currency code ex: dollar or rupees  //https://www.countries-ofthe-world.com/world-currencies.html
        [Required]
        public double AdLatitude { get; set; }
        [Required]
        public double AdLongitude { get; set; }
        [Required]
        public string AdTitle { get; set; }
        [Required]
        public string AdZipCode { get; set; }   //http://www.zipinfo.com/products/zcug/zcug.htm
        public DateTime? ArchivedDateTime { get; set; }
        public int? AttachedAssetsInCloudCount { get; set; }
        [Required]
        public Guid AttachedAssetsInCloudStorageId { get; set; }
        public string AttachedAssetsStoredInCloudBaseFolderPath { get; set; }
        [Required]
        public DateTime CreatedDateTime { get; set; }
        public DateTime? DeletedDateTime { get; set; }
        public bool IsActive { get; set; }
        public bool IsAdInDraftMode { get; set; }
        public bool IsArchived { get; set; }
        public bool IsDeleted { get; set; }
        public string Tag1 { get; set; }
        public string Tag2 { get; set; }
        public string Tag3 { get; set; }
        public string Tag4 { get; set; }
        public string Tag5 { get; set; }
        public string Tag6 { get; set; }
        public string Tag7 { get; set; }
        public string Tag8 { get; set; }
        public string Tag9 { get; set; }
        public string Tag10 { get; set; }
        public string Tag11 { get; set; }
        public string Tag12 { get; set; }
        [Required]
        public DateTime UpdatedDateTime { get; set; }
        public string UserEmail { get; set; }
        public string UserId { get; set; }
        public string UserLoggedInSocialProviderName { get; set; }
        public string UserName { get; set; }
        public string UserPhoneNumber { get; set; }
        public string UserSocialAvatarUrl { get; set; }

        public GoogleStorageAdFileDto GoogleStorageAdFileDto { get; set; }
        public dynamic AdDtoAsAnonymous
        {
            get
            {
                return new
                {
                    activedays = this.ActiveDays,
                    adaddressatpublicsecuritynearlandmarkname = this.AdAddressAtPublicSecurityNearLandmarkName,
                    adaddresscity = this.AdAddressCity,
                    adaddressdistrictorcounty = this.AdAddressDistrictOrCounty,
                    adaddressstate = this.AdAddressState,
                    adaddressstreet = this.AdAddressStreet,
                    adbody = this.AdBody,
                    adcountrycode = this.AdCountryCode,
                    adcountrycurrencyiso_4217 = this.AdCountryCurrencyISO_4217,
                    adcountryname = this.AdCountryName,
                };
            }
        }
    }
}
