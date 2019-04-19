USE [COMP1640_DB]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 4/19/2019 06:23:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Category](
	[CategoryID] [uniqueidentifier] NOT NULL CONSTRAINT [DF_Category_CategoryID]  DEFAULT (newsequentialid()),
	[CategoryName] [varchar](200) NULL,
	[ClosureDate] [datetime] NULL,
	[FinalClosureDate] [datetime] NULL,
	[Faculty_DetailID] [uniqueidentifier] NULL,
	[Description] [varchar](500) NULL,
	[Status] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[CategoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Comment]    Script Date: 4/19/2019 06:23:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Comment](
	[CommentID] [uniqueidentifier] NOT NULL CONSTRAINT [DF_Comment_CommentID]  DEFAULT (newsequentialid()),
	[Content] [varchar](max) NULL,
	[Creator] [uniqueidentifier] NULL,
	[Contribution] [uniqueidentifier] NULL,
	[CommentDate] [datetime] NULL,
	[Status] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[CommentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Contribution]    Script Date: 4/19/2019 06:23:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Contribution](
	[ContributionID] [uniqueidentifier] NOT NULL CONSTRAINT [DF_Contribution_ContributionID]  DEFAULT (newsequentialid()),
	[Title] [varchar](200) NULL,
	[Description] [varchar](500) NULL,
	[CreationDate] [datetime] NULL,
	[Category] [uniqueidentifier] NULL,
	[Creator] [uniqueidentifier] NULL,
	[Status] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ContributionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Document]    Script Date: 4/19/2019 06:23:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Document](
	[DocumentID] [uniqueidentifier] NOT NULL CONSTRAINT [DF_Document_DocumentID]  DEFAULT (newsequentialid()),
	[DocumentName] [varchar](200) NULL,
	[Path] [varchar](300) NULL,
	[UploadDate] [datetime] NULL,
	[Contribution] [uniqueidentifier] NULL,
PRIMARY KEY CLUSTERED 
(
	[DocumentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Faculty]    Script Date: 4/19/2019 06:23:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Faculty](
	[FacultyID] [uniqueidentifier] NOT NULL CONSTRAINT [DF_Faculty_FacultyID]  DEFAULT (newsequentialid()),
	[FacultyName] [varchar](200) NULL,
	[Description] [varchar](200) NULL,
PRIMARY KEY CLUSTERED 
(
	[FacultyID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Faculty_Detail]    Script Date: 4/19/2019 06:23:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Faculty_Detail](
	[Faculty_DetailID] [uniqueidentifier] NOT NULL CONSTRAINT [DF_Faculty_Detail_Faculty_DetailID]  DEFAULT (newsequentialid()),
	[FacultyID] [uniqueidentifier] NULL,
	[UserCoordinator] [uniqueidentifier] NULL,
PRIMARY KEY CLUSTERED 
(
	[Faculty_DetailID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Image]    Script Date: 4/19/2019 06:23:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Image](
	[ImageID] [uniqueidentifier] NOT NULL CONSTRAINT [DF_Image_ImageID]  DEFAULT (newsequentialid()),
	[ImageName] [varchar](200) NULL,
	[Description] [varchar](500) NULL,
	[Path] [varchar](300) NULL,
	[UploadDate] [datetime] NULL,
	[Contribution] [uniqueidentifier] NULL,
PRIMARY KEY CLUSTERED 
(
	[ImageID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Role]    Script Date: 4/19/2019 06:23:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Role](
	[RoleID] [uniqueidentifier] NOT NULL CONSTRAINT [DF_Role_RoleID]  DEFAULT (newsequentialid()),
	[RoleName] [varchar](200) NULL,
	[Description] [varchar](500) NULL,
PRIMARY KEY CLUSTERED 
(
	[RoleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[User]    Script Date: 4/19/2019 06:23:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[User](
	[UserID] [uniqueidentifier] NOT NULL CONSTRAINT [DF_User_UserID]  DEFAULT (newsequentialid()),
	[Email] [varchar](200) NULL,
	[Password] [varchar](200) NULL,
	[Name] [varchar](200) NULL,
	[Address] [varchar](200) NULL,
	[Phone] [varchar](100) NULL,
	[Role] [uniqueidentifier] NULL,
	[Status] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[Category] ([CategoryID], [CategoryName], [ClosureDate], [FinalClosureDate], [Faculty_DetailID], [Description], [Status]) VALUES (N'54cb45d2-e546-e911-9d2d-e4115bf5193d', N'Program C', CAST(N'2019-03-30 23:59:00.000' AS DateTime), CAST(N'2019-04-08 23:59:00.000' AS DateTime), N'9d58d5a5-8146-e911-9d2c-e4115bf5193d', N'It is base of the subject.', 1)
INSERT [dbo].[Category] ([CategoryID], [CategoryName], [ClosureDate], [FinalClosureDate], [Faculty_DetailID], [Description], [Status]) VALUES (N'99629fe2-e646-e911-9d2d-e4115bf5193d', N'Interaction Design', CAST(N'2019-04-08 23:59:00.000' AS DateTime), CAST(N'2019-04-15 23:59:00.000' AS DateTime), N'c6f6e670-d646-e911-9d2d-e4115bf5193d', N'It is an important subject of faculty.', 1)
INSERT [dbo].[Category] ([CategoryID], [CategoryName], [ClosureDate], [FinalClosureDate], [Faculty_DetailID], [Description], [Status]) VALUES (N'a2e35465-6148-e911-9d31-e4115bf5193d', N'Manage document and statistical', CAST(N'2019-03-10 10:03:00.000' AS DateTime), CAST(N'2019-04-10 23:59:00.000' AS DateTime), N'688cecc8-ce46-e911-9d2d-e4115bf5193d', N'It describe about away manage document, help you can learn anything.', 1)
INSERT [dbo].[Category] ([CategoryID], [CategoryName], [ClosureDate], [FinalClosureDate], [Faculty_DetailID], [Description], [Status]) VALUES (N'a4a24ff1-845a-e911-9d62-e4115bf5193d', N'.NET', CAST(N'2019-04-09 00:00:00.000' AS DateTime), CAST(N'2019-04-30 23:59:00.000' AS DateTime), N'c6f6e670-d646-e911-9d2d-e4115bf5193d', N'IDE is visual studio', 1)
INSERT [dbo].[Comment] ([CommentID], [Content], [Creator], [Contribution], [CommentDate], [Status]) VALUES (N'bf531fcf-ca48-e911-9d31-e4115bf5193d', N'It is so very good. I will publish your document as soon as possible', N'4d0eec35-f497-4e1b-ae0e-5239cba7dfd1', N'37648d6b-4b47-e911-9d2f-e4115bf5193d', CAST(N'2019-03-17 22:39:11.520' AS DateTime), 1)
INSERT [dbo].[Comment] ([CommentID], [Content], [Creator], [Contribution], [CommentDate], [Status]) VALUES (N'83ff8e6f-cd48-e911-9d31-e4115bf5193d', N'OK, thank you', N'533a2e8c-a9da-4177-a1bd-50427945f615', N'37648d6b-4b47-e911-9d2f-e4115bf5193d', CAST(N'2019-03-17 22:57:59.697' AS DateTime), 1)
INSERT [dbo].[Comment] ([CommentID], [Content], [Creator], [Contribution], [CommentDate], [Status]) VALUES (N'aa3b5a9a-cd48-e911-9d31-e4115bf5193d', N'I wish you will response as soon', N'533a2e8c-a9da-4177-a1bd-50427945f615', N'9b288632-4b47-e911-9d2f-e4115bf5193d', CAST(N'2019-03-17 22:59:11.657' AS DateTime), 1)
INSERT [dbo].[Comment] ([CommentID], [Content], [Creator], [Contribution], [CommentDate], [Status]) VALUES (N'0fcf88b9-cd48-e911-9d31-e4115bf5193d', N'It is OK if you can response soon for me', N'533a2e8c-a9da-4177-a1bd-50427945f615', N'85d3dd45-4b47-e911-9d2f-e4115bf5193d', CAST(N'2019-03-17 23:00:03.953' AS DateTime), 1)
INSERT [dbo].[Comment] ([CommentID], [Content], [Creator], [Contribution], [CommentDate], [Status]) VALUES (N'4c5d5a34-ce48-e911-9d31-e4115bf5193d', N'Wish you response soon. I''m thanks', N'40f441f7-ccae-4c68-a225-89e112c64cab', N'7456e9d0-4b47-e911-9d2f-e4115bf5193d', CAST(N'2019-03-17 23:03:30.030' AS DateTime), 1)
INSERT [dbo].[Comment] ([CommentID], [Content], [Creator], [Contribution], [CommentDate], [Status]) VALUES (N'97c019fb-d748-e911-9d31-e4115bf5193d', N'Have a nice day.', N'533a2e8c-a9da-4177-a1bd-50427945f615', N'37648d6b-4b47-e911-9d2f-e4115bf5193d', CAST(N'2019-03-18 00:13:28.790' AS DateTime), 1)
INSERT [dbo].[Comment] ([CommentID], [Content], [Creator], [Contribution], [CommentDate], [Status]) VALUES (N'1872e0bb-d848-e911-9d31-e4115bf5193d', N'No problems.', N'4d0eec35-f497-4e1b-ae0e-5239cba7dfd1', N'37648d6b-4b47-e911-9d2f-e4115bf5193d', CAST(N'2019-03-18 00:18:52.203' AS DateTime), 1)
INSERT [dbo].[Comment] ([CommentID], [Content], [Creator], [Contribution], [CommentDate], [Status]) VALUES (N'7ea11c23-3049-e911-9d32-e4115bf5193d', N'You should additional a image.', N'66b168f7-d719-462f-a858-147bb68a4e58', N'9355389d-4b47-e911-9d2f-e4115bf5193d', CAST(N'2019-03-18 10:44:31.583' AS DateTime), 1)
INSERT [dbo].[Comment] ([CommentID], [Content], [Creator], [Contribution], [CommentDate], [Status]) VALUES (N'9da9481e-3249-e911-9d32-e4115bf5193d', N'Oh, I received your email.Let wait my comment', N'66b168f7-d719-462f-a858-147bb68a4e58', N'eebb82c4-3049-e911-9d32-e4115bf5193d', CAST(N'2019-03-18 10:58:42.670' AS DateTime), 1)
INSERT [dbo].[Comment] ([CommentID], [Content], [Creator], [Contribution], [CommentDate], [Status]) VALUES (N'176876ca-d14b-e911-9d38-e4115bf5193d', N'dasjhdkashd', N'4d0eec35-f497-4e1b-ae0e-5239cba7dfd1', N'7456e9d0-4b47-e911-9d2f-e4115bf5193d', CAST(N'2019-03-21 19:06:43.630' AS DateTime), 1)
INSERT [dbo].[Comment] ([CommentID], [Content], [Creator], [Contribution], [CommentDate], [Status]) VALUES (N'6f6c2a9e-024c-e911-9d39-e4115bf5193d', N'You should upload image for document', N'4d0eec35-f497-4e1b-ae0e-5239cba7dfd1', N'7456e9d0-4b47-e911-9d2f-e4115bf5193d', CAST(N'2019-03-22 00:56:14.687' AS DateTime), 1)
INSERT [dbo].[Comment] ([CommentID], [Content], [Creator], [Contribution], [CommentDate], [Status]) VALUES (N'acac65e7-024c-e911-9d39-e4115bf5193d', N'I uploaded image, thank you', N'40f441f7-ccae-4c68-a225-89e112c64cab', N'7456e9d0-4b47-e911-9d2f-e4115bf5193d', CAST(N'2019-03-22 00:58:17.733' AS DateTime), 1)
INSERT [dbo].[Comment] ([CommentID], [Content], [Creator], [Contribution], [CommentDate], [Status]) VALUES (N'd9f65911-034c-e911-9d39-e4115bf5193d', N'OK, I published', N'66b168f7-d719-462f-a858-147bb68a4e58', N'9b288632-4b47-e911-9d2f-e4115bf5193d', CAST(N'2019-03-22 00:59:28.120' AS DateTime), 1)
INSERT [dbo].[Comment] ([CommentID], [Content], [Creator], [Contribution], [CommentDate], [Status]) VALUES (N'd906282a-034c-e911-9d39-e4115bf5193d', N'OK, I published', N'66b168f7-d719-462f-a858-147bb68a4e58', N'85d3dd45-4b47-e911-9d2f-e4115bf5193d', CAST(N'2019-03-22 01:00:09.737' AS DateTime), 1)
INSERT [dbo].[Comment] ([CommentID], [Content], [Creator], [Contribution], [CommentDate], [Status]) VALUES (N'84f3b6ec-cb4c-e911-9d3c-e4115bf5193d', N'I think, it''s OK', N'533a2e8c-a9da-4177-a1bd-50427945f615', N'7456e9d0-4b47-e911-9d2f-e4115bf5193d', CAST(N'2019-03-23 00:57:15.307' AS DateTime), 1)
INSERT [dbo].[Comment] ([CommentID], [Content], [Creator], [Contribution], [CommentDate], [Status]) VALUES (N'68ff3b7a-234d-e911-9d3d-e4115bf5193d', N'Thank you so much', N'533a2e8c-a9da-4177-a1bd-50427945f615', N'85d3dd45-4b47-e911-9d2f-e4115bf5193d', CAST(N'2019-03-23 11:23:58.937' AS DateTime), 1)
INSERT [dbo].[Comment] ([CommentID], [Content], [Creator], [Contribution], [CommentDate], [Status]) VALUES (N'45742884-234d-e911-9d3d-e4115bf5193d', N'Thank you', N'533a2e8c-a9da-4177-a1bd-50427945f615', N'9b288632-4b47-e911-9d2f-e4115bf5193d', CAST(N'2019-03-23 11:24:15.793' AS DateTime), 1)
INSERT [dbo].[Comment] ([CommentID], [Content], [Creator], [Contribution], [CommentDate], [Status]) VALUES (N'59d0bec3-925b-e911-9d65-e4115bf5193d', N'Very useful, thanks guy.', N'40f441f7-ccae-4c68-a225-89e112c64cab', N'9b288632-4b47-e911-9d2f-e4115bf5193d', CAST(N'2019-04-10 20:15:52.687' AS DateTime), 2)
INSERT [dbo].[Comment] ([CommentID], [Content], [Creator], [Contribution], [CommentDate], [Status]) VALUES (N'0008f4d0-925b-e911-9d65-e4115bf5193d', N'I think so', N'4d0eec35-f497-4e1b-ae0e-5239cba7dfd1', N'9b288632-4b47-e911-9d2f-e4115bf5193d', CAST(N'2019-04-10 20:16:15.050' AS DateTime), 2)
INSERT [dbo].[Comment] ([CommentID], [Content], [Creator], [Contribution], [CommentDate], [Status]) VALUES (N'81ba55ee-925b-e911-9d65-e4115bf5193d', N'I don''t thinks so. I think it is less important content.', N'16ba30bf-526e-47df-a19b-efd8de7a4803', N'9b288632-4b47-e911-9d2f-e4115bf5193d', CAST(N'2019-04-10 20:17:04.343' AS DateTime), 2)
INSERT [dbo].[Comment] ([CommentID], [Content], [Creator], [Contribution], [CommentDate], [Status]) VALUES (N'653b1c01-935b-e911-9d65-e4115bf5193d', N'It is very good, content is very well.', N'16ba30bf-526e-47df-a19b-efd8de7a4803', N'37648d6b-4b47-e911-9d2f-e4115bf5193d', CAST(N'2019-04-10 20:17:35.843' AS DateTime), 2)
INSERT [dbo].[Comment] ([CommentID], [Content], [Creator], [Contribution], [CommentDate], [Status]) VALUES (N'b185906a-0b60-e911-9d6c-e4115bf5193d', N'I think so.', N'b2f03c0f-b359-e911-9d5f-e4115bf5193d', N'9b288632-4b47-e911-9d2f-e4115bf5193d', CAST(N'2019-04-16 12:49:36.840' AS DateTime), 2)
INSERT [dbo].[Comment] ([CommentID], [Content], [Creator], [Contribution], [CommentDate], [Status]) VALUES (N'2b254c78-0b60-e911-9d6c-e4115bf5193d', N'It is OK', N'b2f03c0f-b359-e911-9d5f-e4115bf5193d', N'85d3dd45-4b47-e911-9d2f-e4115bf5193d', CAST(N'2019-04-16 12:50:00.067' AS DateTime), 2)
INSERT [dbo].[Comment] ([CommentID], [Content], [Creator], [Contribution], [CommentDate], [Status]) VALUES (N'5226f586-0b60-e911-9d6c-e4115bf5193d', N'very useful, I will reference it.', N'b2f03c0f-b359-e911-9d5f-e4115bf5193d', N'37648d6b-4b47-e911-9d2f-e4115bf5193d', CAST(N'2019-04-16 12:50:24.660' AS DateTime), 2)
INSERT [dbo].[Comment] ([CommentID], [Content], [Creator], [Contribution], [CommentDate], [Status]) VALUES (N'2641fd93-0b60-e911-9d6c-e4115bf5193d', N'Oh perfect describe.', N'b2f03c0f-b359-e911-9d5f-e4115bf5193d', N'7456e9d0-4b47-e911-9d2f-e4115bf5193d', CAST(N'2019-04-16 12:50:46.523' AS DateTime), 2)
INSERT [dbo].[Contribution] ([ContributionID], [Title], [Description], [CreationDate], [Category], [Creator], [Status]) VALUES (N'9b288632-4b47-e911-9d2f-e4115bf5193d', N'Cognition in interaction design', N'It is so OK', CAST(N'2019-03-22 12:29:26.187' AS DateTime), N'99629fe2-e646-e911-9d2d-e4115bf5193d', N'533a2e8c-a9da-4177-a1bd-50427945f615', 2)
INSERT [dbo].[Contribution] ([ContributionID], [Title], [Description], [CreationDate], [Category], [Creator], [Status]) VALUES (N'85d3dd45-4b47-e911-9d2f-e4115bf5193d', N'Smart home device in interaction', N'It is very simple', CAST(N'2019-03-22 11:09:45.227' AS DateTime), N'99629fe2-e646-e911-9d2d-e4115bf5193d', N'533a2e8c-a9da-4177-a1bd-50427945f615', 2)
INSERT [dbo].[Contribution] ([ContributionID], [Title], [Description], [CreationDate], [Category], [Creator], [Status]) VALUES (N'37648d6b-4b47-e911-9d2f-e4115bf5193d', N'This program is a simple example', N'It very easy understand language C', CAST(N'2019-03-16 16:26:27.473' AS DateTime), N'54cb45d2-e546-e911-9d2d-e4115bf5193d', N'533a2e8c-a9da-4177-a1bd-50427945f615', 2)
INSERT [dbo].[Contribution] ([ContributionID], [Title], [Description], [CreationDate], [Category], [Creator], [Status]) VALUES (N'9355389d-4b47-e911-9d2f-e4115bf5193d', N'Cognition in interaction design', N'It research about thinking, reasoning, etc.', CAST(N'2019-03-16 00:56:10.723' AS DateTime), N'99629fe2-e646-e911-9d2d-e4115bf5193d', N'40f441f7-ccae-4c68-a225-89e112c64cab', 1)
INSERT [dbo].[Contribution] ([ContributionID], [Title], [Description], [CreationDate], [Category], [Creator], [Status]) VALUES (N'7456e9d0-4b47-e911-9d2f-e4115bf5193d', N'An example for program, language C', N'A design program and report about language C', CAST(N'2019-03-18 00:30:21.200' AS DateTime), N'54cb45d2-e546-e911-9d2d-e4115bf5193d', N'40f441f7-ccae-4c68-a225-89e112c64cab', 2)
INSERT [dbo].[Contribution] ([ContributionID], [Title], [Description], [CreationDate], [Category], [Creator], [Status]) VALUES (N'eebb82c4-3049-e911-9d32-e4115bf5193d', N'Design website and interaction', N'A subject very open for this faculty', CAST(N'2019-03-18 10:49:50.060' AS DateTime), N'99629fe2-e646-e911-9d2d-e4115bf5193d', N'40f441f7-ccae-4c68-a225-89e112c64cab', 1)
INSERT [dbo].[Contribution] ([ContributionID], [Title], [Description], [CreationDate], [Category], [Creator], [Status]) VALUES (N'1c99ca7a-0d4f-e911-9d42-e4115bf5193d', N'ABC', N'Quality Systems in IT -Assignment_Frontsheet', CAST(N'2019-03-26 19:29:43.800' AS DateTime), N'a2e35465-6148-e911-9d31-e4115bf5193d', N'40f441f7-ccae-4c68-a225-89e112c64cab', 2)
INSERT [dbo].[Contribution] ([ContributionID], [Title], [Description], [CreationDate], [Category], [Creator], [Status]) VALUES (N'227b7d28-855a-e911-9d62-e4115bf5193d', N'An example for program .NET', N'description about an example and explain it', CAST(N'2019-04-09 12:05:57.777' AS DateTime), N'a4a24ff1-845a-e911-9d62-e4115bf5193d', N'533a2e8c-a9da-4177-a1bd-50427945f615', 1)
INSERT [dbo].[Contribution] ([ContributionID], [Title], [Description], [CreationDate], [Category], [Creator], [Status]) VALUES (N'c0129530-4e60-e911-9d6e-e4115bf5193d', N'Skill manage in business', N'Analysis some main skills in business and give examples for them', CAST(N'2019-04-16 20:47:35.797' AS DateTime), N'a2e35465-6148-e911-9d31-e4115bf5193d', N'ea88487b-20fe-4132-9f01-fc58ea6cf125', 1)
INSERT [dbo].[Contribution] ([ContributionID], [Title], [Description], [CreationDate], [Category], [Creator], [Status]) VALUES (N'43d515c7-4e60-e911-9d6e-e4115bf5193d', N'Strategies in business', N'Introduction some strategies and give example, explain them', CAST(N'2019-04-16 20:51:48.527' AS DateTime), N'a2e35465-6148-e911-9d31-e4115bf5193d', N'533a2e8c-a9da-4177-a1bd-50427945f615', 1)
INSERT [dbo].[Contribution] ([ContributionID], [Title], [Description], [CreationDate], [Category], [Creator], [Status]) VALUES (N'43ca9fb1-4f60-e911-9d6e-e4115bf5193d', N'Project sem 3 about .net', N'Description about application and explain', CAST(N'2019-04-16 20:58:22.017' AS DateTime), N'a4a24ff1-845a-e911-9d62-e4115bf5193d', N'ea88487b-20fe-4132-9f01-fc58ea6cf125', 1)
INSERT [dbo].[Document] ([DocumentID], [DocumentName], [Path], [UploadDate], [Contribution]) VALUES (N'9c288632-4b47-e911-9d2f-e4115bf5193d', N'Employability and Professional Development', N'~/Files/Employability and Professional Development.docx', CAST(N'2019-03-22 12:29:26.187' AS DateTime), N'9b288632-4b47-e911-9d2f-e4115bf5193d')
INSERT [dbo].[Document] ([DocumentID], [DocumentName], [Path], [UploadDate], [Contribution]) VALUES (N'86d3dd45-4b47-e911-9d2f-e4115bf5193d', N'meet', N'~/Files/meet.docx', CAST(N'2019-03-22 11:09:45.227' AS DateTime), N'85d3dd45-4b47-e911-9d2f-e4115bf5193d')
INSERT [dbo].[Document] ([DocumentID], [DocumentName], [Path], [UploadDate], [Contribution]) VALUES (N'38648d6b-4b47-e911-9d2f-e4115bf5193d', N'SAMPLE TEMPLATE ', N'~/Files/SAMPLE TEMPLATE .docx', CAST(N'2019-03-16 23:29:06.713' AS DateTime), N'37648d6b-4b47-e911-9d2f-e4115bf5193d')
INSERT [dbo].[Document] ([DocumentID], [DocumentName], [Path], [UploadDate], [Contribution]) VALUES (N'9455389d-4b47-e911-9d2f-e4115bf5193d', N'Pham-Xuan-Tien-EPD', N'~/Files/Pham-Xuan-Tien-EPD.docx', CAST(N'2019-03-16 00:56:10.723' AS DateTime), N'9355389d-4b47-e911-9d2f-e4115bf5193d')
INSERT [dbo].[Document] ([DocumentID], [DocumentName], [Path], [UploadDate], [Contribution]) VALUES (N'7556e9d0-4b47-e911-9d2f-e4115bf5193d', N'doc-1', N'~/Files/doc-1.pdf', CAST(N'2019-03-18 00:30:21.200' AS DateTime), N'7456e9d0-4b47-e911-9d2f-e4115bf5193d')
INSERT [dbo].[Document] ([DocumentID], [DocumentName], [Path], [UploadDate], [Contribution]) VALUES (N'efbb82c4-3049-e911-9d32-e4115bf5193d', N'1286_tungndgc00952_Assignment1', N'~/Files/1286_tungndgc00952_Assignment1.docx', CAST(N'2019-03-18 10:49:50.060' AS DateTime), N'eebb82c4-3049-e911-9d32-e4115bf5193d')
INSERT [dbo].[Document] ([DocumentID], [DocumentName], [Path], [UploadDate], [Contribution]) VALUES (N'1d99ca7a-0d4f-e911-9d42-e4115bf5193d', N'Quality Systems in IT -Assignment_Frontsheet', N'~/Files/Quality Systems in IT -Assignment_Frontsheet.docx', CAST(N'2019-03-26 19:29:43.800' AS DateTime), N'1c99ca7a-0d4f-e911-9d42-e4115bf5193d')
INSERT [dbo].[Document] ([DocumentID], [DocumentName], [Path], [UploadDate], [Contribution]) VALUES (N'237b7d28-855a-e911-9d62-e4115bf5193d', N'1537_tungndgc00952_Assignment2', N'~/Files/1537_tungndgc00952_Assignment2.docx', CAST(N'2019-04-09 12:05:57.777' AS DateTime), N'227b7d28-855a-e911-9d62-e4115bf5193d')
INSERT [dbo].[Document] ([DocumentID], [DocumentName], [Path], [UploadDate], [Contribution]) VALUES (N'c1129530-4e60-e911-9d6e-e4115bf5193d', N'1525_tungndgc00952_Assignment1', N'~/Files/1525_tungndgc00952_Assignment1.docx', CAST(N'2019-04-16 20:47:35.797' AS DateTime), N'c0129530-4e60-e911-9d6e-e4115bf5193d')
INSERT [dbo].[Document] ([DocumentID], [DocumentName], [Path], [UploadDate], [Contribution]) VALUES (N'44d515c7-4e60-e911-9d6e-e4115bf5193d', N'1525_tungndgc00952_Assignment2', N'~/Files/1525_tungndgc00952_Assignment2.docx', CAST(N'2019-04-16 20:51:48.527' AS DateTime), N'43d515c7-4e60-e911-9d6e-e4115bf5193d')
INSERT [dbo].[Document] ([DocumentID], [DocumentName], [Path], [UploadDate], [Contribution]) VALUES (N'44ca9fb1-4f60-e911-9d6e-e4115bf5193d', N'1537_tungndgc00952_Assignment3', N'~/Files/1537_tungndgc00952_Assignment3.docx', CAST(N'2019-04-16 20:58:22.017' AS DateTime), N'43ca9fb1-4f60-e911-9d6e-e4115bf5193d')
INSERT [dbo].[Faculty] ([FacultyID], [FacultyName], [Description]) VALUES (N'46c0049f-7046-e911-9d2c-e4115bf5193d', N'Information Technology', N'qwew')
INSERT [dbo].[Faculty] ([FacultyID], [FacultyName], [Description]) VALUES (N'13e594a7-7146-e911-9d2c-e4115bf5193d', N'Marketing Business', N'It is so OK')
INSERT [dbo].[Faculty] ([FacultyID], [FacultyName], [Description]) VALUES (N'8b565beb-7146-e911-9d2c-e4115bf5193d', N'Artificial Intelligence', N'A hot trend of technology. it is very beautiful')
INSERT [dbo].[Faculty_Detail] ([Faculty_DetailID], [FacultyID], [UserCoordinator]) VALUES (N'9d58d5a5-8146-e911-9d2c-e4115bf5193d', N'8b565beb-7146-e911-9d2c-e4115bf5193d', N'4d0eec35-f497-4e1b-ae0e-5239cba7dfd1')
INSERT [dbo].[Faculty_Detail] ([Faculty_DetailID], [FacultyID], [UserCoordinator]) VALUES (N'688cecc8-ce46-e911-9d2d-e4115bf5193d', N'13e594a7-7146-e911-9d2c-e4115bf5193d', N'4d0eec35-f497-4e1b-ae0e-5239cba7dfd1')
INSERT [dbo].[Faculty_Detail] ([Faculty_DetailID], [FacultyID], [UserCoordinator]) VALUES (N'c6f6e670-d646-e911-9d2d-e4115bf5193d', N'46c0049f-7046-e911-9d2c-e4115bf5193d', N'66b168f7-d719-462f-a858-147bb68a4e58')
INSERT [dbo].[Image] ([ImageID], [ImageName], [Description], [Path], [UploadDate], [Contribution]) VALUES (N'09ffe888-5e48-e911-9d31-e4115bf5193d', N'VietNam', N'It is an image.', N'~/Images/VietNam.png', CAST(N'2019-03-17 09:44:08.047' AS DateTime), N'37648d6b-4b47-e911-9d2f-e4115bf5193d')
INSERT [dbo].[Image] ([ImageID], [ImageName], [Description], [Path], [UploadDate], [Contribution]) VALUES (N'6fbcb099-5e48-e911-9d31-e4115bf5193d', N'abc', N'It is an image.', N'~/Images/abc.png', CAST(N'2019-03-22 11:09:45.227' AS DateTime), N'85d3dd45-4b47-e911-9d2f-e4115bf5193d')
INSERT [dbo].[Image] ([ImageID], [ImageName], [Description], [Path], [UploadDate], [Contribution]) VALUES (N'797eeab1-024c-e911-9d39-e4115bf5193d', N'Nhat', N'It is an image.', N'~/Images/Nhat.png', CAST(N'2019-03-22 00:56:47.977' AS DateTime), N'7456e9d0-4b47-e911-9d2f-e4115bf5193d')
INSERT [dbo].[Image] ([ImageID], [ImageName], [Description], [Path], [UploadDate], [Contribution]) VALUES (N'49e8e7be-624c-e911-9d3a-e4115bf5193d', N'Communication', N'It is an image.', N'~/Images/Communication.jpg', CAST(N'2019-03-22 12:29:26.187' AS DateTime), N'9b288632-4b47-e911-9d2f-e4115bf5193d')
INSERT [dbo].[Image] ([ImageID], [ImageName], [Description], [Path], [UploadDate], [Contribution]) VALUES (N'cb9eecaa-c24f-e911-9d44-e4115bf5193d', N'hd1', N'It is an image.', N'~/Images/hd1.jpg', CAST(N'2019-03-26 19:29:43.800' AS DateTime), N'1c99ca7a-0d4f-e911-9d42-e4115bf5193d')
INSERT [dbo].[Role] ([RoleID], [RoleName], [Description]) VALUES (N'7036266e-2d6e-4913-b730-02ecbe1ecc5b', N'Guest Account', N'Guest Account')
INSERT [dbo].[Role] ([RoleID], [RoleName], [Description]) VALUES (N'4c42d248-eac8-4e75-9541-42541245b667', N'Student', N'Student')
INSERT [dbo].[Role] ([RoleID], [RoleName], [Description]) VALUES (N'559c2837-e749-44ae-b9da-9cf2135e33bb', N'Marketing Coordinator', N'Marketing Coordinator')
INSERT [dbo].[Role] ([RoleID], [RoleName], [Description]) VALUES (N'e29d3ff8-4cb9-4c64-83b2-b15dff5eb895', N'Marketing Manager', N'Marketing Manager')
INSERT [dbo].[Role] ([RoleID], [RoleName], [Description]) VALUES (N'a7b58d6f-bf1f-469a-8075-e177ca53b757', N'Administrator', N'Administrator')
INSERT [dbo].[User] ([UserID], [Email], [Password], [Name], [Address], [Phone], [Role], [Status]) VALUES (N'66b168f7-d719-462f-a858-147bb68a4e58', N'thanhldgc00959@fpt.edu.vn', N'37213902101311706410244100199109113607173', N'ThanhLD', N'Ha Noi', N'1234567890', N'559c2837-e749-44ae-b9da-9cf2135e33bb', 1)
INSERT [dbo].[User] ([UserID], [Email], [Password], [Name], [Address], [Phone], [Role], [Status]) VALUES (N'da26e35d-b2f1-4500-bbb1-16f0127d35b0', N'trungntch18001@fpt.edu.vn', N'37213902101311706410244100199109113607173', N'TrungNT', N'Ha Noi', N'0987654321', N'559c2837-e749-44ae-b9da-9cf2135e33bb', 1)
INSERT [dbo].[User] ([UserID], [Email], [Password], [Name], [Address], [Phone], [Role], [Status]) VALUES (N'533a2e8c-a9da-4177-a1bd-50427945f615', N'tungndgc00952@fpt.edu.vn', N'37213902101311706410244100199109113607173', N'TungND', N'Ha Noi', N'0123456789', N'4c42d248-eac8-4e75-9541-42541245b667', 1)
INSERT [dbo].[User] ([UserID], [Email], [Password], [Name], [Address], [Phone], [Role], [Status]) VALUES (N'4d0eec35-f497-4e1b-ae0e-5239cba7dfd1', N'tuann@fpt.aptech.ac.vn', N'37213902101311706410244100199109113607173', N'TuanN', N'Ha Noi', N'1234567890', N'559c2837-e749-44ae-b9da-9cf2135e33bb', 1)
INSERT [dbo].[User] ([UserID], [Email], [Password], [Name], [Address], [Phone], [Role], [Status]) VALUES (N'164ff138-c1a5-4533-9653-61be45991225', N'longnvgc00982@fpt.edu.vn', N'37213902101311706410244100199109113607173', N'LongNV', N'Ha Noi', N'1234567890', N'559c2837-e749-44ae-b9da-9cf2135e33bb', 1)
INSERT [dbo].[User] ([UserID], [Email], [Password], [Name], [Address], [Phone], [Role], [Status]) VALUES (N'40f441f7-ccae-4c68-a225-89e112c64cab', N'alwaysrememberyouth@gmail.com', N'37213902101311706410244100199109113607173', N'TungND1', N'Ha Noi', N'5678998765', N'4c42d248-eac8-4e75-9541-42541245b667', 1)
INSERT [dbo].[User] ([UserID], [Email], [Password], [Name], [Address], [Phone], [Role], [Status]) VALUES (N'c3dc3e59-0249-4fda-af80-a774d5e25b0c', N'duongpt@fe.edu.vn', N'37213902101311706410244100199109113607173', N'DuongPT', N'Ha Noi', N'4567887654', N'a7b58d6f-bf1f-469a-8075-e177ca53b757', 1)
INSERT [dbo].[User] ([UserID], [Email], [Password], [Name], [Address], [Phone], [Role], [Status]) VALUES (N'edbf1e2f-3b1d-4b0c-9f77-d664b0a6db54', N'hannn@fe.edu.vn', N'37213902101311706410244100199109113607173', N'HanNN', N'Ha Noi', N'0965826780', N'4c42d248-eac8-4e75-9541-42541245b667', 1)
INSERT [dbo].[User] ([UserID], [Email], [Password], [Name], [Address], [Phone], [Role], [Status]) VALUES (N'b2f03c0f-b359-e911-9d5f-e4115bf5193d', N'ducpagc00970@fpt.edu.vn', N'37213902101311706410244100199109113607173', N'ducpagc00970', N'Ha Noi', N'0987654345', N'7036266e-2d6e-4913-b730-02ecbe1ecc5b', 1)
INSERT [dbo].[User] ([UserID], [Email], [Password], [Name], [Address], [Phone], [Role], [Status]) VALUES (N'16ba30bf-526e-47df-a19b-efd8de7a4803', N'canhndgt00545@fpt.edu.vn', N'37213902101311706410244100199109113607173', N'CanhND', N'Ha Noi', N'2345665432', N'e29d3ff8-4cb9-4c64-83b2-b15dff5eb895', 1)
INSERT [dbo].[User] ([UserID], [Email], [Password], [Name], [Address], [Phone], [Role], [Status]) VALUES (N'ea88487b-20fe-4132-9f01-fc58ea6cf125', N'thangpdgch15325@fpt.edu.vn', N'37213902101311706410244100199109113607173', N'ThangPD', N'Ha Noi', N'3456776543', N'4c42d248-eac8-4e75-9541-42541245b667', 1)
ALTER TABLE [dbo].[Category]  WITH CHECK ADD FOREIGN KEY([Faculty_DetailID])
REFERENCES [dbo].[Faculty_Detail] ([Faculty_DetailID])
GO
ALTER TABLE [dbo].[Comment]  WITH CHECK ADD FOREIGN KEY([Contribution])
REFERENCES [dbo].[Contribution] ([ContributionID])
GO
ALTER TABLE [dbo].[Comment]  WITH CHECK ADD FOREIGN KEY([Creator])
REFERENCES [dbo].[User] ([UserID])
GO
ALTER TABLE [dbo].[Contribution]  WITH CHECK ADD FOREIGN KEY([Category])
REFERENCES [dbo].[Category] ([CategoryID])
GO
ALTER TABLE [dbo].[Contribution]  WITH CHECK ADD FOREIGN KEY([Creator])
REFERENCES [dbo].[User] ([UserID])
GO
ALTER TABLE [dbo].[Document]  WITH CHECK ADD FOREIGN KEY([Contribution])
REFERENCES [dbo].[Contribution] ([ContributionID])
GO
ALTER TABLE [dbo].[Faculty_Detail]  WITH CHECK ADD FOREIGN KEY([FacultyID])
REFERENCES [dbo].[Faculty] ([FacultyID])
GO
ALTER TABLE [dbo].[Faculty_Detail]  WITH CHECK ADD FOREIGN KEY([UserCoordinator])
REFERENCES [dbo].[User] ([UserID])
GO
ALTER TABLE [dbo].[Image]  WITH CHECK ADD FOREIGN KEY([Contribution])
REFERENCES [dbo].[Contribution] ([ContributionID])
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD FOREIGN KEY([Role])
REFERENCES [dbo].[Role] ([RoleID])
GO
