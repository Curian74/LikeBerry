--CREATE DATABASE [LikeBerry] 

USE [LikeBerry];

CREATE TABLE [Role](
	[role_id] INT IDENTITY(1,1),
	[role_name] NVARCHAR(50),
	PRIMARY KEY CLUSTERED ([role_id] ASC)
);

CREATE TABLE [Users](
	[user_id] INT IDENTITY(1,1),
	[full_name] NVARCHAR(50),
	[email] NVARCHAR(70) UNIQUE,
	[password] NVARCHAR(50),
	[role_id] INT,
	[status] BIT,
	[phone_number] NVARCHAR(50),
	[address] NVARCHAR(MAX),
	PRIMARY KEY CLUSTERED ([user_id] ASC),
    FOREIGN KEY ([role_id]) REFERENCES [Role]([role_id])
);

CREATE TABLE [Genres](
	[genre_id] INT IDENTITY(1,1),
	[genre_name] NVARCHAR(MAX),
	PRIMARY KEY CLUSTERED ([genre_id] ASC)
);

CREATE TABLE [Authors](
	[author_id] INT IDENTITY(1,1),
	[author_name] NVARCHAR(MAX),
	PRIMARY KEY CLUSTERED ([author_id] ASC)
);

CREATE TABLE [Books](
	[book_id] INT IDENTITY(1,1),
	[book_name] NVARCHAR(MAX) NOT NULL,
	[img] NVARCHAR(MAX),
	[ISBN] NVARCHAR(13) UNIQUE NOT NULL,
	[book_title] NVARCHAR(MAX),
	[issue_date] DATE,
	[author_id] INT,
	[instock_quantity] INT,
	[genre_id] INT,
	[status] BIT DEFAULT 1,
	PRIMARY KEY CLUSTERED ([book_id] ASC),
    FOREIGN KEY ([genre_id]) REFERENCES [Genres]([genre_id]),
	FOREIGN KEY ([author_id]) REFERENCES [Authors]([author_id])
);


CREATE TABLE [Bookings](
	[booking_id] INT IDENTITY(1,1) PRIMARY KEY,
	[user_id] INT,
	[booking_date] DATETIME DEFAULT GETDATE(),
	[return_date] DATE,
	[status] NVARCHAR(20),
	[is_finished] BIT,
	[processed_by] INT 
	FOREIGN KEY ([user_id]) REFERENCES [Users]([user_id]),
	FOREIGN KEY ([processed_by]) REFERENCES [Users]([user_id])
);

CREATE TABLE [BookingDetails](
	[booking_detail_id] INT IDENTITY(1,1) PRIMARY KEY,
	[booking_id] INT,
	[book_id] INT,
	[quantity] INT CHECK ([quantity] > 0),
	[full_name] NVARCHAR(50),
	[phone_number] NVARCHAR(50),
	[address] NVARCHAR(MAX),
	FOREIGN KEY ([booking_id]) REFERENCES [Bookings]([booking_id]),
	FOREIGN KEY ([book_id]) REFERENCES [Books]([book_id])
);
