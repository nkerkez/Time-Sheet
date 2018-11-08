/*    ==Scripting Parameters==

    Source Server Version : SQL Server 2016 (13.0.4001)
    Source Database Engine Edition : Microsoft SQL Server Enterprise Edition
    Source Database Engine Type : Standalone SQL Server

    Target Server Version : SQL Server 2017
    Target Database Engine Edition : Microsoft SQL Server Standard Edition
    Target Database Engine Type : Standalone SQL Server
*/
USE [master]
GO
/****** Object:  Database [TimeSheet]    Script Date: 10.8.2018. 16.05.27 ******/
CREATE DATABASE [TimeSheet]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'TimeSheet', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER2016\MSSQL\DATA\TimeSheet.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'TimeSheet_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER2016\MSSQL\DATA\TimeSheet_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [TimeSheet] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [TimeSheet].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [TimeSheet] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [TimeSheet] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [TimeSheet] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [TimeSheet] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [TimeSheet] SET ARITHABORT OFF 
GO
ALTER DATABASE [TimeSheet] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [TimeSheet] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [TimeSheet] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [TimeSheet] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [TimeSheet] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [TimeSheet] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [TimeSheet] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [TimeSheet] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [TimeSheet] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [TimeSheet] SET  DISABLE_BROKER 
GO
ALTER DATABASE [TimeSheet] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [TimeSheet] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [TimeSheet] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [TimeSheet] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [TimeSheet] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [TimeSheet] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [TimeSheet] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [TimeSheet] SET RECOVERY FULL 
GO
ALTER DATABASE [TimeSheet] SET  MULTI_USER 
GO
ALTER DATABASE [TimeSheet] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [TimeSheet] SET DB_CHAINING OFF 
GO
ALTER DATABASE [TimeSheet] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [TimeSheet] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [TimeSheet] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'TimeSheet', N'ON'
GO
ALTER DATABASE [TimeSheet] SET QUERY_STORE = OFF
GO
USE [TimeSheet]
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO
USE [TimeSheet]
GO
/****** Object:  Table [dbo].[Client]    Script Date: 10.8.2018. 16.05.27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Client](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Address] [nvarchar](max) NULL,
	[City] [nvarchar](max) NULL,
	[PostalCode] [nvarchar](max) NULL,
	[CountryId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Client] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Country]    Script Date: 10.8.2018. 16.05.27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Country](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Country] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Index [IX_Country]    Script Date: 10.8.2018. 16.05.27 ******/
