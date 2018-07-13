using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.Article.Entities;

namespace DbContexts.ArticleConfigurations
{
    public class ArticleCommitConfig : IEntityTypeConfiguration<ArticleCommit>
    {
        public void Configure(EntityTypeBuilder<ArticleCommit> e)
        {
            e.ToTable("ArticleCommit");
            e.Property(p => p.ArticleCommitId).ValueGeneratedNever();
            e.Property(p => p.ArticleId).IsRequired();
            e.Property(p => p.UserIdOrEmail).IsRequired().IsUnicode(false).HasMaxLength(100);
            e.Property(p => p.UserSocialAvatarUrl).IsUnicode(false);
            e.Property(p => p.CommittedDate).IsRequired().HasColumnType("datetime2(7)");
            e.Property(x => x.IsAdminCommited);
            e.Property(p => p.Commit).IsUnicode(false);
        }
    }
}