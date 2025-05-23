USE [master]
GO
/****** Object:  Database [quanlyeventskhachsan]    Script Date: 1/4/2025 2:03:23 PM ******/
CREATE DATABASE [quanlyeventskhachsan]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'quanlyeventskhachsan', FILENAME = N'D:\quanlyeventskhachsan.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'quanlyeventskhachsan_log', FILENAME = N'D:\quanlyeventskhachsan_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [quanlyeventskhachsan] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [quanlyeventskhachsan].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [quanlyeventskhachsan] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [quanlyeventskhachsan] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [quanlyeventskhachsan] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [quanlyeventskhachsan] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [quanlyeventskhachsan] SET ARITHABORT OFF 
GO
ALTER DATABASE [quanlyeventskhachsan] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [quanlyeventskhachsan] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [quanlyeventskhachsan] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [quanlyeventskhachsan] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [quanlyeventskhachsan] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [quanlyeventskhachsan] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [quanlyeventskhachsan] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [quanlyeventskhachsan] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [quanlyeventskhachsan] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [quanlyeventskhachsan] SET  DISABLE_BROKER 
GO
ALTER DATABASE [quanlyeventskhachsan] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [quanlyeventskhachsan] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [quanlyeventskhachsan] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [quanlyeventskhachsan] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [quanlyeventskhachsan] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [quanlyeventskhachsan] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [quanlyeventskhachsan] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [quanlyeventskhachsan] SET RECOVERY FULL 
GO
ALTER DATABASE [quanlyeventskhachsan] SET  MULTI_USER 
GO
ALTER DATABASE [quanlyeventskhachsan] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [quanlyeventskhachsan] SET DB_CHAINING OFF 
GO
ALTER DATABASE [quanlyeventskhachsan] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [quanlyeventskhachsan] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [quanlyeventskhachsan] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [quanlyeventskhachsan] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [quanlyeventskhachsan] SET QUERY_STORE = ON
GO
ALTER DATABASE [quanlyeventskhachsan] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [quanlyeventskhachsan]
GO
/****** Object:  Table [dbo].[CO_SO]    Script Date: 1/4/2025 2:03:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CO_SO](
	[MaCoSo] [varchar](50) NOT NULL,
	[TenCoSo] [nvarchar](50) NULL,
	[SDT] [varchar](10) NULL,
	[Email] [varchar](50) NULL,
	[NgayThanhLap] [date] NULL,
	[Hide] [bit] NULL,
 CONSTRAINT [PK_CO_SO] PRIMARY KEY CLUSTERED 
(
	[MaCoSo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DANH_GIA]    Script Date: 1/4/2025 2:03:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DANH_GIA](
	[MaDanhGia] [varchar](10) NOT NULL,
	[MaHD] [int] NOT NULL,
	[MSNV] [varchar](10) NULL,
	[NoiDung] [ntext] NULL,
	[Hide] [bit] NULL,
 CONSTRAINT [PK_DANH_GIA_1] PRIMARY KEY CLUSTERED 
(
	[MaDanhGia] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DANHGIA_DETAILS]    Script Date: 1/4/2025 2:03:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DANHGIA_DETAILS](
	[MaDanhGia] [varchar](10) NOT NULL,
	[HangMuc] [int] NOT NULL,
	[MucDo] [int] NULL,
	[GhiChu] [ntext] NULL,
 CONSTRAINT [PK_DANHGIA_DETAILS] PRIMARY KEY CLUSTERED 
(
	[MaDanhGia] ASC,
	[HangMuc] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DOI_TAC]    Script Date: 1/4/2025 2:03:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DOI_TAC](
	[ID_DoiTac] [int] IDENTITY(1,1) NOT NULL,
	[TenDoiTac] [nvarchar](100) NULL,
	[DaiDien] [nvarchar](50) NULL,
	[SDT] [varchar](10) NULL,
	[Email] [varchar](50) NULL,
	[Hide] [bit] NULL,
 CONSTRAINT [PK_DOI_TAC] PRIMARY KEY CLUSTERED 
(
	[ID_DoiTac] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HANG_MUC]    Script Date: 1/4/2025 2:03:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HANG_MUC](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TenHangMuc] [ntext] NULL,
	[Hide] [bit] NULL,
 CONSTRAINT [PK_HANG_MUC] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HD_DOITAC]    Script Date: 1/4/2025 2:03:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HD_DOITAC](
	[ID_DoiTac] [int] NOT NULL,
	[MaHD] [int] NOT NULL,
	[NoiDung] [ntext] NULL,
 CONSTRAINT [PK_DT_HOATDONG] PRIMARY KEY CLUSTERED 
(
	[ID_DoiTac] ASC,
	[MaHD] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HD_NHAN_VIEN]    Script Date: 1/4/2025 2:03:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HD_NHAN_VIEN](
	[MaHD] [int] NOT NULL,
	[MSNV] [varchar](10) NOT NULL,
	[VaiTro] [nvarchar](50) NULL,
	[GhiChu] [ntext] NULL,
 CONSTRAINT [PK_NV_HOATDONG] PRIMARY KEY CLUSTERED 
(
	[MaHD] ASC,
	[MSNV] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HD_QUANLY]    Script Date: 1/4/2025 2:03:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HD_QUANLY](
	[MaHD] [int] NOT NULL,
	[MaQL] [varchar](50) NOT NULL,
	[VaiTro] [nvarchar](50) NULL,
 CONSTRAINT [PK_HD_QUANLY] PRIMARY KEY CLUSTERED 
(
	[MaHD] ASC,
	[MaQL] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HD_TAITRO]    Script Date: 1/4/2025 2:03:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HD_TAITRO](
	[ID_TaiTro] [int] NOT NULL,
	[MaHD] [int] NOT NULL,
	[NoiDung] [ntext] NULL,
 CONSTRAINT [PK_HD_TAITRO] PRIMARY KEY CLUSTERED 
(
	[ID_TaiTro] ASC,
	[MaHD] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HOAT_DONG]    Script Date: 1/4/2025 2:03:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HOAT_DONG](
	[MaHD] [int] NOT NULL,
	[TenHoatDong] [ntext] NULL,
	[Loai] [nvarchar](50) NULL,
	[NgayBatDau] [datetime] NULL,
	[NgayKetThuc] [datetime] NULL,
	[Hide] [bit] NULL,
	[CreatedDate] [datetime] NULL,
 CONSTRAINT [PK_HOAT_DONG] PRIMARY KEY CLUSTERED 
(
	[MaHD] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NHAN_VIEN]    Script Date: 1/4/2025 2:03:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NHAN_VIEN](
	[MSNV] [varchar](10) NOT NULL,
	[HoTen] [nvarchar](100) NULL,
	[CoSo] [varchar](50) NULL,
	[Hide] [bit] NULL,
 CONSTRAINT [PK_SINH_VIEN] PRIMARY KEY CLUSTERED 
(
	[MSNV] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PHU_TRACH]    Script Date: 1/4/2025 2:03:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PHU_TRACH](
	[MaQL] [varchar](50) NOT NULL,
	[SuKien] [nvarchar](50) NOT NULL,
	[GhiChu] [ntext] NULL,
 CONSTRAINT [PK_PHU_TRACH_1] PRIMARY KEY CLUSTERED 
(
	[MaQL] ASC,
	[SuKien] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[QUAN_LY]    Script Date: 1/4/2025 2:03:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QUAN_LY](
	[MaQL] [varchar](50) NOT NULL,
	[HoTenLot] [nvarchar](50) NULL,
	[Ten] [nvarchar](50) NULL,
	[CoSo] [varchar](50) NULL,
	[Hide] [bit] NULL,
 CONSTRAINT [PK_QUAN_LY] PRIMARY KEY CLUSTERED 
(
	[MaQL] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TAI_CHINH]    Script Date: 1/4/2025 2:03:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TAI_CHINH](
	[ID_TaiChinh] [int] IDENTITY(1,1) NOT NULL,
	[MaHD] [int] NOT NULL,
	[UEF] [decimal](12, 0) NULL,
	[TaiTro] [decimal](12, 0) NULL,
	[Khac] [ntext] NULL,
	[TieuDe] [ntext] NULL,
	[CreatedDate] [datetime] NULL,
	[Hide] [bit] NULL,
 CONSTRAINT [PK_TAI_CHINH] PRIMARY KEY CLUSTERED 
(
	[ID_TaiChinh] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TAI_TRO]    Script Date: 1/4/2025 2:03:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TAI_TRO](
	[ID_TaiTro] [int] IDENTITY(1,1) NOT NULL,
	[TenTaiTro] [nvarchar](100) NULL,
	[DaiDien] [nvarchar](50) NULL,
	[SDT] [varchar](10) NULL,
	[Email] [varchar](50) NULL,
	[Hide] [bit] NULL,
 CONSTRAINT [PK_TAI_TRO] PRIMARY KEY CLUSTERED 
(
	[ID_TaiTro] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[CO_SO] ([MaCoSo], [TenCoSo], [SDT], [Email], [NgayThanhLap], [Hide]) VALUES (N'CS001     ', N'Victoria', N'0911222333', N'victoria@gmail.com', CAST(N'2005-09-22' AS Date), 0)
INSERT [dbo].[CO_SO] ([MaCoSo], [TenCoSo], [SDT], [Email], [NgayThanhLap], [Hide]) VALUES (N'CS002     ', N'Hestia', N'0284222666', N'hestia@gmail.com', CAST(N'2000-06-14' AS Date), 0)
INSERT [dbo].[CO_SO] ([MaCoSo], [TenCoSo], [SDT], [Email], [NgayThanhLap], [Hide]) VALUES (N'CS003     ', N'Hera', N'0283534427', N'herat@gmail.com', CAST(N'2006-01-25' AS Date), 0)
INSERT [dbo].[CO_SO] ([MaCoSo], [TenCoSo], [SDT], [Email], [NgayThanhLap], [Hide]) VALUES (N'CS004     ', N'Eurynome', N'0952648445', N'eurynome@gmail.com', CAST(N'2016-01-05' AS Date), 0)
GO
INSERT [dbo].[DANH_GIA] ([MaDanhGia], [MaHD], [MSNV], [NoiDung], [Hide]) VALUES (N'1         ', 1, N'001126    ', NULL, NULL)
INSERT [dbo].[DANH_GIA] ([MaDanhGia], [MaHD], [MSNV], [NoiDung], [Hide]) VALUES (N'2         ', 3, N'001925    ', N'event P12-5', NULL)
INSERT [dbo].[DANH_GIA] ([MaDanhGia], [MaHD], [MSNV], [NoiDung], [Hide]) VALUES (N'3         ', 2, N'002765    ', NULL, NULL)
INSERT [dbo].[DANH_GIA] ([MaDanhGia], [MaHD], [MSNV], [NoiDung], [Hide]) VALUES (N'4         ', 5, N'003189    ', NULL, NULL)
INSERT [dbo].[DANH_GIA] ([MaDanhGia], [MaHD], [MSNV], [NoiDung], [Hide]) VALUES (N'5         ', 8, N'003592    ', N'event Sảnh A', NULL)
GO
INSERT [dbo].[DANHGIA_DETAILS] ([MaDanhGia], [HangMuc], [MucDo], [GhiChu]) VALUES (N'1         ', 1, 5, NULL)
INSERT [dbo].[DANHGIA_DETAILS] ([MaDanhGia], [HangMuc], [MucDo], [GhiChu]) VALUES (N'1         ', 4, 3, NULL)
INSERT [dbo].[DANHGIA_DETAILS] ([MaDanhGia], [HangMuc], [MucDo], [GhiChu]) VALUES (N'3         ', 1, 2, NULL)
INSERT [dbo].[DANHGIA_DETAILS] ([MaDanhGia], [HangMuc], [MucDo], [GhiChu]) VALUES (N'5         ', 3, 4, NULL)
GO
SET IDENTITY_INSERT [dbo].[DOI_TAC] ON 

INSERT [dbo].[DOI_TAC] ([ID_DoiTac], [TenDoiTac], [DaiDien], [SDT], [Email], [Hide]) VALUES (7, N'Cty TNHH Hasaki Beauty & Spa', N'Nguyễn Văn A', N'0935455254', N'NhatMinhCorp@gmail.com', 0)
INSERT [dbo].[DOI_TAC] ([ID_DoiTac], [TenDoiTac], [DaiDien], [SDT], [Email], [Hide]) VALUES (8, N'Cty Cổ phần Victory', N'Chi Mot Minh Em', N'0285355647', N'lqdHigh@uef.edu.vn', 0)
INSERT [dbo].[DOI_TAC] ([ID_DoiTac], [TenDoiTac], [DaiDien], [SDT], [Email], [Hide]) VALUES (9, N'Đại Học Khoa Học Tự Nhiên TPHCM', N'Th.S Lê Trần Thị B', N'028665417 ', N'VLU@gmail.com', 0)
INSERT [dbo].[DOI_TAC] ([ID_DoiTac], [TenDoiTac], [DaiDien], [SDT], [Email], [Hide]) VALUES (10, N'Hợp tác xã Tu Tiên', N'Nguyễn Thanh Thản', N'025541265 ', N'TT@Bussiness.mail.com', 0)
INSERT [dbo].[DOI_TAC] ([ID_DoiTac], [TenDoiTac], [DaiDien], [SDT], [Email], [Hide]) VALUES (11, N'Tập Đoàn VINGRPOUP', N'Nguyễn Việt Quang', N'028665425 ', N'nvq@vin.com', 0)
SET IDENTITY_INSERT [dbo].[DOI_TAC] OFF
GO
SET IDENTITY_INSERT [dbo].[HANG_MUC] ON 

INSERT [dbo].[HANG_MUC] ([ID], [TenHangMuc], [Hide]) VALUES (1, N'Hạng Mục A', 0)
INSERT [dbo].[HANG_MUC] ([ID], [TenHangMuc], [Hide]) VALUES (2, N'Hạng Mục B', 0)
INSERT [dbo].[HANG_MUC] ([ID], [TenHangMuc], [Hide]) VALUES (3, N'Hạng Mục C', 0)
INSERT [dbo].[HANG_MUC] ([ID], [TenHangMuc], [Hide]) VALUES (4, N'Hạng Mục D', 0)
SET IDENTITY_INSERT [dbo].[HANG_MUC] OFF
GO
INSERT [dbo].[HD_DOITAC] ([ID_DoiTac], [MaHD], [NoiDung]) VALUES (9, 3, N'event P03-3')
INSERT [dbo].[HD_DOITAC] ([ID_DoiTac], [MaHD], [NoiDung]) VALUES (10, 3, N'event P12-5')
INSERT [dbo].[HD_DOITAC] ([ID_DoiTac], [MaHD], [NoiDung]) VALUES (11, 8, N'event Sảnh A')
GO
INSERT [dbo].[HD_NHAN_VIEN] ([MaHD], [MSNV], [VaiTro], [GhiChu]) VALUES (1, N'001126    ', N'Tham gia', N'Tiệc cưới')
INSERT [dbo].[HD_NHAN_VIEN] ([MaHD], [MSNV], [VaiTro], [GhiChu]) VALUES (2, N'001126    ', N'', N'Event 2.0')
INSERT [dbo].[HD_NHAN_VIEN] ([MaHD], [MSNV], [VaiTro], [GhiChu]) VALUES (2, N'001348    ', N'', N'Event 2.1')
INSERT [dbo].[HD_NHAN_VIEN] ([MaHD], [MSNV], [VaiTro], [GhiChu]) VALUES (2, N'001925    ', N'', N'Event 2.3')
INSERT [dbo].[HD_NHAN_VIEN] ([MaHD], [MSNV], [VaiTro], [GhiChu]) VALUES (2, N'002765    ', N'', N'Event 2.2')
INSERT [dbo].[HD_NHAN_VIEN] ([MaHD], [MSNV], [VaiTro], [GhiChu]) VALUES (3, N'001126    ', N'Tham gia', N'Cuộc thi')
INSERT [dbo].[HD_NHAN_VIEN] ([MaHD], [MSNV], [VaiTro], [GhiChu]) VALUES (3, N'001925    ', N'Tham gia', N'Cuộc thi')
INSERT [dbo].[HD_NHAN_VIEN] ([MaHD], [MSNV], [VaiTro], [GhiChu]) VALUES (4, N'001471    ', N'Tổ chức', N'Tiệc cty')
INSERT [dbo].[HD_NHAN_VIEN] ([MaHD], [MSNV], [VaiTro], [GhiChu]) VALUES (4, N'002765    ', N'Tham gia', N'Ti?c cty')
INSERT [dbo].[HD_NHAN_VIEN] ([MaHD], [MSNV], [VaiTro], [GhiChu]) VALUES (5, N'001126    ', N'', N'Event 5.0')
INSERT [dbo].[HD_NHAN_VIEN] ([MaHD], [MSNV], [VaiTro], [GhiChu]) VALUES (5, N'003189    ', N'', N'Event 5.1')
INSERT [dbo].[HD_NHAN_VIEN] ([MaHD], [MSNV], [VaiTro], [GhiChu]) VALUES (8, N'001348    ', N'Tổ chức', N'Sinh nhật')
INSERT [dbo].[HD_NHAN_VIEN] ([MaHD], [MSNV], [VaiTro], [GhiChu]) VALUES (8, N'002765    ', N'Tham gia', N'Sinh nhật')
INSERT [dbo].[HD_NHAN_VIEN] ([MaHD], [MSNV], [VaiTro], [GhiChu]) VALUES (8, N'003592    ', N'Tham gia', N'Liên hoan')
GO
INSERT [dbo].[HD_QUANLY] ([MaHD], [MaQL], [VaiTro]) VALUES (3, N'111567    ', N'Tổ chức')
INSERT [dbo].[HD_QUANLY] ([MaHD], [MaQL], [VaiTro]) VALUES (3, N'333472    ', N'Tham gia')
INSERT [dbo].[HD_QUANLY] ([MaHD], [MaQL], [VaiTro]) VALUES (4, N'111567    ', N'Tổ chức')
INSERT [dbo].[HD_QUANLY] ([MaHD], [MaQL], [VaiTro]) VALUES (8, N'111567    ', N'Tham gia')
INSERT [dbo].[HD_QUANLY] ([MaHD], [MaQL], [VaiTro]) VALUES (8, N'222159    ', N'Tổ chức')
GO
INSERT [dbo].[HD_TAITRO] ([ID_TaiTro], [MaHD], [NoiDung]) VALUES (1, 2, N'500,000,000đ')
INSERT [dbo].[HD_TAITRO] ([ID_TaiTro], [MaHD], [NoiDung]) VALUES (1, 4, N'Nhà tài trợ Vàng')
INSERT [dbo].[HD_TAITRO] ([ID_TaiTro], [MaHD], [NoiDung]) VALUES (1, 7, N'Nhà tài trợ Kim Cương')
GO
INSERT [dbo].[HOAT_DONG] ([MaHD], [TenHoatDong], [Loai], [NgayBatDau], [NgayKetThuc], [Hide], [CreatedDate]) VALUES (1, N'Event A', N'Sự kiện', CAST(N'2023-02-01T00:00:00.000' AS DateTime), CAST(N'2023-02-22T00:00:00.000' AS DateTime), 0, CAST(N'2023-10-23T20:07:04.250' AS DateTime))
INSERT [dbo].[HOAT_DONG] ([MaHD], [TenHoatDong], [Loai], [NgayBatDau], [NgayKetThuc], [Hide], [CreatedDate]) VALUES (2, N'Event B', N'Sự kiện', CAST(N'2023-10-08T12:45:42.000' AS DateTime), CAST(N'2023-10-17T12:45:42.000' AS DateTime), 0, CAST(N'2023-11-02T12:51:45.853' AS DateTime))
INSERT [dbo].[HOAT_DONG] ([MaHD], [TenHoatDong], [Loai], [NgayBatDau], [NgayKetThuc], [Hide], [CreatedDate]) VALUES (3, N'Event C', N'Sự kiện', CAST(N'2023-10-16T17:29:31.000' AS DateTime), CAST(N'2023-10-24T17:29:31.000' AS DateTime), 0, CAST(N'2023-11-02T12:50:25.457' AS DateTime))
INSERT [dbo].[HOAT_DONG] ([MaHD], [TenHoatDong], [Loai], [NgayBatDau], [NgayKetThuc], [Hide], [CreatedDate]) VALUES (4, N'Event D', N'Sự kiện', CAST(N'2023-10-15T17:35:04.000' AS DateTime), CAST(N'2023-10-17T17:35:04.000' AS DateTime), 0, CAST(N'2023-12-08T02:06:43.433' AS DateTime))
INSERT [dbo].[HOAT_DONG] ([MaHD], [TenHoatDong], [Loai], [NgayBatDau], [NgayKetThuc], [Hide], [CreatedDate]) VALUES (5, N'Event E', N'Sự kiện', CAST(N'2023-10-03T13:01:37.000' AS DateTime), CAST(N'2023-10-18T13:01:37.000' AS DateTime), 0, CAST(N'2023-10-22T01:09:48.267' AS DateTime))
INSERT [dbo].[HOAT_DONG] ([MaHD], [TenHoatDong], [Loai], [NgayBatDau], [NgayKetThuc], [Hide], [CreatedDate]) VALUES (7, N'Event 1 year', N'Sự kiện', CAST(N'2023-10-17T14:20:25.000' AS DateTime), CAST(N'2023-10-20T14:20:25.000' AS DateTime), 0, CAST(N'2023-10-20T15:11:47.680' AS DateTime))
INSERT [dbo].[HOAT_DONG] ([MaHD], [TenHoatDong], [Loai], [NgayBatDau], [NgayKetThuc], [Hide], [CreatedDate]) VALUES (8, N'Event 1 day', N'Sự kiện', CAST(N'2023-10-17T07:55:23.000' AS DateTime), CAST(N'2023-11-01T07:55:23.000' AS DateTime), 0, CAST(N'2023-10-31T07:58:58.703' AS DateTime))
INSERT [dbo].[HOAT_DONG] ([MaHD], [TenHoatDong], [Loai], [NgayBatDau], [NgayKetThuc], [Hide], [CreatedDate]) VALUES (9, N'Event 1 month', N'Sự kiện', CAST(N'2023-11-29T00:16:03.080' AS DateTime), CAST(N'2023-11-29T00:16:03.077' AS DateTime), 1, CAST(N'2023-11-29T00:16:04.183' AS DateTime))
GO
INSERT [dbo].[NHAN_VIEN] ([MSNV], [HoTen], [CoSo], [Hide]) VALUES (N'001126    ', N'Nguyen Trai', N'CS001     ', 0)
INSERT [dbo].[NHAN_VIEN] ([MSNV], [HoTen], [CoSo], [Hide]) VALUES (N'001348    ', N'Nguyễn Huệ', N'CS001     ', 0)
INSERT [dbo].[NHAN_VIEN] ([MSNV], [HoTen], [CoSo], [Hide]) VALUES (N'001471    ', N'Đinh Bộ Lĩnh', N'CS001     ', 0)
INSERT [dbo].[NHAN_VIEN] ([MSNV], [HoTen], [CoSo], [Hide]) VALUES (N'001925    ', N'Tôn Nữ Diễm Quỳnh', N'CS001     ', 0)
INSERT [dbo].[NHAN_VIEN] ([MSNV], [HoTen], [CoSo], [Hide]) VALUES (N'002765    ', N'Nguyễn Thị Đẹp', N'CS002     ', 0)
INSERT [dbo].[NHAN_VIEN] ([MSNV], [HoTen], [CoSo], [Hide]) VALUES (N'003189    ', N'Trần Thị Dậu', N'CS003     ', 0)
INSERT [dbo].[NHAN_VIEN] ([MSNV], [HoTen], [CoSo], [Hide]) VALUES (N'003592    ', N'Lê Vui Tươi', N'CS003     ', 0)
GO
INSERT [dbo].[PHU_TRACH] ([MaQL], [SuKien], [GhiChu]) VALUES (N'111567    ', N'LienHoan', NULL)
INSERT [dbo].[PHU_TRACH] ([MaQL], [SuKien], [GhiChu]) VALUES (N'111567    ', N'Tiec', NULL)
INSERT [dbo].[PHU_TRACH] ([MaQL], [SuKien], [GhiChu]) VALUES (N'222159    ', N'SinhNhat', NULL)
GO
INSERT [dbo].[QUAN_LY] ([MaQL], [HoTenLot], [Ten], [CoSo], [Hide]) VALUES (N'111567    ', N'Nguyễn Trần An', N'Kiên', N'CS001     ', 0)
INSERT [dbo].[QUAN_LY] ([MaQL], [HoTenLot], [Ten], [CoSo], [Hide]) VALUES (N'222159    ', N'Mai Thanh', N'Thảo', N'CS002     ', 0)
INSERT [dbo].[QUAN_LY] ([MaQL], [HoTenLot], [Ten], [CoSo], [Hide]) VALUES (N'333472    ', N'Ngô Tất', N'Tố', N'CS003     ', 0)
INSERT [dbo].[QUAN_LY] ([MaQL], [HoTenLot], [Ten], [CoSo], [Hide]) VALUES (N'444512', N'Nguyễn', N'Huệ', N'CS004     ', 0)
GO
SET IDENTITY_INSERT [dbo].[TAI_CHINH] ON 

INSERT [dbo].[TAI_CHINH] ([ID_TaiChinh], [MaHD], [UEF], [TaiTro], [Khac], [TieuDe], [CreatedDate], [Hide]) VALUES (1, 4, CAST(0 AS Decimal(12, 0)), CAST(0 AS Decimal(12, 0)), N'', N'', CAST(N'2023-12-08T01:52:12.620' AS DateTime), 0)
INSERT [dbo].[TAI_CHINH] ([ID_TaiChinh], [MaHD], [UEF], [TaiTro], [Khac], [TieuDe], [CreatedDate], [Hide]) VALUES (2, 4, CAST(2000000 AS Decimal(12, 0)), CAST(20000000 AS Decimal(12, 0)), N'20 Phần quà xe đạp', N'Event C Tai Chinh', CAST(N'2023-12-08T02:07:41.610' AS DateTime), 0)
SET IDENTITY_INSERT [dbo].[TAI_CHINH] OFF
GO
SET IDENTITY_INSERT [dbo].[TAI_TRO] ON 

INSERT [dbo].[TAI_TRO] ([ID_TaiTro], [TenTaiTro], [DaiDien], [SDT], [Email], [Hide]) VALUES (1, N'Tập Đoàn Xăng Dầu Miền Nam', N'Phạm Anh Tuấn', N'025356254 ', N'daukhi@miennam.com', 1)
INSERT [dbo].[TAI_TRO] ([ID_TaiTro], [TenTaiTro], [DaiDien], [SDT], [Email], [Hide]) VALUES (2, N'Công Ty CP Tập Đoàn Hòa Phát', N'Nguyễn Việt Thắng', N'025356254 ', N'daukhi@miennam.com', 1)
SET IDENTITY_INSERT [dbo].[TAI_TRO] OFF
GO
ALTER TABLE [dbo].[DANH_GIA]  WITH CHECK ADD  CONSTRAINT [FK_DANH_GIA_HOAT_DONG] FOREIGN KEY([MaHD])
REFERENCES [dbo].[HOAT_DONG] ([MaHD])
GO
ALTER TABLE [dbo].[DANH_GIA] CHECK CONSTRAINT [FK_DANH_GIA_HOAT_DONG]
GO
ALTER TABLE [dbo].[DANH_GIA]  WITH CHECK ADD  CONSTRAINT [FK_DANH_GIA_NHAN_VIEN] FOREIGN KEY([MSNV])
REFERENCES [dbo].[NHAN_VIEN] ([MSNV])
GO
ALTER TABLE [dbo].[DANH_GIA] CHECK CONSTRAINT [FK_DANH_GIA_NHAN_VIEN]
GO
ALTER TABLE [dbo].[DANHGIA_DETAILS]  WITH CHECK ADD  CONSTRAINT [FK_DANHGIA_DETAILS_DANH_GIA] FOREIGN KEY([MaDanhGia])
REFERENCES [dbo].[DANH_GIA] ([MaDanhGia])
GO
ALTER TABLE [dbo].[DANHGIA_DETAILS] CHECK CONSTRAINT [FK_DANHGIA_DETAILS_DANH_GIA]
GO
ALTER TABLE [dbo].[DANHGIA_DETAILS]  WITH CHECK ADD  CONSTRAINT [FK_DANHGIA_DETAILS_HANG_MUC] FOREIGN KEY([HangMuc])
REFERENCES [dbo].[HANG_MUC] ([ID])
GO
ALTER TABLE [dbo].[DANHGIA_DETAILS] CHECK CONSTRAINT [FK_DANHGIA_DETAILS_HANG_MUC]
GO
ALTER TABLE [dbo].[HD_DOITAC]  WITH CHECK ADD  CONSTRAINT [FK_DT_HOATDONG_DOI_TAC] FOREIGN KEY([ID_DoiTac])
REFERENCES [dbo].[DOI_TAC] ([ID_DoiTac])
GO
ALTER TABLE [dbo].[HD_DOITAC] CHECK CONSTRAINT [FK_DT_HOATDONG_DOI_TAC]
GO
ALTER TABLE [dbo].[HD_DOITAC]  WITH CHECK ADD  CONSTRAINT [FK_DT_HOATDONG_HOAT_DONG] FOREIGN KEY([MaHD])
REFERENCES [dbo].[HOAT_DONG] ([MaHD])
GO
ALTER TABLE [dbo].[HD_DOITAC] CHECK CONSTRAINT [FK_DT_HOATDONG_HOAT_DONG]
GO
ALTER TABLE [dbo].[HD_NHAN_VIEN]  WITH CHECK ADD  CONSTRAINT [FK_NV_HOATDONG_HOAT_DONG] FOREIGN KEY([MaHD])
REFERENCES [dbo].[HOAT_DONG] ([MaHD])
GO
ALTER TABLE [dbo].[HD_NHAN_VIEN] CHECK CONSTRAINT [FK_NV_HOATDONG_HOAT_DONG]
GO
ALTER TABLE [dbo].[HD_NHAN_VIEN]  WITH CHECK ADD  CONSTRAINT [FK_SV_HOATDONG_NHAN_VIEN] FOREIGN KEY([MSNV])
REFERENCES [dbo].[NHAN_VIEN] ([MSNV])
GO
ALTER TABLE [dbo].[HD_NHAN_VIEN] CHECK CONSTRAINT [FK_SV_HOATDONG_NHAN_VIEN]
GO
ALTER TABLE [dbo].[HD_QUANLY]  WITH CHECK ADD  CONSTRAINT [FK_HD_QUANLY_HD] FOREIGN KEY([MaHD])
REFERENCES [dbo].[HOAT_DONG] ([MaHD])
GO
ALTER TABLE [dbo].[HD_QUANLY] CHECK CONSTRAINT [FK_HD_QUANLY_HD]
GO
ALTER TABLE [dbo].[HD_QUANLY]  WITH CHECK ADD  CONSTRAINT [FK_HD_QUANLY_QUAN_LY] FOREIGN KEY([MaQL])
REFERENCES [dbo].[QUAN_LY] ([MaQL])
GO
ALTER TABLE [dbo].[HD_QUANLY] CHECK CONSTRAINT [FK_HD_QUANLY_QUAN_LY]
GO
ALTER TABLE [dbo].[HD_TAITRO]  WITH CHECK ADD  CONSTRAINT [FK_HD_TAITRO_HD] FOREIGN KEY([MaHD])
REFERENCES [dbo].[HOAT_DONG] ([MaHD])
GO
ALTER TABLE [dbo].[HD_TAITRO] CHECK CONSTRAINT [FK_HD_TAITRO_HD]
GO
ALTER TABLE [dbo].[HD_TAITRO]  WITH CHECK ADD  CONSTRAINT [FK_HD_TAITRO_TAITRO] FOREIGN KEY([ID_TaiTro])
REFERENCES [dbo].[TAI_TRO] ([ID_TaiTro])
GO
ALTER TABLE [dbo].[HD_TAITRO] CHECK CONSTRAINT [FK_HD_TAITRO_TAITRO]
GO
ALTER TABLE [dbo].[NHAN_VIEN]  WITH CHECK ADD  CONSTRAINT [FK_NHAN_VIEN_CO_SO] FOREIGN KEY([CoSo])
REFERENCES [dbo].[CO_SO] ([MaCoSo])
GO
ALTER TABLE [dbo].[NHAN_VIEN] CHECK CONSTRAINT [FK_NHAN_VIEN_CO_SO]
GO
ALTER TABLE [dbo].[PHU_TRACH]  WITH CHECK ADD  CONSTRAINT [FK_PHU_TRACH_QUAN_LY] FOREIGN KEY([MaQL])
REFERENCES [dbo].[QUAN_LY] ([MaQL])
GO
ALTER TABLE [dbo].[PHU_TRACH] CHECK CONSTRAINT [FK_PHU_TRACH_QUAN_LY]
GO
ALTER TABLE [dbo].[QUAN_LY]  WITH CHECK ADD  CONSTRAINT [FK_QUAN_LY_CO_SO] FOREIGN KEY([CoSo])
REFERENCES [dbo].[CO_SO] ([MaCoSo])
GO
ALTER TABLE [dbo].[QUAN_LY] CHECK CONSTRAINT [FK_QUAN_LY_CO_SO]
GO
ALTER TABLE [dbo].[TAI_CHINH]  WITH CHECK ADD  CONSTRAINT [FK_TAI_CHINH_HOAT_DONG] FOREIGN KEY([MaHD])
REFERENCES [dbo].[HOAT_DONG] ([MaHD])
GO
ALTER TABLE [dbo].[TAI_CHINH] CHECK CONSTRAINT [FK_TAI_CHINH_HOAT_DONG]
GO
USE [master]
GO
ALTER DATABASE [quanlyeventskhachsan] SET  READ_WRITE 
GO
