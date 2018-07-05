using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Ad.AdController
{
    public class PostAdModel
    {
        public string Name { get; set; }
        public string Occupation { get; set; }

        public object ConvertToAnonymousType(PostAdModel input)
        {
            return new { Name = input.Name, Occupation = input.Occupation  };
        }
    }
}
