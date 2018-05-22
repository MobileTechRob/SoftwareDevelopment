USE [MyTherapist]
GO

/****** Object:  Index [IX_PatientAppointmentInformation_PatientId]    Script Date: 5/10/18 8:27:49 PM ******/
CREATE NONCLUSTERED INDEX [IX_PatientAppointmentInformation_PatientId] ON [dbo].[PatientAppointmentInformation]
(
	[PatientId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

