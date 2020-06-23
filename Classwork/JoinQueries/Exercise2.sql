/*
	Get the Company Name, Order Date, and each order detail’s 
	Product name for USA customers only.
*/

USE Northwind;
GO

SELECT c.CompanyName, Orders.OrderDate, p.ProductName
FROM Orders
	INNER JOIN Customers c ON c.CustomerID = Orders.CustomerID
	INNER JOIN [Order Details] d ON d.OrderID = Orders.OrderID
	INNER JOIN Products p ON p.ProductID = d.ProductID
WHERE c.Country = 'USA'