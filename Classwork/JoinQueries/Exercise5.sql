/*
	Find a list of all the Employees who have never found a Grant
*/

USE SWCCorp;
GO

SELECT FirstName, LastName FROM Employee
WHERE EmpID NOT IN (
	SELECT e.EmpID FROM Employee e
		INNER JOIN [Grant] g ON e.EmpID = g.EmpID
)

SELECT * FROM [Grant]