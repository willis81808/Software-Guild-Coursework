USE Northwind;
GO

-- 1
SELECT * FROM Products

-- 2
SELECT * FROM Products WHERE ProductName = 'Queso Cabrales'

-- 3
SELECT ProductName, UnitsInStock
FROM Products
WHERE ProductName IN ('Laughing Lumberjack Lager', 'Outback Lager', 'Ravioli Angelo')

-- 4
SELECT * FROM Orders WHERE CustomerID = 'QUEDE'

-- 5
SELECT * FROM Orders WHERE Freight > 100.00

-- 6
SELECT *
FROM Orders 
WHERE ShipCountry = 'USA' AND Freight BETWEEN 10 AND 20

-- 7
SELECT * FROM Suppliers WHERE ContactTitle Like 'Sales%'