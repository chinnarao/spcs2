using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using LinqKitt;

namespace Share.Models.Ad.Dtos
{
    public class AdSearchDto
    {
        public string SearchText { get; set; }

        public byte ConditionId { get; set; }
        public byte SortOptionsId { get; set; } // pending work
        public byte MileOptionsId { get; set; } // pending work
        public byte CategoryId { get; set; }
        public string CountryCode { get; set; }
        public string CurrencyCode { get; set; }
        public string CityName { get; set; }
        public string ZipCode { get; set; }
        public double MinPrice { get; set; }
        public double MaxPrice { get; set; }
        public string MapAddress { get; set; }  // pending work
        public string MapLongitude { get; set; } // pending work
        public string MapLattitude { get; set; } // pending work

        public bool IsValidCategoryId { get; set; }
        public bool IsValidConditionId { get; set; }
        public bool IsValidMileOptionsId { get; set; }
        public bool IsValidSortOptionsId { get; set; }
        public bool IsValidCountryCode { get; set; }
        public bool IsValidCurrencyCode { get; set; }
        public bool IsValidMinPrice { get; set; }
        public bool IsValidMaxPrice { get; set; }
        public bool IsValidCityName { get; set; }
        public bool IsValidZipCode { get; set; }
        public bool IsValidSearchText { get; set; }

        public int PageNumber { get; set; }
        public int DefaultPageSize { get; set; } = 10;//how many records should display in the screen
        public int SearchResultCount { get; private set; }

        public string StateWithComma { get; set; }

        public Expression<Func<Models.Ad.Entities.Ad, bool>> CreatePredicate()
        {
            var predicate = PredicateBuilder.True<Models.Ad.Entities.Ad>();

            if (IsValidCategoryId)
                predicate = predicate.And(exp => exp.AdCategoryId == CategoryId);
            if (IsValidConditionId)
                predicate = predicate.And(exp => exp.ItemConditionId == ConditionId);
            if (IsValidCountryCode)
                predicate = predicate.And(exp => exp.AddressCountryCode == CountryCode);
            if (IsValidCurrencyCode)
                predicate = predicate.And(exp => exp.ItemCurrencyCode == CurrencyCode);
            if (IsValidCityName)
                predicate = predicate.And(exp => exp.AddressCity.ToLower() == CityName);
            if (IsValidZipCode)
                predicate = predicate.And(exp => exp.AddressZipCode.ToLower() == ZipCode);
            if (IsValidMinPrice)
                predicate = predicate.And(exp => exp.ItemCost >= MinPrice);
            if (IsValidMaxPrice)
                predicate = predicate.And(exp => exp.ItemCost <= MaxPrice);
            if (IsValidSearchText)
                predicate = predicate.And(exp => exp.AdContent.ToLower() == SearchText);

            //SortOptionsBy sort = (SortOptionsBy)Enum.Parse(typeof(SortOptionsBy), sortOptionsBy, true);
            //switch (sort)
            //{
            //    case SortOptionsBy.NewestFirst:
            //        break;
            //    case SortOptionsBy.Closest:
            //        break;
            //    case SortOptionsBy.PriceLowToHigh:
            //        break;
            //    case SortOptionsBy.PriceHighToLow:
            //        break;
            //    default:
            //        break;
            //}

            return predicate;
        }
    }
}
