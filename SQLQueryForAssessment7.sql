create database LibraryDB;
USE LibraryDB;

CREATE TABLE Books (
    BookId INT IDENTITY(1,1) PRIMARY KEY,
    Title NVARCHAR(100),
    Author NVARCHAR(100),
    Genre NVARCHAR(50),
    Quantity INT
);
INSERT INTO Books (Title, Author, Genre, Quantity)VALUES ('KGF', 'Naman', 'Action', 6);

drop table Books
SELECT * FROM Books

