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
            e.Property(p => p.ArticleId).ValueGeneratedNever();
            e.Property(p => p.AllRelatedSubjectsIncludesVersionsWithComma).IsUnicode(false).HasMaxLength(1000);
            e.Property(p => p.AttachedAssetsInCloudCount);
            e.Property(p => p.AttachedAssetsInCloudStorageId).IsRequired();
            e.Property(p => p.AttachedAssetsStoredInCloudBaseFolderPath).IsUnicode(false);
            e.Property(p => p.AttachmentURLsComma).IsUnicode(false);
            e.Property(x => x.BiodataUrl).IsUnicode(false);
            e.Property(x => x.Body);
            e.Property(x => x.Commits);  //sample shuld be : N'{"id": 1}'
            e.Property(x => x.CreatedDateTime).IsRequired().HasColumnType("datetime2(7)");
            e.Property(x => x.Description).IsUnicode(false).HasMaxLength(500);
            e.Property(x => x.HireMe).HasDefaultValue<bool>(false);
            e.Property(x => x.IsActive).HasDefaultValue<bool>(false);
            e.Property(x => x.IsArchived).HasDefaultValue<bool>(false);
            e.Property(x => x.IsArticleInDraftMode).HasDefaultValue<bool>(false);
            e.Property(x => x.License);
            e.Property(x => x.OpenSourceUrls).IsUnicode(false);
            e.Property(x => x.SocialURLsWithComma).IsUnicode(false);
            e.Property(x => x.Tag1).IsUnicode(false).HasMaxLength(32);
            e.Property(x => x.Tag2).IsUnicode(false).HasMaxLength(32);
            e.Property(x => x.Tag3).IsUnicode(false).HasMaxLength(32);
            e.Property(x => x.Tag4).IsUnicode(false).HasMaxLength(32);
            e.Property(x => x.Tag5).IsUnicode(false).HasMaxLength(32);
            e.Property(x => x.Tag6).IsUnicode(false).HasMaxLength(32);
            e.Property(x => x.Tag7).IsUnicode(false).HasMaxLength(32);
            e.Property(x => x.Tag8).IsUnicode(false).HasMaxLength(32);
            e.Property(x => x.Tag9).IsUnicode(false).HasMaxLength(32);
            e.Property(x => x.Tag10).IsUnicode(false).HasMaxLength(32);
            e.Property(x => x.Tag11).IsUnicode(false).HasMaxLength(32);
            e.Property(x => x.Tag12).IsUnicode(false).HasMaxLength(32);
            e.Property(x => x.Title).IsUnicode(false).HasMaxLength(500);
            e.Property(x => x.TotalVotedPersonsCount);
            e.Property(x => x.TotalVotes);
            e.Property(x => x.UpdatedDateTime).IsRequired().HasColumnType("datetime2(7)");
            e.Property(x => x.UserEmail).IsUnicode(false).HasMaxLength(100);
            e.Property(x => x.UserId).IsUnicode(false).HasMaxLength(100);
            e.Property(x => x.UserLoggedInSocialProviderName).IsUnicode(false).HasMaxLength(32);
            e.Property(x => x.UserName).IsUnicode(false).HasMaxLength(100);
            e.Property(x => x.UserPhoneNumber).IsUnicode(false).HasMaxLength(15);
            e.Property(x => x.UserSocialAvatarUrl).IsUnicode(false);
        }
    }
}