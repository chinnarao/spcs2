﻿using Microsoft.EntityFrameworkCore;
using System;

namespace DbContexts.Ad
{
    //https://github.com/damienbod/AspNetCoreMultipleProject/blob/master/src/DataAccessMsSqlServerProvider/DomainModelMsSqlServerContext.cs
    //http://www.entityframeworktutorial.net/code-first/configure-one-to-one-relationship-in-code-first.aspx
    public class AdDbContext : DbContext
    {
        public AdDbContext(DbContextOptions<AdDbContext> options) : base(options)
        {
        }

        /// <summary>
        /// this method will be called by DbContexts.AdMigration Project, for first time db creation purpose
        /// </summary>
        /// <param name="connection">connection string</param>
        /// <returns>AdDbContext</returns>
        public static AdDbContext Create(string connection)
        {
            if (string.IsNullOrEmpty(connection)) throw new ArgumentNullException(nameof(connection));
            return new AdDbContext(new DbContextOptionsBuilder<AdDbContext>().UseSqlServer(connection).Options);
        }

        public DbSet<Models.Ad.Entities.Ad> Ads { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Models.Ad.Entities.Ad>().HasQueryFilter(a => !a.IsDeleted);  //IgnoreQueryFilters()
            modelBuilder.ApplyConfiguration(new AdConfig());
        }
    }
}
