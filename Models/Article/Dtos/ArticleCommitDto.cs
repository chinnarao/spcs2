using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models.Article.Dtos
{
    public class ArticleCommitDto
    {
        public long ArticleCommitId { get; set; }
        public string Commit { get; set; }
        [Required]
        public DateTime CommittedDate { get; set; }
        [Required]
        public string UserIdOrEmail { get; set; }
        public string UserSocialAvatarUrl { get; set; }
        public bool? IsAdminCommited { get; set; }   // typo mistakes can fix by any one, courtesy
        [Required]
        public long ArticleId { get; set; }
    }
}
