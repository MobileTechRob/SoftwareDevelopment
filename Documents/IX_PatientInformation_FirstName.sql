USE [MyTherapist]
GO

SET ANSI_PADDING ON
GO

/****** Object:  Index [IX_PatientInformation_FirstName]    Script Date: 5/10/18 8:28:11 PM ******/
CREATE NONCLUSTERED INDEX [IX_PatientInformation_FirstName] ON [dbo].[PatientInformation]
(
	[FirstName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

