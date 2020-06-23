/*
	Get all the order information for any order where Chai was sold.
*/

USE Northwind;
GO

SELECT o.* FROM Orders o
	INNER JOIN [Order Details] od ON od.OrderID = o.OrderID
	INNER JOIN Products p ON od.ProductID = p.ProductID
WHERE p.ProductName = 'Chai'