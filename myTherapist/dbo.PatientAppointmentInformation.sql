CREATE TABLE [dbo].[PatientAppointmentInformation] (
    [ApptDate]           DATETIME       NOT NULL,
    [PatientId]          BIGINT         NOT NULL,
    [RLU]                NCHAR (10)     NULL,
    [SP]                 NCHAR (10)     NULL,
    [KD1]                NCHAR (10)     NULL,
    [LHT]                NCHAR (10)     NULL,
    [LV]                 NCHAR (10)     NULL,
    [KD2]                NCHAR (10)     NULL,
    [TherapyPerformed]   NVARCHAR (MAX) NULL,
    [OilsUsed]           NVARCHAR (MAX) NULL,
    [SessionGoals]       NVARCHAR (MAX) NULL,
    [ImageBeforeTherapy] NVARCHAR (MAX) NULL,
    [ImageAfterTherapy]  NVARCHAR (MAX) NULL,
    [GUID] UNIQUEIDENTIFIER NOT NULL, 
    PRIMARY KEY CLUSTERED ([ApptDate] ASC, [PatientId] ASC, [GUID] ASC)
);

