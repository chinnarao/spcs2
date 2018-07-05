using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DbContexts.ArticleMigration.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Article",
                columns: table => new
                {
                    ArticleId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ActiveDays = table.Column<int>(nullable: false, defaultValue: 7),
                    Title = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    Description = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    Body = table.Column<string>(maxLength: 100, nullable: true),
                    License = table.Column<string>(nullable: true),
                    Commits = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2(7)", nullable: false),
                    UpdatedDateTime = table.Column<DateTime>(type: "datetime2(7)", nullable: false),
                    UserEmail = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    UserId = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    UserLoggedInSocialProviderName = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    UserName = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    UserPhoneNumber = table.Column<string>(unicode: false, maxLength: 15, nullable: true),
                    UserSocialAvatarUrl = table.Column<string>(unicode: false, maxLength: 2000, nullable: true),
                    BiodataUrl = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    HireMe = table.Column<bool>(nullable: false),
                    OpenSourceUrls = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    IsArticleInDraftMode = table.Column<bool>(nullable: false),
                    IsArchived = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    AttachedAssetsInCloudCount = table.Column<int>(nullable: false),
                    AttachedAssetsInCloudStorageId = table.Column<Guid>(nullable: true),
                    AttachedAssetsStoredInCloudBaseFolderPath = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    AllRelatedSubjectsIncludesVersionsWithComma = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    AttachmentURLsComma = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    SocialURLsWithComma = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    TotalVotes = table.Column<int>(nullable: false),
                    TotalVotedPersonsCount = table.Column<int>(nullable: false),
                    Tag1 = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    Tag2 = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    Tag3 = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    Tag4 = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    Tag5 = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    Tag6 = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    Tag7 = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    Tag8 = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    Tag9 = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    Tag10 = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    Tag11 = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    Tag12 = table.Column<string>(unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Article", x => x.ArticleId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Article");
        }
    }
}
