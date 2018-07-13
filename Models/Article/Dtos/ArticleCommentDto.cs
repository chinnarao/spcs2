using System;
using System.ComponentModel.DataAnnotations;

namespace Models.Article.Dtos
{
    public class ArticleCommentDto
    {
        public long ArticleCommentId { get; set; }
        [Required]
        public long ArticleId { get; set; }
        [Required]
        public string Comment { get; set; }
        [Required]
        public string UserIdOrEmail { get; set; }
        public string UserSocialAvatarUrl { get; set; }
        public bool? IsAdminCommented { get; set; }
        [Required]
        public DateTime CommentedDate { get; set; }
    }
}
