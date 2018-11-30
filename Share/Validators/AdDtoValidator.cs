using FluentValidation;
using Share.Models.Ad.Dtos;
using System;
using System.Linq;
using Share.Enums;

namespace Share.Validators
{
    public class AdDtoValidator : AbstractValidator<AdDto>
    {
        public AdDtoValidator()
        {
            RuleFor(x => x.AdId).MaximumLength(19);
            RuleFor(x => x.AdTitle).NotEmpty().Length(2, 500);
            RuleFor(x => x.AdContent).NotEmpty();  //db update has to
            RuleFor(x => x.AdCategoryId).Must(IsValidCategory);
            RuleFor(x => x.AdDisplayDays).NotNull().GreaterThan<AdDto, Byte>(Byte.MinValue).LessThan<AdDto, Byte>(Byte.MaxValue);

            RuleFor(x => x.UserIdOrEmail).NotEmpty().MaximumLength(50);
            RuleFor(x => x.UserSocialAvatarUrl).MaximumLength(5000);  //db update has to
            RuleFor(x => x.UserSocialProviderName).MaximumLength(12);

            RuleFor(x => x.AddressStreet).MaximumLength(150);
            RuleFor(x => x.AddressCity).MaximumLength(60);
            RuleFor(x => x.AddressDistrictOrCounty).MaximumLength(30);
            RuleFor(x => x.AddressState).MaximumLength(30);
            RuleFor(x => x.AddressPartiesMeetingLandmark).MaximumLength(500);
            RuleFor(x => x.AddressZipCode).MaximumLength(16);
            RuleFor(x => x.AddressCountryCode).MaximumLength(2);
            RuleFor(x => x.AddressCountryName).MaximumLength(75);

            RuleFor(x => x.ItemCost).GreaterThanOrEqualTo(0);
            RuleFor(x => x.ItemCurrencyCode).MaximumLength(3);
            RuleFor(x => x.AttachedAssetsInCloudStorageId).NotEqual(Guid.Empty);
            RuleFor(x => x.AttachedAssetsStoredInCloudBaseFolderPath).MaximumLength(5000);  // db update has to

            RuleFor(x => x.Tag1).MaximumLength(20);
            RuleFor(x => x.Tag2).MaximumLength(20);
            RuleFor(x => x.Tag3).MaximumLength(20);
            RuleFor(x => x.Tag4).MaximumLength(20);
            RuleFor(x => x.Tag5).MaximumLength(20);
            RuleFor(x => x.Tag6).MaximumLength(20);
            RuleFor(x => x.Tag7).MaximumLength(20);
            RuleFor(x => x.Tag8).MaximumLength(20);
            RuleFor(x => x.Tag9).MaximumLength(20);
            RuleFor(x => x.Tag10).MaximumLength(20);
        }

        private bool IsValidCategory(byte categoryId)
        {
            byte max = Enum.GetValues(typeof(CategoryOptionsBy)).Cast<byte>().Max<byte>();
            byte min = Enum.GetValues(typeof(CategoryOptionsBy)).Cast<byte>().Min<byte>();
            return categoryId >= min && categoryId <= max;
        }
    }
}
