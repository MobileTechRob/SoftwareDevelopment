﻿CREATE TABLE [MyTherapist].[PatientInformation] (
    [Id]              INT            IDENTITY (1, 1) NOT NULL,
    [FirstName]            NVARCHAR (50)  NULL,
    [EmailAddress]    NVARCHAR (MAX) NULL,
    [TelephoneNumber] NVARCHAR (50)  NULL,
    [LastName] NVARCHAR(50) NULL, 
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE NONCLUSTERED INDEX IX_PatientInformation_FirstName   
    ON MyTherapist.PatientInformation (FirstName);   
GO 

