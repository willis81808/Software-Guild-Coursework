/*
	Get a list of each employee first name and lastname
	and the territory names they are associated with
*/

USE Northwind;
GO

SELECT e.FirstName, e.LastName, Territories.TerritoryDescription
FROM Employees e
	INNER JOIN EmployeeTerritories et
		ON et.EmployeeID = e.EmployeeID
	INNER JOIN Territories
		ON et.TerritoryID = Territories.TerritoryID