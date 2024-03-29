USE [N5Now]
GO
/****** Object:  Table [dbo].[Employee]    Script Date: 02/02/2024 15:19:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employee](
	[IdEmployee] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](100) NOT NULL,
	[LastName] [varchar](100) NOT NULL,
	[Age] [tinyint] NOT NULL,
	[Company] [varchar](100) NOT NULL,
	[Department] [varchar](100) NOT NULL,
	[State] [bit] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[CreationUser] [varchar](50) NOT NULL,
	[LastModificationDate] [datetime] NOT NULL,
	[LastModificationUser] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdEmployee] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Permission]    Script Date: 02/02/2024 15:19:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Permission](
	[IdPermission] [int] IDENTITY(1,1) NOT NULL,
	[IdEmployee] [int] NOT NULL,
	[IdTypePermission] [int] NOT NULL,
	[DateFrom] [datetime] NOT NULL,
	[DateUntil] [datetime] NOT NULL,
	[Observation] [varchar](100) NOT NULL,
	[State] [bit] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[CreationUser] [varchar](50) NOT NULL,
	[LastModificationDate] [datetime] NOT NULL,
	[LastModificationUser] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdPermission] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TypePermission]    Script Date: 02/02/2024 15:19:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TypePermission](
	[IdTypePermission] [int] IDENTITY(1,1) NOT NULL,
	[Description] [varchar](100) NOT NULL,
	[State] [bit] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[CreationUser] [varchar](50) NOT NULL,
	[LastModificationDate] [datetime] NOT NULL,
	[LastModificationUser] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdTypePermission] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[Permission]  WITH CHECK ADD  CONSTRAINT [FK_Permission_Employee] FOREIGN KEY([IdEmployee])
REFERENCES [dbo].[Employee] ([IdEmployee])
GO
ALTER TABLE [dbo].[Permission] CHECK CONSTRAINT [FK_Permission_Employee]
GO
ALTER TABLE [dbo].[Permission]  WITH CHECK ADD  CONSTRAINT [FK_Permission_TypePermission] FOREIGN KEY([IdTypePermission])
REFERENCES [dbo].[TypePermission] ([IdTypePermission])
GO
ALTER TABLE [dbo].[Permission] CHECK CONSTRAINT [FK_Permission_TypePermission]
GO
