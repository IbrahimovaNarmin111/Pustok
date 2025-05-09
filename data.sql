USE [master]
GO
/****** Object:  Database [PustokBB103]    Script Date: 11/17/2023 1:15:22 PM ******/
CREATE DATABASE [PustokBB103]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'PustokBB103', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\PustokBB103.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'PustokBB103_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\PustokBB103_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [PustokBB103] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [PustokBB103].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [PustokBB103] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [PustokBB103] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [PustokBB103] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [PustokBB103] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [PustokBB103] SET ARITHABORT OFF 
GO
ALTER DATABASE [PustokBB103] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [PustokBB103] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [PustokBB103] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [PustokBB103] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [PustokBB103] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [PustokBB103] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [PustokBB103] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [PustokBB103] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [PustokBB103] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [PustokBB103] SET  ENABLE_BROKER 
GO
ALTER DATABASE [PustokBB103] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [PustokBB103] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [PustokBB103] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [PustokBB103] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [PustokBB103] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [PustokBB103] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [PustokBB103] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [PustokBB103] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [PustokBB103] SET  MULTI_USER 
GO
ALTER DATABASE [PustokBB103] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [PustokBB103] SET DB_CHAINING OFF 
GO
ALTER DATABASE [PustokBB103] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [PustokBB103] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [PustokBB103] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [PustokBB103] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [PustokBB103] SET QUERY_STORE = OFF
GO
USE [PustokBB103]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 11/17/2023 1:15:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Authors]    Script Date: 11/17/2023 1:15:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Authors](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FullName] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Authors] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BookImages]    Script Date: 11/17/2023 1:15:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BookImages](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IsPrimary] [bit] NULL,
	[Image] [nvarchar](max) NOT NULL,
	[BookId] [int] NOT NULL,
 CONSTRAINT [PK_BookImages] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Books]    Script Date: 11/17/2023 1:15:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Books](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Page] [int] NOT NULL,
	[CostPrice] [float] NOT NULL,
	[SalePrice] [float] NOT NULL,
	[Discount] [float] NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[AuthorId] [int] NOT NULL,
	[CategoryId] [int] NOT NULL,
 CONSTRAINT [PK_Books] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BookTags]    Script Date: 11/17/2023 1:15:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BookTags](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[BookId] [int] NOT NULL,
	[TagId] [int] NOT NULL,
 CONSTRAINT [PK_BookTags] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 11/17/2023 1:15:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Features]    Script Date: 11/17/2023 1:15:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Features](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Icon] [nvarchar](max) NOT NULL,
	[Title] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Features] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sliders]    Script Date: 11/17/2023 1:15:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sliders](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Image] [nvarchar](max) NOT NULL,
	[Title1] [nvarchar](max) NOT NULL,
	[Title2] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[Order] [int] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Sliders] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tags]    Script Date: 11/17/2023 1:15:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tags](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Tags] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20231116081236_CreateSlidersTable', N'7.0.14')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20231116081714_CreateFeaturesTable', N'7.0.14')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20231117074432_CreateAllTables', N'7.0.14')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20231117082319_CreateAllTables1', N'7.0.14')
GO
SET IDENTITY_INSERT [dbo].[Authors] ON 

INSERT [dbo].[Authors] ([Id], [FullName]) VALUES (1, N'Narmin Ibrahimova')
INSERT [dbo].[Authors] ([Id], [FullName]) VALUES (2, N'Aysu Mikayilzade')
INSERT [dbo].[Authors] ([Id], [FullName]) VALUES (3, N'Eliyeva Meryem')
INSERT [dbo].[Authors] ([Id], [FullName]) VALUES (4, N'Suleymanli Gulgez')
SET IDENTITY_INSERT [dbo].[Authors] OFF
GO
SET IDENTITY_INSERT [dbo].[BookImages] ON 

