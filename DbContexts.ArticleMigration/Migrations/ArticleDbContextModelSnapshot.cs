﻿// <auto-generated />
using System;
using DbContexts.Article;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DbContexts.ArticleMigration.Migrations
{
    [DbContext(typeof(ArticleDbContext))]
    partial class ArticleDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.0-rtm-30799")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Models.Article.Entities.Article", b =>
                {
                    b.Property<long>("ArticleId");

                    b.Property<string>("AllRelatedSubjectsIncludesVersionsWithComma")
                        .HasMaxLength(1000)
                        .IsUnicode(false);

                    b.Property<double?>("ArticleAverageVotes");

                    b.Property<int>("AttachedAssetsInCloudCount");

                    b.Property<Guid>("AttachedAssetsInCloudStorageId");

                    b.Property<string>("AttachedAssetsStoredInCloudBaseFolderPath")
                        .IsUnicode(false);

                    b.Property<string>("AttachmentURLsComma")
                        .IsUnicode(false);

                    b.Property<string>("BiodataUrl")
                        .IsUnicode(false);

                    b.Property<string>("Content")
                        .IsRequired();

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("datetime2(7)");

                    b.Property<bool>("HireMe")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

                    b.Property<bool>("IsArticleInDraftMode")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

                    b.Property<bool>("IsPublished")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

                    b.Property<string>("OpenSourceUrls")
                        .IsUnicode(false);

                    b.Property<DateTime?>("PublishedDate")
                        .HasColumnType("datetime2(7)");

                    b.Property<string>("SocialURLsWithComma")
                        .IsUnicode(false);

                    b.Property<string>("Tag1")
                        .HasMaxLength(32)
                        .IsUnicode(false);

                    b.Property<string>("Tag10")
                        .HasMaxLength(32)
                        .IsUnicode(false);

                    b.Property<string>("Tag11")
                        .HasMaxLength(32)
                        .IsUnicode(false);

                    b.Property<string>("Tag12")
                        .HasMaxLength(32)
                        .IsUnicode(false);

                    b.Property<string>("Tag2")
                        .HasMaxLength(32)
                        .IsUnicode(false);

                    b.Property<string>("Tag3")
                        .HasMaxLength(32)
                        .IsUnicode(false);

                    b.Property<string>("Tag4")
                        .HasMaxLength(32)
                        .IsUnicode(false);

                    b.Property<string>("Tag5")
                        .HasMaxLength(32)
                        .IsUnicode(false);

                    b.Property<string>("Tag6")
                        .HasMaxLength(32)
                        .IsUnicode(false);

                    b.Property<string>("Tag7")
                        .HasMaxLength(32)
                        .IsUnicode(false);

                    b.Property<string>("Tag8")
                        .HasMaxLength(32)
                        .IsUnicode(false);

                    b.Property<string>("Tag9")
                        .HasMaxLength(32)
                        .IsUnicode(false);

                    b.Property<string>("Title")
                        .IsRequired()
                        .IsUnicode(false);

                    b.Property<int?>("TotalVotedPersonsCount");

                    b.Property<int?>("TotalVotes");

                    b.Property<DateTime>("UpdatedDateTime")
                        .HasColumnType("datetime2(7)");

                    b.Property<string>("UserIdOrEmail")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false);

                    b.Property<string>("UserLoggedInSocialProviderName")
                        .HasMaxLength(32)
                        .IsUnicode(false);

                    b.Property<string>("UserPhoneNumber")
                        .HasMaxLength(15)
                        .IsUnicode(false);

                    b.Property<string>("UserSocialAvatarUrl")
                        .IsUnicode(false);

                    b.HasKey("ArticleId");

                    b.ToTable("Article");
                });

            modelBuilder.Entity("Models.Article.Entities.ArticleComment", b =>
                {
                    b.Property<long>("ArticleCommentId");

                    b.Property<long>("ArticleId");

                    b.Property<string>("Comment")
                        .IsRequired()
                        .IsUnicode(false);

                    b.Property<DateTime>("CommentedDate")
                        .HasColumnType("datetime2(7)");

                    b.Property<bool?>("IsAdminCommented");

                    b.Property<string>("UserIdOrEmail")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false);

                    b.Property<string>("UserSocialAvatarUrl")
                        .IsUnicode(false);

                    b.HasKey("ArticleCommentId");

                    b.HasIndex("ArticleId");

                    b.ToTable("ArticleComment");
                });

            modelBuilder.Entity("Models.Article.Entities.ArticleCommit", b =>
                {
                    b.Property<long>("ArticleCommitId");

                    b.Property<long>("ArticleId");

                    b.Property<string>("Commit")
                        .IsUnicode(false);

                    b.Property<DateTime?>("CommittedDate")
                        .HasColumnType("datetime2(7)");

                    b.Property<bool?>("IsAdminCommited");

                    b.Property<string>("UserIdOrEmail")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false);

                    b.Property<string>("UserSocialAvatarUrl")
                        .IsUnicode(false);

                    b.HasKey("ArticleCommitId");

                    b.HasIndex("ArticleId");

                    b.ToTable("ArticleCommit");
                });

            modelBuilder.Entity("Models.Article.Entities.ArticleLicense", b =>
                {
                    b.Property<long>("ArticleLicenseId");

                    b.Property<long>("ArticleId");

                    b.Property<string>("License")
                        .IsUnicode(false);

                    b.Property<DateTime?>("LicensedDate")
                        .HasColumnType("datetime2(7)");

                    b.HasKey("ArticleLicenseId");

                    b.HasIndex("ArticleId")
                        .IsUnique();

                    b.ToTable("ArticleLicense");
                });

            modelBuilder.Entity("Models.Article.Entities.ArticleComment", b =>
                {
                    b.HasOne("Models.Article.Entities.Article", "Article")
                        .WithMany("ArticleComments")
                        .HasForeignKey("ArticleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Models.Article.Entities.ArticleCommit", b =>
                {
                    b.HasOne("Models.Article.Entities.Article", "Article")
                        .WithMany("ArticleCommits")
                        .HasForeignKey("ArticleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Models.Article.Entities.ArticleLicense", b =>
                {
                    b.HasOne("Models.Article.Entities.Article", "Article")
                        .WithOne("ArticleLicense")
                        .HasForeignKey("Models.Article.Entities.ArticleLicense", "ArticleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
