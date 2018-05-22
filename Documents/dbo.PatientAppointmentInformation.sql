USE [MyTherapist]
GO

/****** Object:  Table [dbo].[PatientAppointmentInformation]    Script Date: 5/10/18 8:25:00 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[PatientAppointmentInformation](
	[ApptDate] [datetime] NOT NULL,
	[PatientId] [bigint] NOT NULL,
	[RLU] [nchar](10) NULL,
	[SP] [nchar](10) NULL,
	[KD1] [nchar](10) NULL,
	[LHT] [nchar](10) NULL,
	[LV] [nchar](10) NULL,
	[KD2] [nchar](10) NULL,
	[TherapyPerformed] [nvarchar](max) NULL,
	[OilsUsed] [nvarchar](max) NULL,
	[SessionGoals] [nvarchar](max) NULL,
	[ImageBeforeTherapy] [nvarchar](max) NULL,
	[ImageAfterTherapy] [nvarchar](max) NULL,
	[ApptId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_PatientAppointmentInformation] PRIMARY KEY CLUSTERED 
(
	[ApptId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

