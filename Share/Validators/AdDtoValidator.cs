using FluentValidation;
using Share.Models.Ad.Dtos;
using System;
using System.Linq;
using Share.Enums;
using System.Text.RegularExpressions;
using FluentValidation.Results;

namespace Share.Validators
{
    public class AdDtoValidator : AbstractValidator<AdDto>
    {
        public AdDtoValidator()
        {
            RuleFor(x => x.AdId).MaximumLength(19);
            RuleFor(x => x.AdTitle).NotEmpty().Length(2, 500);
            RuleFor(x => x.AdContent).NotEmpty().MaximumLength(8000);
            RuleFor(x => x.AdCategoryId).Must(IsValidCategory);
            RuleFor(x => x.AdDisplayDays).NotNull().GreaterThan<AdDto, Byte>(Byte.MinValue).LessThan<AdDto, Byte>(Byte.MaxValue);

            RuleFor(x => x.UserIdOrEmail).NotEmpty().MaximumLength(50);
            //http://marcin-chwedczuk.github.io/fluent-validation-and-complex-dependencies-between-properties
            RuleFor(x => x).Custom((dto, context) => {
                if (string.IsNullOrEmpty(dto?.UserPhoneCountryCode))
                    return;
                int phoneNumber;
                bool ret = int.TryParse(dto.UserPhoneCountryCode, out phoneNumber);
                if (!ret)
                    return;
                bool isValid = Utilities.Utility.IsValidCountryCallingCode(phoneNumber);
                if (isValid)
                    return;
                if (!isValid)
                    context.AddFailure(new ValidationFailure($"UserPhoneCountryCode", $"Phone Country Code is not a valid: ['{dto.UserPhoneCountryCode}']"));
            });
            RuleFor(x => x.UserSocialAvatarUrl).MaximumLength(5000).Must(IsValidURL);
            RuleFor(x => x.UserSocialProviderName).MaximumLength(12);

            RuleFor(x => x.AddressStreet).MaximumLength(150);
            RuleFor(x => x.AddressCity).MaximumLength(60);
            RuleFor(x => x.AddressDistrictOrCounty).MaximumLength(30);
            RuleFor(x => x.AddressState).MaximumLength(30);
            RuleFor(x => x.AddressPartiesMeetingLandmark).MaximumLength(500);
            RuleFor(x => x.AddressZipCode).MaximumLength(16);
            RuleFor(x => x.AddressCountryCode).MaximumLength(2);
            RuleFor(x => x.AddressCountryName).MaximumLength(75);
            RuleFor(x => x.AddressLatitude).Must(IsValidLatitude);
            RuleFor(x => x.AddressLongitude).Must(IsValidLongitude);

            RuleFor(x => x.ItemCost).GreaterThanOrEqualTo(0);
            RuleFor(x => x.ItemCurrencyCode).MaximumLength(3);
            RuleFor(x => x.AttachedAssetsInCloudStorageId).NotEqual(Guid.Empty);
            RuleFor(x => x.AttachedAssetsStoredInCloudBaseFolderPath).MaximumLength(5000).Must(IsValidURL);

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

        //http://urlregex.com/
        private bool IsValidURL(string URL)
        {
            if (String.IsNullOrWhiteSpace(URL))
                return true;
            Regex regex = new Regex(Utilities.RegexUtility.GetUrlRegex());
            Match match = regex.Match(URL);
            return match.Success;
        }

        private bool IsValid10DigitPhoneNumber(string phone)
        {
            if (String.IsNullOrWhiteSpace(phone))
                return true;
            return phone.Length == 10;
        }

        //-180.0 to 180.0.
        private bool IsValidLongitude(string longitude)
        {
            if (String.IsNullOrWhiteSpace(longitude))
                return true;
            double l;
            if (double.TryParse(longitude, out l))
            {
                if (l < -180 || l > 180)
                    return false;
                return true;
            }
            else
                return false;
        }

        //-90.0 to 90.0.
        private bool IsValidLatitude(string latitude)
        {
            if (String.IsNullOrWhiteSpace(latitude))
                return true;
            double l;
            if (double.TryParse(latitude, out l))
            {
                if (l < -90 || l > 90)
                    return false;
                return true;
            }
            else
                return false;
        }
    }
}
