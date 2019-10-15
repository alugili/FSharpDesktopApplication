USE master;
GO

IF DB_ID (N'ShopDatabase') IS NOT NULL
DROP DATABASE ShopDatabase;
GO
CREATE DATABASE ShopDatabase;
GO

Use ShopDatabase
GO

CREATE TABLE Customers (
  CustomerId INT PRIMARY KEY,
  Name nvarchar(25)
);

GO

INSERT INTO Customers(CustomerId, Name)
VALUES (1, 'Bassam'), (2,'Mays'), (3,'Rami'), (4,'Fadi'), (5,'Alugili');