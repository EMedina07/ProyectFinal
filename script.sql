USE [master]
GO
/****** Object:  Database [OrientalMedicalSystemDB]    Script Date: 01/08/2022 23:36:05 ******/
CREATE DATABASE [OrientalMedicalSystemDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'OrientalMedicalSystemDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\OrientalMedicalSystemDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'OrientalMedicalSystemDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\OrientalMedicalSystemDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [OrientalMedicalSystemDB] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [OrientalMedicalSystemDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [OrientalMedicalSystemDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [OrientalMedicalSystemDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [OrientalMedicalSystemDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [OrientalMedicalSystemDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [OrientalMedicalSystemDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [OrientalMedicalSystemDB] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [OrientalMedicalSystemDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [OrientalMedicalSystemDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [OrientalMedicalSystemDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [OrientalMedicalSystemDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [OrientalMedicalSystemDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [OrientalMedicalSystemDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [OrientalMedicalSystemDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [OrientalMedicalSystemDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [OrientalMedicalSystemDB] SET  ENABLE_BROKER 
GO
ALTER DATABASE [OrientalMedicalSystemDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [OrientalMedicalSystemDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [OrientalMedicalSystemDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [OrientalMedicalSystemDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [OrientalMedicalSystemDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [OrientalMedicalSystemDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [OrientalMedicalSystemDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [OrientalMedicalSystemDB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [OrientalMedicalSystemDB] SET  MULTI_USER 
GO
ALTER DATABASE [OrientalMedicalSystemDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [OrientalMedicalSystemDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [OrientalMedicalSystemDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [OrientalMedicalSystemDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [OrientalMedicalSystemDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [OrientalMedicalSystemDB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [OrientalMedicalSystemDB] SET QUERY_STORE = OFF
GO
USE [OrientalMedicalSystemDB]
GO
/****** Object:  Table [dbo].[Asistente]    Script Date: 01/08/2022 23:36:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Asistente](
	[AsistenteID] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[Apellido] [varchar](50) NOT NULL,
	[Cedula] [varchar](11) NOT NULL,
	[DoctorID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[AsistenteID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UQ_Doctor_AsistenteFijo] UNIQUE NONCLUSTERED 
(
	[DoctorID] ASC,
	[AsistenteID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Ausencia]    Script Date: 01/08/2022 23:36:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ausencia](
	[AusenciaID] [int] IDENTITY(1,1) NOT NULL,
	[DoctorID] [int] NOT NULL,
	[MotivoAusencia] [varchar](50) NULL,
	[fechaInicio] [datetime] NULL,
	[FechaReintegro] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[AusenciaID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Citas]    Script Date: 01/08/2022 23:36:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Citas](
	[CitaID] [int] IDENTITY(1,1) NOT NULL,
	[FechaCita] [datetime] NULL,
	[EspecialidadId] [int] NOT NULL,
	[DoctorID] [int] NOT NULL,
	[PacienteID] [int] NOT NULL,
	[Estado] [int] NOT NULL,
	[Comentario] [varchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[CitaID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UQ_Citas] UNIQUE NONCLUSTERED 
(
	[EspecialidadId] ASC,
	[DoctorID] ASC,
	[PacienteID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Especialidad]    Script Date: 01/08/2022 23:36:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Especialidad](
	[EspecialidadId] [int] IDENTITY(1,1) NOT NULL,
	[Especialidad] [varchar](50) NOT NULL,
	[DoctorID] [int] NOT NULL,
	[AsitenteID] [int] NULL,
	[HoraInicio] [varchar](8) NULL,
	[HoraFin] [varchar](8) NULL,
PRIMARY KEY CLUSTERED 
(
	[EspecialidadId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UQ_Especialidad] UNIQUE NONCLUSTERED 
(
	[DoctorID] ASC,
	[AsitenteID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Paciente]    Script Date: 01/08/2022 23:36:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Paciente](
	[PacienteID] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[Apellido] [varchar](50) NOT NULL,
	[Cedula] [varchar](11) NOT NULL,
	[Telefono] [varchar](10) NOT NULL,
	[Asistente] [varchar](11) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[PacienteID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Personal]    Script Date: 01/08/2022 23:36:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Personal](
	[PersonalID] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[Apellido] [varchar](50) NOT NULL,
	[Cedula] [varchar](11) NOT NULL,
	[Ocupacion] [varchar](50) NOT NULL,
	[DoctorID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[PersonalID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UQ_Doctor_Asistente] UNIQUE NONCLUSTERED 
(
	[DoctorID] ASC,
	[PersonalID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuarios]    Script Date: 01/08/2022 23:36:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuarios](
	[UsuarioID] [int] IDENTITY(1,1) NOT NULL,
	[PersonalID] [int] NULL,
	[AsistenteID] [int] NULL,
	[Usuario] [varchar](50) NOT NULL,
	[Clave] [varchar](50) NOT NULL,
	[Estado] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UsuarioID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Asistente]  WITH CHECK ADD  CONSTRAINT [Fk_Doctor_AsistenteFijo] FOREIGN KEY([DoctorID])
REFERENCES [dbo].[Personal] ([PersonalID])
GO
ALTER TABLE [dbo].[Asistente] CHECK CONSTRAINT [Fk_Doctor_AsistenteFijo]
GO
ALTER TABLE [dbo].[Ausencia]  WITH CHECK ADD  CONSTRAINT [Fk_Ausencia_Doctor] FOREIGN KEY([DoctorID])
REFERENCES [dbo].[Personal] ([PersonalID])
GO
ALTER TABLE [dbo].[Ausencia] CHECK CONSTRAINT [Fk_Ausencia_Doctor]
GO
ALTER TABLE [dbo].[Citas]  WITH CHECK ADD  CONSTRAINT [Fk_Cita_Doctor] FOREIGN KEY([DoctorID])
REFERENCES [dbo].[Personal] ([PersonalID])
GO
ALTER TABLE [dbo].[Citas] CHECK CONSTRAINT [Fk_Cita_Doctor]
GO
ALTER TABLE [dbo].[Citas]  WITH CHECK ADD  CONSTRAINT [Fk_Cita_Especialidad] FOREIGN KEY([EspecialidadId])
REFERENCES [dbo].[Especialidad] ([EspecialidadId])
GO
ALTER TABLE [dbo].[Citas] CHECK CONSTRAINT [Fk_Cita_Especialidad]
GO
ALTER TABLE [dbo].[Citas]  WITH CHECK ADD  CONSTRAINT [Fk_Cita_Paciente] FOREIGN KEY([PacienteID])
REFERENCES [dbo].[Paciente] ([PacienteID])
GO
ALTER TABLE [dbo].[Citas] CHECK CONSTRAINT [Fk_Cita_Paciente]
GO
ALTER TABLE [dbo].[Especialidad]  WITH CHECK ADD  CONSTRAINT [Fk_Especialidad_Asistente] FOREIGN KEY([AsitenteID])
REFERENCES [dbo].[Personal] ([PersonalID])
GO
ALTER TABLE [dbo].[Especialidad] CHECK CONSTRAINT [Fk_Especialidad_Asistente]
GO
ALTER TABLE [dbo].[Especialidad]  WITH CHECK ADD  CONSTRAINT [Fk_Especialidad_Doctor] FOREIGN KEY([DoctorID])
REFERENCES [dbo].[Personal] ([PersonalID])
GO
ALTER TABLE [dbo].[Especialidad] CHECK CONSTRAINT [Fk_Especialidad_Doctor]
GO
ALTER TABLE [dbo].[Personal]  WITH CHECK ADD  CONSTRAINT [Fk_Doctor_Asistente] FOREIGN KEY([DoctorID])
REFERENCES [dbo].[Personal] ([PersonalID])
GO
ALTER TABLE [dbo].[Personal] CHECK CONSTRAINT [Fk_Doctor_Asistente]
GO
ALTER TABLE [dbo].[Usuarios]  WITH CHECK ADD  CONSTRAINT [Fk_Usuarios_Asistente] FOREIGN KEY([AsistenteID])
REFERENCES [dbo].[Asistente] ([AsistenteID])
GO
ALTER TABLE [dbo].[Usuarios] CHECK CONSTRAINT [Fk_Usuarios_Asistente]
GO
ALTER TABLE [dbo].[Usuarios]  WITH CHECK ADD  CONSTRAINT [Fk_Usuarios_Personal] FOREIGN KEY([PersonalID])
REFERENCES [dbo].[Personal] ([PersonalID])
GO
ALTER TABLE [dbo].[Usuarios] CHECK CONSTRAINT [Fk_Usuarios_Personal]
GO
USE [master]
GO
ALTER DATABASE [OrientalMedicalSystemDB] SET  READ_WRITE 
GO
