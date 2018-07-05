using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DbContexts.AdMigration.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ad",
                columns: table => new
                {
                    AdId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ActiveDays = table.Column<int>(nullable: false, defaultValue: 7),
                    AdAddressAtPublicSecurityNearLandmarkName = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    AdAddressCity = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    AdAddressDistrictOrCounty = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    AdAddressState = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    AdAddressStreet = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    AdBody = table.Column<string>(unicode: false, maxLength: 1000, nullable: true),
                    AdCountryCode = table.Column<string>(unicode: false, maxLength: 2, nullable: true),
                    AdCountryCurrency = table.Column<string>(unicode: false, maxLength: 2, nullable: true),
                    AdCountryCurrencyISO_4217 = table.Column<string>(unicode: false, maxLength: 3, nullable: true),
                    AdCountryName = table.Column<string>(unicode: false, maxLength: 2, nullable: true),
                    AdDescription = table.Column<string>(unicode: false, maxLength: 500, nullable: true),
                    AdItemsCost = table.Column<double>(nullable: false),
                    AdItemsCostInCurrencyName = table.Column<string>(unicode: false, maxLength: 500, nullable: true),
                    AdLatitude = table.Column<double>(nullable: false),
                    AdLongitude = table.Column<double>(nullable: false),
                    AdTitle = table.Column<string>(unicode: false, maxLength: 500, nullable: false),
                    AdZipCode = table.Column<string>(unicode: false, maxLength: 16, nullable: false),
                    ArchivedDateTime = table.Column<DateTime>(type: "datetime2(7)", nullable: false),
                    AttachedAssetsInCloudCount = table.Column<int>(nullable: false),
                    AttachedAssetsInCloudStorageId = table.Column<Guid>(nullable: true),
                    AttachedAssetsStoredInCloudBaseFolderPath = table.Column<string>(unicode: false, maxLength: 1000, nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2(7)", nullable: false),
                    DeletedDateTime = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false, defaultValue: false),
                    IsAdInDraftMode = table.Column<bool>(nullable: false, defaultValue: false),
                    IsArchived = table.Column<bool>(nullable: false, defaultValue: false),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false),
                    Tag1 = table.Column<string>(unicode: false, maxLength: 32, nullable: true),
                    Tag2 = table.Column<string>(unicode: false, maxLength: 32, nullable: true),
                    Tag3 = table.Column<string>(unicode: false, maxLength: 32, nullable: true),
                    Tag4 = table.Column<string>(unicode: false, maxLength: 32, nullable: true),
                    Tag5 = table.Column<string>(unicode: false, maxLength: 32, nullable: true),
                    Tag6 = table.Column<string>(unicode: false, maxLength: 32, nullable: true),
                    Tag7 = table.Column<string>(unicode: false, maxLength: 32, nullable: true),
                    Tag8 = table.Column<string>(unicode: false, maxLength: 32, nullable: true),
                    Tag9 = table.Column<string>(unicode: false, maxLength: 32, nullable: true),
                    Tag10 = table.Column<string>(unicode: false, maxLength: 32, nullable: true),
                    Tag11 = table.Column<string>(unicode: false, maxLength: 32, nullable: true),
                    Tag12 = table.Column<string>(unicode: false, maxLength: 32, nullable: true),
                    UpdatedDateTime = table.Column<DateTime>(nullable: false),
                    UserEmail = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    UserId = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    UserLoggedInSocialProviderName = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    UserName = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    UserPhoneNumber = table.Column<string>(unicode: false, maxLength: 15, nullable: true),
                    UserSocialAvatarUrl = table.Column<string>(unicode: false, maxLength: 2000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ad", x => x.AdId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ad");
        }
    }
}
