USE [Ad]
GO

/****** Object:  Table [dbo].[Ad]    Script Date: 7/4/2018 1:41:12 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Ad](
	[AdId] [bigint] IDENTITY(1,1) NOT NULL,
	[ActiveDays] [int] NOT NULL,
	[AdAddressAtPublicSecurityNearLandmarkName] [varchar](100) NULL,
	[AdAddressCity] [varchar](100) NULL,
	[AdAddressDistrictOrCounty] [varchar](100) NULL,
	[AdAddressState] [varchar](100) NULL,
	[AdAddressStreet] [varchar](100) NULL,
	[AdBody] [varchar](1000) NULL,
	[AdCountryCode] [varchar](2) NULL,
	[AdCountryCurrency] [varchar](2) NULL,
	[AdCountryCurrencyISO_4217] [varchar](3) NULL,
	[AdCountryName] [varchar](2) NULL,
	[AdDescription] [varchar](500) NULL,
	[AdItemsCost] [float] NOT NULL,
	[AdItemsCostInCurrencyName] [varchar](500) NULL,
	[AdLatitude] [float] NOT NULL,
	[AdLongitude] [float] NOT NULL,
	[AdTitle] [varchar](500) NOT NULL,
	[AdZipCode] [varchar](16) NOT NULL,
	[ArchivedDateTime] [datetime2](7) NOT NULL,
	[AttachedAssetsInCloudCount] [int] NOT NULL,
	[AttachedAssetsInCloudStorageId] [uniqueidentifier] NULL,
	[AttachedAssetsStoredInCloudBaseFolderPath] [varchar](1000) NOT NULL,
	[CreatedDateTime] [datetime2](7) NOT NULL,
	[DeletedDateTime] [datetime2](7) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[IsAdInDraftMode] [bit] NOT NULL,
	[IsArchived] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[Tag1] [varchar](32) NULL,
	[Tag2] [varchar](32) NULL,
	[Tag3] [varchar](32) NULL,
	[Tag4] [varchar](32) NULL,
	[Tag5] [varchar](32) NULL,
	[Tag6] [varchar](32) NULL,
	[Tag7] [varchar](32) NULL,
	[Tag8] [varchar](32) NULL,
	[Tag9] [varchar](32) NULL,
	[Tag10] [varchar](32) NULL,
	[Tag11] [varchar](32) NULL,
	[Tag12] [varchar](32) NULL,
	[UpdatedDateTime] [datetime2](7) NOT NULL,
	[UserEmail] [varchar](100) NULL,
	[UserId] [varchar](100) NOT NULL,
	[UserLoggedInSocialProviderName] [varchar](100) NOT NULL,
	[UserName] [varchar](100) NULL,
	[UserPhoneNumber] [varchar](15) NULL,
	[UserSocialAvatarUrl] [varchar](2000) NULL,
 CONSTRAINT [PK_Ad] PRIMARY KEY CLUSTERED 
(
	[AdId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Ad] ADD  DEFAULT ((7)) FOR [ActiveDays]
GO

ALTER TABLE [dbo].[Ad] ADD  DEFAULT ((0)) FOR [IsActive]
GO

ALTER TABLE [dbo].[Ad] ADD  DEFAULT ((0)) FOR [IsAdInDraftMode]
GO

ALTER TABLE [dbo].[Ad] ADD  DEFAULT ((0)) FOR [IsArchived]
GO

ALTER TABLE [dbo].[Ad] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO


