using FluentValidation;
using Models.Ad.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ad.Util
{
    public class AdDtoValidator : AbstractValidator<AdDto>
    {
        public AdDtoValidator()
        {
            RuleFor(x => x.AdId).MaximumLength(19);
            RuleFor(x => x.AdTitle).NotEmpty().MaximumLength(500);
            RuleFor(x => x.AdContent).NotEmpty();  //db update has to
            //RuleFor(x => x.AdDisplayDays).NotEmpty().GreaterThanOrEqualTo(7).LessThanOrEqualTo(30); // value shoud come from config

            RuleFor(x => x.UserIdOrEmail).NotEmpty().MaximumLength(100);
            RuleFor(x => x.UserPhoneNumber).MaximumLength(15);
            RuleFor(x => x.UserSocialAvatarUrl).MaximumLength(5000);  //db update has to
            RuleFor(x => x.UserLoggedInSocialProviderName).MaximumLength(32);

            RuleFor(x => x.AddressStreet).MaximumLength(100);
            RuleFor(x => x.AddressCity).MaximumLength(100);
            RuleFor(x => x.AddressDistrictOrCounty).MaximumLength(100);
            RuleFor(x => x.AddressState).MaximumLength(100);
            RuleFor(x => x.AddressPartiesMeetingLandmarkName).MaximumLength(100);
            RuleFor(x => x.AddressZipCode).MaximumLength(16);
            RuleFor(x => x.AddressCountryCode).MaximumLength(2);
            RuleFor(x => x.AddressCountryName).MaximumLength(100);
            //RuleFor(x => x.AddressLatitude).NotEmpty();
            //RuleFor(x => x.AddressLongitude).NotEmpty();
            RuleFor(x => x.ItemCost).GreaterThanOrEqualTo(0);
            RuleFor(x => x.ItemCostCurrencyName).MaximumLength(50);
            RuleFor(x => x.ItemCurrencyISO_4217).MaximumLength(3);
            RuleFor(x => x.AttachedAssetsInCloudCount).LessThanOrEqualTo(20);
            RuleFor(x => x.AttachedAssetsInCloudStorageId).NotEqual(Guid.Empty);
            RuleFor(x => x.AttachedAssetsStoredInCloudBaseFolderPath).MaximumLength(5000);  // db update has to

            //RuleFor(x => x.CreatedDateTime).GreaterThanOrEqualTo(new DateTime(2018, 10, 31)); // value shoud come from config
            //RuleFor(x => x.UpdatedDateTime);
            //RuleFor(x => x.IsDeleted).NotEmpty();
            //RuleFor(x => x.DeletedDateTime).GreaterThanOrEqualTo(new DateTime(2018, 10, 31));
            //RuleFor(x => x.IsPublished).NotEmpty();
            //RuleFor(x => x.LastDraftOrBeforePublishedDateTime).GreaterThanOrEqualTo(new DateTime(2018, 10, 31));
            //RuleFor(x => x.LastPublishedDateTime).GreaterThanOrEqualTo(new DateTime(2018, 10, 31));
            //RuleFor(x => x.IsActive).NotEmpty();
            //RuleFor(x => x.LastActiveDateTime).GreaterThanOrEqualTo(new DateTime(2018, 10, 31));
            //RuleFor(x => x.LastInActiveDateTime).GreaterThanOrEqualTo(new DateTime(2018, 10, 31));

            RuleFor(x => x.Tag1).MaximumLength(32);
            RuleFor(x => x.Tag2).MaximumLength(32);
            RuleFor(x => x.Tag3).MaximumLength(32);
            RuleFor(x => x.Tag4).MaximumLength(32);
            RuleFor(x => x.Tag5).MaximumLength(32);
            RuleFor(x => x.Tag6).MaximumLength(32);
            RuleFor(x => x.Tag7).MaximumLength(32);
            RuleFor(x => x.Tag8).MaximumLength(32);
            RuleFor(x => x.Tag9).MaximumLength(32);
        }
    }
}