INSERT [dbo].[BookImages] ([Id], [IsPrimary], [Image], [BookId]) VALUES (1, 1, N'product-1.jpg', 1)
INSERT [dbo].[BookImages] ([Id], [IsPrimary], [Image], [BookId]) VALUES (2, 0, N'product-2.jpg', 1)
INSERT [dbo].[BookImages] ([Id], [IsPrimary], [Image], [BookId]) VALUES (3, 1, N'product-3.jpg', 2)
INSERT [dbo].[BookImages] ([Id], [IsPrimary], [Image], [BookId]) VALUES (4, 0, N'product-4.jpg', 2)
INSERT [dbo].[BookImages] ([Id], [IsPrimary], [Image], [BookId]) VALUES (5, 1, N'product-5.jpg', 3)
INSERT [dbo].[BookImages] ([Id], [IsPrimary], [Image], [BookId]) VALUES (6, 0, N'product-6.jpg', 3)
INSERT [dbo].[BookImages] ([Id], [IsPrimary], [Image], [BookId]) VALUES (7, 1, N'product-7.jpg', 4)
INSERT [dbo].[BookImages] ([Id], [IsPrimary], [Image], [BookId]) VALUES (8, 0, N'product-8.jpg', 4)
INSERT [dbo].[BookImages] ([Id], [IsPrimary], [Image], [BookId]) VALUES (9, 1, N'product-9.jpg', 5)
INSERT [dbo].[BookImages] ([Id], [IsPrimary], [Image], [BookId]) VALUES (10, 0, N'product-10.jpg', 5)
INSERT [dbo].[BookImages] ([Id], [IsPrimary], [Image], [BookId]) VALUES (12, 1, N'product-11.jpg', 6)
INSERT [dbo].[BookImages] ([Id], [IsPrimary], [Image], [BookId]) VALUES (13, 0, N'product-12.jpg', 6)
INSERT [dbo].[BookImages] ([Id], [IsPrimary], [Image], [BookId]) VALUES (14, 1, N'product-13.jpg', 7)
INSERT [dbo].[BookImages] ([Id], [IsPrimary], [Image], [BookId]) VALUES (15, NULL, N'product-2.jpg', 7)
INSERT [dbo].[BookImages] ([Id], [IsPrimary], [Image], [BookId]) VALUES (16, 1, N'product-3.jpg', 8)
INSERT [dbo].[BookImages] ([Id], [IsPrimary], [Image], [BookId]) VALUES (17, 0, N'product-5.jpg', 8)
SET IDENTITY_INSERT [dbo].[BookImages] OFF
GO
SET IDENTITY_INSERT [dbo].[Books] ON 

INSERT [dbo].[Books] ([Id], [Name], [Page], [CostPrice], [SalePrice], [Discount], [Description], [IsDeleted], [AuthorId], [CategoryId]) VALUES (1, N'Telebe drami', 200, 10, 20, 3, N'Drama haqqinda', 0, 1, 1)
INSERT [dbo].[Books] ([Id], [Name], [Page], [CostPrice], [SalePrice], [Discount], [Description], [IsDeleted], [AuthorId], [CategoryId]) VALUES (2, N'Yay tetili', 100, 8, 12, 1, N'Tetil', 0, 2, 2)
INSERT [dbo].[Books] ([Id], [Name], [Page], [CostPrice], [SalePrice], [Discount], [Description], [IsDeleted], [AuthorId], [CategoryId]) VALUES (3, N'Proqramlasdirma', 150, 9, 15, 2, N'Coding', 0, 4, 4)
INSERT [dbo].[Books] ([Id], [Name], [Page], [CostPrice], [SalePrice], [Discount], [Description], [IsDeleted], [AuthorId], [CategoryId]) VALUES (4, N'Pozitiv olmanin yollari', 90, 7, 10, 1, N'Pozitiv olun', 0, 3, 3)
INSERT [dbo].[Books] ([Id], [Name], [Page], [CostPrice], [SalePrice], [Discount], [Description], [IsDeleted], [AuthorId], [CategoryId]) VALUES (5, N'Super High', 50, 8, 13, 2, N'Lorem Ipsum', 0, 1, 1)
INSERT [dbo].[Books] ([Id], [Name], [Page], [CostPrice], [SalePrice], [Discount], [Description], [IsDeleted], [AuthorId], [CategoryId]) VALUES (6, N'Grow Flower', 80, 10, 14, 3, N'About Flower', 0, 2, 4)
INSERT [dbo].[Books] ([Id], [Name], [Page], [CostPrice], [SalePrice], [Discount], [Description], [IsDeleted], [AuthorId], [CategoryId]) VALUES (7, N'The Winter', 95, 7, 10, 2, N'About Winter', 0, 3, 3)
INSERT [dbo].[Books] ([Id], [Name], [Page], [CostPrice], [SalePrice], [Discount], [Description], [IsDeleted], [AuthorId], [CategoryId]) VALUES (8, N'Seker Portakali', 100, 8, 10, 1, N'Lorem Ipsum', 0, 4, 1)
SET IDENTITY_INSERT [dbo].[Books] OFF
GO
SET IDENTITY_INSERT [dbo].[BookTags] ON 

INSERT [dbo].[BookTags] ([Id], [BookId], [TagId]) VALUES (1, 1, 1)
INSERT [dbo].[BookTags] ([Id], [BookId], [TagId]) VALUES (2, 2, 2)
INSERT [dbo].[BookTags] ([Id], [BookId], [TagId]) VALUES (3, 3, 1)
INSERT [dbo].[BookTags] ([Id], [BookId], [TagId]) VALUES (4, 4, 2)
INSERT [dbo].[BookTags] ([Id], [BookId], [TagId]) VALUES (5, 5, 1)
INSERT [dbo].[BookTags] ([Id], [BookId], [TagId]) VALUES (6, 6, 2)
INSERT [dbo].[BookTags] ([Id], [BookId], [TagId]) VALUES (7, 7, 1)
INSERT [dbo].[BookTags] ([Id], [BookId], [TagId]) VALUES (8, 8, 1)
SET IDENTITY_INSERT [dbo].[BookTags] OFF
GO
SET IDENTITY_INSERT [dbo].[Categories] ON 

