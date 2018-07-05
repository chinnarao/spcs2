using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Article.Pocos
{
    /// <summary>
    /// this class used to convert a json string in article entity
    /// </summary>
    public class Commit
    {
        public DateTime CommittedDate { get; set; }
        public DateTime CommittedComment { get; set; }
    }
}
