USE [Article]
GO
/****** Object:  Table [dbo].[Article]    Script Date: 7/13/2018 1:01:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Article](
	[ArticleId] [bigint] NOT NULL,
	[Title] [varchar](max) NOT NULL,
	[Content] [nvarchar](max) NOT NULL,
	[UserIdOrEmail] [varchar](100) NOT NULL,
	[UserLoggedInSocialProviderName] [varchar](32) NULL,
	[UserPhoneNumber] [varchar](15) NULL,
	[UserSocialAvatarUrl] [varchar](max) NULL,
	[BiodataUrl] [varchar](max) NULL,
	[HireMe] [bit] NOT NULL,
	[OpenSourceUrls] [varchar](max) NULL,
	[IsActive] [bit] NOT NULL,
	[IsArticleInDraftMode] [bit] NOT NULL,
	[IsPublished] [bit] NOT NULL,
	[AttachedAssetsInCloudCount] [int] NOT NULL,
	[AttachedAssetsInCloudStorageId] [uniqueidentifier] NOT NULL,
	[AttachedAssetsStoredInCloudBaseFolderPath] [varchar](max) NULL,
	[AllRelatedSubjectsIncludesVersionsWithComma] [varchar](1000) NULL,
	[AttachmentURLsComma] [varchar](max) NULL,
	[PublishedDate] [datetime2](7) NULL,
	[SocialURLsWithComma] [varchar](max) NULL,
	[TotalVotes] [int] NULL,
	[TotalVotedPersonsCount] [int] NULL,
	[ArticleAverageVotes] [float] NULL,
	[CreatedDateTime] [datetime2](7) NOT NULL,
	[UpdatedDateTime] [datetime2](7) NOT NULL,
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
 CONSTRAINT [PK_Article] PRIMARY KEY CLUSTERED 
(
	[ArticleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ArticleComment]    Script Date: 7/13/2018 1:01:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ArticleComment](
	[ArticleCommentId] [bigint] NOT NULL,
	[ArticleId] [bigint] NOT NULL,
	[UserIdOrEmail] [varchar](100) NOT NULL,
	[UserSocialAvatarUrl] [varchar](max) NULL,
	[IsAdminCommented] [bit] NULL,
	[CommentedDate] [datetime2](7) NOT NULL,
	[Comment] [varchar](max) NOT NULL,
 CONSTRAINT [PK_ArticleComment] PRIMARY KEY CLUSTERED 
(
	[ArticleCommentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ArticleCommit]    Script Date: 7/13/2018 1:01:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ArticleCommit](
	[ArticleCommitId] [bigint] NOT NULL,
	[ArticleId] [bigint] NOT NULL,
	[CommittedDate] [datetime2](7) NULL,
	[UserIdOrEmail] [varchar](100) NOT NULL,
	[UserSocialAvatarUrl] [varchar](max) NULL,
	[IsAdminCommited] [bit] NULL,
	[Commit] [varchar](max) NULL,
 CONSTRAINT [PK_ArticleCommit] PRIMARY KEY CLUSTERED 
(
	[ArticleCommitId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ArticleLicense]    Script Date: 7/13/2018 1:01:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ArticleLicense](
	[ArticleLicenseId] [bigint] NOT NULL,
	[ArticleId] [bigint] NOT NULL,
	[License] [varchar](max) NULL,
	[LicensedDate] [datetime2](7) NULL,
 CONSTRAINT [PK_ArticleLicense] PRIMARY KEY CLUSTERED 
(
	[ArticleLicenseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[Article] ADD  DEFAULT ((0)) FOR [HireMe]
GO
ALTER TABLE [dbo].[Article] ADD  DEFAULT ((0)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Article] ADD  DEFAULT ((0)) FOR [IsArticleInDraftMode]
GO
ALTER TABLE [dbo].[Article] ADD  DEFAULT ((0)) FOR [IsPublished]
GO
ALTER TABLE [dbo].[ArticleComment]  WITH CHECK ADD  CONSTRAINT [FK_ArticleComment_Article_ArticleId] FOREIGN KEY([ArticleId])
REFERENCES [dbo].[Article] ([ArticleId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ArticleComment] CHECK CONSTRAINT [FK_ArticleComment_Article_ArticleId]
GO
ALTER TABLE [dbo].[ArticleCommit]  WITH CHECK ADD  CONSTRAINT [FK_ArticleCommit_Article_ArticleId] FOREIGN KEY([ArticleId])
REFERENCES [dbo].[Article] ([ArticleId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ArticleCommit] CHECK CONSTRAINT [FK_ArticleCommit_Article_ArticleId]
GO
ALTER TABLE [dbo].[ArticleLicense]  WITH CHECK ADD  CONSTRAINT [FK_ArticleLicense_Article_ArticleId] FOREIGN KEY([ArticleId])
REFERENCES [dbo].[Article] ([ArticleId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ArticleLicense] CHECK CONSTRAINT [FK_ArticleLicense_Article_ArticleId]
GO
