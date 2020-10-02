USE master
GO

/* Drop database if it already exists */
IF EXISTS (SELECT * FROM sysdatabases WHERE name = 'GuildCars')
BEGIN
	DROP DATABASE GuildCars
END
GO

CREATE DATABASE GuildCars
GO