/* DDL: Run this first. It creates the new database and its tables. */
IF DB_ID('GameLibraryWebDB') IS NULL CREATE DATABASE GameLibraryWebDB;
GO
USE GameLibraryWebDB;
GO
IF OBJECT_ID('dbo.Games', 'U') IS NOT NULL DROP TABLE dbo.Games;
IF OBJECT_ID('dbo.Franchises', 'U') IS NOT NULL DROP TABLE dbo.Franchises;
IF OBJECT_ID('dbo.Platforms', 'U') IS NOT NULL DROP TABLE dbo.Platforms;
GO
CREATE TABLE Franchises (
    FranchiseId INT IDENTITY(1,1) PRIMARY KEY,
    FranchiseName NVARCHAR(100) NOT NULL UNIQUE,
    Publisher NVARCHAR(100) NOT NULL
);
CREATE TABLE Platforms (
    PlatformId INT IDENTITY(1,1) PRIMARY KEY,
    PlatformName NVARCHAR(100) NOT NULL UNIQUE,
    Manufacturer NVARCHAR(100) NOT NULL
);
CREATE TABLE Games (
    GameId INT IDENTITY(1,1) PRIMARY KEY,
    GameTitle NVARCHAR(150) NOT NULL,
    ReleaseYear INT NOT NULL CHECK (ReleaseYear BETWEEN 1970 AND 2100),
    FranchiseId INT NOT NULL FOREIGN KEY REFERENCES Franchises(FranchiseId),
    PlatformId INT NOT NULL FOREIGN KEY REFERENCES Platforms(PlatformId)
);
GO