CREATE NONCLUSTERED INDEX [IX_Country] ON [dbo].[Country]
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Client]  WITH CHECK ADD  CONSTRAINT [FK_Client_Country] FOREIGN KEY([CountryId])
REFERENCES [dbo].[Country] ([Id])
GO
ALTER TABLE [dbo].[Client] CHECK CONSTRAINT [FK_Client_Country]
GO
/****** Object:  StoredProcedure [dbo].[spDeleteClient]    Script Date: 10.8.2018. 16.05.27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Nikola,,Kerkez>
-- Create date: <02.08.2018>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[spDeleteClient]
	-- Add the parameters for the stored procedure here
	@ClientId uniqueidentifier
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT OFF;

    -- Insert statements for procedure here
	DELETE FROM dbo.Client where Id = @ClientId
END
GO
/****** Object:  StoredProcedure [dbo].[spDeleteCountry]    Script Date: 10.8.2018. 16.05.27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Nikola Kerkez>
-- Create date: <31.07.2018>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[spDeleteCountry]
	-- Add the parameters for the stored procedure here
	@CountryId uniqueidentifier
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT OFF;

    -- Insert statements for procedure here
	DELETE FROM dbo.Country where Id = @CountryId
END
GO
/****** Object:  StoredProcedure [dbo].[spFilterClientByFirstLetterOfName]    Script Date: 10.8.2018. 16.05.27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Nikola Kerkez>
-- Create date: <02.08.2018>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[spFilterClientByFirstLetterOfName]
	-- Add the parameters for the stored procedure here
	@Letter char
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * from dbo.Client where Name like @Letter+'%';
END
GO
/****** Object:  StoredProcedure [dbo].[spFilterClientByName]    Script Date: 10.8.2018. 16.05.27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Nikola Kerkez>
-- Create date: <02.08.2018>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[spFilterClientByName]
	-- Add the parameters for the stored procedure here
	@Name nvarchar(MAX)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * from dbo.Client where Name like '%'+@Name+'%';
END
GO
/****** Object:  StoredProcedure [dbo].[spGetClientById]    Script Date: 10.8.2018. 16.05.27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Nikola,,Kerkez>
-- Create date: <02.08.2018>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[spGetClientById]
	-- Add the parameters for the stored procedure here
	@ClientId uniqueidentifier
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * from dbo.Client where Id = @ClientId
END
GO
/****** Object:  StoredProcedure [dbo].[spGetClients]    Script Date: 10.8.2018. 16.05.27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Nikola,,Kerkez>
-- Create date: <02.08.2018>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[spGetClients]
	-- Add the parameters for the stored procedure here
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * from dbo.Client;
END
GO
/****** Object:  StoredProcedure [dbo].[spGetCountries]    Script Date: 10.8.2018. 16.05.27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Nikola Kerkez>
-- Create date: <31.07.2018>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[spGetCountries]
	-- Add the parameters for the stored procedure here
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * from dbo.Country;
END
GO
/****** Object:  StoredProcedure [dbo].[spGetCountryById]    Script Date: 10.8.2018. 16.05.27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Nikola Kerkez
-- Create date: <20.11.1993>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[spGetCountryById]
	-- Add the parameters for the stored procedure here
	@CountryId uniqueidentifier
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * from dbo.Country where Id = @CountryId
END
GO
/****** Object:  StoredProcedure [dbo].[spInsertClient]    Script Date: 10.8.2018. 16.05.27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Nikola,,Kerkez>
-- Create date: <02.08.2018>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[spInsertClient]
	-- Add the parameters for the stored procedure here
	@ClientId uniqueidentifier, @ClientName nvarchar(MAX), @ClientCity nvarchar(MAX)=0, @ClientAddress nvarchar(MAX)=0, @ClientPostalCode nvarchar(MAX)=0, @CountryId uniqueidentifier
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT OFF;

    -- Insert statements for procedure here
	INSERT INTO dbo.Client values (@ClientId, @ClientName, @ClientAddress, @ClientCity, @ClientPostalCode, @CountryId)
END
GO
/****** Object:  StoredProcedure [dbo].[spInsertCountry]    Script Date: 10.8.2018. 16.05.27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Nikola Kerkez>
-- Create date: <31.07.2018>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[spInsertCountry]
	-- Add the parameters for the stored procedure here
	@CountryId uniqueidentifier,
	@CountryName nvarchar(MAX)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO dbo.Country values (@CountryId, @CountryName)
END
GO
/****** Object:  StoredProcedure [dbo].[spUpdateClient]    Script Date: 10.8.2018. 16.05.27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Nikola,,Kerkez>
-- Create date: <02.08.2018>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[spUpdateClient]
	-- Add the parameters for the stored procedure here
	@ClientId uniqueidentifier, @ClientName nvarchar(MAX), @ClientCity nvarchar(MAX)= null, @ClientAddress nvarchar(MAX) = null, @ClientPostalCode nvarchar(MAX) = null, @CountryId uniqueidentifier
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT OFF;

    -- Insert statements for procedure here
	UPDATE  dbo.Client SET  Name = @ClientName, Address =  @ClientAddress, City = @ClientCity, PostalCode = @ClientPostalCode, CountryId = @CountryId where Id = @ClientId
END
GO
/****** Object:  StoredProcedure [dbo].[spUpdateCountry]    Script Date: 10.8.2018. 16.05.27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Nikola Kerkez>
-- Create date: <31.07.2018>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[spUpdateCountry]
	-- Add the parameters for the stored procedure here
	@CountryId uniqueidentifier,
	@CountryName nvarchar(MAX)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT OFF;

    -- Insert statements for procedure here
	UPDATE dbo.Country set Name = @CountryName where Id = @CountryId 
END
GO
USE [master]
GO
ALTER DATABASE [TimeSheet] SET  READ_WRITE 
GO
