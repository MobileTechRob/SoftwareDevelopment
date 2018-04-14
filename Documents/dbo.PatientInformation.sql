CREATE TABLE [dbo].[PatientInformation] (
    [Id]              INT            IDENTITY (1, 1) NOT NULL,
    [FirstName]       NVARCHAR (50)  NULL,
    [EmailAddress]    NVARCHAR (MAX) NULL,
    [TelephoneNumber] NVARCHAR (50)  NULL,
    [LastName]        NVARCHAR (50)  NULL,
    [BirthDate] DATE NULL, 
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_PatientInformation_FirstName]
    ON [dbo].[PatientInformation]([FirstName] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_PatientInformation_LastName]
    ON [dbo].[PatientInformation]([LastName] ASC);

