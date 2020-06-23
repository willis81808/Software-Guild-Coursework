/*
   Write a single query to display only the name and number of 
   units in stock for the products Laughing Lumberjack Lager, 
   Outback Lager, and Ravioli Angelo
*/

USE Northwind;
GO

SELECT ProductName, UnitsInStock
FROM Products WHERE
ProductName = 'Laughing Lumberjack Lager' OR
ProductName = 'Outback Lager' OR
ProductName = 'Ravioli Angelo'