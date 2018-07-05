USE [Article]
GO

/****** Object:  Table [dbo].[Article]    Script Date: 7/4/2018 1:40:17 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Article](
	[ArticleId] [bigint] IDENTITY(1,1) NOT NULL,
	[ActiveDays] [int] NOT NULL,
	[Title] [varchar](100) NULL,
	[Description] [varchar](100) NULL,
	[Body] [nvarchar](100) NULL,
	[License] [nvarchar](max) NULL,
	[Commits] [varchar](100) NULL,
	[CreatedDateTime] [datetime2](7) NOT NULL,
	[UpdatedDateTime] [datetime2](7) NOT NULL,
	[UserEmail] [varchar](100) NULL,
	[UserId] [varchar](100) NULL,
	[UserLoggedInSocialProviderName] [varchar](100) NULL,
	[UserName] [varchar](100) NULL,
	[UserPhoneNumber] [varchar](15) NULL,
	[UserSocialAvatarUrl] [varchar](2000) NULL,
	[BiodataUrl] [varchar](100) NULL,
	[HireMe] [bit] NOT NULL,
	[OpenSourceUrls] [varchar](100) NULL,
	[IsActive] [bit] NOT NULL,
	[IsArticleInDraftMode] [bit] NOT NULL,
	[IsArchived] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[AttachedAssetsInCloudCount] [int] NOT NULL,
	[AttachedAssetsInCloudStorageId] [uniqueidentifier] NULL,
	[AttachedAssetsStoredInCloudBaseFolderPath] [varchar](100) NULL,
	[AllRelatedSubjectsIncludesVersionsWithComma] [varchar](100) NULL,
	[AttachmentURLsComma] [varchar](100) NULL,
	[SocialURLsWithComma] [varchar](100) NULL,
	[TotalVotes] [int] NOT NULL,
	[TotalVotedPersonsCount] [int] NOT NULL,
	[Tag1] [varchar](100) NULL,
	[Tag2] [varchar](100) NULL,
	[Tag3] [varchar](100) NULL,
	[Tag4] [varchar](100) NULL,
	[Tag5] [varchar](100) NULL,
	[Tag6] [varchar](100) NULL,
	[Tag7] [varchar](100) NULL,
	[Tag8] [varchar](100) NULL,
	[Tag9] [varchar](100) NULL,
	[Tag10] [varchar](100) NULL,
	[Tag11] [varchar](100) NULL,
	[Tag12] [varchar](100) NULL,
 CONSTRAINT [PK_Article] PRIMARY KEY CLUSTERED 
(
	[ArticleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[Article] ADD  DEFAULT ((7)) FOR [ActiveDays]
GO


