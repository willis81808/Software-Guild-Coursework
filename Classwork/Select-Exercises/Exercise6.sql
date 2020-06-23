/*
   Select the orders shipping to the USA whose freight is 
   between $10 and $20
*/

USE Northwind;
GO

SELECT * FROM Orders
WHERE ShipCountry = 'USA' AND Freight BETWEEN 10 AND 20