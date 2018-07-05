using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.Article.Entities;

namespace DbContexts.ArticleConfigurations
{
    public class ArticleConfig : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> e)
        {
            e.ToTable("Article");
            e.Property(p => p.ArticleId);
            e.Property(p => p.ActiveDays).IsRequired().HasDefaultValue(7);
            e.Property(p => p.AllRelatedSubjectsIncludesVersionsWithComma).IsUnicode(false).HasMaxLength(100);
            e.Property(p => p.AttachedAssetsInCloudCount);
            e.Property(p => p.AttachedAssetsInCloudStorageId);
            e.Property(p => p.AttachedAssetsStoredInCloudBaseFolderPath).IsUnicode(false).HasMaxLength(100);
            e.Property(p => p.AttachmentURLsComma).IsUnicode(false).HasMaxLength(100);
            e.Property(x => x.BiodataUrl).IsUnicode(false).HasMaxLength(100);
            e.Property(x => x.Body).HasMaxLength(100);
            e.Property(x => x.Commits).IsUnicode(false).HasMaxLength(100);
            e.Property(x => x.CreatedDateTime).IsRequired().HasColumnType("datetime2(7)"); //SqlDbType.DateTime2.ToString()
            e.Property(x => x.Description).IsUnicode(false).HasMaxLength(100);
            e.Property(x => x.HireMe);
            e.Property(x => x.IsActive);
            e.Property(x => x.IsArchived);
            e.Property(x => x.IsArticleInDraftMode);
            e.Property(x => x.IsDeleted);
            e.Property(x => x.License);
            e.Property(x => x.OpenSourceUrls).IsUnicode(false).HasMaxLength(100);
            e.Property(x => x.SocialURLsWithComma).IsUnicode(false).HasMaxLength(100);
            e.Property(x => x.Tag1).IsUnicode(false).HasMaxLength(100);
            e.Property(x => x.Tag2).IsUnicode(false).HasMaxLength(100);
            e.Property(x => x.Tag3).IsUnicode(false).HasMaxLength(100);
            e.Property(x => x.Tag4).IsUnicode(false).HasMaxLength(100);
            e.Property(x => x.Tag5).IsUnicode(false).HasMaxLength(100);
            e.Property(x => x.Tag6).IsUnicode(false).HasMaxLength(100);
            e.Property(x => x.Tag7).IsUnicode(false).HasMaxLength(100);
            e.Property(x => x.Tag8).IsUnicode(false).HasMaxLength(100);
            e.Property(x => x.Tag9).IsUnicode(false).HasMaxLength(100);
            e.Property(x => x.Tag10).IsUnicode(false).HasMaxLength(100);
            e.Property(x => x.Tag11).IsUnicode(false).HasMaxLength(100);
            e.Property(x => x.Tag12).IsUnicode(false).HasMaxLength(100);
            e.Property(x => x.Title).IsUnicode(false).HasMaxLength(100);
            e.Property(x => x.TotalVotedPersonsCount);
            e.Property(x => x.TotalVotes);
            e.Property(x => x.UpdatedDateTime).IsRequired().HasColumnType("datetime2(7)");
            e.Property(x => x.UserEmail).IsUnicode(false).HasMaxLength(100);
            e.Property(x => x.UserId).IsUnicode(false).HasMaxLength(100);
            e.Property(x => x.UserLoggedInSocialProviderName).IsUnicode(false).HasMaxLength(100);
            e.Property(x => x.UserName).IsUnicode(false).HasMaxLength(100);
            e.Property(x => x.UserPhoneNumber).IsUnicode(false).HasMaxLength(15);
            e.Property(x => x.UserSocialAvatarUrl).IsUnicode(false).HasMaxLength(2000);
            //relationships
        }
    }
}