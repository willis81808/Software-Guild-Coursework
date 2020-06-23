USE Northwind;
GO

-- 1
SELECT e.FirstName, e.LastName, t.TerritoryDescription
FROM Employees e
	INNER JOIN EmployeeTerritories et ON e.EmployeeID = et.EmployeeID
	INNER JOIN Territories t ON et.TerritoryID = t.TerritoryID

--2
SELECT c.CompanyName, o.OrderDate, p.ProductName
FROM Customers c
	INNER JOIN Orders o ON c.CustomerID = o.CustomerID
	INNER JOIN [Order Details] od ON o.OrderID = od.OrderID
	INNER JOIN Products p ON od.ProductID = p.ProductID
WHERE c.Country = 'USA'

--3
SELECT o.*
FROM Products p
	INNER JOIN [Order Details] od ON od.ProductID = p.ProductID
	INNER JOIN Orders o ON o.OrderID = od.OrderID
WHERE p.ProductName = 'Chai'

USE SWCCorp;
GO

-- 4
SELECT e.FirstName, e.LastName, l.City
FROM Employee e CROSS JOIN [Location] l

--5
SELECT e.FirstName, e.LastName
FROM Employee e
	LEFT JOIN [Grant] g ON e.EmpID = g.EmpID
WHERE g.GrantID IS NULL


