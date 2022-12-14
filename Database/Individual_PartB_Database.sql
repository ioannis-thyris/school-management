USE [master]
GO
/****** Object:  Database [IndividualProject_PartB]    Script Date: 7/6/2022 12:53:18 πμ ******/
CREATE DATABASE [IndividualProject_PartB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'IndividualProject_PartB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\IndividualProject_PartB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'IndividualProject_PartB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\IndividualProject_PartB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [IndividualProject_PartB] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [IndividualProject_PartB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [IndividualProject_PartB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [IndividualProject_PartB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [IndividualProject_PartB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [IndividualProject_PartB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [IndividualProject_PartB] SET ARITHABORT OFF 
GO
ALTER DATABASE [IndividualProject_PartB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [IndividualProject_PartB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [IndividualProject_PartB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [IndividualProject_PartB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [IndividualProject_PartB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [IndividualProject_PartB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [IndividualProject_PartB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [IndividualProject_PartB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [IndividualProject_PartB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [IndividualProject_PartB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [IndividualProject_PartB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [IndividualProject_PartB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [IndividualProject_PartB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [IndividualProject_PartB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [IndividualProject_PartB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [IndividualProject_PartB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [IndividualProject_PartB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [IndividualProject_PartB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [IndividualProject_PartB] SET  MULTI_USER 
GO
ALTER DATABASE [IndividualProject_PartB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [IndividualProject_PartB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [IndividualProject_PartB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [IndividualProject_PartB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [IndividualProject_PartB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [IndividualProject_PartB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [IndividualProject_PartB] SET QUERY_STORE = OFF
GO
USE [IndividualProject_PartB]
GO
/****** Object:  Table [dbo].[Assignment]    Script Date: 7/6/2022 12:53:18 πμ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Assignment](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](100) NOT NULL,
	[Submission] [datetime2](7) NULL,
	[OralMarkPercent] [decimal](4, 1) NULL,
	[TotalMarkPercent] [decimal](4, 1) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AssignmentInCourse]    Script Date: 7/6/2022 12:53:18 πμ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AssignmentInCourse](
	[CourseID] [int] NOT NULL,
	[AssignmentID] [int] NOT NULL,
 CONSTRAINT [PK_AssignmentInCourse] PRIMARY KEY CLUSTERED 
(
	[CourseID] ASC,
	[AssignmentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Course]    Script Date: 7/6/2022 12:53:18 πμ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Course](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
	[Stream] [int] NOT NULL,
	[Type] [int] NOT NULL,
	[StartDate] [date] NULL,
	[EndDate] [date] NULL,
 CONSTRAINT [PK__Course__3214EC277F3936F5] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Course_Student_Assignment]    Script Date: 7/6/2022 12:53:18 πμ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Course_Student_Assignment](
	[CourseID] [int] NOT NULL,
	[AssignmentID] [int] NOT NULL,
	[StudentID] [int] NOT NULL,
 CONSTRAINT [PK_Course_Student_Assignment] PRIMARY KEY CLUSTERED 
(
	[CourseID] ASC,
	[AssignmentID] ASC,
	[StudentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Student]    Script Date: 7/6/2022 12:53:18 πμ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Student](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[DateOfBirth] [date] NULL,
	[TuitionFees] [decimal](6, 2) NOT NULL,
 CONSTRAINT [PK_Student] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StudentInCourse]    Script Date: 7/6/2022 12:53:18 πμ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StudentInCourse](
	[CourseID] [int] NOT NULL,
	[StudentID] [int] NOT NULL,
 CONSTRAINT [PK_StudentsInCourse] PRIMARY KEY CLUSTERED 
(
	[CourseID] ASC,
	[StudentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Trainer]    Script Date: 7/6/2022 12:53:18 πμ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Trainer](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[Subject] [int] NULL,
 CONSTRAINT [PK_Trainer] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TrainerInCourse]    Script Date: 7/6/2022 12:53:18 πμ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TrainerInCourse](
	[CourseID] [int] NOT NULL,
	[TrainerID] [int] NOT NULL,
 CONSTRAINT [PK_Trainer_Course] PRIMARY KEY CLUSTERED 
(
	[CourseID] ASC,
	[TrainerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Assignment] ON 

INSERT [dbo].[Assignment] ([ID], [Title], [Description], [Submission], [OralMarkPercent], [TotalMarkPercent]) VALUES (1, N'Front-End', N'Sketch out the layout of a website of your choosing, using HTML/CSS.', CAST(N'2022-08-06T00:00:00.0000000' AS DateTime2), CAST(10.0 AS Decimal(4, 1)), CAST(20.0 AS Decimal(4, 1)))
INSERT [dbo].[Assignment] ([ID], [Title], [Description], [Submission], [OralMarkPercent], [TotalMarkPercent]) VALUES (2, N'Databases', N'Use a database to implement CRUD functionallity to a site of your choosing.
', CAST(N'2022-10-18T00:00:00.0000000' AS DateTime2), CAST(10.0 AS Decimal(4, 1)), CAST(50.0 AS Decimal(4, 1)))
INSERT [dbo].[Assignment] ([ID], [Title], [Description], [Submission], [OralMarkPercent], [TotalMarkPercent]) VALUES (3, N'Front-End', N'Sketch out the layout of a website of your choosing, using HTML/CSS.', NULL, CAST(40.0 AS Decimal(4, 1)), CAST(50.0 AS Decimal(4, 1)))
INSERT [dbo].[Assignment] ([ID], [Title], [Description], [Submission], [OralMarkPercent], [TotalMarkPercent]) VALUES (4, N'Back-End', N'Implement the required business logic to the given example.', CAST(N'2022-09-01T00:00:00.0000000' AS DateTime2), CAST(40.0 AS Decimal(4, 1)), CAST(50.0 AS Decimal(4, 1)))
INSERT [dbo].[Assignment] ([ID], [Title], [Description], [Submission], [OralMarkPercent], [TotalMarkPercent]) VALUES (5, N'UX/UI', N'Refactor the give code to improve the user experience of the website.', CAST(N'2022-08-30T00:00:00.0000000' AS DateTime2), CAST(20.0 AS Decimal(4, 1)), CAST(30.0 AS Decimal(4, 1)))
INSERT [dbo].[Assignment] ([ID], [Title], [Description], [Submission], [OralMarkPercent], [TotalMarkPercent]) VALUES (6, N'Testing', N'Write unit test methods to check for the validity of the given code.', CAST(N'2022-11-10T00:00:00.0000000' AS DateTime2), CAST(10.0 AS Decimal(4, 1)), CAST(20.0 AS Decimal(4, 1)))
INSERT [dbo].[Assignment] ([ID], [Title], [Description], [Submission], [OralMarkPercent], [TotalMarkPercent]) VALUES (7, N'Databases', N'Use a database to implement CRUD functionallity to a site of your choosing.', CAST(N'2022-09-21T00:00:00.0000000' AS DateTime2), CAST(10.0 AS Decimal(4, 1)), CAST(10.0 AS Decimal(4, 1)))
INSERT [dbo].[Assignment] ([ID], [Title], [Description], [Submission], [OralMarkPercent], [TotalMarkPercent]) VALUES (8, N'Debugging', N'Use the debugging tools shown in the lessons to fix the errors of the given code.', CAST(N'2022-12-13T00:00:00.0000000' AS DateTime2), CAST(20.0 AS Decimal(4, 1)), CAST(40.0 AS Decimal(4, 1)))
INSERT [dbo].[Assignment] ([ID], [Title], [Description], [Submission], [OralMarkPercent], [TotalMarkPercent]) VALUES (9, N'UX/UI', N'Refactor the give code to improve the user experience of the website.', NULL, CAST(30.0 AS Decimal(4, 1)), CAST(40.0 AS Decimal(4, 1)))
INSERT [dbo].[Assignment] ([ID], [Title], [Description], [Submission], [OralMarkPercent], [TotalMarkPercent]) VALUES (10, N'Databases', N'Use a database to implement CRUD functionallity to a site of your choosing.', CAST(N'2022-07-01T00:00:00.0000000' AS DateTime2), CAST(20.0 AS Decimal(4, 1)), CAST(50.0 AS Decimal(4, 1)))
INSERT [dbo].[Assignment] ([ID], [Title], [Description], [Submission], [OralMarkPercent], [TotalMarkPercent]) VALUES (11, N'Back-End', N'Implement the required business logic to the given example.', NULL, CAST(20.0 AS Decimal(4, 1)), CAST(30.0 AS Decimal(4, 1)))
INSERT [dbo].[Assignment] ([ID], [Title], [Description], [Submission], [OralMarkPercent], [TotalMarkPercent]) VALUES (12, N'Test', N'Test', CAST(N'2023-01-19T00:00:00.0000000' AS DateTime2), CAST(20.0 AS Decimal(4, 1)), CAST(40.0 AS Decimal(4, 1)))
SET IDENTITY_INSERT [dbo].[Assignment] OFF
GO
INSERT [dbo].[AssignmentInCourse] ([CourseID], [AssignmentID]) VALUES (1, 3)
INSERT [dbo].[AssignmentInCourse] ([CourseID], [AssignmentID]) VALUES (1, 9)
INSERT [dbo].[AssignmentInCourse] ([CourseID], [AssignmentID]) VALUES (6, 2)
INSERT [dbo].[AssignmentInCourse] ([CourseID], [AssignmentID]) VALUES (6, 10)
INSERT [dbo].[AssignmentInCourse] ([CourseID], [AssignmentID]) VALUES (9, 6)
INSERT [dbo].[AssignmentInCourse] ([CourseID], [AssignmentID]) VALUES (9, 8)
INSERT [dbo].[AssignmentInCourse] ([CourseID], [AssignmentID]) VALUES (9, 11)
INSERT [dbo].[AssignmentInCourse] ([CourseID], [AssignmentID]) VALUES (10, 6)
INSERT [dbo].[AssignmentInCourse] ([CourseID], [AssignmentID]) VALUES (10, 8)
INSERT [dbo].[AssignmentInCourse] ([CourseID], [AssignmentID]) VALUES (10, 11)
GO
SET IDENTITY_INSERT [dbo].[Course] ON 

INSERT [dbo].[Course] ([ID], [Title], [Stream], [Type], [StartDate], [EndDate]) VALUES (1, N'Beginner Front-End', 1, 0, NULL, NULL)
INSERT [dbo].[Course] ([ID], [Title], [Stream], [Type], [StartDate], [EndDate]) VALUES (6, N'Beginner Back-End', 2, 1, NULL, NULL)
INSERT [dbo].[Course] ([ID], [Title], [Stream], [Type], [StartDate], [EndDate]) VALUES (9, N'Advanced Full-Stack', 3, 1, CAST(N'2022-10-01' AS Date), CAST(N'2023-03-01' AS Date))
INSERT [dbo].[Course] ([ID], [Title], [Stream], [Type], [StartDate], [EndDate]) VALUES (10, N'Advanced Full-Stack', 0, 0, CAST(N'2022-09-23' AS Date), CAST(N'2023-02-15' AS Date))
INSERT [dbo].[Course] ([ID], [Title], [Stream], [Type], [StartDate], [EndDate]) VALUES (13, N'asdasd', 2, 1, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Course] OFF
GO
INSERT [dbo].[Course_Student_Assignment] ([CourseID], [AssignmentID], [StudentID]) VALUES (1, 3, 8)
INSERT [dbo].[Course_Student_Assignment] ([CourseID], [AssignmentID], [StudentID]) VALUES (1, 3, 10)
INSERT [dbo].[Course_Student_Assignment] ([CourseID], [AssignmentID], [StudentID]) VALUES (1, 3, 13)
INSERT [dbo].[Course_Student_Assignment] ([CourseID], [AssignmentID], [StudentID]) VALUES (1, 3, 17)
INSERT [dbo].[Course_Student_Assignment] ([CourseID], [AssignmentID], [StudentID]) VALUES (1, 3, 22)
INSERT [dbo].[Course_Student_Assignment] ([CourseID], [AssignmentID], [StudentID]) VALUES (1, 3, 25)
INSERT [dbo].[Course_Student_Assignment] ([CourseID], [AssignmentID], [StudentID]) VALUES (1, 9, 8)
INSERT [dbo].[Course_Student_Assignment] ([CourseID], [AssignmentID], [StudentID]) VALUES (1, 9, 10)
INSERT [dbo].[Course_Student_Assignment] ([CourseID], [AssignmentID], [StudentID]) VALUES (1, 9, 13)
INSERT [dbo].[Course_Student_Assignment] ([CourseID], [AssignmentID], [StudentID]) VALUES (1, 9, 17)
INSERT [dbo].[Course_Student_Assignment] ([CourseID], [AssignmentID], [StudentID]) VALUES (1, 9, 34)
INSERT [dbo].[Course_Student_Assignment] ([CourseID], [AssignmentID], [StudentID]) VALUES (6, 2, 8)
INSERT [dbo].[Course_Student_Assignment] ([CourseID], [AssignmentID], [StudentID]) VALUES (6, 2, 22)
INSERT [dbo].[Course_Student_Assignment] ([CourseID], [AssignmentID], [StudentID]) VALUES (6, 2, 27)
INSERT [dbo].[Course_Student_Assignment] ([CourseID], [AssignmentID], [StudentID]) VALUES (6, 2, 31)
INSERT [dbo].[Course_Student_Assignment] ([CourseID], [AssignmentID], [StudentID]) VALUES (6, 2, 40)
INSERT [dbo].[Course_Student_Assignment] ([CourseID], [AssignmentID], [StudentID]) VALUES (6, 2, 41)
INSERT [dbo].[Course_Student_Assignment] ([CourseID], [AssignmentID], [StudentID]) VALUES (6, 2, 53)
INSERT [dbo].[Course_Student_Assignment] ([CourseID], [AssignmentID], [StudentID]) VALUES (6, 10, 8)
INSERT [dbo].[Course_Student_Assignment] ([CourseID], [AssignmentID], [StudentID]) VALUES (6, 10, 27)
INSERT [dbo].[Course_Student_Assignment] ([CourseID], [AssignmentID], [StudentID]) VALUES (6, 10, 31)
INSERT [dbo].[Course_Student_Assignment] ([CourseID], [AssignmentID], [StudentID]) VALUES (6, 10, 40)
INSERT [dbo].[Course_Student_Assignment] ([CourseID], [AssignmentID], [StudentID]) VALUES (6, 10, 41)
INSERT [dbo].[Course_Student_Assignment] ([CourseID], [AssignmentID], [StudentID]) VALUES (6, 10, 53)
INSERT [dbo].[Course_Student_Assignment] ([CourseID], [AssignmentID], [StudentID]) VALUES (9, 6, 10)
INSERT [dbo].[Course_Student_Assignment] ([CourseID], [AssignmentID], [StudentID]) VALUES (9, 6, 14)
INSERT [dbo].[Course_Student_Assignment] ([CourseID], [AssignmentID], [StudentID]) VALUES (9, 6, 16)
INSERT [dbo].[Course_Student_Assignment] ([CourseID], [AssignmentID], [StudentID]) VALUES (9, 6, 25)
INSERT [dbo].[Course_Student_Assignment] ([CourseID], [AssignmentID], [StudentID]) VALUES (9, 6, 36)
INSERT [dbo].[Course_Student_Assignment] ([CourseID], [AssignmentID], [StudentID]) VALUES (9, 6, 55)
INSERT [dbo].[Course_Student_Assignment] ([CourseID], [AssignmentID], [StudentID]) VALUES (9, 8, 10)
INSERT [dbo].[Course_Student_Assignment] ([CourseID], [AssignmentID], [StudentID]) VALUES (9, 8, 14)
INSERT [dbo].[Course_Student_Assignment] ([CourseID], [AssignmentID], [StudentID]) VALUES (9, 8, 16)
INSERT [dbo].[Course_Student_Assignment] ([CourseID], [AssignmentID], [StudentID]) VALUES (9, 8, 25)
INSERT [dbo].[Course_Student_Assignment] ([CourseID], [AssignmentID], [StudentID]) VALUES (9, 8, 36)
INSERT [dbo].[Course_Student_Assignment] ([CourseID], [AssignmentID], [StudentID]) VALUES (9, 8, 55)
INSERT [dbo].[Course_Student_Assignment] ([CourseID], [AssignmentID], [StudentID]) VALUES (9, 11, 10)
INSERT [dbo].[Course_Student_Assignment] ([CourseID], [AssignmentID], [StudentID]) VALUES (9, 11, 14)
INSERT [dbo].[Course_Student_Assignment] ([CourseID], [AssignmentID], [StudentID]) VALUES (9, 11, 16)
INSERT [dbo].[Course_Student_Assignment] ([CourseID], [AssignmentID], [StudentID]) VALUES (9, 11, 25)
INSERT [dbo].[Course_Student_Assignment] ([CourseID], [AssignmentID], [StudentID]) VALUES (9, 11, 36)
INSERT [dbo].[Course_Student_Assignment] ([CourseID], [AssignmentID], [StudentID]) VALUES (9, 11, 55)
INSERT [dbo].[Course_Student_Assignment] ([CourseID], [AssignmentID], [StudentID]) VALUES (10, 6, 11)
INSERT [dbo].[Course_Student_Assignment] ([CourseID], [AssignmentID], [StudentID]) VALUES (10, 6, 22)
INSERT [dbo].[Course_Student_Assignment] ([CourseID], [AssignmentID], [StudentID]) VALUES (10, 6, 25)
INSERT [dbo].[Course_Student_Assignment] ([CourseID], [AssignmentID], [StudentID]) VALUES (10, 6, 33)
INSERT [dbo].[Course_Student_Assignment] ([CourseID], [AssignmentID], [StudentID]) VALUES (10, 6, 53)
INSERT [dbo].[Course_Student_Assignment] ([CourseID], [AssignmentID], [StudentID]) VALUES (10, 8, 11)
INSERT [dbo].[Course_Student_Assignment] ([CourseID], [AssignmentID], [StudentID]) VALUES (10, 8, 19)
INSERT [dbo].[Course_Student_Assignment] ([CourseID], [AssignmentID], [StudentID]) VALUES (10, 8, 25)
INSERT [dbo].[Course_Student_Assignment] ([CourseID], [AssignmentID], [StudentID]) VALUES (10, 8, 28)
INSERT [dbo].[Course_Student_Assignment] ([CourseID], [AssignmentID], [StudentID]) VALUES (10, 8, 42)
INSERT [dbo].[Course_Student_Assignment] ([CourseID], [AssignmentID], [StudentID]) VALUES (10, 8, 53)
INSERT [dbo].[Course_Student_Assignment] ([CourseID], [AssignmentID], [StudentID]) VALUES (10, 11, 11)
INSERT [dbo].[Course_Student_Assignment] ([CourseID], [AssignmentID], [StudentID]) VALUES (10, 11, 19)
INSERT [dbo].[Course_Student_Assignment] ([CourseID], [AssignmentID], [StudentID]) VALUES (10, 11, 22)
INSERT [dbo].[Course_Student_Assignment] ([CourseID], [AssignmentID], [StudentID]) VALUES (10, 11, 25)
INSERT [dbo].[Course_Student_Assignment] ([CourseID], [AssignmentID], [StudentID]) VALUES (10, 11, 28)
INSERT [dbo].[Course_Student_Assignment] ([CourseID], [AssignmentID], [StudentID]) VALUES (10, 11, 33)
INSERT [dbo].[Course_Student_Assignment] ([CourseID], [AssignmentID], [StudentID]) VALUES (10, 11, 42)
INSERT [dbo].[Course_Student_Assignment] ([CourseID], [AssignmentID], [StudentID]) VALUES (10, 11, 53)
GO
SET IDENTITY_INSERT [dbo].[Student] ON 

INSERT [dbo].[Student] ([ID], [FirstName], [LastName], [DateOfBirth], [TuitionFees]) VALUES (3, N'Helen', N'Johnson', CAST(N'1995-07-03' AS Date), CAST(2373.00 AS Decimal(6, 2)))
INSERT [dbo].[Student] ([ID], [FirstName], [LastName], [DateOfBirth], [TuitionFees]) VALUES (7, N'Jim', N'Jones', CAST(N'1987-01-20' AS Date), CAST(2500.00 AS Decimal(6, 2)))
INSERT [dbo].[Student] ([ID], [FirstName], [LastName], [DateOfBirth], [TuitionFees]) VALUES (8, N'Nick', N'Johnson', NULL, CAST(1500.00 AS Decimal(6, 2)))
INSERT [dbo].[Student] ([ID], [FirstName], [LastName], [DateOfBirth], [TuitionFees]) VALUES (9, N'Bill', N'Brown', CAST(N'1995-02-05' AS Date), CAST(500.00 AS Decimal(6, 2)))
INSERT [dbo].[Student] ([ID], [FirstName], [LastName], [DateOfBirth], [TuitionFees]) VALUES (10, N'Emma', N'Gonzalez', NULL, CAST(1200.00 AS Decimal(6, 2)))
INSERT [dbo].[Student] ([ID], [FirstName], [LastName], [DateOfBirth], [TuitionFees]) VALUES (11, N'Jake', N'Davis', CAST(N'1991-01-13' AS Date), CAST(0.00 AS Decimal(6, 2)))
INSERT [dbo].[Student] ([ID], [FirstName], [LastName], [DateOfBirth], [TuitionFees]) VALUES (12, N'Tina', N'Hernandez', CAST(N'1992-06-06' AS Date), CAST(2500.00 AS Decimal(6, 2)))
INSERT [dbo].[Student] ([ID], [FirstName], [LastName], [DateOfBirth], [TuitionFees]) VALUES (13, N'Anna', N'Smith', NULL, CAST(1400.00 AS Decimal(6, 2)))
INSERT [dbo].[Student] ([ID], [FirstName], [LastName], [DateOfBirth], [TuitionFees]) VALUES (14, N'Joe', N'Lopez', CAST(N'1989-01-01' AS Date), CAST(950.00 AS Decimal(6, 2)))
INSERT [dbo].[Student] ([ID], [FirstName], [LastName], [DateOfBirth], [TuitionFees]) VALUES (16, N'George', N'Hernandez', CAST(N'1971-08-18' AS Date), CAST(1800.00 AS Decimal(6, 2)))
INSERT [dbo].[Student] ([ID], [FirstName], [LastName], [DateOfBirth], [TuitionFees]) VALUES (17, N'Jim', N'Green', CAST(N'1991-05-02' AS Date), CAST(2500.00 AS Decimal(6, 2)))
INSERT [dbo].[Student] ([ID], [FirstName], [LastName], [DateOfBirth], [TuitionFees]) VALUES (18, N'George', N'Davis', CAST(N'1990-03-29' AS Date), CAST(2000.00 AS Decimal(6, 2)))
INSERT [dbo].[Student] ([ID], [FirstName], [LastName], [DateOfBirth], [TuitionFees]) VALUES (19, N'Emma', N'Allen', CAST(N'1992-10-01' AS Date), CAST(200.00 AS Decimal(6, 2)))
INSERT [dbo].[Student] ([ID], [FirstName], [LastName], [DateOfBirth], [TuitionFees]) VALUES (21, N'Jane', N'Thomson', CAST(N'1999-04-14' AS Date), CAST(2250.00 AS Decimal(6, 2)))
INSERT [dbo].[Student] ([ID], [FirstName], [LastName], [DateOfBirth], [TuitionFees]) VALUES (22, N'Andrew', N'Smith', CAST(N'1990-10-09' AS Date), CAST(2456.00 AS Decimal(6, 2)))
INSERT [dbo].[Student] ([ID], [FirstName], [LastName], [DateOfBirth], [TuitionFees]) VALUES (23, N'Anna', N'Hernandex', NULL, CAST(500.00 AS Decimal(6, 2)))
INSERT [dbo].[Student] ([ID], [FirstName], [LastName], [DateOfBirth], [TuitionFees]) VALUES (24, N'Sophia', N'Peter', CAST(N'1985-09-18' AS Date), CAST(900.00 AS Decimal(6, 2)))
INSERT [dbo].[Student] ([ID], [FirstName], [LastName], [DateOfBirth], [TuitionFees]) VALUES (25, N'Peter', N'Smith', NULL, CAST(2055.00 AS Decimal(6, 2)))
INSERT [dbo].[Student] ([ID], [FirstName], [LastName], [DateOfBirth], [TuitionFees]) VALUES (27, N'James', N'Moore', CAST(N'1998-10-01' AS Date), CAST(1000.00 AS Decimal(6, 2)))
INSERT [dbo].[Student] ([ID], [FirstName], [LastName], [DateOfBirth], [TuitionFees]) VALUES (28, N'Tina', N'Lopez', CAST(N'1981-02-12' AS Date), CAST(2200.00 AS Decimal(6, 2)))
INSERT [dbo].[Student] ([ID], [FirstName], [LastName], [DateOfBirth], [TuitionFees]) VALUES (29, N'Anna', N'Allen', CAST(N'1995-08-04' AS Date), CAST(2100.00 AS Decimal(6, 2)))
INSERT [dbo].[Student] ([ID], [FirstName], [LastName], [DateOfBirth], [TuitionFees]) VALUES (30, N'James', N'Davis', CAST(N'1985-02-03' AS Date), CAST(2500.00 AS Decimal(6, 2)))
INSERT [dbo].[Student] ([ID], [FirstName], [LastName], [DateOfBirth], [TuitionFees]) VALUES (31, N'Jim', N'Moore', NULL, CAST(1600.00 AS Decimal(6, 2)))
INSERT [dbo].[Student] ([ID], [FirstName], [LastName], [DateOfBirth], [TuitionFees]) VALUES (32, N'Luke', N'Moore', CAST(N'1991-02-28' AS Date), CAST(1300.00 AS Decimal(6, 2)))
INSERT [dbo].[Student] ([ID], [FirstName], [LastName], [DateOfBirth], [TuitionFees]) VALUES (33, N'Jane', N'Davis', CAST(N'1992-08-17' AS Date), CAST(1500.00 AS Decimal(6, 2)))
INSERT [dbo].[Student] ([ID], [FirstName], [LastName], [DateOfBirth], [TuitionFees]) VALUES (34, N'Anna', N'Davis', NULL, CAST(1900.00 AS Decimal(6, 2)))
INSERT [dbo].[Student] ([ID], [FirstName], [LastName], [DateOfBirth], [TuitionFees]) VALUES (35, N'Maria', N'Thomas', CAST(N'1996-12-25' AS Date), CAST(2500.00 AS Decimal(6, 2)))
INSERT [dbo].[Student] ([ID], [FirstName], [LastName], [DateOfBirth], [TuitionFees]) VALUES (36, N'Mary', N'Campbell', NULL, CAST(1900.00 AS Decimal(6, 2)))
INSERT [dbo].[Student] ([ID], [FirstName], [LastName], [DateOfBirth], [TuitionFees]) VALUES (37, N'Sophia', N'Allen', CAST(N'1991-03-06' AS Date), CAST(2450.00 AS Decimal(6, 2)))
INSERT [dbo].[Student] ([ID], [FirstName], [LastName], [DateOfBirth], [TuitionFees]) VALUES (38, N'Helen', N'Hernandez', CAST(N'1997-05-21' AS Date), CAST(2150.00 AS Decimal(6, 2)))
INSERT [dbo].[Student] ([ID], [FirstName], [LastName], [DateOfBirth], [TuitionFees]) VALUES (39, N'Joe', N'Moore', NULL, CAST(950.00 AS Decimal(6, 2)))
INSERT [dbo].[Student] ([ID], [FirstName], [LastName], [DateOfBirth], [TuitionFees]) VALUES (40, N'Bill', N'Garcia', NULL, CAST(1000.00 AS Decimal(6, 2)))
INSERT [dbo].[Student] ([ID], [FirstName], [LastName], [DateOfBirth], [TuitionFees]) VALUES (41, N'Joe', N'Davis', CAST(N'1900-01-19' AS Date), CAST(2500.00 AS Decimal(6, 2)))
INSERT [dbo].[Student] ([ID], [FirstName], [LastName], [DateOfBirth], [TuitionFees]) VALUES (42, N'George', N'Johnson', CAST(N'2000-01-03' AS Date), CAST(2000.00 AS Decimal(6, 2)))
INSERT [dbo].[Student] ([ID], [FirstName], [LastName], [DateOfBirth], [TuitionFees]) VALUES (43, N'Peter', N'Williams', CAST(N'1987-11-12' AS Date), CAST(1900.00 AS Decimal(6, 2)))
INSERT [dbo].[Student] ([ID], [FirstName], [LastName], [DateOfBirth], [TuitionFees]) VALUES (44, N'Nick', N'Brown', NULL, CAST(2500.00 AS Decimal(6, 2)))
INSERT [dbo].[Student] ([ID], [FirstName], [LastName], [DateOfBirth], [TuitionFees]) VALUES (47, N'Jake', N'Davis', CAST(N'1991-05-01' AS Date), CAST(2100.00 AS Decimal(6, 2)))
INSERT [dbo].[Student] ([ID], [FirstName], [LastName], [DateOfBirth], [TuitionFees]) VALUES (49, N'Nick', N'Miller', NULL, CAST(1800.00 AS Decimal(6, 2)))
INSERT [dbo].[Student] ([ID], [FirstName], [LastName], [DateOfBirth], [TuitionFees]) VALUES (53, N'Nick', N'Hernandez', CAST(N'1992-01-20' AS Date), CAST(2280.00 AS Decimal(6, 2)))
INSERT [dbo].[Student] ([ID], [FirstName], [LastName], [DateOfBirth], [TuitionFees]) VALUES (54, N'Nick', N'Gonzalez', CAST(N'1992-02-01' AS Date), CAST(2431.00 AS Decimal(6, 2)))
INSERT [dbo].[Student] ([ID], [FirstName], [LastName], [DateOfBirth], [TuitionFees]) VALUES (55, N'Mary', N'Jackson', CAST(N'1989-11-21' AS Date), CAST(2166.00 AS Decimal(6, 2)))
INSERT [dbo].[Student] ([ID], [FirstName], [LastName], [DateOfBirth], [TuitionFees]) VALUES (57, N'George', N'Davis', CAST(N'1998-11-17' AS Date), CAST(2014.00 AS Decimal(6, 2)))
SET IDENTITY_INSERT [dbo].[Student] OFF
GO
INSERT [dbo].[StudentInCourse] ([CourseID], [StudentID]) VALUES (1, 8)
INSERT [dbo].[StudentInCourse] ([CourseID], [StudentID]) VALUES (1, 9)
INSERT [dbo].[StudentInCourse] ([CourseID], [StudentID]) VALUES (1, 10)
INSERT [dbo].[StudentInCourse] ([CourseID], [StudentID]) VALUES (1, 11)
INSERT [dbo].[StudentInCourse] ([CourseID], [StudentID]) VALUES (1, 12)
INSERT [dbo].[StudentInCourse] ([CourseID], [StudentID]) VALUES (1, 13)
INSERT [dbo].[StudentInCourse] ([CourseID], [StudentID]) VALUES (1, 17)
INSERT [dbo].[StudentInCourse] ([CourseID], [StudentID]) VALUES (1, 22)
INSERT [dbo].[StudentInCourse] ([CourseID], [StudentID]) VALUES (1, 25)
INSERT [dbo].[StudentInCourse] ([CourseID], [StudentID]) VALUES (1, 34)
INSERT [dbo].[StudentInCourse] ([CourseID], [StudentID]) VALUES (6, 8)
INSERT [dbo].[StudentInCourse] ([CourseID], [StudentID]) VALUES (6, 22)
INSERT [dbo].[StudentInCourse] ([CourseID], [StudentID]) VALUES (6, 27)
INSERT [dbo].[StudentInCourse] ([CourseID], [StudentID]) VALUES (6, 28)
INSERT [dbo].[StudentInCourse] ([CourseID], [StudentID]) VALUES (6, 31)
INSERT [dbo].[StudentInCourse] ([CourseID], [StudentID]) VALUES (6, 40)
INSERT [dbo].[StudentInCourse] ([CourseID], [StudentID]) VALUES (6, 41)
INSERT [dbo].[StudentInCourse] ([CourseID], [StudentID]) VALUES (6, 53)
INSERT [dbo].[StudentInCourse] ([CourseID], [StudentID]) VALUES (9, 10)
INSERT [dbo].[StudentInCourse] ([CourseID], [StudentID]) VALUES (9, 14)
INSERT [dbo].[StudentInCourse] ([CourseID], [StudentID]) VALUES (9, 16)
INSERT [dbo].[StudentInCourse] ([CourseID], [StudentID]) VALUES (9, 25)
INSERT [dbo].[StudentInCourse] ([CourseID], [StudentID]) VALUES (9, 31)
INSERT [dbo].[StudentInCourse] ([CourseID], [StudentID]) VALUES (9, 33)
INSERT [dbo].[StudentInCourse] ([CourseID], [StudentID]) VALUES (9, 36)
INSERT [dbo].[StudentInCourse] ([CourseID], [StudentID]) VALUES (9, 55)
INSERT [dbo].[StudentInCourse] ([CourseID], [StudentID]) VALUES (10, 11)
INSERT [dbo].[StudentInCourse] ([CourseID], [StudentID]) VALUES (10, 19)
INSERT [dbo].[StudentInCourse] ([CourseID], [StudentID]) VALUES (10, 22)
INSERT [dbo].[StudentInCourse] ([CourseID], [StudentID]) VALUES (10, 25)
INSERT [dbo].[StudentInCourse] ([CourseID], [StudentID]) VALUES (10, 28)
INSERT [dbo].[StudentInCourse] ([CourseID], [StudentID]) VALUES (10, 33)
INSERT [dbo].[StudentInCourse] ([CourseID], [StudentID]) VALUES (10, 42)
INSERT [dbo].[StudentInCourse] ([CourseID], [StudentID]) VALUES (10, 53)
GO
SET IDENTITY_INSERT [dbo].[Trainer] ON 

INSERT [dbo].[Trainer] ([ID], [FirstName], [LastName], [Subject]) VALUES (1, N'George', N'Davis', 0)
INSERT [dbo].[Trainer] ([ID], [FirstName], [LastName], [Subject]) VALUES (2, N'John', N'Smith', 0)
INSERT [dbo].[Trainer] ([ID], [FirstName], [LastName], [Subject]) VALUES (3, N'John', N'Allen', 0)
INSERT [dbo].[Trainer] ([ID], [FirstName], [LastName], [Subject]) VALUES (4, N'George', N'Thompson', 1)
INSERT [dbo].[Trainer] ([ID], [FirstName], [LastName], [Subject]) VALUES (5, N'James', N'Lopez', 0)
INSERT [dbo].[Trainer] ([ID], [FirstName], [LastName], [Subject]) VALUES (6, N'Nick', N'Johnson', 2)
INSERT [dbo].[Trainer] ([ID], [FirstName], [LastName], [Subject]) VALUES (7, N'Peter', N'Moore', 2)
INSERT [dbo].[Trainer] ([ID], [FirstName], [LastName], [Subject]) VALUES (8, N'Nick', N'Brown', 2)
INSERT [dbo].[Trainer] ([ID], [FirstName], [LastName], [Subject]) VALUES (9, N'Jane', N'Brown', 0)
INSERT [dbo].[Trainer] ([ID], [FirstName], [LastName], [Subject]) VALUES (10, N'James', N'Brown', 0)
INSERT [dbo].[Trainer] ([ID], [FirstName], [LastName], [Subject]) VALUES (11, N'George', N'Lopez', 0)
INSERT [dbo].[Trainer] ([ID], [FirstName], [LastName], [Subject]) VALUES (12, N'Jane', N'Moore', 1)
INSERT [dbo].[Trainer] ([ID], [FirstName], [LastName], [Subject]) VALUES (13, N'Mary', N'Hernandez', 1)
INSERT [dbo].[Trainer] ([ID], [FirstName], [LastName], [Subject]) VALUES (14, N'Mary', N'Garcia', 0)
INSERT [dbo].[Trainer] ([ID], [FirstName], [LastName], [Subject]) VALUES (15, N'Peter', N'Thomas', 2)
INSERT [dbo].[Trainer] ([ID], [FirstName], [LastName], [Subject]) VALUES (16, N'Joe', N'Jones', 0)
INSERT [dbo].[Trainer] ([ID], [FirstName], [LastName], [Subject]) VALUES (17, N'Jake', N'Davis', 1)
INSERT [dbo].[Trainer] ([ID], [FirstName], [LastName], [Subject]) VALUES (18, N'Jake', N'Thompson', 2)
INSERT [dbo].[Trainer] ([ID], [FirstName], [LastName], [Subject]) VALUES (19, N'John', N'Smith', 1)
INSERT [dbo].[Trainer] ([ID], [FirstName], [LastName], [Subject]) VALUES (21, N'Nick', N'Perry', 2)
SET IDENTITY_INSERT [dbo].[Trainer] OFF
GO
INSERT [dbo].[TrainerInCourse] ([CourseID], [TrainerID]) VALUES (1, 1)
INSERT [dbo].[TrainerInCourse] ([CourseID], [TrainerID]) VALUES (1, 4)
INSERT [dbo].[TrainerInCourse] ([CourseID], [TrainerID]) VALUES (1, 6)
INSERT [dbo].[TrainerInCourse] ([CourseID], [TrainerID]) VALUES (1, 8)
INSERT [dbo].[TrainerInCourse] ([CourseID], [TrainerID]) VALUES (6, 2)
INSERT [dbo].[TrainerInCourse] ([CourseID], [TrainerID]) VALUES (6, 3)
INSERT [dbo].[TrainerInCourse] ([CourseID], [TrainerID]) VALUES (6, 12)
INSERT [dbo].[TrainerInCourse] ([CourseID], [TrainerID]) VALUES (9, 7)
INSERT [dbo].[TrainerInCourse] ([CourseID], [TrainerID]) VALUES (9, 11)
INSERT [dbo].[TrainerInCourse] ([CourseID], [TrainerID]) VALUES (10, 1)
INSERT [dbo].[TrainerInCourse] ([CourseID], [TrainerID]) VALUES (10, 5)
INSERT [dbo].[TrainerInCourse] ([CourseID], [TrainerID]) VALUES (10, 9)
INSERT [dbo].[TrainerInCourse] ([CourseID], [TrainerID]) VALUES (10, 15)
GO
ALTER TABLE [dbo].[AssignmentInCourse]  WITH CHECK ADD  CONSTRAINT [FK_AssignmentInCourse_Assignment] FOREIGN KEY([AssignmentID])
REFERENCES [dbo].[Assignment] ([ID])
GO
ALTER TABLE [dbo].[AssignmentInCourse] CHECK CONSTRAINT [FK_AssignmentInCourse_Assignment]
GO
ALTER TABLE [dbo].[AssignmentInCourse]  WITH CHECK ADD  CONSTRAINT [FK_AssignmentInCourse_Course] FOREIGN KEY([CourseID])
REFERENCES [dbo].[Course] ([ID])
GO
ALTER TABLE [dbo].[AssignmentInCourse] CHECK CONSTRAINT [FK_AssignmentInCourse_Course]
GO
ALTER TABLE [dbo].[Course_Student_Assignment]  WITH CHECK ADD  CONSTRAINT [FK_Course_Student_Assignment_AssignmentInCourse] FOREIGN KEY([CourseID], [AssignmentID])
REFERENCES [dbo].[AssignmentInCourse] ([CourseID], [AssignmentID])
GO
ALTER TABLE [dbo].[Course_Student_Assignment] CHECK CONSTRAINT [FK_Course_Student_Assignment_AssignmentInCourse]
GO
ALTER TABLE [dbo].[Course_Student_Assignment]  WITH CHECK ADD  CONSTRAINT [FK_Course_Student_Assignment_StudentInCourse] FOREIGN KEY([CourseID], [StudentID])
REFERENCES [dbo].[StudentInCourse] ([CourseID], [StudentID])
GO
ALTER TABLE [dbo].[Course_Student_Assignment] CHECK CONSTRAINT [FK_Course_Student_Assignment_StudentInCourse]
GO
ALTER TABLE [dbo].[StudentInCourse]  WITH CHECK ADD  CONSTRAINT [FK_StudentInCourse_Course] FOREIGN KEY([CourseID])
REFERENCES [dbo].[Course] ([ID])
GO
ALTER TABLE [dbo].[StudentInCourse] CHECK CONSTRAINT [FK_StudentInCourse_Course]
GO
ALTER TABLE [dbo].[StudentInCourse]  WITH CHECK ADD  CONSTRAINT [FK_StudentInCourse_Student] FOREIGN KEY([StudentID])
REFERENCES [dbo].[Student] ([ID])
GO
ALTER TABLE [dbo].[StudentInCourse] CHECK CONSTRAINT [FK_StudentInCourse_Student]
GO
ALTER TABLE [dbo].[TrainerInCourse]  WITH CHECK ADD  CONSTRAINT [FK_Trainer_Course_Course] FOREIGN KEY([CourseID])
REFERENCES [dbo].[Course] ([ID])
GO
ALTER TABLE [dbo].[TrainerInCourse] CHECK CONSTRAINT [FK_Trainer_Course_Course]
GO
ALTER TABLE [dbo].[TrainerInCourse]  WITH CHECK ADD  CONSTRAINT [FK_Trainer_Course_Trainer] FOREIGN KEY([TrainerID])
REFERENCES [dbo].[Trainer] ([ID])
GO
ALTER TABLE [dbo].[TrainerInCourse] CHECK CONSTRAINT [FK_Trainer_Course_Trainer]
GO
ALTER TABLE [dbo].[Assignment]  WITH CHECK ADD CHECK  (([OralMarkPercent]<=(100)))
GO
ALTER TABLE [dbo].[Assignment]  WITH CHECK ADD CHECK  (([TotalMarkPercent]<=(100)))
GO
ALTER TABLE [dbo].[Course]  WITH CHECK ADD  CONSTRAINT [CK__Course__24927208] CHECK  (([EndDate]>[StartDate]))
GO
ALTER TABLE [dbo].[Course] CHECK CONSTRAINT [CK__Course__24927208]
GO
USE [master]
GO
ALTER DATABASE [IndividualProject_PartB] SET  READ_WRITE 
GO
