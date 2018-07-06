using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Article.Dtos
{
    public class ArticleCommitDto
    {
        public string Comment { get; set; }
        public DateTime CommentedDateTime { get; set; }
    }
}
