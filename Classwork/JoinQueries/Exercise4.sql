/*
	Write a query to show every combination of employee and location.
*/

USE SWCCorp;
GO

SELECT e.LastName + ', ' + e.FirstName AS EmployeeName, l.City + ', ' + l.[State] AS EmployeeLocation
FROM Employee e
	CROSS JOIN [Location] l
ORDER BY EmployeeName