CREATE PROCEDURE [dbo].[uspGetDictionary]
AS
BEGIN
	SELECT [Name], [Description] FROM [uvwDictionary]
END
