﻿--CREATE TABLE [dbo.Games] (
--	Id int NOT NULL,
--	Name nvarchar(225),
--	StartTime DateTime,
--	PrizePot decimal,
--	EntryFee decimal,
--	CurrencyCode nvarchar(225),
--	PRIMARY KEY (Id)
--)

--CREATE TABLE [dbo.Locations] (
--	Id int NOT NULL,
--	Longitude float,
--	Latitude float,
--	Accuracy decimal,	
--	GameId int,
--	PRIMARY KEY (Id),
--	FOREIGN KEY (GameId) REFERENCES dbo.Games(Id)
--)