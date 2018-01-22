CREATE TABLE [dbo].[OilsUsed] (
    [ApptDate] DATETIME2 (7)  NOT NULL,
    [OilsUsed] NVARCHAR (MAX) NULL,
    [Guid] UNIQUEIDENTIFIER NOT NULL, 
    PRIMARY KEY CLUSTERED ([ApptDate] ASC, [Guid] ASC)
);