INSERT [dbo].[Categories] ([Id], [Name]) VALUES (1, N'Drama')
INSERT [dbo].[Categories] ([Id], [Name]) VALUES (2, N'Comedy')
INSERT [dbo].[Categories] ([Id], [Name]) VALUES (3, N'Action')
INSERT [dbo].[Categories] ([Id], [Name]) VALUES (4, N'Science')
SET IDENTITY_INSERT [dbo].[Categories] OFF
GO
SET IDENTITY_INSERT [dbo].[Features] ON 

INSERT [dbo].[Features] ([Id], [Icon], [Title], [Description]) VALUES (1, N'fas fa-shipping-fast', N'Dasinma', N'500$ edir')
INSERT [dbo].[Features] ([Id], [Icon], [Title], [Description]) VALUES (2, N'fas fa-redo-alt', N'Pul geri qaytarilmasi', N'100% pul qaytarilir')
INSERT [dbo].[Features] ([Id], [Icon], [Title], [Description]) VALUES (3, N'fas fa-piggy-bank', N'Catdirilma Pulu', N'Odenis')
INSERT [dbo].[Features] ([Id], [Icon], [Title], [Description]) VALUES (4, N'fas fa-life-ring', N'Komek ve Destek', N'Bize Zeng edin')
SET IDENTITY_INSERT [dbo].[Features] OFF
GO
SET IDENTITY_INSERT [dbo].[Sliders] ON 

INSERT [dbo].[Sliders] ([Id], [Image], [Title1], [Title2], [Description], [Order], [IsDeleted]) VALUES (1, N'home-slider-1-ai.png', N'Nermin', N'Ibrahimova', N'Kitablarim', 1, 0)
INSERT [dbo].[Sliders] ([Id], [Image], [Title1], [Title2], [Description], [Order], [IsDeleted]) VALUES (2, N'home-slider-2-ai.png', N'Aysu', N'Mikayilzade', N'Kitablarim', 2, 0)
SET IDENTITY_INSERT [dbo].[Sliders] OFF
GO
SET IDENTITY_INSERT [dbo].[Tags] ON 

INSERT [dbo].[Tags] ([Id], [Name]) VALUES (1, N'Best Seller')
INSERT [dbo].[Tags] ([Id], [Name]) VALUES (2, N'New')
SET IDENTITY_INSERT [dbo].[Tags] OFF
GO
/****** Object:  Index [IX_BookImages_BookId]    Script Date: 11/17/2023 1:15:22 PM ******/
CREATE NONCLUSTERED INDEX [IX_BookImages_BookId] ON [dbo].[BookImages]
(
	[BookId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Books_AuthorId]    Script Date: 11/17/2023 1:15:22 PM ******/
CREATE NONCLUSTERED INDEX [IX_Books_AuthorId] ON [dbo].[Books]
(
	[AuthorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Books_CategoryId]    Script Date: 11/17/2023 1:15:22 PM ******/
CREATE NONCLUSTERED INDEX [IX_Books_CategoryId] ON [dbo].[Books]
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_BookTags_BookId]    Script Date: 11/17/2023 1:15:22 PM ******/
CREATE NONCLUSTERED INDEX [IX_BookTags_BookId] ON [dbo].[BookTags]
(
	[BookId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_BookTags_TagId]    Script Date: 11/17/2023 1:15:22 PM ******/
CREATE NONCLUSTERED INDEX [IX_BookTags_TagId] ON [dbo].[BookTags]
(
	[TagId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Sliders] ADD  DEFAULT (CONVERT([bit],(0))) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[BookImages]  WITH CHECK ADD  CONSTRAINT [FK_BookImages_Books_BookId] FOREIGN KEY([BookId])
REFERENCES [dbo].[Books] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[BookImages] CHECK CONSTRAINT [FK_BookImages_Books_BookId]
GO
ALTER TABLE [dbo].[Books]  WITH CHECK ADD  CONSTRAINT [FK_Books_Authors_AuthorId] FOREIGN KEY([AuthorId])
REFERENCES [dbo].[Authors] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Books] CHECK CONSTRAINT [FK_Books_Authors_AuthorId]
GO
ALTER TABLE [dbo].[Books]  WITH CHECK ADD  CONSTRAINT [FK_Books_Categories_CategoryId] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Categories] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Books] CHECK CONSTRAINT [FK_Books_Categories_CategoryId]
GO
ALTER TABLE [dbo].[BookTags]  WITH CHECK ADD  CONSTRAINT [FK_BookTags_Books_BookId] FOREIGN KEY([BookId])
REFERENCES [dbo].[Books] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[BookTags] CHECK CONSTRAINT [FK_BookTags_Books_BookId]
GO
ALTER TABLE [dbo].[BookTags]  WITH CHECK ADD  CONSTRAINT [FK_BookTags_Tags_TagId] FOREIGN KEY([TagId])
REFERENCES [dbo].[Tags] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[BookTags] CHECK CONSTRAINT [FK_BookTags_Tags_TagId]
GO
USE [master]
GO
ALTER DATABASE [PustokBB103] SET  READ_WRITE 
GO
