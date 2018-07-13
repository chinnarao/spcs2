using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models.Article.Entities
{
    public class ArticleCommit
    {
        public long ArticleCommitId { get; set; }
        [Required]
        public long ArticleId { get; set; }
        public DateTime? CommittedDate { get; set; }
        [Required]
        public string UserIdOrEmail { get; set; }
        public string UserSocialAvatarUrl { get; set; }
        public bool? IsAdminCommited { get; set; }   // typo mistakes can fix by any one, courtesy
        public string Commit { get; set; }
        public Article Article { get; set; }
    }
}
