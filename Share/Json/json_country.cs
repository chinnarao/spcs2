using System;
using System.Collections.Generic;
using System.Text;

namespace Share.Models.Json
{
    public class json_country
    {
        public List<country> country { get; set; }
    }

    public class country
    {
        public string countryCode { get; set; }
        public string countryName { get; set; }
        public string currencyCode { get; set; }
        public string countryCallingCode { get; set; }
        public string lattitude { get; set; }
        public string longitude { get; set; }
        public string currencySymbol { get; set; }
    }
}
