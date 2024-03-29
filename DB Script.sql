USE [master]
GO
/****** Object:  Database [ApplicationOutage]    Script Date: 03/05/2019 21:55:22 ******/
CREATE DATABASE [ApplicationOutage] ON  PRIMARY 
( NAME = N'ApplicationOutage', FILENAME = N'C:\Program Files (x86)\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\ApplicationOutage.mdf' , SIZE = 2048KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'ApplicationOutage_log', FILENAME = N'C:\Program Files (x86)\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\ApplicationOutage_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [ApplicationOutage] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ApplicationOutage].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ApplicationOutage] SET ANSI_NULL_DEFAULT OFF
GO
ALTER DATABASE [ApplicationOutage] SET ANSI_NULLS OFF
GO
ALTER DATABASE [ApplicationOutage] SET ANSI_PADDING OFF
GO
ALTER DATABASE [ApplicationOutage] SET ANSI_WARNINGS OFF
GO
ALTER DATABASE [ApplicationOutage] SET ARITHABORT OFF
GO
ALTER DATABASE [ApplicationOutage] SET AUTO_CLOSE OFF
GO
ALTER DATABASE [ApplicationOutage] SET AUTO_CREATE_STATISTICS ON
GO
ALTER DATABASE [ApplicationOutage] SET AUTO_SHRINK OFF
GO
ALTER DATABASE [ApplicationOutage] SET AUTO_UPDATE_STATISTICS ON
GO
ALTER DATABASE [ApplicationOutage] SET CURSOR_CLOSE_ON_COMMIT OFF
GO
ALTER DATABASE [ApplicationOutage] SET CURSOR_DEFAULT  GLOBAL
GO
ALTER DATABASE [ApplicationOutage] SET CONCAT_NULL_YIELDS_NULL OFF
GO
ALTER DATABASE [ApplicationOutage] SET NUMERIC_ROUNDABORT OFF
GO
ALTER DATABASE [ApplicationOutage] SET QUOTED_IDENTIFIER OFF
GO
ALTER DATABASE [ApplicationOutage] SET RECURSIVE_TRIGGERS OFF
GO
ALTER DATABASE [ApplicationOutage] SET  DISABLE_BROKER
GO
ALTER DATABASE [ApplicationOutage] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO
ALTER DATABASE [ApplicationOutage] SET DATE_CORRELATION_OPTIMIZATION OFF
GO
ALTER DATABASE [ApplicationOutage] SET TRUSTWORTHY OFF
GO
ALTER DATABASE [ApplicationOutage] SET ALLOW_SNAPSHOT_ISOLATION OFF
GO
ALTER DATABASE [ApplicationOutage] SET PARAMETERIZATION SIMPLE
GO
ALTER DATABASE [ApplicationOutage] SET READ_COMMITTED_SNAPSHOT OFF
GO
ALTER DATABASE [ApplicationOutage] SET HONOR_BROKER_PRIORITY OFF
GO
ALTER DATABASE [ApplicationOutage] SET  READ_WRITE
GO
ALTER DATABASE [ApplicationOutage] SET RECOVERY SIMPLE
GO
ALTER DATABASE [ApplicationOutage] SET  MULTI_USER
GO
ALTER DATABASE [ApplicationOutage] SET PAGE_VERIFY CHECKSUM
GO
ALTER DATABASE [ApplicationOutage] SET DB_CHAINING OFF
GO
USE [ApplicationOutage]
GO
/****** Object:  Table [dbo].[Application]    Script Date: 03/05/2019 21:55:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Application](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ApplicationName] [nvarchar](150) NOT NULL,
 CONSTRAINT [PK_Application] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[YearlyAvailablity]    Script Date: 03/05/2019 21:55:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[YearlyAvailablity](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Year] [int] NOT NULL,
	[Month] [int] NOT NULL,
	[TotalAvailablity] [int] NOT NULL,
 CONSTRAINT [PK_YearlyAvailablity] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Outage]    Script Date: 03/05/2019 21:55:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Outage](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ApplicationID] [int] NOT NULL,
	[StartDate] [datetime] NOT NULL,
	[EndDate] [datetime] NOT NULL,
	[Impact] [nvarchar](50) NULL,
	[Description] [nvarchar](max) NULL,
	[Component] [nvarchar](50) NULL,
 CONSTRAINT [PK_Outage] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  ForeignKey [FK_Outage_Application]    Script Date: 03/05/2019 21:55:24 ******/
ALTER TABLE [dbo].[Outage]  WITH CHECK ADD  CONSTRAINT [FK_Outage_Application] FOREIGN KEY([ApplicationID])
REFERENCES [dbo].[Application] ([ID])
GO
ALTER TABLE [dbo].[Outage] CHECK CONSTRAINT [FK_Outage_Application]
GO
