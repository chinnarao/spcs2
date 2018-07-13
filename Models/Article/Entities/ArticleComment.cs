using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models.Article.Entities
{
    public class ArticleComment
    {
        public long ArticleCommentId { get; set; }
        [Required]
        public long ArticleId { get; set; }
        [Required]
        public string UserIdOrEmail { get; set; }
        public string UserSocialAvatarUrl { get; set; }
        public bool? IsAdminCommented { get; set; }
        [Required]
        public DateTime CommentedDate { get; set; }
        [Required]
        public string Comment { get; set; }
        public Article Article { get; set; }
    }
}
