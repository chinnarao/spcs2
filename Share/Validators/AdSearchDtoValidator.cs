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
            RuleFor(x => x.ConditionName).NotEmpty().MaximumLength(10);
            RuleFor(x => x.sortOptionsBy).NotEmpty().MaximumLength(15);
            RuleFor(x => x.MileOptionsBy).NotEmpty().MaximumLength(11);
            RuleFor(x => x.CategoryName).NotEmpty().MaximumLength(40);
            RuleFor(x => x.CountryCode).MaximumLength(2);
            RuleFor(x => x.CityName).MaximumLength(100);
            RuleFor(x => x.ZipCode).MaximumLength(16);
            RuleFor(x => x.CurrencyCode).MaximumLength(3);
        }
    }
}
