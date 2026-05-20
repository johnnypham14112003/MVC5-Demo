USE MASTER;
GO

IF EXISTS (SELECT NAME FROM SYS.DATABASES WHERE NAME = 'ExampleDb')
BEGIN
    ALTER DATABASE ExampleDb SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
	DROP DATABASE ExampleDb;
END;
GO

CREATE DATABASE ExampleDb;
GO

USE ExampleDb;
GO

CREATE TABLE Account(
	Id UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,
	Email NVARCHAR(100) NOT NULL,
	[Password] NVARCHAR(100) NOT NULL,
	[Role] NVARCHAR(30) NOT NULL DEFAULT 'User'
);
GO

CREATE TABLE Motor(
	Id UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,
	ImageUrl NVARCHAR(MAX),
	[Name] NVARCHAR(100) NOT NULL,
	[Description] NVARCHAR(MAX),
	Price MONEY NOT NULL DEFAULT 0
);
GO

INSERT INTO Account(Email, [Password], [Role]) VALUES
('admin@s3.com','12345', 'Admin'),
('user@gmail.com','12345', default);
GO

INSERT INTO Motor([Name], [Description], Price,ImageUrl) VALUES
('Kawasaki Ninja 500', 'High volume motorbike with sport style from Kawasaki.', 159000000, 'ninja500.jpg'),
('Honda CBR650RR', 'High volume motorbike with sport style from Honda.', 200000000, 'cbr.jpg');