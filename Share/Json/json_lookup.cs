using System;
using System.Collections.Generic;
using System.Text;

namespace Share.Models.Json
{
    public class json_lookup
    {
        public Dictionary<int, string> conditionOptionsBy { get; set; }
        public Dictionary<int, string> sortOptionsBy { get; set; }
        public Dictionary<int, string> mileOptionsBy { get; set; }
        public Dictionary<int, string> categoryOptionsBy { get; set; }
    }
}
