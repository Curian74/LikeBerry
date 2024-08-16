USE [LikeBerry];

-- Insert data into the Role table
INSERT INTO [Role] ([role_name]) VALUES ('Admin');
INSERT INTO [Role] ([role_name]) VALUES ('Librarian');
INSERT INTO [Role] ([role_name]) VALUES ('Member');

-- Insert data into the Users table
INSERT INTO [Users] ([full_name], [email], [password], [role_id], [status], [phone_number], [address])
VALUES 
('Admin', 'admin@gmail.com', '123', 1, 1, '0346639004', '123 Main St'),
('Librarian', 'librarian@gmail.com', '123', 2, 1, '0346639005', '456 Elm St'),
('Cuong Pham', 'cuongpq@gmail.com', '123', 3, 1, '0346639006', '789 Maple St');

-- Insert data into Genres table
INSERT INTO [Genres] (genre_name) VALUES ('Science Fiction');
INSERT INTO [Genres] (genre_name) VALUES ('Fantasy');
INSERT INTO [Genres] (genre_name) VALUES ('Mystery');

-- Insert data into Authors table
INSERT INTO [Authors] (author_name) VALUES ('Isaac Asimov');
INSERT INTO [Authors] (author_name) VALUES ('J.K. Rowling');
INSERT INTO [Authors] (author_name) VALUES ('Agatha Christie');

-- Insert data into Books table
INSERT INTO [Books] (book_name, img, ISBN, book_title, issue_date, author_id, instock_quantity, genre_id) 
VALUES 
('Foundation', 'https://m.media-amazon.com/images/I/91ZYBjR+gYL._AC_UF1000,1000_QL80_.jpg', '9780553293357', 'Foundation Series', '1951-06-01', 1, 10, 1),
('Harry Potter and the Sorcerer''s Stone', 'https://m.media-amazon.com/images/I/818FB6bF4aL._AC_UF1000,1000_QL80_.jpg', '9780747532743', 'Harry Potter Series', '1997-06-26', 2, 15, 2),
('Murder on the Orient Express', 'https://i.ebayimg.com/images/g/MFEAAOSwN91baXa8/s-l1200.jpg', '9780007527502', 'Hercule Poirot Series', '1934-01-01', 3, 7, 3),
('Harry Potter and the Chamber of Secrets', 'https://m.media-amazon.com/images/I/81hdPg+8GsL._AC_UF1000,1000_QL80_.jpg', '9780747538486', 'Harry Potter Series', '1998-07-02', 2, 12, 2),
('I, Robot', 'image5.jpg', '9780553382563', 'Robot Series', '1950-12-02', 1, 5, 1),
('The Hobbit', 'image6.jpg', '9780618968633', 'The Lord of the Rings Series', '1937-09-21', 1, 8, 2),
('Harry Potter and the Prisoner of Azkaban', 'image7.jpg', '9780747542155', 'Harry Potter Series', '1999-07-08', 2, 9, 2),
('Harry Potter and the Goblet of Fire', 'image8.jpg', '9780747546245', 'Harry Potter Series', '2000-07-08', 2, 11, 2),
('Harry Potter and the Order of the Phoenix', 'image9.jpg', '9780747551003', 'Harry Potter Series', '2003-06-21', 2, 14, 2),
('Harry Potter and the Half-Blood Prince', 'image10.jpg', '9780747581086', 'Harry Potter Series', '2005-07-16', 2, 10, 2),
('Harry Potter and the Deathly Hallows', 'image11.jpg', '9780545010221', 'Harry Potter Series', '2007-07-21', 2, 13, 2),
('The Catcher in the Rye', 'image12.jpg', '9780316769488', 'Classic Literature', '1951-07-16', 1, 6, 1),
('To Kill a Mockingbird', 'image13.jpg', '9780060935467', 'Classic Literature', '1960-07-11', 1, 7, 1),
('1984', 'image14.jpg', '9780451524935', 'Dystopian', '1949-06-08', 1, 10, 1),
('The Great Gatsby', 'image15.jpg', '9780743273565', 'Classic Literature', '1925-04-10', 1, 8, 1);

-- Insert sample data into Bookings table
INSERT INTO [Bookings] (user_id, return_date, status) 
VALUES 
(1, '2023-08-01', 'Returned'),
(2, '2023-08-10', 'Borrowed');

-- Insert sample data into BookingDetails table
INSERT INTO [BookingDetails] (booking_id, book_id, quantity,[full_name],[phone_number],[address]) 
VALUES 
(1, 1, 1,'Smith','0346639006','123 Main St'),
(1, 2, 2,'Smith','0346639006','123 Main St'),
(2, 3, 1,'Smith','0346639006','123 Main St');