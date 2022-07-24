CREATE PROCEDURE [dbo].[uspAddWord]
	@Name VARCHAR(50)
AS
BEGIN
	INSERT INTO [Word] ([Name]) VALUES (@Name)
END
