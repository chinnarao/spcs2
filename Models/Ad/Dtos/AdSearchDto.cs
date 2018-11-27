using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using LinqKitt;
using Models.Ad.Entities;

namespace Models.Ad.Dtos
{
    public class AdSearchDto
    {
        public string SearchText { get; set; }

        public string ConditionName { get; set; }  // map to enum "old or new"
        public string sortOptionsBy { get; set; } // map to enum 
        public string MileOptionsBy { get; set; } // map to enum 
        public string CategoryName { get; set; } // map to enum 

        public int PageNumber { get; set; }
        public int DefaultPageSize { get; set; } = 10;//how many records should display in the screen
        public int SearchResultCount { get; private set; }

        public string CountryCode { get; set; }  // ex: IN, US
        public string CityName { get; set; }
        public string ZipCode { get; set; }

        public string MapAddress { get; set; }
        public string MapLongitude { get; set; }
        public string MapLattitude { get; set; }

        public double MinPrice { get; set; }
        public double MaxPrice { get; set; }
        public string CurrencyCode { get; set; }  // ex: USD , INR, AUD

        public string StateWithComma { get; set; }

        public Expression<Func<Models.Ad.Entities.Ad, bool>> CreatePredicate()
        {
            int categoryId = (byte)Enum.Parse(typeof(CategoryOptionsBy), CategoryName, true);
            int conditionId = (byte)Enum.Parse(typeof(ConditionOptionsBy), ConditionName, true);
            // string categoryName = Enum.GetName(typeof(CategoryOptionsBy), categoryId);

            var predicate = PredicateBuilder.True<Models.Ad.Entities.Ad>();

            if (!string.IsNullOrEmpty(CategoryName))
            {
                predicate = predicate.And(exp => exp.AdCategoryId == categoryId);
            }

            if (!string.IsNullOrEmpty(ConditionName))
            {
                predicate = predicate.And(exp => exp.ItemConditionId == conditionId);
            }

            if (!string.IsNullOrEmpty(CountryCode))
            {
                predicate = predicate.And(exp => exp.AddressCountryCode == CountryCode);
            }

            if (!string.IsNullOrEmpty(CityName))
            {
                predicate = predicate.And(exp => exp.AddressCity.ToLower() == CityName.Trim().ToLower());
            }

            if (!string.IsNullOrEmpty(ZipCode))
            {
                predicate = predicate.And(exp => exp.AddressZipCode.ToLower() == ZipCode.Trim().ToLower());
            }

            if (MinPrice > 0.0)
            {
                predicate = predicate.And(exp => exp.ItemCost >= MinPrice);
            }

            if (MaxPrice > 0.0)
            {
                predicate = predicate.And(exp => exp.ItemCost <= MaxPrice);
            }

            if (!string.IsNullOrEmpty(CurrencyCode) && CurrencyCode.Length == 3)
            {
                predicate = predicate.And(exp => exp.ItemCurrencyCode.ToLower() == CurrencyCode.Trim().ToLower());
            }

            if (!string.IsNullOrEmpty(SearchText))
            {

                predicate = predicate.And(exp => exp.AdContent.ToLower() == CurrencyCode.Trim().ToLower());
            }

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
