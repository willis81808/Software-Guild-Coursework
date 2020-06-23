/*
   Select the orders whose freight is more than $100.00
*/

USE Northwind;
GO

SELECT * FROM Orders
WHERE Freight > 100.00