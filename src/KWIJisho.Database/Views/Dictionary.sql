CREATE VIEW [dbo].[uvwDictionary]
AS
	SELECT
		  [Word].[Name]
		, [Meaning].[Description]
	FROM
		Word
			INNER JOIN
		Meaning
				ON Word.Id = Meaning.WordId
