USE [MyTherapist]
GO

/****** Object:  Table [dbo].[PatientAppointmentInformation]    Script Date: 6/30/18 2:19:10 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[PatientAppointmentInformation](
	[ApptDate] [datetime] NOT NULL,
	[PatientId] [bigint] NOT NULL,
	[RLU] [nvarchar](50) NULL,
	[SP] [nvarchar](50) NULL,
	[KD1] [nvarchar](50) NULL,
	[LHT] [nvarchar](50) NULL,
	[LV] [nvarchar](50) NULL,
	[KD2] [nvarchar](50) NULL,
	[TherapyPerformed] [nvarchar](max) NULL,
	[OilsUsed] [nvarchar](max) NULL,
	[SessionGoals] [nvarchar](max) NULL,
	[ImageBeforeTherapy] [nvarchar](max) NULL,
	[ImageAfterTherapy] [nvarchar](max) NULL,
	[ApptId] [uniqueidentifier] NOT NULL,
	[TherapistId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_PatientAppointmentInformation] PRIMARY KEY CLUSTERED 
(
	[ApptId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

