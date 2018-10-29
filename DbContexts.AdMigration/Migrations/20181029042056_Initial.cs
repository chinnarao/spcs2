using System;
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
                    AdId = table.Column<long>(nullable: false),
                    AdTitle = table.Column<string>(unicode: false, maxLength: 500, nullable: false),
                    AdContent = table.Column<string>(unicode: false, nullable: false),
                    AdDisplayDays = table.Column<int>(nullable: false, defaultValue: 7),
                    UserIdOrEmail = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    UserPhoneNumber = table.Column<string>(unicode: false, maxLength: 15, nullable: true),
                    UserSocialAvatarUrl = table.Column<string>(unicode: false, nullable: true),
                    UserLoggedInSocialProviderName = table.Column<string>(unicode: false, maxLength: 32, nullable: true),
                    AddressStreet = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    AddressCity = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    AddressDistrictOrCounty = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    AddressState = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    AddressPartiesMeetingLandmarkName = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    AddressZipCode = table.Column<string>(unicode: false, maxLength: 16, nullable: true),
                    AddressCountryCode = table.Column<string>(unicode: false, maxLength: 2, nullable: true),
                    AddressCountryName = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    AddressLatitude = table.Column<double>(nullable: false),
                    AddressLongitude = table.Column<double>(nullable: false),
                    ItemCost = table.Column<double>(nullable: true),
                    ItemCostCurrencyName = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    ItemCurrencyISO_4217 = table.Column<string>(unicode: false, maxLength: 3, nullable: true),
                    AttachedAssetsInCloudCount = table.Column<int>(nullable: true),
                    AttachedAssetsInCloudStorageId = table.Column<Guid>(nullable: true),
                    AttachedAssetsStoredInCloudBaseFolderPath = table.Column<string>(unicode: false, nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2(7)", nullable: false),
                    UpdatedDateTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: true),
                    DeletedDateTime = table.Column<DateTime>(type: "datetime2(7)", nullable: true),
                    IsPublished = table.Column<bool>(nullable: false, defaultValue: false),
                    LastDraftOrBeforePublishedDateTime = table.Column<DateTime>(type: "datetime2(7)", nullable: true),
                    LastPublishedDateTime = table.Column<DateTime>(type: "datetime2(7)", nullable: true),
                    IsActive = table.Column<bool>(nullable: false, defaultValue: false),
                    LastActiveDateTime = table.Column<DateTime>(type: "datetime2(7)", nullable: true),
                    LastInActiveDateTime = table.Column<DateTime>(type: "datetime2(7)", nullable: true),
                    Tag1 = table.Column<string>(unicode: false, maxLength: 32, nullable: true),
                    Tag2 = table.Column<string>(unicode: false, maxLength: 32, nullable: true),
                    Tag3 = table.Column<string>(unicode: false, maxLength: 32, nullable: true),
                    Tag4 = table.Column<string>(unicode: false, maxLength: 32, nullable: true),
                    Tag5 = table.Column<string>(unicode: false, maxLength: 32, nullable: true),
                    Tag6 = table.Column<string>(unicode: false, maxLength: 32, nullable: true),
                    Tag7 = table.Column<string>(unicode: false, maxLength: 32, nullable: true),
                    Tag8 = table.Column<string>(unicode: false, maxLength: 32, nullable: true),
                    Tag9 = table.Column<string>(unicode: false, maxLength: 32, nullable: true)
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
