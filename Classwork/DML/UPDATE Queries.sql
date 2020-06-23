USE MovieCatalogue;
GO

UPDATE Movie
SET Title = 'Ghostbusters (1984)', ReleaseDate = '6/8/1984'
WHERE MovieID = 3

UPDATE Genre
SET GenreName = 'Action/Adventure'
WHERE GenreID = 1