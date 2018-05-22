USE [MyTherapist]
GO

SET ANSI_PADDING ON
GO

/****** Object:  Index [IX_PatientInformation_LastName]    Script Date: 5/10/18 8:28:32 PM ******/
CREATE NONCLUSTERED INDEX [IX_PatientInformation_LastName] ON [dbo].[PatientInformation]
(
	[LastName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

