using FluentValidation;
using Share.Models.Ad.Dtos;
using System;
using System.Linq;

namespace Share.Validators
{
    public class AdSearchDtoValidator : AbstractValidator<AdSearchDto>
    {
        public AdSearchDtoValidator()
        {
            RuleFor(x => x.CountryCode).MaximumLength(2);
            RuleFor(x => x.CityName).MaximumLength(60);
            RuleFor(x => x.ZipCode).MaximumLength(16);
            RuleFor(x => x.CurrencyCode).MaximumLength(3);
            RuleFor(x => x.MinPrice).GreaterThanOrEqualTo(0).LessThanOrEqualTo( x => x.MaxPrice);
            RuleFor(x => x.MaxPrice).GreaterThanOrEqualTo(0).GreaterThanOrEqualTo(x => x.MinPrice);
        }
    }
}
