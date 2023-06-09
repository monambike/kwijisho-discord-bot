CREATE TABLE [dbo].[Link]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Title] NVARCHAR(50) NULL, 
    [Description] VARCHAR(255) NULL,
    [Link] NCHAR(2048) NULL,
)
