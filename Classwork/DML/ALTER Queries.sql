USE MovieCatalogue;
GO

ALTER TABLE Actor
ADD DateOfDeath Date

GO

UPDATE Actor
SET DateOfDeath = '3/4/1994'
WHERE ActorID = 3