using System;

namespace Models.Article.Entities
{
    public class Article
    {
        public long ArticleId { get; set; }
        public int ActiveDays { get; set; } //Max 30 days  // readonly // ? help
        public string Title { get; set; }
        public string Description { get; set; }
        public string Body { get; set; }     // navachar, article content , which is a big information
        public string License { get; set; }     // every article owner may have special license, which should be big text
        public string Commits { get; set; }     // json format : [{ "comment": "this is comment","date": "just date part"},{"comment": "this is comment","date": "just date part"}]

        public DateTime CreatedDateTime { get; set; }
        public DateTime UpdatedDateTime { get; set; }
        public string UserEmail { get; set; }
        public string UserId { get; set; }
        public string UserLoggedInSocialProviderName { get; set; }
        public string UserName { get; set; }
        public string UserPhoneNumber { get; set; }
        public string UserSocialAvatarUrl { get; set; }
        public string BiodataUrl { get; set; } // user can provide his resume or profile url
        public bool HireMe { get; set; }
        public string OpenSourceUrls { get; set; }  // if user has any github then he or she can show his info
        public bool IsActive { get; set; }
        public bool IsArticleInDraftMode { get; set; }
        public bool IsArchived { get; set; }
        public bool IsDeleted { get; set; }

        public int AttachedAssetsInCloudCount { get; set; }
        public Guid? AttachedAssetsInCloudStorageId { get; set; }
        public string AttachedAssetsStoredInCloudBaseFolderPath { get; set; }

        // RelatedTo1 to RelatedTo5 :  ex: chapter,section,subsection in codeproject
        public string AllRelatedSubjectsIncludesVersionsWithComma { get; set; }
        public string AttachmentURLsComma { get; set; }     // source code , git hub urls can provide 
        public string SocialURLsWithComma { get; set; }     // user facebook or twitter url can provide
        public int TotalVotes { get; set; }                   // article how many votes got from readers
        public int TotalVotedPersonsCount { get; set; }
        
        public string Tag1 { get; set; }
        public string Tag2 { get; set; }
        public string Tag3 { get; set; }
        public string Tag4 { get; set; }
        public string Tag5 { get; set; }
        public string Tag6 { get; set; }
        public string Tag7 { get; set; }
        public string Tag8 { get; set; }
        public string Tag9 { get; set; }
        public string Tag10 { get; set; }
        public string Tag11 { get; set; }
        public string Tag12 { get; set; }
    }
}
