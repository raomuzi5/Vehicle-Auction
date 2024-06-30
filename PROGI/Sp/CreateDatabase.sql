-- Create Database
CREATE DATABASE Proji;
GO

-- Use Database
USE Proji;
GO

-- Create Vehicles Table
CREATE TABLE Vehicles (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    BasePrice DECIMAL(18, 2) NOT NULL,
    CarType NVARCHAR(50) NOT NULL
);
GO
