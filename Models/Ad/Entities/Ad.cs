using System;

namespace Models.Ad.Entities
{
    public class Ad
    {
        public Int64 AdId { get; set; }

        public int ActiveDays { get; set; } //Max 30 days  // readonly // ? help
        public string AdAddressAtPublicSecurityNearLandmarkName { get; set; }
        public string AdAddressCity { get; set; }
        public string AdAddressDistrictOrCounty { get; set; }
        public string AdAddressState { get; set; }
        public string AdAddressStreet { get; set; }
        public string AdBody { get; set; }   // product details , including category
        public string AdCountryCode { get; set; }
        public string AdCountryCurrency { get; set; }
        public string AdCountryCurrencyISO_4217 { get; set; }
        public string AdCountryName { get; set; }
        public string AdDescription { get; set; }
        public double AdItemsCost { get; set; }   //enter with currency code ex: dollar or rupees
        public string AdItemsCostInCurrencyName { get; set; }   // enter with currency code ex: dollar or rupees
        public double AdLatitude { get; set; }
        public double AdLongitude { get; set; }
        public string AdTitle { get; set; }
        public string AdZipCode { get; set; }   //http://www.zipinfo.com/products/zcug/zcug.htm
        public DateTime ArchivedDateTime { get; set; }
        public int AttachedAssetsInCloudCount { get; set; }
        public Guid? AttachedAssetsInCloudStorageId { get; set; }
        public string AttachedAssetsStoredInCloudBaseFolderPath { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime DeletedDateTime { get; set; }
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

        public DateTime UpdatedDateTime { get; set; }
        public string UserEmail { get; set; }
        public string UserId { get; set; }
        public string UserLoggedInSocialProviderName { get; set; }
        public string UserName { get; set; }
        public string UserPhoneNumber { get; set; }
        public string UserSocialAvatarUrl { get; set; }
    }
}
