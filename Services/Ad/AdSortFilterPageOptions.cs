using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Ad
{
    public class AdSortFilterPageOptions_________
    {
        #region MyRegion
        public string conditionName { get; set; }  // map to enum "old or new"
        public string SortOptionsBy { get; set; } // map to enum 
        public string MileOptionsBy { get; set; } // map to enum 
        public string CategoryName { get; set; } // map to enum 
        
        public int PageNumber { get; set; }
        public int DefaultPageSize { get; set; } = 10;//how many records should display in the screen
        public int SearchResultCount { get; private set; }
        #endregion

        public string CountryCode { get; set; }  // ex: IN, US
        public string CityName { get; set; }
        public string ZipCode { get; set; }

        public string MapAddress { get; set; }
        public string MapLongitude { get; set; }
        public string MapLattitude { get; set; }

        public int MinPrice { get; set; }
        public int MaxPrice { get; set; }
        public string currencyCode { get; set; }  // ex: USD , INR, AUD

        /// <summary>
        /// This holds the state of the key parts of the SortFilterPage parts 
        /// </summary>
        public string PrevCheckState { get; set; }

        public void SetupRestOfDto(int count)
        {
            SearchResultCount = (int)Math.Ceiling((double)(count) / DefaultPageSize);
            PageNumber = Math.Min(Math.Max(1, PageNumber), SearchResultCount);

            var newCheckState = GenerateCheckState();
            if (PrevCheckState != newCheckState)
                PageNumber = 1;

            PrevCheckState = newCheckState;
        }

        /// <summary>
        /// This returns a string containing the state of the SortFilterPage data
        /// that, if they change, should cause the PageNum to be set back to 0
        /// </summary>
        /// <returns></returns>
        private string GenerateCheckState()
        {
            //return $"{(int)FilterBy},{FilterValue},{DefaultPageSize},{TotalPagesBasedOnAvailableQueryDataRecords}";
            return $"{DefaultPageSize},{SearchResultCount}";
        }
    }
}
