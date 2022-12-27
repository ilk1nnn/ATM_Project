CREATE DATABASE UsersDb
GO
USE UsersDb
GO


CREATE TABLE Users(
	[Id] INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
	[FullName] NVARCHAR(MAX) NOT NULL,
	[CardNumber] NVARCHAR(16) NOT NULL,
	[Balance] MONEY NOT NULL

)



GO


INSERT INTO Users([FullName],[CardNumber],[Balance])
VALUES('John Johnlu','4127123456786543',250),
	  ('Akif Akifzade','4169785434965042',300),
	  ('Aysel Ayselzade','1234567891236453',1020),
	  ('Nurlan Shirinov','9876543211234546',5000),
	  ('Alirza Zaidov','8465748967586456',250),
	  ('Ilkin Suleymanov','4358934754893345',1000),
	  ('Ali Alizade','3435435345784953',250)