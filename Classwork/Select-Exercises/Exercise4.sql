/*
   From the Orders table select all the order information for the 
   customer with id of QUEDE
*/

USE Northwind;
GO

SELECT * FROM Orders
WHERE CustomerID = 'QUEDE'