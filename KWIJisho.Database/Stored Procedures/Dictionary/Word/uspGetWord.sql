CREATE PROCEDURE [dbo].[uspGetWord]
	@Name VARCHAR(50)
AS
BEGIN
	SELECT [Name], [Description] FROM [uvwDictionary]
	WHERE [Name] LIKE ('%' + @Name + '%')
END
