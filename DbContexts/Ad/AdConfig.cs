using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DbContexts.Ad
{
    //https://www.learnentityframeworkcore.com/configuration/fluent-api
    public class AdConfig : IEntityTypeConfiguration<Models.Ad.Entities.Ad>
    {
        public void Configure(EntityTypeBuilder<Models.Ad.Entities.Ad> e)
        {
            e.ToTable("Ad");
            e.Property(p => p.AdId).ValueGeneratedNever();  //ForSqlServerUseSequenceHiLo    //UseSqlServerIdentityColumn()
            e.Property(p => p.AdTitle).IsRequired().IsUnicode(false).HasMaxLength(500);
            e.Property(p => p.AdContent).IsRequired().IsUnicode(false);
            e.Property(p => p.AdDisplayDays).IsRequired().HasDefaultValue(7);

            e.Property(p => p.UserIdOrEmail).IsRequired().IsUnicode(false).HasMaxLength(100);
            e.Property(p => p.UserPhoneNumber).IsUnicode(false).HasMaxLength(15);
            e.Property(p => p.UserSocialAvatarUrl).IsUnicode(false);
            e.Property(p => p.UserLoggedInSocialProviderName).IsUnicode(false).HasMaxLength(32);   //facebook or local

            e.Property(p => p.AddressStreet).IsUnicode(false).HasMaxLength(100);
            e.Property(p => p.AddressCity).IsUnicode(false).HasMaxLength(100);
            e.Property(p => p.AddressDistrictOrCounty).IsUnicode(false).HasMaxLength(100);
            e.Property(p => p.AddressState).IsUnicode(false).HasMaxLength(100);
            e.Property(p => p.AddressPartiesMeetingLandmarkName).IsUnicode(false).HasMaxLength(100);
            e.Property(p => p.AddressZipCode).IsUnicode(false).HasMaxLength(16);
            e.Property(p => p.AdressCountryCode).IsUnicode(false).HasMaxLength(2);
            e.Property(p => p.AddressCountryName).IsUnicode(false).HasMaxLength(100);
            e.Property(p => p.AddressLatitude);
            e.Property(p => p.AddressLongitude);

            e.Property(p => p.ItemCost);
            e.Property(p => p.ItemCostCurrencyName).IsUnicode(false).HasMaxLength(50);
            e.Property(p => p.ItmeCurrencyISO_4217).IsUnicode(false).HasMaxLength(3);

            e.Property(p => p.AttachedAssetsInCloudCount);
            e.Property(p => p.AttachedAssetsInCloudStorageId);
            e.Property(p => p.AttachedAssetsStoredInCloudBaseFolderPath).IsUnicode(false);
            
            e.Property(p => p.CreatedDateTime).IsRequired().HasColumnType("datetime2(7)"); // conflict : sqlite and sql server.HasDefaultValueSql("date('now')"); //"getdate()"  or "(SYSDATETIME())"  or GetUtcDate(), [[ worked succes with this date('now'), the reason, this sql lite fn, testinf purpose]]
            e.Property(p => p.UpdatedDateTime).IsRequired(); //.HasDefaultValueSql("date('now')");
            e.Property(p => p.IsDeleted);
            e.Property(p => p.DeletedDateTime).HasColumnType("datetime2(7)");
            e.Property(p => p.IsPublished).IsRequired().HasDefaultValue<bool>(false);
            e.Property(p => p.LastDraftOrBeforePublishedDateTime).HasColumnType("datetime2(7)");
            e.Property(p => p.LastPublishedDateTime).HasColumnType("datetime2(7)");
            e.Property(p => p.IsActive).IsRequired().HasDefaultValue<bool>(false);
            e.Property(p => p.LastActiveDateTime).HasColumnType("datetime2(7)");
            e.Property(p => p.LastInActiveDateTime).HasColumnType("datetime2(7)");

            e.Property(p => p.Tag1).IsUnicode(false).HasMaxLength(32);
            e.Property(p => p.Tag2).IsUnicode(false).HasMaxLength(32);
            e.Property(p => p.Tag3).IsUnicode(false).HasMaxLength(32);
            e.Property(p => p.Tag4).IsUnicode(false).HasMaxLength(32);
            e.Property(p => p.Tag5).IsUnicode(false).HasMaxLength(32);
            e.Property(p => p.Tag6).IsUnicode(false).HasMaxLength(32);
            e.Property(p => p.Tag7).IsUnicode(false).HasMaxLength(32);
            e.Property(p => p.Tag8).IsUnicode(false).HasMaxLength(32);
            e.Property(p => p.Tag9).IsUnicode(false).HasMaxLength(32);
        }
    }
}
