create database ProductlnventoryDB

use ProductlnventoryDB

create table Products
(ProductId int IDENTITY(1,1) primary key,
ProductName nvarchar(50),
Price decimal,
Quantity int,
MfDate Date,
ExpDate Date
)

-- Insert sample data into the Products table
INSERT INTO Products (ProductName, Price, Quantity, MfDate, ExpDate)
VALUES ('Mobile Phone', 12000.99, 50, '2023-01-01', '2023-12-31');

INSERT INTO Products (ProductName, Price, Quantity, MfDate, ExpDate)
VALUES ('Laptop', 22000.99, 30, '2023-02-15', '2024-02-15');

select * from Products
drop table Products 