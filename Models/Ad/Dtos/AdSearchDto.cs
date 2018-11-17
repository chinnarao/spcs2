using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Ad.Dtos
{
    public class AdSearchDto
    {
        public string ConditionName { get; set; }  // map to enum "old or new"
        public string SortOptionsBy { get; set; } // map to enum 
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

        public int MinPrice { get; set; }
        public int MaxPrice { get; set; }
        public string CurrencyCode { get; set; }  // ex: USD , INR, AUD

        public string StateWithComma { get; set; }
    }
}
