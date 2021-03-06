﻿USE Master;
GO

CREATE DATABASE MovieCatalogue;
GO

USE MovieCatalogue;
GO

CREATE TABLE Genre (
	GenreID INT IDENTITY(1,1) PRIMARY KEY,
	GenreName NVARCHAR(30) NOT NULL
)
GO

CREATE TABLE Director (
	DirectorID INT IDENTITY(1,1) PRIMARY KEY,
	FirstName NVARCHAR(30) NOT NULL,
	LastName NVARCHAR(30) NOT NULL,
	BirthDate DATE NULL
)
GO

CREATE TABLE Rating (
	RatingID INT IDENTITY(1,1) PRIMARY KEY,
	RatingName VARCHAR(5) NOT NULL
)
GO

CREATE TABLE Actor (
	ActorID INT IDENTITY(1,1) PRIMARY KEY,
	FirstName NVARCHAR(30) NOT NULL,
	LastName NVARCHAR(30) NOT NULL,
	BirthDate DATE NULL
)
GO

CREATE TABLE Movie (
	MovieID INT IDENTITY(1,1) PRIMARY KEY,
	GenreID INT FOREIGN KEY REFERENCES Genre(GenreID) NOT NULL,
	DirectorID INT FOREIGN KEY REFERENCES Director(DirectorID) NULL,
	RatingID INT FOREIGN KEY REFERENCES Rating(RatingID) NULL,
	Title NVARCHAR(128) NOT NULL,
	ReleaseDate DATE NULL
)
GO

CREATE TABLE CastMembers (
	CastMemberID INT IDENTITY(1,1) PRIMARY KEY,
	ActorID INT FOREIGN KEY REFERENCES Actor(ActorID) NOT NULL,
	MovieID INT FOREIGN KEY REFERENCES Movie(MovieID) NOT NULL,
	Role NVARCHAR(50) NOT NULL
)
GO