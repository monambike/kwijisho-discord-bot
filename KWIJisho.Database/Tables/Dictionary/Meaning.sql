CREATE TABLE [dbo].[Meaning]
(
	[Id] INT NOT NULL CONSTRAINT [PK_MeaningId] PRIMARY KEY IDENTITY(1,1), 
  [Description] NCHAR(255) NOT NULL, 
  [WordId] INT NOT NULL CONSTRAINT [FK_Meaning_Dictionary] REFERENCES [Word]([Id])
)
