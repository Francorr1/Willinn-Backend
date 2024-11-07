CREATE DATABASE Users;
GO

USE Users;
GO

CREATE TABLE Users
(
    Id INT PRIMARY KEY IDENTITY,
    Name NVARCHAR(50) NOT NULL,
    Email NVARCHAR(50) NOT NULL,
    Password NVARCHAR(MAX) NOT NULL,
    IsActive BIT NOT NULL,
);
GO

INSERT INTO Users (Name, Email, Password, IsActive)
VALUES ('Test', 'test@test.com', 'SecurePassword', 1);
GO
