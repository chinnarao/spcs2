using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.Ad.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DbContexts.AdConfigurations
{
    //https://www.learnentityframeworkcore.com/configuration/fluent-api
    public class AdConfig : IEntityTypeConfiguration<Ad>
    {
        public void Configure(EntityTypeBuilder<Ad> e)
        {
            e.ToTable("Ad");
            e.Property(p => p.AdId);
            e.Property(p => p.ActiveDays).IsRequired().HasDefaultValue(7);
            e.Property(p => p.AdAddressAtPublicSecurityNearLandmarkName).IsUnicode(false).HasMaxLength(100);
            e.Property(p => p.AdAddressCity).IsUnicode(false).HasMaxLength(100);
            e.Property(p => p.AdAddressDistrictOrCounty).IsUnicode(false).HasMaxLength(100);
            e.Property(p => p.AdAddressState).IsUnicode(false).HasMaxLength(100);
            e.Property(p => p.AdAddressStreet).IsUnicode(false).HasMaxLength(100);
            e.Property(p => p.AdBody).IsUnicode(false).HasMaxLength(1000);
            e.Property(p => p.AdCountryCode).IsUnicode(false).HasMaxLength(2);
            e.Property(p => p.AdCountryCurrency).IsUnicode(false).HasMaxLength(2);
            e.Property(p => p.AdCountryCurrencyISO_4217).IsUnicode(false).HasMaxLength(3);
            e.Property(p => p.AdCountryName).IsUnicode(false).HasMaxLength(2);
            e.Property(p => p.AdDescription).IsUnicode(false).HasMaxLength(500);
            e.Property(p => p.AdItemsCost).IsRequired();
            e.Property(p => p.AdItemsCostInCurrencyName).IsUnicode(false).HasMaxLength(500); //https://www.countries-ofthe-world.com/world-currencies.html
            e.Property(p => p.AdLatitude).IsRequired();
            e.Property(p => p.AdLongitude).IsRequired();
            e.Property(p => p.AdTitle).IsRequired().IsUnicode(false).HasMaxLength(500);
            e.Property(p => p.AdZipCode).IsRequired().IsUnicode(false).HasMaxLength(16);
            e.Property(p => p.ArchivedDateTime).HasColumnType("datetime2(7)");
            e.Property(p => p.AttachedAssetsInCloudCount);
            e.Property(p => p.AttachedAssetsInCloudStorageId);
            e.Property(p => p.AttachedAssetsStoredInCloudBaseFolderPath).IsRequired().IsUnicode(false).HasMaxLength(1000);
            e.Property(p => p.CreatedDateTime).IsRequired().HasColumnType("datetime2(7)"); // conflict : sqlite and sql server.HasDefaultValueSql("date('now')"); //"getdate()"  or "(SYSDATETIME())"  or GetUtcDate(), [[ worked succes with this date('now'), the reason, this sql lite fn, testinf purpose]]
            e.Property(p => p.DeletedDateTime);
            e.Property(p => p.IsActive).IsRequired().HasDefaultValue<bool>(false);
            e.Property(p => p.IsAdInDraftMode).IsRequired().HasDefaultValue(0);
            e.Property(p => p.IsArchived).IsRequired().HasDefaultValue(0);
            e.Property(p => p.IsDeleted).IsRequired().HasDefaultValue(0);

            e.Property(p => p.Tag1).IsUnicode(false).HasMaxLength(32);
            e.Property(p => p.Tag10).IsUnicode(false).HasMaxLength(32);
            e.Property(p => p.Tag11).IsUnicode(false).HasMaxLength(32);
            e.Property(p => p.Tag12).IsUnicode(false).HasMaxLength(32);
            e.Property(p => p.Tag2).IsUnicode(false).HasMaxLength(32);
            e.Property(p => p.Tag3).IsUnicode(false).HasMaxLength(32);
            e.Property(p => p.Tag4).IsUnicode(false).HasMaxLength(32);
            e.Property(p => p.Tag5).IsUnicode(false).HasMaxLength(32);
            e.Property(p => p.Tag6).IsUnicode(false).HasMaxLength(32);
            e.Property(p => p.Tag7).IsUnicode(false).HasMaxLength(32);
            e.Property(p => p.Tag8).IsUnicode(false).HasMaxLength(32);
            e.Property(p => p.Tag9).IsUnicode(false).HasMaxLength(32);

            e.Property(p => p.UpdatedDateTime).IsRequired(); //.HasDefaultValueSql("date('now')");
            e.Property(p => p.UserEmail).IsUnicode(false).HasMaxLength(100);
            e.Property(p => p.UserId).IsRequired().IsUnicode(false).HasMaxLength(100);
            e.Property(p => p.UserLoggedInSocialProviderName).IsRequired().IsUnicode(false).HasMaxLength(100);   //facebook or local
            e.Property(p => p.UserName).IsUnicode(false).HasMaxLength(100);
            e.Property(p => p.UserPhoneNumber).IsUnicode(false).HasMaxLength(15); 
            e.Property(p => p.UserSocialAvatarUrl).IsUnicode(false).HasMaxLength(2000);
        }
    }
}
