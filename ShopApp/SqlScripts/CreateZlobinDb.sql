CREATE DATABASE [Zlobin];

GO

USE [Zlobin];

GO

CREATE SCHEMA [Shop];

GO

CREATE TABLE [Shop].[Categories] (
	[Id] INT IDENTITY(1, 1) NOT NULL,
	[Name] NVARCHAR(50) UNIQUE NOT NULL,

	CONSTRAINT [CategoriesPk] PRIMARY KEY ([ID])
);

GO

CREATE PROCEDURE [GenerateCategories] (@CategoriesCount INT) --DROP PROCEDURE [GenerateCategories];
AS
DECLARE @i INT = 1;
BEGIN
    WHILE @i <= @CategoriesCount
    BEGIN
		INSERT INTO [Shop].[Categories] ([Name]) VALUES (CONCAT(N'Category ', @i));
		SET @i = @i + 1;
    END
END;

GO

EXEC GenerateCategories @CategoriesCount = 10;

GO

CREATE TABLE [Shop].[Products] (
	[Id] INT IDENTITY(1, 1) NOT NULL,
	[CategoryId] INT NOT NULL,
	[Name] NVARCHAR(50) UNIQUE NOT NULL,
	[Price]	MONEY NULL

	CONSTRAINT [ProductsPk] PRIMARY KEY ([Id]),
	CONSTRAINT [ProductsCategoriesFk] FOREIGN KEY ([CategoryId]) REFERENCES [Shop].[Categories] ([Id])
);

GO

CREATE PROCEDURE [GenerateProducts] (@CategoriesCount INT, @ProductsCount INT) --DROP PROCEDURE [GenerateProducts];
AS
DECLARE @i INT = 1;
BEGIN
    WHILE @i <= @ProductsCount
    BEGIN
		INSERT INTO [Shop].[Products] ([CategoryId], [Name]) VALUES ((ABS(CHECKSUM(NEWID())) % @CategoriesCount) + 1 , CONCAT(N'Product ', @i));
		SET @i = @i + 1;
    END
END;

GO

EXEC [GenerateProducts] @CategoriesCount = 10, @ProductsCount = 150;

GO

SELECT * FROM [Shop].[Categories];
SELECT * FROM [Shop].[Products] ORDER BY [Name];