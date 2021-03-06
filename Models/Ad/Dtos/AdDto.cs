﻿using GeoAPI.Geometries;
using System;
using System.ComponentModel.DataAnnotations;

namespace Models.Ad.Dtos
{
    public class AdDto
    {
        public string AdId { get; set; }  //http://www.talkingdotnet.com/use-hilo-to-generate-keys-with-entity-framework-core/
        public string AdTitle { get; set; }
        public string AdContent { get; set; }   // product details
        public byte AdCategoryId { get; set; }
        public byte AdDisplayDays { get; set; } //Max 30 days  // readonly // ? help

        public string UserIdOrEmail { get; set; }
        public long UserPhoneNumber { get; set; }
        public Int16 UserPhoneCountryCode { get; set; }
        public string UserSocialAvatarUrl { get; set; }
        public string UserSocialProviderName { get; set; } //ex: facebook, twitter

        public string AddressStreet { get; set; }
        public string AddressCity { get; set; }
        public string AddressDistrictOrCounty { get; set; }
        public string AddressState { get; set; }
        public string AddressPartiesMeetingLandmark { get; set; }
        public string AddressZipCode { get; set; }   //http://www.zipinfo.com/products/zcug/zcug.htm
        public string AddressCountryCode { get; set; }
        public string AddressCountryName { get; set; }
        public double AddressLatitude { get; set; }
        public double AddressLongitude { get; set; }
        //public IPoint AddressLocation {
        //    get {
        //        return Utility.CreatePoint(this.AddressLongitude, this.AddressLatitude);
        //    }
        //}


    //    [Required]
    //    [Range(-90.0, 90.0,
    //ErrorMessage = "Value for {0} must be between {1} and {2}.")]
    //    public double Latitude { get; set; }

    //    [Required]
    //    [Range(-180, 180,
    //    ErrorMessage = "Value for {0} must be between {1} and {2}.")]
    //    public double Longitude { get; set; }


        public double ItemCost { get; set; }   //enter with currency code ex: dollar or rupees
        public string ItemCurrencyCode { get; set; } //https://www.countries-ofthe-world.com/world-currencies.html
        public byte ItemConditionId { get; set; }     //old or new

        public byte AttachedAssetsInCloudCount { get; set; }
        public Guid? AttachedAssetsInCloudStorageId { get; set; }
        public string AttachedAssetsStoredInCloudBaseFolderPath { get; set; }

        public DateTime CreatedDateTime { get; set; }
        public DateTime UpdatedDateTime { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDateTime { get; set; }
        public bool IsPublished { get; set; }
        public DateTime? PublishedDateTime { get; set; }
        public bool IsActivated { get; set; }
        public DateTime? ActivatedDateTime { get; set; }

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

        public string UpdatedDateTimeString { get; set; }
        public GoogleStorageAdFileDto GoogleStorageAdFileDto { get; set; }
    }
}
